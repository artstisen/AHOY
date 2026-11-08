using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using AhoyBrowser.Crypto;
using Xunit;

namespace AhoyBrowser.Tests
{
    /// <summary>Tests for issues fixed in the 2.0 code review.</summary>
    public class MessageCryptoFormatTests
    {
        [Fact]
        public void EmptyPassphrase_IsRejected()
        {
            Assert.Throws<ArgumentException>(() => MessageCrypto.Encrypt("hello", ""));
            string cipher = MessageCrypto.Encrypt("hello", "k");
            Assert.False(MessageCrypto.TryDecrypt(cipher, "", out _));
        }

        [Fact]
        public void IterationCount_IsStoredAndAuthenticated()
        {
            string cipher = MessageCrypto.Encrypt("hello", "k");
            byte[] blob = Convert.FromBase64String(cipher[MessageCrypto.Prefix.Length..]);
            Assert.Equal(PoolKey.DefaultIterations, BitConverter.ToInt32(blob, 0));

            // A changed (but valid) iteration count must not decrypt
            BitConverter.GetBytes(PoolKey.DefaultIterations + 1).CopyTo(blob, 0);
            Assert.False(MessageCrypto.TryDecrypt(MessageCrypto.Prefix + Convert.ToBase64String(blob), "k", out _));
        }

        [Fact]
        public void TamperedCiphertext_Fails()
        {
            string cipher = MessageCrypto.Encrypt("hello world", "k");
            byte[] blob = Convert.FromBase64String(cipher[MessageCrypto.Prefix.Length..]);
            blob[blob.Length - 20] ^= 1;
            Assert.False(MessageCrypto.TryDecrypt(MessageCrypto.Prefix + Convert.ToBase64String(blob), "k", out _));
        }

        [Fact]
        public void OutOfRangeIterationCount_IsRejectedWithoutDeriving()
        {
            string cipher = MessageCrypto.Encrypt("hello", "k");
            byte[] blob = Convert.FromBase64String(cipher[MessageCrypto.Prefix.Length..]);
            BitConverter.GetBytes(int.MaxValue).CopyTo(blob, 0);
            Assert.False(MessageCrypto.TryDecrypt(MessageCrypto.Prefix + Convert.ToBase64String(blob), "k", out _));
        }
    }

    public class InvariantCultureTests
    {
        [Fact]
        public void ParsesNumbersWrittenUnderAnyCulture()
        {
            CultureInfo previous = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("da-DK");
                Assert.Equal(0.9, InvariantCulture.ParseDouble("0,9"), 6);   // written by a Danish 1.x install
                Assert.Equal(0.9, InvariantCulture.ParseDouble("0.9"), 6);
                Assert.Equal(-3, InvariantCulture.ParseInt("\u22123"));     // Unicode minus sign
                Assert.Equal(42, InvariantCulture.ParseInt("42"));
            }
            finally
            {
                CultureInfo.CurrentCulture = previous;
            }
        }

        [Fact]
        public void UseScope_FormatsInvariantly_AndRestores()
        {
            CultureInfo previous = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("da-DK");
                using (InvariantCulture.Use())
                {
                    Assert.Equal("0.9", 0.9.ToString());
                }
                Assert.Equal("da-DK", CultureInfo.CurrentCulture.Name);
            }
            finally
            {
                CultureInfo.CurrentCulture = previous;
            }
        }
    }

    [Collection("IO")]
    public sealed class LegacyFileDiscoveryTests : IDisposable
    {
        private readonly string root;

        public LegacyFileDiscoveryTests()
        {
            root = Path.Combine(Path.GetTempPath(), "AhoyTests-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(root);
            IO.DataRootOverride = root;
            IO.ClearCustomFilePath(false);
            IO.UseDemoPool = false;
            IO.UseSecondaryPool = false;
            Log.InitializeLog();
        }

        public void Dispose()
        {
            IO.DataRootOverride = null;
            try { Directory.Delete(root, true); } catch { /* best effort */ }
        }

        [Fact]
        public void OnlyThisPoolsFilesAreOfferedForDeletion()
        {
            string folder = Path.Combine(root, IO.ProgramFolderName);
            Directory.CreateDirectory(folder);
            string legacy = LegacyWriter.EncryptPool(LegacyWriter.SamplePoolXml) + "\r\n";
            string main = Path.Combine(folder, IO.PoolFileName);
            string other = Path.Combine(folder, "SomeOtherPool.ahoy");
            File.WriteAllText(main, legacy, new UTF8Encoding(true));
            File.WriteAllText(other, legacy, new UTF8Encoding(true));

            List<string> found = IO.FindLegacyPoolFiles();

            Assert.Contains(main, found);
            Assert.DoesNotContain(other, found);
        }
    }

    [Collection("IO")]
    public sealed class SettingsStorageTests : IDisposable
    {
        private readonly string root;

        public SettingsStorageTests()
        {
            root = Path.Combine(Path.GetTempPath(), "AhoyTests-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(root);
            IO.DataRootOverride = root;
            IO.ClearCustomFilePath(false);
            IO.UseDemoPool = false;
            IO.UseSecondaryPool = false;
            Log.InitializeLog();
        }

        public void Dispose()
        {
            IO.DataRootOverride = null;
            try { Directory.Delete(root, true); } catch { /* best effort */ }
        }

        [Fact]
        public void StringsWithCdataTerminators_RoundTrip()
        {
            Settings settings = IO.LoadSettings(); // defaults when no file exists
            settings.ThemeTitle = "My ]]> theme <b>";
            settings.OpenFileDialogPath = @"C:\Folder ]]> name";
            Assert.True(IO.SaveSettings(settings));

            Settings loaded = IO.LoadSettings();
            Assert.Equal("My ]]> theme <b>", loaded.ThemeTitle);
            Assert.Equal(@"C:\Folder ]]> name", loaded.OpenFileDialogPath);
        }

        [Fact]
        public void SettingsWrittenUnderDanishCulture_LoadTheSame()
        {
            CultureInfo previous = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("da-DK");
                Settings settings = IO.LoadSettings();
                settings.Opacity = 0.85;
                Assert.True(IO.SaveSettings(settings));

                CultureInfo.CurrentCulture = new CultureInfo("en-US");
                Assert.Equal(0.85, IO.LoadSettings().Opacity, 6);
            }
            finally
            {
                CultureInfo.CurrentCulture = previous;
            }
        }
    }
}
