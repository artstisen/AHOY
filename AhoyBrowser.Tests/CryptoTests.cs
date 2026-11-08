using System;
using System.Text;
using AhoyBrowser.Crypto;
using Xunit;

namespace AhoyBrowser.Tests
{
    public class PoolFileTests
    {
        private const string Xml = "<?xml version=\"1.0\"?><entries><entry><tit><![CDATA[Blåbærgrød – 日本 🚢]]></tit></entry></entries>";

        [Fact]
        public void RoundTrip_WithSameKey()
        {
            using PoolKey key = PoolKey.CreateNew("correct horse battery staple");
            byte[] file = PoolFile.Encrypt(Xml, key);

            Assert.True(PoolFile.IsEncryptedPool(file));
            Assert.True(PoolFile.TryDecrypt(file, key, out string result));
            Assert.Equal(Xml, result);
        }

        [Fact]
        public void RoundTrip_WithKeyRederivedFromHeader()
        {
            byte[] file;
            using (PoolKey key = PoolKey.CreateNew("pässwörd"))
                file = PoolFile.Encrypt(Xml, key);

            Assert.True(PoolFile.TryReadHeader(file, out byte[] salt, out int iterations));
            Assert.Equal(PoolKey.DefaultIterations, iterations);

            using PoolKey rederived = PoolKey.Derive("pässwörd", salt, iterations);
            Assert.True(PoolFile.TryDecrypt(file, rederived, out string result));
            Assert.Equal(Xml, result);
        }

        [Fact]
        public void WrongPassword_Fails()
        {
            byte[] file;
            using (PoolKey key = PoolKey.CreateNew("right"))
                file = PoolFile.Encrypt(Xml, key);

            PoolFile.TryReadHeader(file, out byte[] salt, out int iterations);
            using PoolKey wrong = PoolKey.Derive("wrong", salt, iterations);
            Assert.False(PoolFile.TryDecrypt(file, wrong, out string result));
            Assert.Null(result);
        }

        [Theory]
        [InlineData(6)]   // KDF id
        [InlineData(8)]   // iterations
        [InlineData(15)]  // salt
        [InlineData(30)]  // nonce
        [InlineData(45)]  // ciphertext
        [InlineData(-1)]  // tag (last byte)
        public void TamperingAnyByte_Fails(int offset)
        {
            using PoolKey key = PoolKey.CreateNew("pw");
            byte[] file = PoolFile.Encrypt(Xml, key);
            int i = offset >= 0 ? offset : file.Length + offset;
            file[i] ^= 0x01;

            Assert.False(PoolFile.TryDecrypt(file, key, out _));
        }

        [Fact]
        public void EverySave_UsesFreshNonce()
        {
            using PoolKey key = PoolKey.CreateNew("pw");
            byte[] a = PoolFile.Encrypt(Xml, key);
            byte[] b = PoolFile.Encrypt(Xml, key);

            Assert.False(a.AsSpan(27, PoolFile.NonceSize).SequenceEqual(b.AsSpan(27, PoolFile.NonceSize)));
            Assert.False(a.AsSpan().SequenceEqual(b));
        }

        [Fact]
        public void NewKeys_UseFreshSalt()
        {
            using PoolKey a = PoolKey.CreateNew("pw");
            using PoolKey b = PoolKey.CreateNew("pw");
            Assert.False(a.Salt.AsSpan().SequenceEqual(b.Salt));
            Assert.False(a.KeyBytes.SequenceEqual(b.KeyBytes));
        }

        [Fact]
        public void Header_RejectsUnreasonableIterationCount()
        {
            using PoolKey key = PoolKey.CreateNew("pw");
            byte[] file = PoolFile.Encrypt(Xml, key);
            BitConverter.GetBytes(uint.MaxValue).CopyTo(file, 7);
            Assert.False(PoolFile.TryReadHeader(file, out _, out _));
        }

        [Fact]
        public void LegacyBase64_IsNotMistakenForNewFormat()
        {
            Assert.False(PoolFile.IsEncryptedPool(Encoding.UTF8.GetBytes(LegacyVectors.PoolCipher)));
        }
    }

    public class PoolKeyTests
    {
        [Fact]
        public void Verify_AcceptsOnlyTheRightPassword()
        {
            using PoolKey key = PoolKey.CreateNew("s3cret");
            Assert.True(key.Verify("s3cret"));
            Assert.False(key.Verify("s3cret "));
            Assert.False(key.Verify("S3cret"));
            Assert.False(key.Verify(""));
        }

