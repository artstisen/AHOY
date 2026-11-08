using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AhoyBrowser.Crypto
{
    /// <summary>
    /// READ-ONLY support for the format used by Ahoy 1.x (3DES, Base64 text).
    ///
    /// This exists only so old pools and old "Message Encryption" output can be opened and
    /// upgraded. Nothing is ever written in this format again.
    ///
    /// Ahoy 1.x pools were NOT protected by the user's password: they were encrypted with the
    /// key below, which is embedded in every Ahoy 1.x executable and is therefore public. Treat
    /// any legacy pool file (and its backups) as readable by anyone who has a copy of it.
    /// </summary>
    internal static class Legacy
    {
        // Ahoy 1.x pool key chain: the pool key was stored encrypted (A) under a sub-key (B),
        // which was itself encrypted under C. Kept verbatim for compatibility.
        private const string PoolKeyA = "i7LpiDtiQUkuqFXDB4KJvw==";
        private const string PoolKeyB = "L7ZDqTw/7uAuu6B6X2wA5g==";
        private const string PoolKeyC = "rNIDicTKNPMYgkZpXRcaPA==";

        // Fixed salt used by every Ahoy 1.x key derivation
        private static readonly byte[] Salt = { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };

        /// <summary>Try to decrypt an Ahoy 1.x pool file (Base64 text).</summary>
        internal static bool TryDecryptPool(string fileText, out string xml)
        {
            xml = null;
            try
            {
                string subKey = Decrypt(PoolKeyB, PoolKeyC);
                string poolKey = Decrypt(PoolKeyA, subKey);
                xml = Decrypt(fileText.Trim(), poolKey);
                return xml.TrimStart('﻿', ' ', '\r', '\n').StartsWith("<?xml", StringComparison.Ordinal);
            }
            catch (Exception ex) when (ex is CryptographicException or FormatException or ArgumentException)
            {
                xml = null;
                return false;
            }
        }

        /// <summary>Try to decrypt text produced by the Ahoy 1.x "Message Encryption" tool.</summary>
        internal static bool TryDecryptMessage(string base64, string passphrase, out string message)
        {
            message = null;
            try
            {
                message = Decrypt(base64.Trim(), passphrase);
                return true;
            }
            catch (Exception ex) when (ex is CryptographicException or FormatException or ArgumentException)
            {
                return false;
            }
        }

        private static string Decrypt(string base64, string password)
        {
            byte[] input = Convert.FromBase64String(base64);

#pragma warning disable SYSLIB0041 // PBKDF1-style derivation required to read the legacy format
            using var pdb = new PasswordDeriveBytes(password, Salt);
            byte[] key = pdb.GetBytes(16);
            byte[] iv = pdb.GetBytes(8);
#pragma warning restore SYSLIB0041

            using var des = TripleDES.Create();
            using ICryptoTransform decryptor = des.CreateDecryptor(key, iv);
            using var output = new MemoryStream();
            using (var crypto = new CryptoStream(output, decryptor, CryptoStreamMode.Write))
            {
                crypto.Write(input, 0, input.Length);
                crypto.FlushFinalBlock();
            }
            // A wrong password usually fails the padding check, but not always: only accept valid UTF-8
            return new UTF8Encoding(false, throwOnInvalidBytes: true).GetString(output.ToArray());
        }
    }
}
