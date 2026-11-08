using System;
using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text;

namespace AhoyBrowser.Crypto
{
    /// <summary>
    /// "Message Encryption" tool: encrypts a text selection with a passphrase so it can be shared as
    /// plain text. Output: "AHOY2:" + Base64(iterations[4, LE] | salt[16] | nonce[12] | ciphertext | tag[16]),
    /// AES-256-GCM with a PBKDF2-HMAC-SHA256 key. The prefix and the iteration count are authenticated.
    /// </summary>
    internal static class MessageCrypto
    {
        internal const string Prefix = "AHOY2:";
        private const int IterationsSize = 4;
        private const int NonceSize = 12;
        private const int TagSize = 16;
        private const int HeaderSize = IterationsSize + PoolKey.SaltSize + NonceSize;

        internal static string Encrypt(string message, string passphrase)
        {
            if (string.IsNullOrEmpty(passphrase))
                throw new ArgumentException("A passphrase is required.", nameof(passphrase));

            int iterations = PoolKey.DefaultIterations;
            byte[] plain = Encoding.UTF8.GetBytes(message);
            byte[] blob = new byte[HeaderSize + plain.Length + TagSize];
            BinaryPrimitives.WriteUInt32LittleEndian(blob.AsSpan(0, IterationsSize), (uint)iterations);
            Span<byte> salt = blob.AsSpan(IterationsSize, PoolKey.SaltSize);
            RandomNumberGenerator.Fill(salt);
            Span<byte> nonce = blob.AsSpan(IterationsSize + PoolKey.SaltSize, NonceSize);
            RandomNumberGenerator.Fill(nonce);

            using PoolKey key = PoolKey.Derive(passphrase, salt, iterations);
            using var aes = new AesGcm(key.KeyBytes, TagSize);
            aes.Encrypt(nonce, plain,
                blob.AsSpan(HeaderSize, plain.Length),
                blob.AsSpan(blob.Length - TagSize, TagSize),
                AssociatedData(blob));
            CryptographicOperations.ZeroMemory(plain);

            return Prefix + Convert.ToBase64String(blob);
        }

        /// <summary>Decrypt current-format text, or text from the Ahoy 1.x (3DES) tool.</summary>
        internal static bool TryDecrypt(string text, string passphrase, out string message)
        {
            message = null;
            if (string.IsNullOrEmpty(passphrase))
                return false;
            text = text.Trim();

            if (!text.StartsWith(Prefix, StringComparison.Ordinal))
                return Legacy.TryDecryptMessage(text, passphrase, out message);

            byte[] blob;
            try
            {
                blob = Convert.FromBase64String(text[Prefix.Length..]);
            }
            catch (FormatException)
            {
                return false;
            }
            if (blob.Length < HeaderSize + TagSize)
                return false;

            uint iterations = BinaryPrimitives.ReadUInt32LittleEndian(blob.AsSpan(0, IterationsSize));
            if (iterations < PoolKey.MinIterations || iterations > PoolKey.MaxIterations)
                return false;

            using PoolKey key = PoolKey.Derive(passphrase, blob.AsSpan(IterationsSize, PoolKey.SaltSize), (int)iterations);
            byte[] plain = new byte[blob.Length - HeaderSize - TagSize];
            try
            {
                using var aes = new AesGcm(key.KeyBytes, TagSize);
                aes.Decrypt(blob.AsSpan(IterationsSize + PoolKey.SaltSize, NonceSize),
                    blob.AsSpan(HeaderSize, plain.Length),
                    blob.AsSpan(blob.Length - TagSize, TagSize),
                    plain,
                    AssociatedData(blob));
                message = Encoding.UTF8.GetString(plain);
                return true;
            }
            catch (AuthenticationTagMismatchException)
            {
                return false;
            }
            finally
            {
                CryptographicOperations.ZeroMemory(plain);
            }
        }

        // The prefix and the iteration count are authenticated together with the ciphertext
        private static byte[] AssociatedData(byte[] blob)
        {
            byte[] prefix = Encoding.ASCII.GetBytes(Prefix);
            byte[] data = new byte[prefix.Length + IterationsSize];
            prefix.CopyTo(data, 0);
            Array.Copy(blob, 0, data, prefix.Length, IterationsSize);
            return data;
        }

        /// <summary>True if the text looks like something this tool can decrypt.</summary>
        internal static bool LooksEncrypted(string text)
        {
            text = text.Trim();
            if (text.StartsWith(Prefix, StringComparison.Ordinal))
                return true;
            // Legacy 3DES output: Base64 of whole 8-byte blocks, no whitespace
            if (text.Length == 0 || text.Length % 4 != 0 || text.Contains(' ') || text.Contains('\n'))
                return false;
            Span<byte> buffer = new byte[text.Length];
            return Convert.TryFromBase64String(text, buffer, out int written) && written > 0 && written % 8 == 0;
        }
    }
}
