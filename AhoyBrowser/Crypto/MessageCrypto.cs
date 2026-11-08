using System;
using System.Security.Cryptography;
using System.Text;

namespace AhoyBrowser.Crypto
{
    /// <summary>
    /// "Message Encryption" tool: encrypts a text selection with a passphrase so it can be
    /// shared as plain text. Output: "AHOY2:" + Base64(salt[16] | nonce[12] | ciphertext | tag[16]),
    /// AES-256-GCM with a PBKDF2-HMAC-SHA256 key. The "AHOY2:" prefix is authenticated.
    /// </summary>
    internal static class MessageCrypto
    {
        internal const string Prefix = "AHOY2:";
        private const int NonceSize = 12;
        private const int TagSize = 16;

        internal static string Encrypt(string message, string passphrase)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(PoolKey.SaltSize);
            using PoolKey key = PoolKey.Derive(passphrase, salt, PoolKey.DefaultIterations);

            byte[] plain = Encoding.UTF8.GetBytes(message);
            byte[] blob = new byte[PoolKey.SaltSize + NonceSize + plain.Length + TagSize];
            salt.CopyTo(blob, 0);
            Span<byte> nonce = blob.AsSpan(PoolKey.SaltSize, NonceSize);
            RandomNumberGenerator.Fill(nonce);

            using var aes = new AesGcm(key.KeyBytes, TagSize);
            aes.Encrypt(nonce, plain,
                blob.AsSpan(PoolKey.SaltSize + NonceSize, plain.Length),
                blob.AsSpan(blob.Length - TagSize, TagSize),
                Encoding.ASCII.GetBytes(Prefix));
            CryptographicOperations.ZeroMemory(plain);

            return Prefix + Convert.ToBase64String(blob);
        }

        /// <summary>Decrypt current-format text, or text from the Ahoy 1.x (3DES) tool.</summary>
        internal static bool TryDecrypt(string text, string passphrase, out string message)
        {
            message = null;
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
            if (blob.Length < PoolKey.SaltSize + NonceSize + TagSize)
                return false;

            using PoolKey key = PoolKey.Derive(passphrase, blob.AsSpan(0, PoolKey.SaltSize), PoolKey.DefaultIterations);
            byte[] plain = new byte[blob.Length - PoolKey.SaltSize - NonceSize - TagSize];
            try
            {
                using var aes = new AesGcm(key.KeyBytes, TagSize);
                aes.Decrypt(blob.AsSpan(PoolKey.SaltSize, NonceSize),
                    blob.AsSpan(PoolKey.SaltSize + NonceSize, plain.Length),
                    blob.AsSpan(blob.Length - TagSize, TagSize),
                    plain,
                    Encoding.ASCII.GetBytes(Prefix));
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
