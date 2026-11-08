using System;
using System.Security.Cryptography;
using System.Text;

namespace AhoyBrowser.Crypto
{
    /// <summary>
    /// A 256-bit encryption key derived from the user's password with PBKDF2-HMAC-SHA256.
    /// The password itself is never stored; only this derived key is kept in memory while the pool is open.
    /// </summary>
    internal sealed class PoolKey : IDisposable
    {
        internal const int SaltSize = 16;
        internal const int KeySize = 32; // AES-256

        // OWASP (2023) recommendation for PBKDF2-HMAC-SHA256
        internal const int DefaultIterations = 600_000;

        // Bounds accepted when reading a file header (protects against tampered/absurd values)
        internal const int MinIterations = 100_000;
        internal const int MaxIterations = 10_000_000;

        private readonly byte[] _key;
        private bool _disposed;

        internal byte[] Salt { get; }
        internal int Iterations { get; }

        private PoolKey(byte[] salt, int iterations, byte[] key)
        {
            Salt = salt;
            Iterations = iterations;
            _key = key;
        }

        /// <summary>Create a new key with a fresh random salt (new pool or password change).</summary>
        internal static PoolKey CreateNew(string password)
        {
            return Derive(password, RandomNumberGenerator.GetBytes(SaltSize), DefaultIterations);
        }

        /// <summary>Re-derive a key from a password and the salt/iterations stored in a pool file.</summary>
        internal static PoolKey Derive(string password, ReadOnlySpan<byte> salt, int iterations)
        {
            ArgumentNullException.ThrowIfNull(password);
            if (salt.Length != SaltSize)
                throw new ArgumentException("Invalid salt length.", nameof(salt));
            if (iterations < MinIterations || iterations > MaxIterations)
                throw new ArgumentOutOfRangeException(nameof(iterations));

            // Normalize so the same password typed with composed/decomposed characters gives the same key
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password.Normalize(NormalizationForm.FormC));
            try
            {
                byte[] key = Rfc2898DeriveBytes.Pbkdf2(passwordBytes, salt, iterations, HashAlgorithmName.SHA256, KeySize);
                return new PoolKey(salt.ToArray(), iterations, key);
            }
            finally
            {
                CryptographicOperations.ZeroMemory(passwordBytes);
            }
        }

        /// <summary>Check a password against this key (used for re-authentication prompts, e.g. waking from sleep).</summary>
        internal bool Verify(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;
            using PoolKey candidate = Derive(password, Salt, Iterations);
            return CryptographicOperations.FixedTimeEquals(KeyBytes, candidate.KeyBytes);
        }

        /// <summary>True if a file header was written with this key's salt and iteration count.</summary>
        internal bool Matches(ReadOnlySpan<byte> salt, int iterations)
        {
            return iterations == Iterations && salt.SequenceEqual(Salt);
        }

        internal ReadOnlySpan<byte> KeyBytes
        {
            get
            {
                ObjectDisposedException.ThrowIf(_disposed, this);
                return _key;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                CryptographicOperations.ZeroMemory(_key);
                _disposed = true;
            }
        }
    }
}
