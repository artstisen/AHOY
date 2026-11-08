using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using AhoyBrowser.Crypto;
using Xunit;

namespace AhoyBrowser.Tests
{
    /// <summary>
    /// Runs the real IO.SaveEntries / IO.LoadEntries code against a temporary data folder
    /// (IO.DataRootOverride), never against the user's %APPDATA%\Ahoy.
    /// </summary>
    [Collection("IO")] // IO uses static state: no parallel runs
    public sealed class PoolStorageTests : IDisposable
    {
        private readonly string root;
        private readonly string poolPath;

        public PoolStorageTests()
        {
            root = Path.Combine(Path.GetTempPath(), "AhoyTests-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(root);
            IO.DataRootOverride = root;
            IO.ClearCustomFilePath(false);
            IO.UseDemoPool = false;
            IO.UseSecondaryPool = false;
            Log.InitializeLog();
            poolPath = Path.Combine(root, IO.ProgramFolderName, IO.PoolFileName);
        }

        public void Dispose()
        {
            IO.DataRootOverride = null;
            try { Directory.Delete(root, true); } catch { /* best effort */ }
        }

        private static List<Entry> SampleEntries()
        {
            return new List<Entry>
            {
                new Entry
                {
                    ID = Guid.NewGuid().ToString(), Created = "2026-11-08 12:00:00", Changed = "2026-11-08 12:30:00",
                    Pin = true, Rank = 1, PresetColor = 0, CustomColor = "", Protected = false, CaretPosition = 3, Bookmark = 0,
                    RelatedEntries = "", Title = "Blåbærgrød 🚢", Content = "Line 1\r\nLine 2 – æøå <tag> & ]]> end"
                }
            };
        }

        private bool Save(PoolKey key)
        {
            return IO.SaveEntries(SampleEntries(), new List<StickySettings>(), "log", "", "", "memo text", 0, "home",
                key, false, "cypher", false, "", false, "", "", "", "", false);
        }

        private static PoolUnlocker UnlockWith(string password, Action<PoolKey> onUnlocked = null)
        {
            return (data, load) =>
            {
                Assert.True(PoolFile.TryReadHeader(data, out byte[] salt, out int iterations));
                PoolKey key = PoolKey.Derive(password, salt, iterations);
                if (PoolFile.TryDecrypt(data, key, out string xml))
                {
                    onUnlocked?.Invoke(key);
                    return xml;
                }
                key.Dispose();
                load.UnlockCancelled = true;
                return null;
            };
        }

        [Fact]
        public void SavedPool_IsEncryptedOnDisk_AndRoundTrips()
        {
            using PoolKey key = PoolKey.CreateNew("correct horse");
            Assert.True(Save(key));

            byte[] onDisk = File.ReadAllBytes(poolPath);
            Assert.True(PoolFile.IsEncryptedPool(onDisk));
            string asText = Encoding.UTF8.GetString(onDisk);
            Assert.DoesNotContain("Blåbærgrød", asText);
            Assert.DoesNotContain("memo text", asText);
            Assert.False(File.Exists(poolPath + ".tmp"));
            Assert.True(IO.VerifySavedPool(key));

            Load loaded = IO.LoadEntries(new List<Entry>(), UnlockWith("correct horse"), false, false, false, "");
            Assert.True(loaded.LoadSuccess, loaded.LoadMessage);
            Assert.False(loaded.IsLegacyFormat);
            Assert.Single(loaded.Entries);
            Assert.Equal("Blåbærgrød 🚢", loaded.Entries[0].Title);
            Assert.Equal("memo text", loaded.MemoText);
            Assert.Equal("cypher", loaded.CypherKey);
        }

        [Fact]
        public void SavedPool_DoesNotContainPasswordOrRecoveryQuestion()
        {
            string xml = null;
            using PoolKey key = PoolKey.CreateNew("pw");
            Assert.True(Save(key));
            IO.LoadEntries(new List<Entry>(), (data, load) =>
            {
                PoolFile.TryDecrypt(data, key, out xml);
                return xml;
            }, false, false, false, "");

            Assert.NotNull(xml);
            Assert.DoesNotContain("<seta>", xml);
            Assert.DoesNotContain("<setb>", xml);
            Assert.DoesNotContain("<setc>", xml);
        }

        [Fact]
        public void WrongPassword_DoesNotLoad()
        {
            using PoolKey key = PoolKey.CreateNew("right");
            Assert.True(Save(key));

            Load loaded = IO.LoadEntries(new List<Entry>(), UnlockWith("wrong"), false, false, false, "");
            Assert.False(loaded.LoadSuccess);
            Assert.True(loaded.UnlockCancelled);
            Assert.Empty(loaded.Entries);
        }

        [Fact]
        public void SaveWithoutKey_IsRefused()
        {
            Assert.False(Save(null));
            Assert.False(File.Exists(poolPath));
        }

        [Fact]
        public void LegacyPool_IsReadAndFlagged_ThenUpgradesToNewFormat()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(poolPath));
            File.WriteAllText(poolPath, LegacyWriter.EncryptPool(LegacyWriter.SamplePoolXml) + "\r\n", new UTF8Encoding(true));

