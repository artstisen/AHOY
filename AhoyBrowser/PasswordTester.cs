using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace AhoyBrowser
{
    class PasswordTester
    {
        private static List<string> listCommonPasswords;
        private static bool IsNoGoPassPhrase(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    return false;
                }

                if (listCommonPasswords == null)
                {
                    // List of common passwords
                    listCommonPasswords = new List<string>();
                    // Get years from 1900 til now
                    int startYear = 1900;
                    int endYear = System.DateTime.Now.Year;
                    if (startYear < endYear) // Take false computer clocks into account
                    {
                        for (int i = startYear; i <= endYear; i++)
                        {
                            listCommonPasswords.Add(i.ToString());
                        }
                    }
                    listCommonPasswords.Add("pass");
                    listCommonPasswords.Add("pass1");
                    listCommonPasswords.Add("pass12");
                    listCommonPasswords.Add("pass123");
                    listCommonPasswords.Add("pass1234");
                    listCommonPasswords.Add("pass12345");
                    listCommonPasswords.Add("passpass");
                    listCommonPasswords.Add("password");
                    listCommonPasswords.Add("password1");
                    listCommonPasswords.Add("password12");
                    listCommonPasswords.Add("password123");
                    listCommonPasswords.Add("password1234");
                    listCommonPasswords.Add("p@ssw0rd");
                    listCommonPasswords.Add("default");
                    listCommonPasswords.Add("admin");
                    listCommonPasswords.Add("adm");
                    listCommonPasswords.Add("administrator");
                    listCommonPasswords.Add("guest");
                    listCommonPasswords.Add("test123");
                    listCommonPasswords.Add("test1234");
                    listCommonPasswords.Add("asdf");
                    listCommonPasswords.Add("asdfgh");
                    listCommonPasswords.Add("asdf123");
                    listCommonPasswords.Add("asdf1234");
                    listCommonPasswords.Add("abcd");
                    listCommonPasswords.Add("abcd123");
                    listCommonPasswords.Add("abcd1234");
                    listCommonPasswords.Add("abc");
                    listCommonPasswords.Add("abc1");
                    listCommonPasswords.Add("abc12");
                    listCommonPasswords.Add("abc123");
                    listCommonPasswords.Add("abc1234");
                    listCommonPasswords.Add("123");
                    listCommonPasswords.Add("1234");
                    listCommonPasswords.Add("12345");
                    listCommonPasswords.Add("123456");
                    listCommonPasswords.Add("1234567");
                    listCommonPasswords.Add("12345678");
                    listCommonPasswords.Add("123456789");
                    listCommonPasswords.Add("1234567890");
                    listCommonPasswords.Add("987654321");
                    listCommonPasswords.Add("123123");
                    listCommonPasswords.Add("123321");
                    listCommonPasswords.Add("121212");
                    listCommonPasswords.Add("000000");
                    listCommonPasswords.Add("111111");
                    listCommonPasswords.Add("222222");
                    listCommonPasswords.Add("333333");
                    listCommonPasswords.Add("444444");
                    listCommonPasswords.Add("555555");
                    listCommonPasswords.Add("666666");
                    listCommonPasswords.Add("7777777");
                    listCommonPasswords.Add("654321");
                    listCommonPasswords.Add("8888888");
                    listCommonPasswords.Add("9999999");
                    listCommonPasswords.Add("6666666");
                    listCommonPasswords.Add("qwertyuiop");
                    listCommonPasswords.Add("qwerty");
                    listCommonPasswords.Add("qwertz");
                    listCommonPasswords.Add("qwertzuiop");
                    listCommonPasswords.Add("qwer");
                    listCommonPasswords.Add("zxcv");
                    listCommonPasswords.Add("letmein");
                    listCommonPasswords.Add("iloveyou");
                    listCommonPasswords.Add("welcome");
                    listCommonPasswords.Add("football");
                    listCommonPasswords.Add("baseball");
                    listCommonPasswords.Add("adobe123");
                    listCommonPasswords.Add("photoshop");
                    listCommonPasswords.Add("sunshine");
                    listCommonPasswords.Add("princess");
                    listCommonPasswords.Add("ninja");
                    listCommonPasswords.Add("azerty");
                    listCommonPasswords.Add("batman");
                    listCommonPasswords.Add("superman");
                    listCommonPasswords.Add("passw0rd");
                    listCommonPasswords.Add("master");
                    listCommonPasswords.Add("hello");
                    listCommonPasswords.Add("freedom");
                    listCommonPasswords.Add("whatever");
                    listCommonPasswords.Add("qazwsx");
                    listCommonPasswords.Add("trustno1");
                    listCommonPasswords.Add("login");
                    listCommonPasswords.Add("shadow");
                    listCommonPasswords.Add("mustang");
                    listCommonPasswords.Add("access");
                    listCommonPasswords.Add("login");
                    listCommonPasswords.Add("dragon");
                    listCommonPasswords.Add("starwars");
                    listCommonPasswords.Add("monkey");
                    listCommonPasswords.Add("google");
                    listCommonPasswords.Add("1q2w3e");
                    listCommonPasswords.Add("1q2w3e4r");
                    listCommonPasswords.Add("1q2w3e4r5t");
                    listCommonPasswords.Add("1q2w3e4r5t6y7u");
                    listCommonPasswords.Add("5t6y7u8i9o0p");
                    listCommonPasswords.Add("4r5t6y7u");
                    listCommonPasswords.Add("zxcvbnm");
                    listCommonPasswords.Add("1qaz2wsx");
                    listCommonPasswords.Add("123qwe");
                    listCommonPasswords.Add("1q2w3e");
                    listCommonPasswords.Add("3rjs1la7qe");
                    listCommonPasswords.Add("18atcskd2w");
                    listCommonPasswords.Add("mynoob");
                    listCommonPasswords.Add("gdpr");
                    listCommonPasswords.Add("1337");
                    listCommonPasswords.Add("h4x0r");
                    listCommonPasswords.Add("l33th4x0r");
                    listCommonPasswords.Add("g0ldf1sh");
                }

                // A common password, or a short password built around one ("dragon1!").
                // Long passphrases that merely contain a common word are fine.
                string lower = input.ToLowerInvariant();
                foreach (string s in listCommonPasswords)
                {
                    if (lower == s || (lower.Length < 12 && s.Length >= 4 && lower.Contains(s, StringComparison.Ordinal)))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Minimum length for the pool password (the encryption key is derived from it)
        internal const int MinimumPasswordLength = 8;

        // True if the last tested password is acceptable as a pool password
        internal static bool IsScoreValid()
        {
            return PwStrengthPoints > 0 && PwLength >= MinimumPasswordLength;
        }

        private static int PwStrengthPoints;
        private static int PwLength;
        internal static string GetPasswordStrength(string input)
        {
            PwStrengthPoints = 0;
            PwLength = input?.Length ?? 0;

            // Empty string
            if (string.IsNullOrEmpty(input))
            {
                return "N/A";
            }

            //---------------------------------------------------------------
            // Test for common passwords
            if (IsNoGoPassPhrase(input) || input.Length <= 1)
            {
                PwStrengthPoints -= 2;
            }
            // Spaces (passphrases) and non-ASCII letters (æ, ø, å ...) are allowed: the password is
            // UTF-8 encoded and normalized before the key is derived (see Crypto/PoolKey.cs).
            //---------------------------------------------------------------


            //---------------------------------------------------------------
            // Contains lowercase or uppercase
            if (ContainsLowerOrUppercaseCharacters(input))
            {
                PwStrengthPoints += 1;
            }

            // Contains both lower- and uppercase
            if (ContainsBothLowerAndUppercase(input))
            {
                PwStrengthPoints += 1;
            }

            // Contains numbers
            if (Regex.IsMatch(input, @"\d"))
            {
                PwStrengthPoints += 1;
            }

            // Contains symbols (nonword chars)
            if (Regex.IsMatch(input, @"\W"))
            {
                PwStrengthPoints += 1;
            }

            // More than 8 chars
            if (input.Length >= 8)
            {
                PwStrengthPoints += 1;
            }
            // -- Strong <
            //---------------------------------------------------------------

            // -- Strong >
            // Not containing duplicate chars
            if (input.Length >= 8 && !HasDuplicates(GetAlphabet(), input) && ContainsBothLowerAndUppercase(input))
            {
                PwStrengthPoints += 1;
            }

            // Check for Extreme password (12+ chars with upper- and lowercase letters and numbers, no repeated characters)
            if (Regex.IsMatch(input, @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{12,}$") && !HasDuplicates(GetAlphabet(), input))
            {
                PwStrengthPoints += 1;
            }

            // Evaluate
            return GetStrengthEvaluation(PwStrengthPoints);
        }
        private static bool ContainsLowerOrUppercaseCharacters(string input)
        {
            return Regex.IsMatch(input, @"[a-z]") || Regex.IsMatch(input, @"[A-Z]");
        }

        private static bool ContainsBothLowerAndUppercase(string input)
        {
            return Regex.IsMatch(input, @"[a-z]") && Regex.IsMatch(input, @"[A-Z]");
        }

        private static string GetStrengthEvaluation(int points)
        {
            if (points == 1)
            {
                return "Weak <";
            }
            else if (points == 2)
            {
                return "Weak >";
            }
            else if (points == 3)
            {
                return "Moderate <";
            }
            else if (points == 4)
            {
                return "Moderate >";
            }
            else if (points == 5)
            {
                return "Strong <";
            }
            else if (points == 6)
            {
                return "Strong >";
            }
            else if (points == 7)
            {
                return "Extreme";
            }
            else
            {
                return "Invalid";
            }
        }

        private static bool HasDuplicates(List<char> charList, string input)
        {
            foreach (char s in charList)
            {
                //test = temp.ToLower(); // ToLower to make insensitive to casing
                if (input.Count(f => f == s) > 1)
                {
                    return true;
                }
            }

            return false;
        }

        private static List<char> tempABC;
        private static List<char> GetAlphabet()
        {
            if (tempABC == null)
            {
                tempABC = new List<char>();
                tempABC.Add('a'); tempABC.Add('b'); tempABC.Add('c'); tempABC.Add('d'); tempABC.Add('e'); tempABC.Add('f'); tempABC.Add('g'); tempABC.Add('h'); tempABC.Add('i'); tempABC.Add('j'); tempABC.Add('k'); tempABC.Add('l'); tempABC.Add('m'); tempABC.Add('n'); tempABC.Add('o'); tempABC.Add('p'); tempABC.Add('q'); tempABC.Add('r'); tempABC.Add('s'); tempABC.Add('t'); tempABC.Add('u'); tempABC.Add('v'); tempABC.Add('w'); tempABC.Add('x'); tempABC.Add('y'); tempABC.Add('z');
            }
            return tempABC;
        }
    }
}