        [Fact]
        public void UnicodeNormalization_ComposedAndDecomposedMatch()
        {
            using PoolKey key = PoolKey.CreateNew("café");     // é as one code point
            Assert.True(key.Verify("café"));                  // e + combining accent
        }

        [Fact]
        public void Dispose_ClearsKey()
        {
            PoolKey key = PoolKey.CreateNew("pw");
            key.Dispose();
            Assert.Throws<ObjectDisposedException>(() => key.KeyBytes.ToArray());
        }
    }

    public class MessageCryptoTests
    {
        [Fact]
        public void RoundTrip()
        {
            string cipher = MessageCrypto.Encrypt("Hemmelig besked æøå", "k3y!");
            Assert.StartsWith(MessageCrypto.Prefix, cipher);
            Assert.True(MessageCrypto.LooksEncrypted(cipher));
            Assert.True(MessageCrypto.TryDecrypt(cipher, "k3y!", out string plain));
            Assert.Equal("Hemmelig besked æøå", plain);
        }

        [Fact]
        public void WrongPassphrase_Fails()
        {
            string cipher = MessageCrypto.Encrypt("hello", "a");
            Assert.False(MessageCrypto.TryDecrypt(cipher, "b", out _));
        }

        [Fact]
        public void PlainText_IsNotConsideredEncrypted()
        {
            Assert.False(MessageCrypto.LooksEncrypted("Just a normal sentence."));
        }

        [Fact]
        public void DecryptsLegacy3DesMessage()
        {
            Assert.True(MessageCrypto.LooksEncrypted(LegacyVectors.MsgCipher));
            Assert.True(MessageCrypto.TryDecrypt(LegacyVectors.MsgCipher, LegacyVectors.MsgKey, out string plain));
            Assert.Equal(LegacyVectors.MsgPlain, plain);
        }
    }

    public class LegacyTests
    {
        [Fact]
        public void DecryptsPoolWrittenByAhoy1x()
        {
            Assert.True(Legacy.TryDecryptPool(LegacyVectors.PoolCipher, out string xml));
            Assert.Equal(LegacyVectors.PoolXml, xml);
        }

        [Fact]
        public void DecryptsPoolFileAsWrittenToDisk()
        {
            // Ahoy 1.x wrote the Base64 with StreamWriter(UTF-8) + WriteLine: BOM + text + CRLF
            string onDisk = "﻿" + LegacyVectors.PoolCipher + "\r\n";
            Assert.True(Legacy.TryDecryptPool(onDisk.TrimStart('﻿'), out string xml));
            Assert.Equal(LegacyVectors.PoolXml, xml);
        }

        [Fact]
        public void GarbageIsRejected()
        {
            Assert.False(Legacy.TryDecryptPool("not a pool", out _));
            Assert.False(Legacy.TryDecryptPool(Convert.ToBase64String(new byte[64]), out _));
        }
    }

    /// <summary>
    /// Produced by the original Ahoy 1.x Authorization.cs (git: initial import) compiled and run
    /// on .NET Framework 4.8 (CLR 4.0.30319.42000).
    /// </summary>
    internal static class LegacyVectors
    {
        internal const string PoolXml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\" ?>\r\n<entries>\r\n    <entry><tit><![CDATA[Blåbærgrød – 日本 🚢]]></tit></entry>\r\n</entries>\r\n";
        internal const string PoolCipher = "OmonPBBsWbu0SCytodgHzTf8+SKHJeeAp63kdMtzjrGRb3MjoOJQ+pOct7hzLFHDz49tTCUzSLwaL6uivNSHEejpzIQHQPRk5WdqpRxJtpYmSB/rN1yqHWN78tIxtPO0BEPQMy+hXORgbzp/52rg5pPLaUBYIkDCBaE9IdqU8vZzOhqvRZOs53fLhvoz1+iXkHgsVFUreOSPuD5dFhbFCg==";
        internal const string MsgPlain = "Hemmelig besked æøå";
        internal const string MsgKey = "k3y!";
        internal const string MsgCipher = "jgHAKWxdD5chYi2CAb9oZKs8IcxNUy/B";
    }
}