            bool unlockerCalled = false;
            Load loaded = IO.LoadEntries(new List<Entry>(), (d, l) => { unlockerCalled = true; return null; }, false, false, false, "");

            Assert.True(loaded.LoadSuccess, loaded.LoadMessage);
            Assert.False(unlockerCalled);                 // no password involved in the old format
            Assert.True(loaded.IsLegacyFormat);
            Assert.Equal("oldpass", loaded.LegacyPassword);
            Assert.Equal(2, loaded.Entries.Count);
            Assert.Equal("Indkøbsliste", loaded.Entries[0].Title);
            Assert.Equal("Huskeseddel", loaded.MemoText);

            // The old file shows up as a legacy file to clean up
            Assert.Contains(poolPath, IO.FindLegacyPoolFiles());

            // Upgrade: save with a new password, then read back
            using PoolKey newKey = PoolKey.CreateNew("new password");
            Assert.True(IO.SaveEntries(loaded.Entries, loaded.Stickies, loaded.LogText, "", "", loaded.MemoText, 0, loaded.DisplayText,
                newKey, false, loaded.CypherKey, false, "", false, "", "", "", "", false));
            Assert.True(PoolFile.IsEncryptedPool(File.ReadAllBytes(poolPath)));
            Assert.DoesNotContain(poolPath, IO.FindLegacyPoolFiles());

            Load upgraded = IO.LoadEntries(new List<Entry>(), UnlockWith("new password"), false, false, false, "");
            Assert.True(upgraded.LoadSuccess, upgraded.LoadMessage);
            Assert.False(upgraded.IsLegacyFormat);
            Assert.Equal(2, upgraded.Entries.Count);
            Assert.Equal("Mælk, æg, brød", upgraded.Entries[0].Content);
            Assert.Equal("", upgraded.LegacyPassword);
        }

        [Fact]
        public void LegacyBackups_AreFound()
        {
            string backupDir = Path.Combine(root, IO.ProgramFolderName, IO.BackupFolderName);
            Directory.CreateDirectory(backupDir);
            string legacyBackup = Path.Combine(backupDir, "A-" + IO.BackupFileName);
            File.WriteAllText(legacyBackup, LegacyWriter.EncryptPool(LegacyWriter.SamplePoolXml));

            using PoolKey key = PoolKey.CreateNew("pw");
            string newBackup = Path.Combine(backupDir, "B-" + IO.BackupFileName);
            File.WriteAllBytes(newBackup, PoolFile.Encrypt(LegacyWriter.SamplePoolXml, key));
            File.WriteAllText(Path.Combine(backupDir, "notes" + IO.PoolFileExtension), "not a pool");

            List<string> found = IO.FindLegacyPoolFiles();
            Assert.Contains(legacyBackup, found);
            Assert.DoesNotContain(newBackup, found);
            Assert.Single(found);
        }

