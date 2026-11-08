using System;
using System.Globalization;

namespace AhoyBrowser
{
    /// <summary>
    /// Files written by AHOY! (Pool, Settings.xml, themes, exports) must not depend on the
    /// regional settings of the PC: "0.9" vs "0,9", or the Unicode minus sign used by some
    /// locales, would otherwise make a file unreadable on another machine.
    /// </summary>
    internal static class InvariantCulture
    {
        /// <summary>
        /// Use the invariant culture for number formatting until the returned object is disposed:
        /// <c>using var _ = InvariantCulture.Use();</c>
        /// </summary>
        internal static IDisposable Use()
        {
            return new Scope(CultureInfo.CurrentCulture);
        }

        /// <summary>Parse an integer written by any version of AHOY! on any locale.</summary>
        internal static int ParseInt(string text)
        {
            return int.Parse(Normalize(text), NumberStyles.Integer, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Parse a decimal written by any version of AHOY! on any locale. Older versions wrote the
        /// current culture's decimal separator (for example "0,9"); AHOY! never writes thousands separators.
        /// </summary>
        internal static double ParseDouble(string text)
        {
            return double.Parse(Normalize(text).Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        private static string Normalize(string text)
        {
            return (text ?? "").Trim().Replace('−', '-'); // Unicode minus (e.g. sv-SE, nb-NO)
        }

        private sealed class Scope : IDisposable
        {
            private readonly CultureInfo previous;

            internal Scope(CultureInfo previous)
            {
                this.previous = previous;
                CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            }

            public void Dispose()
            {
                CultureInfo.CurrentCulture = previous;
            }
        }
    }
}
