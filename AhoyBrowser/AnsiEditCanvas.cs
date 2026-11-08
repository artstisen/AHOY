using System;
using System.Collections.Generic;
using System.IO;


namespace AhoyBrowser
{
    class AnsiEditCanvas
    {
        // Define symbol sets
        private static string[] ANSICharacterSymbolsSetA = { "░", "▒", "▓", "█", "▀", "▄", "▌", "▐", "■", "▪", " " };
        private static string[] ANSICharacterSymbolsSetB = { "╔", "╗", "╚", "╝", "═", "║", "╦", "╩", "╠", "╣", " " };
        private static string[] ANSICharacterSymbolsSetC = { "┌", "┐", "└", "┘", "─", "│", "┬", "┴", "├", "┤", " " };
        private static string[] ANSICharacterSymbolsSetD = { "╓", "╖", "╙", "╜", "╒", "╕", "╘", "╛", "╞", "╡", " " };
        private static string[] ANSICharacterSymbolsSetE = { "┼", "╪", "╫", "╬", "╟", "╢", "╤", "╧", " ", " ", " " };
        private static string[] ANSICharacterSymbolsSetF = { "\\", "|", "/", "_", "-", ".", "'", ":", ";", "¦", " " };
        private static string[] ANSICharacterSymbolsSetG = { "●", "○", "°", "◦", "•", "·", "♦", "◊", "□", "¤", " " };
        private static string[] ANSICharacterSymbolsSetH = { "▪", "▫", "˄", "˅", "˂", "˃", "▲", "▼", "◄", "►", " " };
        private static string[] ANSICharacterSymbolsSetI = { "∆", "§", "$", "@", "(", ")", "[", "]", "{", "}", " " }; // Escaped && will produce two &...
        private static string[] ANSICharacterSymbolsSetJ = { "=", "+", "!", "*", "%", "#", "?", "~", "^", "'", " " };
        private static string[] ANSICharacterSymbolsSetK = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", " " };
        private static string[] Alphabet1 = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        private static string[] Alphabet2 = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        public static List<string> CS { get; set; }
        public static List<string> CompleteCharacterSet { get; set; }
        private static int CurrentCharacterSetIndex { get; set; }
        public const string VersionNumber = "v1.06";

        public static void NextCharacterSet()
        {
            if (CurrentCharacterSetIndex < 10)
            {
                CurrentCharacterSetIndex += 1;
                SelectCharacterSet(CurrentCharacterSetIndex);
            }
            else
            {
                CurrentCharacterSetIndex = 0;
                SelectCharacterSet(CurrentCharacterSetIndex);
            }
        }

        public static void PreviousCharacterSet()
        {
            if (CurrentCharacterSetIndex > 0)
            {
                CurrentCharacterSetIndex -= 1;
                SelectCharacterSet(CurrentCharacterSetIndex);
            }
            else
            {
                CurrentCharacterSetIndex = 10;
                SelectCharacterSet(CurrentCharacterSetIndex);
            }
        }

        public static void SelectCharacterSet(int characterSetIndex)
        {
            CurrentCharacterSetIndex = characterSetIndex;
            if (CS == null)
            {
                CS = new List<string>();
                CompleteCharacterSet = new List<string>();
                CompleteCharacterSet.AddRange(ANSICharacterSymbolsSetA);
                CompleteCharacterSet.AddRange(ANSICharacterSymbolsSetB);
                CompleteCharacterSet.AddRange(ANSICharacterSymbolsSetC);
                CompleteCharacterSet.AddRange(ANSICharacterSymbolsSetD);
                CompleteCharacterSet.AddRange(ANSICharacterSymbolsSetE);
                CompleteCharacterSet.AddRange(ANSICharacterSymbolsSetF);
                CompleteCharacterSet.AddRange(ANSICharacterSymbolsSetG);
                CompleteCharacterSet.AddRange(ANSICharacterSymbolsSetH);
                CompleteCharacterSet.AddRange(ANSICharacterSymbolsSetI);
                CompleteCharacterSet.AddRange(ANSICharacterSymbolsSetJ);
                CompleteCharacterSet.AddRange(ANSICharacterSymbolsSetK);
                CompleteCharacterSet.AddRange(Alphabet1);
                CompleteCharacterSet.AddRange(Alphabet2);
            }
            else
            {
                CS.Clear();
            }

            switch (characterSetIndex)
            {
                case 0:
                    CS.AddRange(ANSICharacterSymbolsSetA);
                    break;
                case 1:
                    CS.AddRange(ANSICharacterSymbolsSetB);
                    break;
                case 2:
                    CS.AddRange(ANSICharacterSymbolsSetC);
                    break;
                case 3:
                    CS.AddRange(ANSICharacterSymbolsSetD);
                    break;
                case 4:
                    CS.AddRange(ANSICharacterSymbolsSetE);
                    break;
                case 5:
                    CS.AddRange(ANSICharacterSymbolsSetF);
                    break;
                case 6:
                    CS.AddRange(ANSICharacterSymbolsSetG);
                    break;
                case 7:
                    CS.AddRange(ANSICharacterSymbolsSetH);
                    break;
                case 8:
                    CS.AddRange(ANSICharacterSymbolsSetI);
                    break;
                case 9:
                    CS.AddRange(ANSICharacterSymbolsSetJ);
                    break;
                case 10:
                    CS.AddRange(ANSICharacterSymbolsSetK);
                    break;
                default:
                    CS.AddRange(ANSICharacterSymbolsSetA);
                    break;
            }
        }

