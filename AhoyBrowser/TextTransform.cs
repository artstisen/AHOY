using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AhoyBrowser
{
    class TextTransform
    {
        private static string TempSlickFrameBuilder { get; set; }
        private static string TempOriginalSelection { get; set; }
        private static string TempFrameSide { get; set; }
        private static int TempFrameLength { get; set; }
        private static int TempLineCount { get; set; }
        private static string TempFrameTop { get; set; }
        private static string TempFrameBottom { get; set; }
        private static List<string> TempLines { get; set; }

        private static string TempSpaces { get; set; }
        internal static string GetBlankSpace(int spaces)
        {
            TempSpaces = "";
            for (int i = 0; i < spaces; i++)
            {
                TempSpaces += " ";
            }
            return TempSpaces;
        }

        private static string TempNewLines { get; set; }
        internal static string GetNewLines(int lines)
        {
            TempNewLines = "";
            for (int i = 0; i < lines; i++)
            {
                TempNewLines += Environment.NewLine;
            }
            return TempNewLines;
        }

        private static int tempNewLineCounter;
        internal static int GetNewLineCount(HistoryTextBox tb)
        {
            tempNewLineCounter = 0;
            if (tb.Text.Length > 0)
            {
                tempNewLineCounter = tb.Lines.Length;
            }
            return tempNewLineCounter;
        }

        private static int tempDistance;
        internal static string CalculateSpacesFromStringToGivenLength(string shortcutText, int totalLength)
        {
            // Calculate the remaining spaces to reach a maximum of X char-length
            tempDistance = totalLength - shortcutText.Length;
            return TextTransform.GetBlankSpace(tempDistance);
        }

        internal static string CalculateSpacesFromStringToGivenLengthReturnChar(string shortcutText, int totalLength, string repeatedChar)
        {
            // Calculate the remaining spaces to reach a maximum of X char-length
            tempDistance = totalLength - shortcutText.Length;
            return TextTransform.GetRepeatedCharacter(tempDistance, repeatedChar);
        }

        internal static string CalculateSpacesFromStringToGivenLengthReturnChar(string titleText, int totalLength, string repeatedChar, string inputText)
        {
            // Calculate the remaining spaces to reach a maximum of X char-length
            tempDistance = totalLength - titleText.Length;
            return titleText + " " + TextTransform.GetRepeatedCharacter(tempDistance, repeatedChar) + " " + inputText;
        }

        internal static string GetRepeatedCharacter(int count, string character)
        {
            TempSpaces = "";
            for (int i = 0; i < count; i++)
            {
                TempSpaces += character;
            }
            return TempSpaces;
        }
        public static string SelectionToBulletAndLine(string text, bool toUpper)
        {
            if (text.Length > 0)
            {
                // Reset variables
                TempSlickFrameBuilder = "";
                TempOriginalSelection = "";
                TempFrameSide = "";
                TempFrameLength = 0;

                // Find longest string
                TempFrameLength = CountLongestString(text);
                // Add bullets
                text = SelectionToBullet(text, toUpper);

                TempOriginalSelection = text;
                TempSlickFrameBuilder = Environment.NewLine + TempOriginalSelection + Environment.NewLine;
                for (int i = 0; i < TempFrameLength; i++)
                {
                    TempFrameSide += "-";
                }
                TempSlickFrameBuilder += GetBlankSpace(2) + TempFrameSide;

                return TempSlickFrameBuilder;
            }
            else
            {
                return text;
            }
        }

        private static int tempMaxPeekLength;
        private static int tempPeekLength;
        private static List<string> tempPeekList;
        private static int CountLongestString(string text)
        {
            tempMaxPeekLength = 0;
            tempPeekLength = 0;
            if (tempPeekList == null)
            {
                tempPeekList = new List<string>();
            }
            if (text.Length > 0)
            {
                tempPeekList.Clear();
                tempPeekList.AddRange(text.Split('\n'));

                for (int i = 0; i < tempPeekList.Count; i++)
                {
                    tempPeekLength = tempPeekList[i].Length;
                    if (tempPeekLength > tempMaxPeekLength)
                    {
                        tempMaxPeekLength = tempPeekLength;
                    }
                }
            }
            return tempMaxPeekLength;
        }


        private static string tempBulletedString;
        private static List<string> tempBulletedList;
        public static string SelectionToBullet(string text, bool toUpper)
        {
            if (tempBulletedList == null)
            {
                tempBulletedList = new List<string>();
            }

            tempBulletedList.Clear();
            tempBulletedString = "";
            if (text.Length > 0)
            {
                // Split
                tempBulletedList.AddRange(text.Split('\n'));

                for (int i = 0; i < tempBulletedList.Count; i++)
                {
                    if (i == tempBulletedList.Count - 1)
                    {
                        tempBulletedString += "- " + tempBulletedList[i];
                    }
                    else
                    {
                        tempBulletedString += "- " + tempBulletedList[i] + Environment.NewLine;
                    }
                }

                if (toUpper)
                {
                    tempBulletedString = tempBulletedString.ToUpper();
                }

                return tempBulletedString;
            }
            else
            {
                return tempBulletedString;
            }
        }

        public static string SelectionToUnderline(string text, bool toUpper)
        {
            if (text.Length > 0)
            {
                // Reset variables
                TempSlickFrameBuilder = "";
                TempOriginalSelection = "";
                TempFrameSide = "";
                TempFrameLength = 0;

                if (toUpper)
                {
                    text = text.ToUpper();
                }

                TempOriginalSelection = text;
                TempFrameLength = CountLongestString(TempOriginalSelection);
                TempSlickFrameBuilder = Environment.NewLine + TempOriginalSelection + Environment.NewLine;
                for (int i = 0; i < TempFrameLength; i++)
                {
                    TempFrameSide += "-";
                }
                TempSlickFrameBuilder += TempFrameSide;

                return TempSlickFrameBuilder;
            }
            else
            {
                return text;
            }
        }


        //// Convert to Unicode


        private static List<char> printableChars;
        private static string tempCharString;
        public static string GetCharSelection()
        {
            if (printableChars == null)
            {
                printableChars = new List<char>();
            }

            printableChars.Clear();
            tempCharString = "";

            for (int i = 32; i < 256; i++)
            {
                char c = Convert.ToChar(i);
                if (!char.IsControl(c))
                {
                    printableChars.Add(c);
                }
            }

            if (printableChars.Count > 0)
            {
                foreach (char c in printableChars)
                {
                    tempCharString += c.ToString() + " ";
                }
            }

            return tempCharString;
        }


        // Will return all Unicode chars...
        public static string GetPrintableChars()
        {
            if (printableChars == null)
            {
                printableChars = new List<char>();
            }

            printableChars.Clear();
            tempCharString = "";


            for (int i = char.MinValue; i <= char.MaxValue; i++)
            {
                char c = Convert.ToChar(i);
                if (!char.IsControl(c))
                {
                    printableChars.Add(c);
                }
            }

            if (printableChars.Count > 0)
            {
                foreach (char c in printableChars)
                {
                    tempCharString += c.ToString() + " ";
                }
            }

            return tempCharString;
        }

        internal static string GetOnOff(bool state)
        {
            if (state)
            {
                return "ON";
            }
            else
            {
                return "OFF";
            }
        }

        internal static string RemoveNewLines(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                try
                {
                    input = input.Trim();
                    input = input.Replace(Environment.NewLine, " ");
                    input = Regex.Replace(input, @"\r\n?|\n", " ");
                    return input;
                }
                catch
                {
                    // Do nothing
                    return input;
                }
            }
            else
            {
                return input;
            }
        }

        internal static string SortText(string input, bool descending)
        {
            if (!string.IsNullOrEmpty(input))
            {
                try
                {
                    // Split string by newlines into list and sort
                    List<string> tempList = new List<string>();
                    tempList.AddRange(input.Split('\n'));

                    // Trim trailing whitespace (keep indentation) and sort
                    if (tempList.Count > 0)
                    {
                        for (int i = 0; i < tempList.Count; i++)
                        {
                            tempList[i] = tempList[i].TrimEnd();
                        }

                        if (!descending)
                        {
                            tempList.Sort(delegate (string a, string z) { return String.Compare(a, z); });
                        }
                        else
                        {
                            tempList.Sort(delegate (string a, string z) { return String.Compare(z, a); });
                        }
                    }

                    // Return list into string
                    input = "";
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        // Get string from index
                        input += tempList[i];
                        // Ad new lines to all except the last
                        if (i < tempList.Count - 1)
                        {
                            input += Environment.NewLine;
                        }
                    }
                }
                catch
                {
                    // Log
                    Log.LogGeneralError("Sorting failed");
                }
                return input;
            }
            else
            {
                return input;
            }
        }

        public static string FormatTitleForCategoryPreview(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.Length > 18) // Category title specific length
                {
                    return "\"" + input.Substring(0, 15) + "...\"";
                }
                else
                {
                    return "\"" + input + "\"";
                }
            }
            else
            {
                return "N/A";
            }
        }

    }
}
