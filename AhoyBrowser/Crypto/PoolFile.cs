using System;
using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text;

namespace AhoyBrowser.Crypto
{
    /// <summary>
    /// Ahoy pool file format, version 2 (AES-256-GCM).
    ///
    ///   offset  size  field
    ///   0       5     magic "AHOY\0"  (the 0x00 byte can never occur in the legacy Base64 format)
    ///   5       1     format version (2)
    ///   6       1     KDF id (1 = PBKDF2-HMAC-SHA256)
    ///   7       4     KDF iterations (uint32, little-endian)
    ///   11      16    salt
    ///   27      12    nonce (random, new for every save)
    ///   39      n     ciphertext (UTF-8 XML)
    ///   39+n    16    GCM authentication tag
    ///
    /// The whole 39-byte header is authenticated as associated data, so any change to
    /// the header, ciphertext or tag makes decryption fail.
    /// </summary>
    internal static class PoolFile
    {
        private static ReadOnlySpan<byte> Magic => "AHOY\0"u8;

        internal const byte FormatVersion = 2;
        internal const byte KdfPbkdf2Sha256 = 1;
        internal const int NonceSize = 12;
        internal const int TagSize = 16;
        internal const int HeaderSize = 5 + 1 + 1 + 4 + PoolKey.SaltSize + NonceSize; // 39

        internal const string AlgorithmDescription = "AES-256-GCM";
        internal static string KdfDescription => $"PBKDF2-HMAC-SHA256, {PoolKey.DefaultIterations:N0} iterations";

        /// <summary>True if the data starts with the version 2 magic (otherwise it may be a legacy pool).</summary>
        internal static bool IsEncryptedPool(ReadOnlySpan<byte> data)
        {
            return data.Length >= Magic.Length && data[..Magic.Length].SequenceEqual(Magic);
        }

        /// <summary>Read the KDF parameters from a version 2 header without decrypting.</summary>
        internal static bool TryReadHeader(ReadOnlySpan<byte> data, out byte[] salt, out int iterations)
        {
            salt = null;
            iterations = 0;

            if (!IsEncryptedPool(data) || data.Length < HeaderSize + TagSize)
                return false;
            if (data[5] != FormatVersion || data[6] != KdfPbkdf2Sha256)
                return false;

            uint iter = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(7, 4));
            if (iter < PoolKey.MinIterations || iter > PoolKey.MaxIterations)
                return false;

            iterations = (int)iter;
            salt = data.Slice(11, PoolKey.SaltSize).ToArray();
            return true;
        }

        internal static byte[] Encrypt(string plaintext, PoolKey key)
        {
            ArgumentNullException.ThrowIfNull(plaintext);
            ArgumentNullException.ThrowIfNull(key);

            byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext);
            try
            {
                byte[] output = new byte[HeaderSize + plainBytes.Length + TagSize];
                Span<byte> header = output.AsSpan(0, HeaderSize);

                Magic.CopyTo(header);
                header[5] = FormatVersion;
                header[6] = KdfPbkdf2Sha256;
                BinaryPrimitives.WriteUInt32LittleEndian(header.Slice(7, 4), (uint)key.Iterations);
                key.Salt.CopyTo(header.Slice(11, PoolKey.SaltSize));
                Span<byte> nonce = header.Slice(11 + PoolKey.SaltSize, NonceSize);
                RandomNumberGenerator.Fill(nonce);

                Span<byte> ciphertext = output.AsSpan(HeaderSize, plainBytes.Length);
                Span<byte> tag = output.AsSpan(HeaderSize + plainBytes.Length, TagSize);

                using var aes = new AesGcm(key.KeyBytes, TagSize);
                aes.Encrypt(nonce, plainBytes, ciphertext, tag, header);
                return output;
            }
            finally
            {
                CryptographicOperations.ZeroMemory(plainBytes);
            }
        }

        /// <summary>
        /// Decrypt a version 2 pool. Returns false if the key is wrong or the file was
        /// modified or corrupted (GCM cannot tell these apart).
        /// </summary>
        internal static bool TryDecrypt(ReadOnlySpan<byte> data, PoolKey key, out string plaintext)
        {
            plaintext = null;

            if (key == null || !TryReadHeader(data, out byte[] salt, out int iterations) || !key.Matches(salt, iterations))
                return false;

            ReadOnlySpan<byte> header = data[..HeaderSize];
            ReadOnlySpan<byte> nonce = header.Slice(11 + PoolKey.SaltSize, NonceSize);
            ReadOnlySpan<byte> ciphertext = data[HeaderSize..^TagSize];
            ReadOnlySpan<byte> tag = data[^TagSize..];

            byte[] plainBytes = new byte[ciphertext.Length];
            try
            {
                using var aes = new AesGcm(key.KeyBytes, TagSize);
                aes.Decrypt(nonce, ciphertext, tag, plainBytes, header);
                plaintext = Encoding.UTF8.GetString(plainBytes);
                return true;
            }
            catch (AuthenticationTagMismatchException)
            {
                return false;
            }
            finally
            {
                CryptographicOperations.ZeroMemory(plainBytes);
            }
        }
    }
}
