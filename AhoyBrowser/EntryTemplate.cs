using System;
using System.Text.RegularExpressions;

namespace AhoyBrowser
{
    class EntryTemplate
    {
        #region Checklist Template
        // Define constants
        private const string TemplateTrigger = "[tplprog]"; // The template trigger
        private const string TemplateErrorMessage = "(Template formatting error)";
        private const string TemplateIncompleteMessage = "(Template formatting incomplete)";
        private const int DefaultBarLength = 25;
        private const int MaxBarLength = 50;
        private const int MinBarLength = 10;

        private static string TemplateOpenTag = "";
        private static string TemplateInProgressTag = "";
        private static string TemplateClosedTag = "";
        private static string LeftTagBrace = "\\-"; // Set default
        private static string RightTagBrace = "\\-"; // Set default
        private static string LeftTagBraceNoEsc = "-"; // Set default
        private static string RightTagBraceNoEsc = "-"; // Set default
        private static string BarChar; // Char used (Just use one)
        private static int BarLength; // (Size)
        private static string BarLabel;
        private static string BarTitle;
        private static int Preset;

        // Get content and return string based on tags in content
        private static int CurrentValue;
        private static int MaxValue;
        private static string PrecedingString;
        private static string SubsequentString;
        private static string FillChar;
        private static string Input;
        public static bool Triggered;
        public static string GetResult(string content, string title)
        {
            Triggered = false;
            // Prepare input
            Input = content;
            // If input has trigger continue or else return nothing
            if (HasTemplate(Input))
            {
                try
                {
                    FillChar = "";
                    BarLabel = "";
                    BarTitle = "";

                    // Get Bar Character
                    BarChar = "";
                    BarLength = 0;

                    // Get preset
                    Match match = Regex.Match(Input, @"\[\bpreset:(\d{1})]", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        Preset = Convert.ToInt32(match.Groups[1].Value);
                    }
                    else
                    {
                        Preset = 1; // Fall back on preset 1
                    }

                    ResolveTagBraces(Input);

                    // Get open tag
                    match = Regex.Match(Input, @"\[\bopen:([\w ]+)]", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        TemplateOpenTag = LeftTagBrace + @"\b" + match.Groups[1].Value + RightTagBrace;
                    }
                    else
                    {
                        TemplateOpenTag = LeftTagBrace + @"\bpending" + RightTagBrace; // Default open tag
                    }

                    // Get in progress tag
                    match = Regex.Match(Input, @"\[\bmark:([\w ]+)]", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        TemplateInProgressTag = LeftTagBrace + @"\b" + match.Groups[1].Value + RightTagBrace;
                    }
                    else
                    {
                        TemplateInProgressTag = LeftTagBrace + @"\binprog" + RightTagBrace; // Default inprog tag
                    }

                    // Get closed tag
                    match = Regex.Match(Input, @"\[\bclosed:([\w ]+)]", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        TemplateClosedTag = LeftTagBrace + @"\b" + match.Groups[1].Value + RightTagBrace;
                    }
                    else
                    {
                        TemplateClosedTag = LeftTagBrace + @"\bdone" + RightTagBrace; // Default closed tag
                    }

                    // Count pending, in progress and done tags
                    MaxValue = Regex.Matches(Input, TemplateOpenTag).Count;
                    MaxValue += Regex.Matches(Input, TemplateInProgressTag).Count;
                    CurrentValue = Regex.Matches(Input, TemplateClosedTag).Count;
                    MaxValue = MaxValue + CurrentValue;
                    if (MaxValue == 0 && CurrentValue == 0)
                    {
                        // If no tags are found
                        Triggered = true;
                        return TemplateIncompleteMessage;
                    }

                    // Get Bar Length (not preset dependant)
                    match = Regex.Match(Input, @"\[\bwidth:(\p{N}+)]", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        try
                        {
                            BarLength = Convert.ToInt32(match.Groups[1].Value);
                        }
                        catch
                        {
                            BarLength = DefaultBarLength;
                        }
                    }
                    else
                    {
                        BarLength = DefaultBarLength;
                    }

                    // Validate min/max bar length
                    if (BarLength > MaxBarLength)
                    {
                        BarLength = MaxBarLength;
                    }
                    else if (BarLength < MinBarLength)
                    {
                        BarLength = MinBarLength;
                    }

                    // Get title (Match unicode word with or without spaces)
                    match = Regex.Match(Input, @"\[\btitle:([\w \p{P}]*)]", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        if (match.Groups[1].Value == string.Empty)
                        {
                            BarTitle = "";
                        }
                        else
                        {
                            BarTitle = match.Groups[1].Value + " ";
                        }
                    }
                    else
                    {
                        BarTitle = "";
                    }


                    // Custom preset
                    if (Preset == 0)
                    {
                        // Get label (Match unicode word with or without spaces)
                        match = Regex.Match(Input, @"\[\blabel:([\w ]*)]", RegexOptions.IgnoreCase);
                        if (match.Success)
                        {
                            BarLabel = match.Groups[1].Value;
                        }
                        else
                        {
                            BarLabel = "";
                        }

                        // Get Bar char
                        match = Regex.Match(Input, @"\[\bchar:(\p{S}|\p{L}|\p{P}|\p{N})]", RegexOptions.IgnoreCase);
                        if (match.Success)
                        {
                            BarChar = match.Groups[1].Value;
                        }
                        else
                        {
                            BarChar = "?";
                        }

                        // Get Fill char
                        match = Regex.Match(Input, @"\[\bfill:(\p{S}|\p{L}|\p{P}|\p{N})]", RegexOptions.IgnoreCase);
                        if (match.Success)
                        {
                            FillChar = match.Groups[1].Value;
                        }
                        else
                        {
                            FillChar = " "; // Space
                        }

                        // Get PrecedingString
                        match = Regex.Match(Input, @"\[\bleft:([\d\p{S}\p{L}\p{P} ]{1,4})]", RegexOptions.IgnoreCase);
                        if (match.Success)
                        {
                            PrecedingString = match.Groups[1].Value;
                        }
                        else
                        {
                            PrecedingString = "";
                        }

                        // Get SubsequentString
                        match = Regex.Match(Input, @"\[\bright:([\d\p{S}\p{L}\p{P} ]{1,4})]", RegexOptions.IgnoreCase);
                        if (match.Success)
                        {
                            SubsequentString = match.Groups[1].Value;
                        }
                        else
                        {
                            SubsequentString = "";
                        }
                    }
                    else
                    {
                        if (Preset == 1) // Preset   ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓░░░░░░░░░░░░░░ 50%
                        {
                            PrecedingString = "";
                            SubsequentString = " %";
                            BarChar = "▓";
                            FillChar = "░";
                        }
                        else if (Preset == 2) // Preset 0% ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓░░░░░░░░░░░░░░ 50%
                        {
                            PrecedingString = "& ";
                            SubsequentString = " %";
                            BarChar = "▓";
                            FillChar = "░";
                        }
                        else if (Preset == 3) // Preset 0% ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓______________ 50%
                        {
                            PrecedingString = "& ";
                            SubsequentString = " %";
                            BarChar = "▓";
                            FillChar = "_";
                        }
                        else if (Preset == 4) // Preset 50% ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓░░░░░░░░░░░░░░
                        {
                            PrecedingString = "% ";
                            SubsequentString = "";
                            BarChar = "▓";
                            FillChar = "░";
                        }
                        else if (Preset == 5) // Preset 0% ▌▌▌▌▌▌▌▌▌▌▌▌▌▌▌______________ 50%
                        {
                            PrecedingString = "& ";
                            SubsequentString = " %";
                            BarChar = "▌";
                            FillChar = "_";
                        }
                        else if (Preset == 6) // Preset    ▌▌▌▌▌▌▌▌▌▌▌▌▌▌▌               50%
                        {
                            PrecedingString = "";
                            SubsequentString = " %";
                            BarChar = "▌";
                            FillChar = " ";
                        }
                        else if (Preset == 7) // Preset 0% ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓░░░░░░░░░░░░░░ 100%
                        {
                            PrecedingString = "& ";
                            SubsequentString = " &";
                            BarChar = "▓";
                            FillChar = "░";
                        }
                        else if (Preset == 8) // Preset  - ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓░░░░░░░░░░░░░░ + 
                        {
                            PrecedingString = "- ";
                            SubsequentString = " +";
                            BarChar = "▓";
                            FillChar = "░";
                        }
                        else if (Preset == 9) // Preset    ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓░░░░░░░░░░░░░░
                        {
                            PrecedingString = "";
                            SubsequentString = "";
                            BarChar = "▓";
                            FillChar = "░";
                        }
                        else
                        {
                            // Same as preset 1
                            PrecedingString = "";
                            SubsequentString = " %";
                            BarChar = "▓";
                            FillChar = "░";
                        }
                    }

                    // Generate progress bar
                    Log.LogEntryTemplateStatus("Parsed");
                    Triggered = true;
                    return GetProgressBar(CurrentValue, MaxValue, BarLength, BarChar, BarTitle, BarLabel, PrecedingString, SubsequentString);
                }
                catch
                {
                    Triggered = true;
                    Log.LogEntryTemplateStatus("Error (Parsing)");
                    return TemplateErrorMessage;
                }
            }
            else
            {
                return title;
            }
        }

