using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace AhoyBrowser.Export
{
    /// <summary>
    /// Writes the notes of an Ahoy pool as a Marbles data file (.mbls), for people moving to Marbles
    /// (https://github.com/artstisen/marbles). Every Ahoy entry becomes a Marbles entry; sticky notes and the
    /// memo field become normal entries.
    ///
    /// Mapping of Ahoy properties to the options of a Marbles entry:
    ///   pinned           -> pinned ("p"), in the order of the Ahoy pinned list (rank 1, 2, ...)
    ///   pin color        -> the nearest of the 16 console colors ("A".."P"); pinned entries only, as in Marbles
    ///   locked           -> read only ("r")
    /// The cursor position is kept. Categories (related entries) and text bookmarks have no counterpart in Marbles:
    /// the entries are exported, the grouping and bookmarks are not.
    /// </summary>
    internal static class MarblesExport
    {
        internal const string FileExtension = ".mbls";
        internal const string DefaultFileName = "data.mbls"; // the file Marbles opens by default
        internal const int MinPasswordLength = 8;   // Marbles password rules
        internal const int MaxPasswordLength = 128;

        // Marbles option tags (Marbles/Entries.cs, Marbles/PinColors.cs)
        private const char PinnedTag = 'p';
        private const char ReadOnlyTag = 'r';
        private const string ColorTags = "ABCDEFGHIJKLMNOP"; // in the order of System.ConsoleColor

        internal sealed class MarblesEntry
        {
            internal string Id { get; set; }
            internal string Title { get; set; }
            internal string Text { get; set; }
            internal int CursorIndex { get; set; }
            internal string Created { get; set; }
            internal string Changed { get; set; }
            internal string Options { get; set; }
        }

        internal sealed class Summary
        {
            internal int Entries { get; set; }
            internal int StickyNotes { get; set; }
            internal bool Memo { get; set; }
            internal int Total => Entries + StickyNotes + (Memo ? 1 : 0);
        }

        #region Convert

        /// <summary>
        /// The Marbles entries for the content of a pool
        /// </summary>
        /// <param name="entries">The entries of the pool</param>
        /// <param name="stickies">The sticky notes (removed and empty ones are left out)</param>
        /// <param name="memoText">The memo field (left out when empty)</param>
        /// <param name="pinColor">The color an entry is shown with in the pinned list, or null (none, or unknown)</param>
        /// <param name="now">The time used for new entries (sticky notes and memo have no dates of their own)</param>
        internal static List<MarblesEntry> Convert(IEnumerable<Entry> entries, IEnumerable<StickySettings> stickies, string memoText,
            Func<Entry, Color?> pinColor, DateTime now, out Summary summary)
        {
            summary = new Summary();
            var result = new List<MarblesEntry>();
            var ids = new HashSet<string>();
            long idSeed = now.Ticks;
            string nowText = now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            List<Entry> notes = (entries ?? Enumerable.Empty<Entry>()).Where(e => e != null && !e.IsDummyEntry).ToList();

            // Pinned order: Ahoy sorts the pinned list by rank (lowest first)
            var pinRank = new Dictionary<Entry, int>();
            int rank = 1;
            foreach (Entry pinned in notes.Where(e => e.Pin).OrderBy(e => e.Rank))
            {
                pinRank[pinned] = rank++;
            }

            foreach (Entry e in notes)
            {
                var options = new StringBuilder();
                if (e.Protected) options.Append(ReadOnlyTag);
                if (e.Pin)
                {
                    options.Append(PinnedTag);
                    Color? color = pinColor?.Invoke(e);
                    if (color.HasValue)
                    {
                        options.Append(ColorTags[(int)NearestConsoleColor(color.Value)]);
                    }
                    options.Append(pinRank[e].ToString(CultureInfo.InvariantCulture)); // the rank is the number in the options
                }

                string text = NormalizeNewLines(e.Content);
                string created = NormalizeDate(e.Created, nowText);
                result.Add(new MarblesEntry
                {
                    Id = NewId(ref idSeed, ids),
                    Title = TitleOrDefault(e.Title, "Untitled"),
                    Text = text,
                    CursorIndex = Math.Clamp(e.CaretPosition, 0, text.Length),
                    Created = created,
                    Changed = NormalizeDate(e.Changed, created),
                    Options = options.ToString()
                });
                summary.Entries++;
            }

            int stickyNumber = 0;
            foreach (StickySettings sticky in stickies ?? Enumerable.Empty<StickySettings>())
            {
                if (sticky == null || sticky.Remove || string.IsNullOrWhiteSpace(sticky.Text))
                {
                    continue;
                }
                stickyNumber++;
                result.Add(NewEntry("Sticky note " + stickyNumber.ToString(CultureInfo.InvariantCulture), sticky.Text, nowText, ref idSeed, ids));
                summary.StickyNotes++;
            }

            if (!string.IsNullOrWhiteSpace(memoText))
            {
                result.Add(NewEntry("Memo", memoText, nowText, ref idSeed, ids));
                summary.Memo = true;
            }

            return result;
        }

        private static MarblesEntry NewEntry(string title, string text, string now, ref long idSeed, HashSet<string> ids)
        {
            return new MarblesEntry
            {
                Id = NewId(ref idSeed, ids),
                Title = title,
                Text = NormalizeNewLines(text),
                CursorIndex = 0,
                Created = now,
                Changed = now,
                Options = ""
            };
        }

        // Marbles ids are ticks followed by a 4-digit number; unique within the file
        private static string NewId(ref long seed, HashSet<string> ids)
        {
            string id;
            do
            {
                id = seed.ToString(CultureInfo.InvariantCulture) + RandomNumberGenerator.GetInt32(1000, 10000).ToString(CultureInfo.InvariantCulture);
                seed++;
            }
            while (!ids.Add(id));
            return id;
        }

        private static string TitleOrDefault(string title, string fallback)
        {
            title = (title ?? "").Replace("\r", " ").Replace("\n", " ").Trim();
            return title.Length > 0 ? title : fallback;
        }

        // Marbles keeps "\n" as the line break of a text
        internal static string NormalizeNewLines(string text)
        {
            return (text ?? "").Replace("\r\n", "\n").Replace('\r', '\n');
        }

        private static readonly string[] DateFormats =
        {
            "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd HH:mm", "yyyy-MM-dd",
            "dd-MM-yyyy HH:mm:ss", "dd-MM-yyyy HH:mm", "dd-MM-yyyy",
            "dd.MM.yyyy HH:mm:ss", "dd/MM/yyyy HH:mm:ss"
        };

        // Marbles dates are "yyyy-MM-dd HH:mm:ss" (Ahoy 1.x wrote some dates with the day first)
        internal static string NormalizeDate(string date, string fallback)
        {
            if (!string.IsNullOrWhiteSpace(date) &&
                DateTime.TryParseExact(date.Trim(), DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsed))
            {
                return parsed.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }
            return fallback;
        }

        // The 16 console colors as Windows Terminal shows them (Campbell scheme), in ConsoleColor order
        private static readonly Color[] ConsolePalette =
        {
            Color.FromArgb(12, 12, 12), Color.FromArgb(0, 55, 218), Color.FromArgb(19, 161, 14), Color.FromArgb(58, 150, 221),
            Color.FromArgb(197, 15, 31), Color.FromArgb(136, 23, 152), Color.FromArgb(193, 156, 0), Color.FromArgb(204, 204, 204),
            Color.FromArgb(118, 118, 118), Color.FromArgb(59, 120, 255), Color.FromArgb(22, 198, 12), Color.FromArgb(97, 214, 214),
            Color.FromArgb(231, 72, 86), Color.FromArgb(180, 0, 158), Color.FromArgb(249, 241, 165), Color.FromArgb(242, 242, 242)
        };

        /// <summary>
        /// The console color closest to a color. Black is never chosen: Marbles shows the color as text on a dark background.
        /// </summary>
        internal static ConsoleColor NearestConsoleColor(Color color)
        {
            int best = 1;
            double bestDistance = double.MaxValue;
            for (int i = 1; i < ConsolePalette.Length; i++)
            {
                // Weighted RGB distance ("redmean"), close enough to how colors are perceived
                Color c = ConsolePalette[i];
                double rMean = (color.R + c.R) / 2.0;
                int dr = color.R - c.R, dg = color.G - c.G, db = color.B - c.B;
                double distance = (2 + rMean / 256) * dr * dr + 4 * dg * dg + (2 + (255 - rMean) / 256) * db * db;
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    best = i;
                }
            }
            return (ConsoleColor)best;
        }

        #endregion

        #region Marbles document

        /// <summary>
        /// The decrypted Marbles document (as Marbles/FileIo.GetEntriesAsXml writes it)
        /// </summary>
        internal static string ToXml(IReadOnlyList<MarblesEntry> entries)
        {
            var xml = new StringBuilder();
            xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            xml.Append("<entries cdata=\"split\">"); // "]]>" in a text is written as two CDATA sections
            foreach (MarblesEntry e in entries)
            {
                xml.Append("<entry>");
                xml.Append("<id>").Append(e.Id).Append("</id>");
                xml.Append("<created>").Append(e.Created).Append("</created>");
                xml.Append("<changed>").Append(e.Changed).Append("</changed>");
                xml.Append("<title><![CDATA[").Append(Cdata(e.Title)).Append("]]></title>");
                xml.Append("<text><![CDATA[").Append(Cdata(e.Text)).Append("]]></text>");
                xml.Append("<cindex>").Append(e.CursorIndex.ToString(CultureInfo.InvariantCulture)).Append("</cindex>");
                xml.Append("<options>").Append(e.Options).Append("</options>");
                xml.Append("</entry>");
            }
            xml.Append("</entries>");
            return xml.ToString();
        }

        private static string Cdata(string s)
        {
            return RemoveInvalidXmlChars(s ?? "").Replace("]]>", "]]]]><![CDATA[>");
        }

        // Characters that are not allowed in XML (most control characters, broken surrogate pairs)
        private static string RemoveInvalidXmlChars(string s)
        {
            var cleaned = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (XmlConvert.IsXmlChar(s[i]))
                {
                    cleaned.Append(s[i]);
                }
                else if (i + 1 < s.Length && XmlConvert.IsXmlSurrogatePair(s[i + 1], s[i]))
                {
                    cleaned.Append(s[i]).Append(s[i + 1]);
                    i++;
                }
            }
            return cleaned.ToString();
        }

        #endregion

        #region Encryption (Marbles data file format version 2)

        // All numbers big-endian (Marbles/Encryptor.cs):
        //   0  4   "MBLS"
        //   4  1   format version (2)
        //   5  1   key derivation id (1 = PBKDF2-HMAC-SHA256)
        //   6  4   iterations
        //   10 16  salt
        //   26 12  nonce
        //   38 16  authentication tag
        //   54 n   AES-256-GCM ciphertext of the UTF-8 document; bytes 0-37 are the associated data
        private const int Iterations = 600_000;
        private const int SaltOffset = 10, SaltLength = 16;
        private const int NonceOffset = 26, NonceLength = 12;
        private const int TagOffset = 38, TagLength = 16;
        private const int HeaderLength = 54;

        internal static byte[] Encrypt(string document, string password)
        {
            if (password == null || password.Length < MinPasswordLength || password.Length > MaxPasswordLength)
            {
                throw new ArgumentException("The password must be " + MinPasswordLength + "-" + MaxPasswordLength + " characters.", nameof(password));
            }

            byte[] plain = Encoding.UTF8.GetBytes(document);
            byte[] output = new byte[HeaderLength + plain.Length];
            byte[] key = null;
            try
            {
                output[0] = (byte)'M'; output[1] = (byte)'B'; output[2] = (byte)'L'; output[3] = (byte)'S';
                output[4] = 2;
                output[5] = 1;
                BinaryPrimitives.WriteInt32BigEndian(output.AsSpan(6, 4), Iterations);
                RandomNumberGenerator.Fill(output.AsSpan(SaltOffset, SaltLength));
                RandomNumberGenerator.Fill(output.AsSpan(NonceOffset, NonceLength));

                key = Rfc2898DeriveBytes.Pbkdf2(password, output.AsSpan(SaltOffset, SaltLength).ToArray(), Iterations, HashAlgorithmName.SHA256, 32);
                using var aes = new AesGcm(key, TagLength);
                aes.Encrypt(output.AsSpan(NonceOffset, NonceLength), plain, output.AsSpan(HeaderLength),
                    output.AsSpan(TagOffset, TagLength), output.AsSpan(0, TagOffset));
                return output;
            }
            finally
            {
                CryptographicOperations.ZeroMemory(plain);
                if (key != null) CryptographicOperations.ZeroMemory(key);
            }
        }

        /// <summary>
        /// Read a file written by Encrypt (used to check the written file)
        /// </summary>
        internal static bool TryDecrypt(byte[] data, string password, out string document)
        {
            document = null;
            if (data == null || data.Length < HeaderLength || data[0] != 'M' || data[1] != 'B' || data[2] != 'L' || data[3] != 'S' || data[4] != 2 || data[5] != 1)
            {
                return false;
            }
            int iterations = BinaryPrimitives.ReadInt32BigEndian(data.AsSpan(6, 4));
            if (iterations < 100_000 || iterations > 10_000_000)
            {
                return false;
            }
            byte[] key = Rfc2898DeriveBytes.Pbkdf2(password, data.AsSpan(SaltOffset, SaltLength).ToArray(), iterations, HashAlgorithmName.SHA256, 32);
            byte[] plain = new byte[data.Length - HeaderLength];
            try
            {
                using var aes = new AesGcm(key, TagLength);
                aes.Decrypt(data.AsSpan(NonceOffset, NonceLength), data.AsSpan(HeaderLength), data.AsSpan(TagOffset, TagLength),
                    plain, data.AsSpan(0, TagOffset));
                document = Encoding.UTF8.GetString(plain);
                return true;
            }
            catch (AuthenticationTagMismatchException)
            {
                return false;
            }
            finally
            {
                CryptographicOperations.ZeroMemory(key);
                CryptographicOperations.ZeroMemory(plain);
            }
        }

        #endregion

        #region Write

        /// <summary>
        /// Write a new Marbles data file. An existing file is never replaced (it could be someone's Marbles notes).
        /// The written file is read back and checked before it is reported as done.
        /// </summary>
        /// <returns>Empty when written, else the reason it was not</returns>
        internal static string WriteNewFile(string path, IReadOnlyList<MarblesEntry> entries, string password)
        {
            if (entries == null || entries.Count == 0)
            {
                return "There is nothing to export.";
            }
            if (File.Exists(path))
            {
                return "The file already exists. Marbles data files are never overwritten: choose another name.";
            }

            string document = ToXml(entries);
            byte[] data = Encrypt(document, password);
            try
            {
                using (var stream = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                {
                    stream.Write(data, 0, data.Length);
                    stream.Flush(true);
                }

                if (!TryDecrypt(File.ReadAllBytes(path), password, out string check) || check != document)
                {
                    TryDelete(path);
                    return "The written file could not be verified.";
                }
                return "";
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                return ex.Message;
            }
        }

        private static void TryDelete(string path)
        {
            try { File.Delete(path); } catch { /* best effort */ }
        }

        #endregion
    }
}
