using System;
using System.Collections.Generic;

namespace AhoyBrowser
{
    class PasswordGeneratorC
    {
        // Set the password length here (should not be shorter than 8 characters to match the
        // criteria for a strong password)
        private static int NumberOfCharsInPassword { get; set; }
        private static Random ran;
        private static List<int> PreviousRandomNumbers { get; set; }
        private static List<string> Numbers { get; set; }
        private static List<string> Uppercase { get; set; }
        private static List<string> Lowercase { get; set; }

        private static void InitializeGenerator()
        {
            // Create content
            if (Numbers == null)
            {
                // ASCII chars:
                // 0123456789
                // abcdefghijklmnopqrstuvwxyz
                // ABDCEFGHIJKLMNOPQRSTUVWXYZ
                string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                Numbers = new List<string>(numbers);
                string[] lowercase = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o",
                                    "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
                Lowercase = new List<string>(lowercase);
                string[] uppercase = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O",
                                    "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
                Uppercase = new List<string>(uppercase);
            }

            if (PreviousRandomNumbers == null)
            {
                PreviousRandomNumbers = new List<int>();
            }

            if (ran == null)
            {
                ran = new Random();
            }
        }


        private static string tempOutput;
        public static string GeneratePassword(int passwordLength)
        {
            InitializeGenerator();
            CreateCombinedCharacterString();
            PreviousRandomNumbers.Clear();
            NumberOfCharsInPassword = passwordLength - 1; // Zero indexed
            tempOutput = "";

            do
            {
                tempOutput = Generate();
            }
            while (!DoesStringContainOneOfEachCharClass(tempOutput));

            return tempOutput;

        }

        private static string tempGenerate;
        private static string Generate()
        {
            tempGenerate = "";
            for (int i = 0; i <= NumberOfCharsInPassword; i++)
            {
                tempGenerate += GetChars(GetRandomNumber());
            }

            return tempGenerate;
        }

        private static int tempRandomNumber;
        private static int GetRandomNumber()
        {
            // Generate test number. If already in list, generate a new one
            if (PreviousRandomNumbers.Count > 0)
            {
                do
                {
                    tempRandomNumber = ran.Next(0, strChars.Length);
                }
                while (DoesRandomNumbeExist(tempRandomNumber));
            }
            else
            {
                tempRandomNumber = ran.Next(0, strChars.Length);
            }

            PreviousRandomNumbers.Add(tempRandomNumber);
            return tempRandomNumber;
        }

        private static bool DoesRandomNumbeExist(int number)
        {
            foreach (int i in PreviousRandomNumbers)
            {
                if (i == number)
                {
                    return true;
                }
            }
            return false;
        }

        private static int validationPoints;
        private static bool DoesStringContainOneOfEachCharClass(string generatedPassword)
        {
            validationPoints = 0;

            foreach (string s in Numbers)
            {
                if (generatedPassword.Contains(s))
                {
                    validationPoints += 1;
                    break;
                }
            }
            foreach (string s in Lowercase)
            {
                if (generatedPassword.Contains(s))
                {
                    validationPoints += 1;
                    break;
                }
            }
            foreach (string s in Uppercase)
            {
                if (generatedPassword.Contains(s))
                {
                    validationPoints += 1;
                    break;
                }
            }

            if (validationPoints >= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string strChars;
        private static void CreateCombinedCharacterString()
        {
            strChars = "";
            foreach (string s in Numbers)
            {
                strChars += s;
            }
            foreach (string s in Lowercase)
            {
                strChars += s;
            }
            foreach (string s in Uppercase)
            {
                strChars += s;
            }
        }

        private static char GetChars(int tmp)
        {
            return strChars[tmp];
        }
    }
}