        private static void ResolveTagBraces(string input)
        {
            // Get left tag brace // \[\btag:([\p{P}])([\p{P}])]
            Match match = Regex.Match(input, @"\[\btag:([\p{P}|\p{S}])([\p{P}|\p{S}])]", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                LeftTagBraceNoEsc = match.Groups[1].Value;
                LeftTagBrace = @"\" + LeftTagBraceNoEsc;
            }
            else
            {
                LeftTagBraceNoEsc = "-";
                LeftTagBrace = @"\-"; // Default left brace
            }

            // Get right tag brace
            match = Regex.Match(input, @"\[\btag:([\p{P}|\p{S}])([\p{P}|\p{S}])]", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                RightTagBraceNoEsc = match.Groups[2].Value;
                RightTagBrace = @"\" + RightTagBraceNoEsc;
            }
            else
            {
                RightTagBraceNoEsc = "-";
                RightTagBrace = @"\-"; // Default right brace
            }

        }

        public static bool HasTemplate(string input)
        {
            if (input.ToLower().Contains(TemplateTrigger))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //private static string GetParamsValue(string input)
        //{
        //    return input.Substring(6, 0);
        //}

        // Example: GetProgressBar(3, 4, 100, "#","", ">", "<");
        private static int ProgressBarLength;
        private static string ProgressBarSymbol;
        private static decimal MaximumValue;
        private static string TempProgressBarOutput;
        private static string GetProgressBar(decimal currentValue, decimal maxValue, int barSize, string barCharacter, string barTitle, string barLabel, string precedingString, string subsequentString)
        {
            try
            {
                // Set values
                ProgressBarLength = barSize;
                MaximumValue = maxValue;
                ProgressBarSymbol = barCharacter;
                TempProgressBarOutput = "";

                if (currentValue > 0)
                {
                    // Calculate progress bar
                    for (int i = 0; i < (currentValue / (MaximumValue / ProgressBarLength)); i++)
                    {
                        if (TempProgressBarOutput.Length < ProgressBarLength)
                        {
                            TempProgressBarOutput += ProgressBarSymbol;
                        }
                    }

                    if (TempProgressBarOutput.Length < ProgressBarLength)
                    {
                        if (FillChar != "")
                        {
                            TempProgressBarOutput = TempProgressBarOutput + TextTransform.GetRepeatedCharacter((ProgressBarLength - TempProgressBarOutput.Length), FillChar);
                        }
                        else
                        {
                            TempProgressBarOutput = TempProgressBarOutput + TextTransform.GetBlankSpace((ProgressBarLength - TempProgressBarOutput.Length));
                        }
                    }
                }
                else
                {
                    if (FillChar != "")
                    {
                        TempProgressBarOutput = barCharacter + TextTransform.GetRepeatedCharacter((ProgressBarLength) - 1, FillChar);
                    }
                    else
                    {
                        TempProgressBarOutput = barCharacter + TextTransform.GetBlankSpace((ProgressBarLength) - 1);
                    }
                }

                // Output value
                if (subsequentString.Contains("%")) // Display dynamic percentage
                {
                    subsequentString = subsequentString.Replace("%", ((currentValue / MaximumValue) * 100).ToString("0") + "%");
                }

                if (subsequentString.Contains("&")) // Display static percentage
                {
                    subsequentString = subsequentString.Replace("&", "100%");
                }

                if (precedingString.Contains("%")) // Display dynamic percentage
                {
                    precedingString = precedingString.Replace("%", ((currentValue / MaximumValue) * 100).ToString("0") + "%");
                }

                if (precedingString.Contains("&")) // Display static percentage
                {
                    precedingString = precedingString.Replace("&", "0%");
                }

                return barTitle + precedingString + barLabel + TempProgressBarOutput + subsequentString;
            }
            catch
            {
                Log.LogEntryTemplateStatus("Error (Bar)");
                return TemplateErrorMessage;
            }
        }

        // Get template (the entire text for using the template)
        //public static string GetTemplateText()
        //{
        //    return IO.ChecklistTemplateTextReader();
        //}
        #endregion



        #region Bookmark Template

        public static bool IdentifyBookmarkTemplate(string input)
        {
            if (input.ToLower().Contains(BookmarkTemplateTrigger))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private const string BookmarkTemplateTrigger = "[tplmark]"; // The bookmark template trigger
        // Check for bookmark with valid mark tag
        public static bool HasBookmarkTemplate(string input)
        {
            try
            {
                // Contains trigger
                if (input.ToLower().Contains(BookmarkTemplateTrigger))
                {
                    Log.LogEntryTemplateStatus("Mark identified");
                    // Contains valid mark tag
                    Match match = Regex.Match(input, @"\[\bmark:([\w ]+)]", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        Log.LogEntryTemplateStatus("Mark tag validated");
                        return true;
                    }
                    else
                    {
                        Log.LogEntryTemplateStatus("Mark tag invalid");
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                Log.LogEntryTemplateStatus("Error (Parsing)");
                return false;
            }
        }


        // Find tag and return index to scroll to
        private static string BookmarkTag;
        private static int BookmarkIndex;
        public static int GetBookmarkCaretIndex(string input)
        {
            try
            {
                // Get bookmark tag
                // Resolve template tag ex.: [inprog] find it and return its index
                BookmarkTag = "";
                BookmarkIndex = -1;
                Match match = Regex.Match(input, @"\[\bmark:([\w ]+)]", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    ResolveTagBraces(input);
                    BookmarkTag = LeftTagBraceNoEsc + match.Groups[1].Value + RightTagBraceNoEsc;
                    // Return index where the bookmark is found
                    Log.LogEntryTemplateStatus("Mark: \"" + BookmarkTag + "\"");
                    // Find index and return it
                    BookmarkIndex = GetIndexOf(input, BookmarkTag);
                    Log.LogEntryTemplateStatus("Mark index: " + BookmarkIndex.ToString());
                    return BookmarkIndex;
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                Log.LogEntryTemplateStatus("Error (Parsing)");
                return -1;
            }
        }

        // Get index of query in input string
        private static int GetIndexOf(string input, string query)
        {
            try
            {
                // Ensure the source and query string contains text, and
                // that the source is longer or equal to the query to avoid out of
                // range exceptions
                if (input.Length > 0 && query.Length > 0 && input.Length >= query.Length)
                {
                    input = input.ToLower();
                    query = query.ToLower();
                    for (int i = 0; i < input.Length; i++)
                    {
                        if (i + query.Length <= input.Length)
                        {
                            //Log.LogEntryTemplateStatus("TEST: " + input.Substring(i, query.Length));
                            if (input.Substring(i, query.Length) == query)
                            {
                                return i;
                            }
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    return -1;
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
        }
        #endregion
    }
}
