using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using AhoyBrowser.Export;
using Xunit;

namespace AhoyBrowser.Tests
{
    public sealed class MarblesExportTests : IDisposable
    {
        private const string Password = "marbles-test-pw";
        private readonly string folder;

        public MarblesExportTests()
        {
            folder = Path.Combine(Path.GetTempPath(), "AhoyMarblesTests-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(folder);
        }

        public void Dispose()
        {
            try { Directory.Delete(folder, true); } catch { /* best effort */ }
        }

        private static Entry Note(string title, string content, bool pin = false, int rank = 0, bool locked = false)
        {
            return new Entry
            {
                ID = Guid.NewGuid().ToString(), Created = "2021-03-04 05:06:07", Changed = "2022-01-02 03:04:05",
                Title = title, Content = content, Pin = pin, Rank = rank, Protected = locked, CaretPosition = 2,
                CustomColor = "", RelatedEntries = ""
            };
        }

        private static List<MarblesExport.MarblesEntry> Convert(out MarblesExport.Summary summary, Func<Entry, Color?> color = null)
        {
            var entries = new List<Entry>
            {
                Note("Plain", "line 1\r\nline 2"),
                Note("Second pin", "b", pin: true, rank: 5),
                Note("First pin", "a", pin: true, rank: -3, locked: true),
                Note("", "no title ]]> here"),
                new Entry { Title = "dummy", Content = "", IsDummyEntry = true }
            };
            var stickies = new List<StickySettings>
            {
                new StickySettings { ID = "1", Text = "Sticky text" },
                new StickySettings { ID = "2", Text = "   " },               // empty: left out
                new StickySettings { ID = "3", Text = "Gone", Remove = true }  // removed: left out
            };
            return MarblesExport.Convert(entries, stickies, "Memo text", color, new DateTime(2026, 11, 8, 12, 0, 0), out summary);
        }

        [Fact]
        public void Convert_MapsEntriesStickiesAndMemo()
        {
            List<MarblesExport.MarblesEntry> result = Convert(out MarblesExport.Summary summary);

            Assert.Equal(4, summary.Entries);
            Assert.Equal(1, summary.StickyNotes);
            Assert.True(summary.Memo);
            Assert.Equal(6, result.Count);
            Assert.Equal(6, result.Select(e => e.Id).Distinct().Count());

            var plain = result.Single(e => e.Title == "Plain");
            Assert.Equal("line 1\nline 2", plain.Text);          // Marbles line breaks
            Assert.Equal("2021-03-04 05:06:07", plain.Created);
            Assert.Equal("2022-01-02 03:04:05", plain.Changed);
            Assert.Equal(2, plain.CursorIndex);
            Assert.Equal("", plain.Options);

            // Pinned in the order of the Ahoy pinned list (rank), locked -> read only
            Assert.Equal("rp1", result.Single(e => e.Title == "First pin").Options);
            Assert.Equal("p2", result.Single(e => e.Title == "Second pin").Options);

            Assert.Equal("no title ]]> here", result.Single(e => e.Title == "Untitled").Text);
            var sticky = result.Single(e => e.Title == "Sticky note 1");
            Assert.Equal("Sticky text", sticky.Text);
            Assert.Equal("2026-11-08 12:00:00", sticky.Created);
            Assert.Equal("Memo text", result.Single(e => e.Title == "Memo").Text);
        }

        [Fact]
        public void Convert_PinColorBecomesNearestConsoleColor()
        {
            List<MarblesExport.MarblesEntry> result = Convert(out _, e => e.Title == "Second pin" ? Color.FromArgb(200, 20, 30) : null);
            Assert.Equal("pE2", result.Single(e => e.Title == "Second pin").Options); // E = DarkRed (4)
            Assert.Equal("rp1", result.Single(e => e.Title == "First pin").Options);
        }

        [Fact]
        public void NearestConsoleColor_NeverBlack()
        {
            Assert.NotEqual(ConsoleColor.Black, MarblesExport.NearestConsoleColor(Color.Black));
            Assert.Equal(ConsoleColor.DarkRed, MarblesExport.NearestConsoleColor(Color.FromArgb(200, 20, 30)));
            Assert.Equal(ConsoleColor.Yellow, MarblesExport.NearestConsoleColor(Color.FromArgb(250, 240, 160)));
        }

        [Fact]
        public void DayFirstDates_AreTurnedAround()
        {
            Assert.Equal("2021-11-17 12:21:46", MarblesExport.NormalizeDate("17-11-2021 12:21:46", "x"));
            Assert.Equal("fallback", MarblesExport.NormalizeDate("not a date", "fallback"));
        }

        [Fact]
        public void Document_IsValidXml_AndKeepsCdataTerminators()
        {
            List<MarblesExport.MarblesEntry> result = Convert(out _);
            string xml = MarblesExport.ToXml(result);
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            Assert.Equal("split", doc.DocumentElement.GetAttribute("cdata"));
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("entry");
            Assert.Equal(6, nodes.Count);
            Assert.Contains(nodes.Cast<XmlNode>(), n => n["text"].InnerText == "no title ]]> here");
        }

        [Fact]
        public void File_IsMarblesFormat_AndRoundTrips()
        {
            List<MarblesExport.MarblesEntry> result = Convert(out _);
            string path = Path.Combine(folder, "data.mbls");
            Assert.Equal("", MarblesExport.WriteNewFile(path, result, Password));

            byte[] data = File.ReadAllBytes(path);
            Assert.Equal("MBLS", System.Text.Encoding.ASCII.GetString(data, 0, 4));
            Assert.Equal(2, data[4]);
            Assert.Equal(1, data[5]);
            Assert.True(MarblesExport.TryDecrypt(data, Password, out string xml));
            Assert.Equal(MarblesExport.ToXml(result), xml);
            Assert.False(MarblesExport.TryDecrypt(data, "wrong-password", out _));
        }

        [Fact]
        public void ExistingFile_IsNeverOverwritten()
        {
            string path = Path.Combine(folder, "data.mbls");
            File.WriteAllText(path, "someone's notes");
            string error = MarblesExport.WriteNewFile(path, Convert(out _), Password);
            Assert.NotEqual("", error);
            Assert.Equal("someone's notes", File.ReadAllText(path));
        }

        [Fact]
        public void ShortPassword_IsRefused()
        {
            Assert.Throws<ArgumentException>(() => MarblesExport.Encrypt("<entries/>", "short"));
        }
    }
}