        [Fact]
        public void LegacyWriter_MatchesRealAhoy1xOutput()
        {
            // Sanity check of the test helper against the vector produced by Ahoy 1.x on .NET Framework
            Assert.Equal(LegacyVectors.PoolCipher, LegacyWriter.EncryptPool(LegacyVectors.PoolXml));
        }
    }

    [CollectionDefinition("IO", DisableParallelization = true)]
    public class IoCollection { }

    /// <summary>Test-only re-implementation of the Ahoy 1.x writer (3DES, built-in key).</summary>
    internal static class LegacyWriter
    {
        private static readonly byte[] Salt = { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };

        internal static string EncryptPool(string xml)
        {
            string subKey = Decrypt("L7ZDqTw/7uAuu6B6X2wA5g==", "rNIDicTKNPMYgkZpXRcaPA==");
            string poolKey = Decrypt("i7LpiDtiQUkuqFXDB4KJvw==", subKey);
            return Encrypt(xml, poolKey);
        }

        private static (byte[] key, byte[] iv) Derive(string password)
        {
#pragma warning disable SYSLIB0041
            using var pdb = new PasswordDeriveBytes(password, Salt);
            return (pdb.GetBytes(16), pdb.GetBytes(8));
#pragma warning restore SYSLIB0041
        }

        private static string Encrypt(string text, string password)
        {
            var (key, iv) = Derive(password);
            using var des = TripleDES.Create();
            byte[] input = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(des.CreateEncryptor(key, iv).TransformFinalBlock(input, 0, input.Length));
        }

        private static string Decrypt(string base64, string password)
        {
            var (key, iv) = Derive(password);
            using var des = TripleDES.Create();
            byte[] input = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(des.CreateDecryptor(key, iv).TransformFinalBlock(input, 0, input.Length));
        }

        // Shaped like a pool written by Ahoy 1.6 (including the data of features that were removed)
        internal const string SamplePoolXml =
            "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\" ?>\r\n" +
            "<entries>\r\n" +
            "    <settings>\r\n" +
            "        <seta><![CDATA[Athena?]]></seta>\r\n" +
            "        <setb><![CDATA[Palladium]]></setb>\r\n" +
            "        <setc><![CDATA[oldpass]]></setc>\r\n" +
            "        <protect>False</protect>\r\n" +
            "    </settings>\r\n" +
            "    <session>\r\n        <id>abc</id>\r\n        <changed>2022-08-08 10:00:00</changed>\r\n    </session>\r\n" +
            "    <log>\r\n        <text><![CDATA[]]></text>\r\n    </log>\r\n" +
            "    <hold>\r\n        <ha></ha>\r\n        <hb></hb>\r\n    </hold>\r\n" +
            "    <memo>\r\n        <text><![CDATA[Huskeseddel]]></text>\r\n    </memo>\r\n" +
            "    <mcaret>\r\n        <caret><![CDATA[0]]></caret>\r\n    </mcaret>\r\n" +
            "    <uebanka>\r\n        <text><![CDATA[utility text]]></text>\r\n    </uebanka>\r\n" +
            "    <uelinkedid>\r\n        <id></id>\r\n    </uelinkedid>\r\n" +
            "    <cypher>\r\n       <text><![CDATA[]]></text>\r\n    </cypher>\r\n" +
            "    <typermaxb>\r\n       <high><![CDATA[42]]></high>\r\n    </typermaxb>\r\n" +
            "    <display>\r\n        <text><![CDATA[]]></text>\r\n    </display>\r\n" +
            "    <dividers>\r\n        <dividera><![CDATA[---]]></dividera>\r\n        <dividerb><![CDATA[===]]></dividerb>\r\n        <dividerc><![CDATA[***]]></dividerc>\r\n        <dividerd><![CDATA[...]]></dividerd>\r\n    </dividers>\r\n" +
            "    <entry>\r\n        <id>1</id>\r\n        <cre>2022-08-08 10:00:00</cre>\r\n        <cha>2022-08-08 10:00:00</cha>\r\n" +
            "        <ntf>2022-08-09 10:00:00</ntf>\r\n        <ntr></ntr>\r\n        <ntt><![CDATA[remind me]]></ntt>\r\n        <ntp>0</ntp>\r\n" +
            "        <pin>True</pin>\r\n        <rnk>0</rnk>\r\n        <sta>0</sta>\r\n        <cuc><![CDATA[]]></cuc>\r\n        <pro>False</pro>\r\n        <pos>0</pos>\r\n        <bmk>0</bmk>\r\n" +
            "        <rel></rel>\r\n        <rev>False</rev>\r\n        <res>0</res>\r\n" +
            "        <tit><![CDATA[Indkøbsliste]]></tit>\r\n        <con><![CDATA[Mælk, æg, brød]]></con>\r\n    </entry>\r\n" +
            "    <entry>\r\n        <id>2</id>\r\n        <cre>2022-08-08 11:00:00</cre>\r\n        <cha>2022-08-08 11:00:00</cha>\r\n" +
            "        <pin>False</pin>\r\n        <pro>False</pro>\r\n        <pos>0</pos>\r\n" +
            "        <tit><![CDATA[Second]]></tit>\r\n        <con><![CDATA[Body]]></con>\r\n    </entry>\r\n" +
            "</entries>\r\n";
    }
}