        private static string tempCanvas;
        public static int CanvasHeight { get; set; }
        public static int CanvasWidth { get; set; }
        public static int CanvasActiveArea { get; set; }
        public static int HeaderCharCount { get; set; }
        public static int CanvasEndIndex { get; set; }
        public static string GetNewCanvas(int width, int height)
        {
            HeaderCharCount = 0;
            CanvasHeight = height;
            CanvasWidth = width;
            CanvasActiveArea = width * height;
            tempCanvas = "";

            // Create active canvas area
            for (int i = 0; i < height; i++)
            {
                if (i < height - 1)
                    tempCanvas += GetBlankSpace(width) + Environment.NewLine;
                else
                    tempCanvas += GetBlankSpace(width);
            }
            // Register canvas active end index
            CanvasEndIndex = ((tempCanvas.Length + 1) - height);

            // Calculate center canvas
            GetCanvasCenterIndex();

            // Return the completed canvas
            return tempCanvas;
        }

        private static string tempInfoText;
        public static string GetInfoText()
        {
            tempInfoText = "";
            // Create info field
            tempInfoText += GetBlankSpace(10) + "┌──┐┌──┐┌──┐┌──┐┌──┐┌──┐┌──┐┌──┐┌──┐┌──┐┌─────────┐" + Environment.NewLine;
            tempInfoText += "Preset " + (CurrentCharacterSetIndex + 1).ToString("00") + GetBlankSpace(1) + "│1" + CS[0] + "││2" + CS[1] + "││3" + CS[2] + "││4" + CS[3] + "││5" + CS[4] + "││6" + CS[5] +
                "││7" + CS[6] + "││8" + CS[7] + "││9" + CS[8] + "││0" + CS[9] + "││SPACE (X)│" + Environment.NewLine;
            tempInfoText += GetBlankSpace(10) + "└──┘└──┘└──┘└──┘└──┘└──┘└──┘└──┘└──┘└──┘└─────────┘";
            return tempInfoText;
        }

        private static string tempHelpText;
        public static string GetHelpText()
        {
            tempHelpText = "";
            // Create info field
            tempHelpText += "ANSI Edit " + VersionNumber + Environment.NewLine + Environment.NewLine;
            tempHelpText += "Press: ← ↑ → ↓ to navigate canvas (or use a mouse)" + Environment.NewLine;
            tempHelpText += "Press: Alt + ← ↑ → ↓ to move content" + Environment.NewLine;
            tempHelpText += "Press: SPACE to clear selected character (delete)" + Environment.NewLine;
            tempHelpText += "Press: CTRL + ↑ ↓ to cycle character sets" + Environment.NewLine;
            tempHelpText += "Press: CTRL + Z/Y to undo/redo changes" + Environment.NewLine;
            tempHelpText += "Press: CTRL + A to make auto-selection/crop" + Environment.NewLine;
            tempHelpText += "Press: CTRL + ALT + C to auto-select and copy" + Environment.NewLine;
            tempHelpText += "Press: CTRL + D to deselect auto-selection/crop" + Environment.NewLine;
            tempHelpText += "Press: CTRL + SHIFT + C to copy content or selection" + Environment.NewLine;
            tempHelpText += "Press: CTRL + C to copy character" + Environment.NewLine;
            tempHelpText += "Press: CTRL + V to paste character" + Environment.NewLine;
            tempHelpText += "Press: ESC to clear canvas" + Environment.NewLine;
            tempHelpText += "Press: CTRL + W to exit" + Environment.NewLine + Environment.NewLine;
            tempHelpText += "ANSI Edit " + VersionNumber + " is a simple ANSI/ASCII Art editor created and used as a utility during development of AHOY!." + Environment.NewLine;
            tempHelpText += "This version of ANSI Edit does not exchange data or interact with AHOY! (apart from preventing AHOY! from entering sleep mode while open). It is simply included as a bonus feature. Copy your artwork manually from this editor into AHOY! or any preferred editor. It is possible to run multiple instances of ANSI Edit." + Environment.NewLine;
            tempHelpText += "(c)2018 by LBS";
            return tempHelpText;
        }

        // Calculate center on active canvas (for caret position)
        public static int GetCanvasCenterIndex()
        {
            // Canvas length / 2 + half of the with to place in center
            return (CanvasEndIndex / 2) + (CanvasWidth / 2) + 2;
        }

        public static int GetCanvasUpperIndex()
        {
            return 0;
        }

        public static int GetCanvasLowerIndex()
        {
            return CanvasEndIndex;
        }

        private static string tempString;
        internal static string GetBlankSpace(int count)
        {
            tempString = "";
            for (int i = 0; i < count; i++)
            {
                tempString += " ";
            }
            return tempString;
        }

        public static bool IsCharacterValid(string characterToTest, int caretPosition)
        {
            if (caretPosition != CanvasEndIndex)
            {
                for (int i = 0; i < AnsiEditCanvas.CompleteCharacterSet.Count; i++)
                {
                    if (characterToTest == AnsiEditCanvas.CompleteCharacterSet[i])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }

    class LogoReader
    {
        public static string GetLogoText()
        {
            StreamReader reader;
            // Load embedded dictionary file
            reader = new StreamReader(Splash.asm.GetManifestResourceStream("AhoyBrowser.TextFiles.AnsiEditLogo.txt"));

            try
            {
                string tmpContent = "";

                while (reader.Peek() != -1)
                {
                    tmpContent += reader.ReadLine() + Environment.NewLine;
                }

                return tmpContent;
            }
            catch
            {
                return "";
            }
            finally
            {
                // always close the reader when finished
                reader.Close();
            }
        }
    }
}
