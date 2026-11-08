using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AhoyBrowser
{
    class StringSearch
    {
        private static bool tempResult;
        internal static string MatchingSearchPattern { get; set; }

        // Basic search on direct pattern
        private static string tempStandardMatch;
        private static string tempURLMatch;
        public static bool IsMatchA(string input, string pattern)
        {
            MatchingSearchPattern = "";
            tempResult = false;
            try
            {
                // Fetch values
                tempStandardMatch = pattern;
                tempURLMatch = GetStrippedURL(pattern);
                if (tempStandardMatch.Length > 0 && input.Contains(tempStandardMatch))
                {
                    MatchingSearchPattern = tempStandardMatch;
                    tempResult = true;
                }
                else if (tempURLMatch.Length > 0 && input.Contains(GetStrippedURL(tempURLMatch)))
                {
                    MatchingSearchPattern = tempURLMatch;
                    tempResult = true;
                }
            }
            catch
            {
                return false;
            }
            return tempResult;
        }

        private static string tempStrippedURL;
        private static string GetStrippedURL(string input)
        {
            tempStrippedURL = "";
            if (IsValidURL(input))
            {
                tempStrippedURL = ProcessStrippedURL(input);
                return tempStrippedURL;
            }
            else
            {
                return input;
            }
        }

        private static string ProcessStrippedURL(string input)
        {
            // Remove prefixes
            if (input.Contains("http://"))
            {
                input = input.Replace("http://", "");
            }
            if (input.Contains("https://"))
            {
                input = input.Replace("https://", "");
            }
            if (input.Contains("http://www."))
            {
                input = input.Replace("http://www.", "");
            }
            if (input.Contains("https://www."))
            {
                input = input.Replace("https://www.", "");
            }
            if (input.Contains("ttps://"))
            {
                input = input.Replace("ttps://", "");
            }
            if (input.Contains("ttp://"))
            {
                input = input.Replace("ttp://", "");
            }
            if (input.Contains("www."))
            {
                input = input.Replace("www.", "");
            }
            if (input.Contains("ww."))
            {
                input = input.Replace("ww.", "");
            }
            // Remove postfixes
            if (input.Contains("/"))
            {
                while (input.Contains("/"))
                {
                    input = input.Remove(input.Length - 1, 1);
                }
            }
            return input;
        }


        private static bool tempValidUrl;
        private static string tempValidUrlString;
        private const string tempRegexExpression = @"^((http|https|ftp|www):\/\/)?" +
                                                   @"([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)" +
                                                   @"(\.)" +
                                                   @"([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]+)";
        private static bool IsValidURL(string tmp)
        {
            try
            {
                // Trim initial string
                tempValidUrlString = tmp.Trim();
                tempValidUrl = false;
                // If contains chars and string is only one word
                if (tempValidUrlString.Length > 0 && tempValidUrlString.Split(' ').Length == 1)
                {
                    // Trim string even more...
                    tempValidUrlString = tempValidUrlString.Replace('\n', ' ');
                    tempValidUrlString = tempValidUrlString.Replace('\r', ' ');
                    tempValidUrlString = tempValidUrlString.Replace('?', ' ');
                    tempValidUrlString = tempValidUrlString.Trim();

                    // Run through the string char by char until a URL match is registered, if registered
                    for (int i = 5; i < tempValidUrlString.Length; i++)
                    {
                        if (Regex.IsMatch(tempValidUrlString.Substring(0, i), tempRegexExpression, RegexOptions.Singleline))
                        {
                            tempValidUrl = true;
                            break;
                        }
                    }
                }
            }
            catch
            {
                // Do nothing
            }
            // Return true if tmp is a valid URL.
            return tempValidUrl;
        }



        // Alternate search using pattern list
        public static bool IsMatchB(string input, string pattern)
        {
            tempResult = false;
            try
            {
                if (MatchOnReplacementCharacters(input))
                {
                    tempResult = true;
                }
            }
            catch
            {
                return false;
            }
            return tempResult;
        }


        // Rules for replacement strings (Languages DA, EN, DE):
        private static bool tempReplacementResult;
        private static bool MatchOnReplacementCharacters(string input)
        {
            // Ready result
            MatchingSearchPattern = "";
            tempReplacementResult = false;
            foreach (string pattern in SearchPatternsList)
            {
                // If there is a match with the current rule
                // break out of the loop and return result
                if (pattern.Length > 0 && input.Contains(pattern))
                {
                    MatchingSearchPattern = pattern;
                    tempReplacementResult = true;
                    break;
                }
            }

            return tempReplacementResult;
        }

        private static List<string> SearchPatternsList;
        private static string tempSearchPattern;
        public static void InitializeSearchPatterns(string searchPattern)
        {
            // Initialize patterns list
            if (SearchPatternsList == null)
            {
                SearchPatternsList = new List<string>();
            }
            // Initialize rules
            InitializeSearchRules();
            SearchPatternsList.Clear();
            tempSearchPattern = searchPattern.Trim().ToLower();

            // Add original search strings
            SearchPatternsList.Add(tempSearchPattern);
            // Add nudged indexes
            if (tempSearchPattern.Length >= 3)
            {
                SearchPatternsList.Add(tempSearchPattern.Substring(0, tempSearchPattern.Length - 1));
                SearchPatternsList.Add(tempSearchPattern.Substring(1, tempSearchPattern.Length - 1));
            }
            // Add nudged indexes
            if (tempSearchPattern.Length >= 5)
            {
                SearchPatternsList.Add(tempSearchPattern.Substring(1, tempSearchPattern.Length - 2));
                SearchPatternsList.Add(tempSearchPattern.Substring(2, tempSearchPattern.Length - 3));
            }

            foreach (string s in searchPattern.Split(' '))
            {
                PopulateSearchPatternsList(s.Trim().ToLower());
            }
        }

        private static void PopulateSearchPatternsList(string pattern)
        {
            foreach (SearchRule rule in SearchRulesList)
            {
                // Add original search string before adding
                // the modified ones
                if (pattern.Contains(rule.Source))
                {
                    SearchPatternsList.Add(pattern.Replace(rule.Source, rule.Target));
                }
            }
        }

        private static List<SearchRule> SearchRulesList;
        private static void InitializeSearchRules()
        {
            // Initialize Search Rules (Done the first time the method is run)
            if (SearchRulesList == null)
            {
                SearchRulesList = new List<SearchRule>();

                // Double and triple switches
                SearchRulesList.Add(new SearchRule("ck", "kc"));
                SearchRulesList.Add(new SearchRule("kc", "ck"));
                SearchRulesList.Add(new SearchRule("ck", "k"));
                SearchRulesList.Add(new SearchRule("k", "ck"));
                SearchRulesList.Add(new SearchRule("ht", "th"));
                SearchRulesList.Add(new SearchRule("th", "ht"));
                SearchRulesList.Add(new SearchRule("t", "th"));
                SearchRulesList.Add(new SearchRule("th", "t"));
                SearchRulesList.Add(new SearchRule("ch", "sch"));
                SearchRulesList.Add(new SearchRule("sch", "ch"));
                SearchRulesList.Add(new SearchRule("hc", "ch"));
                SearchRulesList.Add(new SearchRule("ch", "hc"));
                SearchRulesList.Add(new SearchRule("dt", "t"));
                SearchRulesList.Add(new SearchRule("t", "dt"));
                SearchRulesList.Add(new SearchRule("pf", "f"));
                SearchRulesList.Add(new SearchRule("f", "pf"));
                SearchRulesList.Add(new SearchRule("ps", "s"));
                SearchRulesList.Add(new SearchRule("s", "ps"));
                SearchRulesList.Add(new SearchRule("n", "kn"));
                SearchRulesList.Add(new SearchRule("kn", "n"));
                SearchRulesList.Add(new SearchRule("dge", "ge"));
                SearchRulesList.Add(new SearchRule("ge", "dge"));
                SearchRulesList.Add(new SearchRule("ou", "uo"));
                SearchRulesList.Add(new SearchRule("uo", "ou"));
                SearchRulesList.Add(new SearchRule("ugh", "ug"));
                SearchRulesList.Add(new SearchRule("ug", "ugh"));
                SearchRulesList.Add(new SearchRule("ang", "an"));
                SearchRulesList.Add(new SearchRule("an", "ang"));
                SearchRulesList.Add(new SearchRule("q", "kv"));
                SearchRulesList.Add(new SearchRule("kv", "q"));
                SearchRulesList.Add(new SearchRule("q", "ku"));
                SearchRulesList.Add(new SearchRule("ku", "q"));

                // Special character switches
                SearchRulesList.Add(new SearchRule("x", "*"));
                SearchRulesList.Add(new SearchRule("*", "x"));
                SearchRulesList.Add(new SearchRule("-", "_"));
                SearchRulesList.Add(new SearchRule("_", "-"));
                SearchRulesList.Add(new SearchRule(".", "-"));
                SearchRulesList.Add(new SearchRule("-", "."));
                SearchRulesList.Add(new SearchRule("_", "."));
                SearchRulesList.Add(new SearchRule(".", "_"));
                SearchRulesList.Add(new SearchRule("..", "."));
                SearchRulesList.Add(new SearchRule(".", ".."));
                SearchRulesList.Add(new SearchRule("...", ".."));
                SearchRulesList.Add(new SearchRule("..", "..."));
                SearchRulesList.Add(new SearchRule("...", "."));
                SearchRulesList.Add(new SearchRule(".", "..."));
                SearchRulesList.Add(new SearchRule("-", " "));
                SearchRulesList.Add(new SearchRule(" ", "-"));
                SearchRulesList.Add(new SearchRule("-", "~"));
                SearchRulesList.Add(new SearchRule("~", "-"));
                SearchRulesList.Add(new SearchRule(":", "="));
                SearchRulesList.Add(new SearchRule(":", "="));
                SearchRulesList.Add(new SearchRule("(", "["));
                SearchRulesList.Add(new SearchRule("(", "{"));
                SearchRulesList.Add(new SearchRule("[", "("));
                SearchRulesList.Add(new SearchRule("[", "{"));
                SearchRulesList.Add(new SearchRule("{", "("));
                SearchRulesList.Add(new SearchRule("{", "["));
                SearchRulesList.Add(new SearchRule(")", "]"));
                SearchRulesList.Add(new SearchRule(")", "}"));
                SearchRulesList.Add(new SearchRule("]", ")"));
                SearchRulesList.Add(new SearchRule("]", "}"));
                SearchRulesList.Add(new SearchRule("}", ")"));
                SearchRulesList.Add(new SearchRule("}", "]"));
                SearchRulesList.Add(new SearchRule("´´", "\""));
                SearchRulesList.Add(new SearchRule("\"", "´´"));
                SearchRulesList.Add(new SearchRule("''", "\""));
                SearchRulesList.Add(new SearchRule("\"", "''"));
                SearchRulesList.Add(new SearchRule("´", "'"));
                SearchRulesList.Add(new SearchRule("'", "´"));
                SearchRulesList.Add(new SearchRule(" ", ""));
                SearchRulesList.Add(new SearchRule("-", ""));
                SearchRulesList.Add(new SearchRule("?", ""));

                // Character switches
                SearchRulesList.Add(new SearchRule("e", "i"));
                SearchRulesList.Add(new SearchRule("i", "e"));
                SearchRulesList.Add(new SearchRule("l", "i"));
                SearchRulesList.Add(new SearchRule("i", "l"));
                SearchRulesList.Add(new SearchRule("o", "u"));
                SearchRulesList.Add(new SearchRule("u", "o"));
                SearchRulesList.Add(new SearchRule("c", "k"));
                SearchRulesList.Add(new SearchRule("k", "c"));
                SearchRulesList.Add(new SearchRule("w", "v"));
                SearchRulesList.Add(new SearchRule("v", "w"));
                SearchRulesList.Add(new SearchRule("s", "c"));
                SearchRulesList.Add(new SearchRule("s", "s"));
                SearchRulesList.Add(new SearchRule("ü", "u"));
                SearchRulesList.Add(new SearchRule("u", "ü"));
                SearchRulesList.Add(new SearchRule("ä", "a"));
                SearchRulesList.Add(new SearchRule("a", "ä"));
                SearchRulesList.Add(new SearchRule("ö", "o"));
                SearchRulesList.Add(new SearchRule("o", "ö"));
                SearchRulesList.Add(new SearchRule("z", "s"));
                SearchRulesList.Add(new SearchRule("s", "z"));
                SearchRulesList.Add(new SearchRule("æ", "a"));
                SearchRulesList.Add(new SearchRule("a", "æ"));
                SearchRulesList.Add(new SearchRule("æ", "e"));
                SearchRulesList.Add(new SearchRule("e", "æ"));
                SearchRulesList.Add(new SearchRule("æ", "ä"));
                SearchRulesList.Add(new SearchRule("ä", "æ"));
                SearchRulesList.Add(new SearchRule("ø", "o"));
                SearchRulesList.Add(new SearchRule("o", "ø"));
                SearchRulesList.Add(new SearchRule("ø", "ö"));
                SearchRulesList.Add(new SearchRule("ö", "ø"));
                SearchRulesList.Add(new SearchRule("m", "n"));
                SearchRulesList.Add(new SearchRule("n", "m"));
                //SearchRulesList.Add(new SearchRule("ü", "y"));
                //SearchRulesList.Add(new SearchRule("y", "ü"));
                //SearchRulesList.Add(new SearchRule("q", "k"));
                //SearchRulesList.Add(new SearchRule("k", "q"));

                // Vocal dobules aeioquæøåäöüß
                //SearchRulesList.Add(new SearchRule("aa", "a"));
                //SearchRulesList.Add(new SearchRule("a", "aa"));
                //SearchRulesList.Add(new SearchRule("ee", "e"));
                //SearchRulesList.Add(new SearchRule("e", "ee"));
                //SearchRulesList.Add(new SearchRule("ii", "i"));
                //SearchRulesList.Add(new SearchRule("i", "ii"));
                //SearchRulesList.Add(new SearchRule("oo", "o"));
                //SearchRulesList.Add(new SearchRule("o", "oo"));
                //SearchRulesList.Add(new SearchRule("qq", "q"));
                //SearchRulesList.Add(new SearchRule("q", "qq"));
                //SearchRulesList.Add(new SearchRule("uu", "u"));
                //SearchRulesList.Add(new SearchRule("u", "uu"));
                SearchRulesList.Add(new SearchRule("å", "aa"));
                SearchRulesList.Add(new SearchRule("aa", "å"));
                SearchRulesList.Add(new SearchRule("æ", "ae"));
                SearchRulesList.Add(new SearchRule("ae", "æ"));
                SearchRulesList.Add(new SearchRule("ä", "ae"));
                SearchRulesList.Add(new SearchRule("ae", "ä"));
                SearchRulesList.Add(new SearchRule("ø", "oe"));
                SearchRulesList.Add(new SearchRule("oe", "ø"));
                SearchRulesList.Add(new SearchRule("ö", "oe"));
                SearchRulesList.Add(new SearchRule("oe", "ö"));
                SearchRulesList.Add(new SearchRule("ß", "ss"));
                SearchRulesList.Add(new SearchRule("ss", "ß"));

                // Consonant doubles bcdfghjklmnprstvwxz + www
                //SearchRulesList.Add(new SearchRule("bb", "b"));
                //SearchRulesList.Add(new SearchRule("b", "bb"));
                //SearchRulesList.Add(new SearchRule("cc", "c"));
                //SearchRulesList.Add(new SearchRule("c", "cc"));
                //SearchRulesList.Add(new SearchRule("dd", "d"));
                //SearchRulesList.Add(new SearchRule("d", "dd"));
                //SearchRulesList.Add(new SearchRule("ff", "f"));
                //SearchRulesList.Add(new SearchRule("f", "ff"));
                //SearchRulesList.Add(new SearchRule("gg", "g"));
                //SearchRulesList.Add(new SearchRule("g", "gg"));
                //SearchRulesList.Add(new SearchRule("hh", "h"));
                //SearchRulesList.Add(new SearchRule("h", "hh"));
                //SearchRulesList.Add(new SearchRule("jj", "j"));
                //SearchRulesList.Add(new SearchRule("j", "jj"));
                //SearchRulesList.Add(new SearchRule("kk", "k"));
                //SearchRulesList.Add(new SearchRule("k", "kk"));
                //SearchRulesList.Add(new SearchRule("ll", "l"));
                //SearchRulesList.Add(new SearchRule("l", "ll"));
                //SearchRulesList.Add(new SearchRule("mm", "m"));
                //SearchRulesList.Add(new SearchRule("m", "mm"));
                //SearchRulesList.Add(new SearchRule("nn", "n"));
                //SearchRulesList.Add(new SearchRule("n", "nn"));
                //SearchRulesList.Add(new SearchRule("pp", "p"));
                //SearchRulesList.Add(new SearchRule("p", "pp"));
                //SearchRulesList.Add(new SearchRule("rr", "r"));
                //SearchRulesList.Add(new SearchRule("r", "rr"));
                //SearchRulesList.Add(new SearchRule("ss", "s"));
                //SearchRulesList.Add(new SearchRule("s", "ss"));
                //SearchRulesList.Add(new SearchRule("tt", "t"));
                //SearchRulesList.Add(new SearchRule("t", "tt"));
                //SearchRulesList.Add(new SearchRule("vv", "v"));
                //SearchRulesList.Add(new SearchRule("v", "vv"));
                //SearchRulesList.Add(new SearchRule("xx", "x"));
                //SearchRulesList.Add(new SearchRule("x", "xx"));
                //SearchRulesList.Add(new SearchRule("zz", "z"));
                //SearchRulesList.Add(new SearchRule("z", "zz"));
                SearchRulesList.Add(new SearchRule("ww", "w"));
                SearchRulesList.Add(new SearchRule("w", "ww"));
                SearchRulesList.Add(new SearchRule("www", "ww"));
                SearchRulesList.Add(new SearchRule("ww", "www"));
                SearchRulesList.Add(new SearchRule("www.", ""));
                SearchRulesList.Add(new SearchRule("http://", "www."));
                SearchRulesList.Add(new SearchRule("www.", "http://"));
                SearchRulesList.Add(new SearchRule("https://", "http://"));
                SearchRulesList.Add(new SearchRule("http://", "https://"));
                SearchRulesList.Add(new SearchRule("https://", "www."));
                SearchRulesList.Add(new SearchRule("www.", "https://"));
                SearchRulesList.Add(new SearchRule("www.", "https://www."));
                SearchRulesList.Add(new SearchRule("https://www.", "www."));
                SearchRulesList.Add(new SearchRule("www.", "http://www."));
                SearchRulesList.Add(new SearchRule("http://www.", "www."));
                SearchRulesList.Add(new SearchRule(".dk/", ".dk"));
                SearchRulesList.Add(new SearchRule(".de/", ".de"));
                SearchRulesList.Add(new SearchRule(".com/", ".com"));
                SearchRulesList.Add(new SearchRule(".co.uk/", ".co.uk"));
                SearchRulesList.Add(new SearchRule(".uk/", ".uk"));
                SearchRulesList.Add(new SearchRule(".uk", ".uk/"));
                SearchRulesList.Add(new SearchRule(".se/", ".se"));
                SearchRulesList.Add(new SearchRule(".se", ".se/"));
                SearchRulesList.Add(new SearchRule(".no/", ".no"));
                SearchRulesList.Add(new SearchRule(".no", ".no/"));
                SearchRulesList.Add(new SearchRule(".nu/", ".nu"));
                SearchRulesList.Add(new SearchRule(".nu", ".nu/"));
                SearchRulesList.Add(new SearchRule(".fr/", ".fr"));
                SearchRulesList.Add(new SearchRule(".fr", ".fr/"));
                SearchRulesList.Add(new SearchRule(".as/", ".as"));
                SearchRulesList.Add(new SearchRule(".as", ".as/"));
                SearchRulesList.Add(new SearchRule(".it/", ".it"));
                SearchRulesList.Add(new SearchRule(".it", ".it/"));
                SearchRulesList.Add(new SearchRule(".ru/", ".ru"));
                SearchRulesList.Add(new SearchRule(".ru", ".ru/"));
                SearchRulesList.Add(new SearchRule(".us/", ".us"));
                SearchRulesList.Add(new SearchRule(".us", ".us/"));
                SearchRulesList.Add(new SearchRule(".eu/", ".eu"));
                SearchRulesList.Add(new SearchRule(".eu", ".eu/"));
                SearchRulesList.Add(new SearchRule(".pl/", ".pl"));
                SearchRulesList.Add(new SearchRule(".pl", ".pl/"));
                SearchRulesList.Add(new SearchRule(".es/", ".es"));
                SearchRulesList.Add(new SearchRule(".es", ".es/"));
                SearchRulesList.Add(new SearchRule(".at/", ".at"));
                SearchRulesList.Add(new SearchRule(".at", ".at/"));
                SearchRulesList.Add(new SearchRule(".be/", ".be"));
                SearchRulesList.Add(new SearchRule(".be", ".be/"));
                SearchRulesList.Add(new SearchRule(".io/", ".io"));
                SearchRulesList.Add(new SearchRule(".io", ".io/"));
                SearchRulesList.Add(new SearchRule(".fi/", ".fi"));
                SearchRulesList.Add(new SearchRule(".fi", ".fi/"));
                SearchRulesList.Add(new SearchRule(".xyz/", ".xyz"));
                SearchRulesList.Add(new SearchRule(".xyz", ".xyz/"));
                SearchRulesList.Add(new SearchRule(".org/", ".org"));
                SearchRulesList.Add(new SearchRule(".org", ".org/"));
                SearchRulesList.Add(new SearchRule(".net/", ".net"));
                SearchRulesList.Add(new SearchRule(".net", ".net/"));
                SearchRulesList.Add(new SearchRule(".info/", ".info"));
                SearchRulesList.Add(new SearchRule(".info", ".info/"));
                SearchRulesList.Add(new SearchRule(".biz/", ".biz"));
                SearchRulesList.Add(new SearchRule(".biz", ".biz/"));
                SearchRulesList.Add(new SearchRule(".store/", ".store"));
                SearchRulesList.Add(new SearchRule(".store", ".store/"));
                SearchRulesList.Add(new SearchRule(".media/", ".media"));
                SearchRulesList.Add(new SearchRule(".media", ".media/"));


                // Numeral switches
                SearchRulesList.Add(new SearchRule("1", "l"));
                SearchRulesList.Add(new SearchRule("l", "1"));
                SearchRulesList.Add(new SearchRule("1", "i"));
                SearchRulesList.Add(new SearchRule("i", "1"));

                // Roman Numerals switches
                //SearchRulesList.Add(new SearchRule("i", "1"));
                //SearchRulesList.Add(new SearchRule("1", "i"));
                //SearchRulesList.Add(new SearchRule("ii", "2"));
                //SearchRulesList.Add(new SearchRule("2", "ii"));
                //SearchRulesList.Add(new SearchRule("iii", "3"));
                //SearchRulesList.Add(new SearchRule("3", "iii"));
                //SearchRulesList.Add(new SearchRule("iv", "4"));
                //SearchRulesList.Add(new SearchRule("4", "iv"));
                //SearchRulesList.Add(new SearchRule("v", "5"));
                //SearchRulesList.Add(new SearchRule("5", "v"));
                //SearchRulesList.Add(new SearchRule("vi", "6"));
                //SearchRulesList.Add(new SearchRule("6", "vi"));
                //SearchRulesList.Add(new SearchRule("vii", "7"));
                //SearchRulesList.Add(new SearchRule("7", "vii"));
                //SearchRulesList.Add(new SearchRule("viii", "8"));
                //SearchRulesList.Add(new SearchRule("8", "viii"));
                //SearchRulesList.Add(new SearchRule("ix", "9"));
                //SearchRulesList.Add(new SearchRule("9", "ix"));
                //SearchRulesList.Add(new SearchRule("x", "10"));
                //SearchRulesList.Add(new SearchRule("10", "x"));
                //SearchRulesList.Add(new SearchRule("xi", "11"));
                //SearchRulesList.Add(new SearchRule("11", "xi"));
                //SearchRulesList.Add(new SearchRule("xii", "12"));
                //SearchRulesList.Add(new SearchRule("12", "xii"));

                // Keyboard-related (close) characters (typos)
                //SearchRulesList.Add(new SearchRule("j", "k"));
                //SearchRulesList.Add(new SearchRule("k", "j"));
                //SearchRulesList.Add(new SearchRule("h", "j"));
                //SearchRulesList.Add(new SearchRule("j", "h"));
                //SearchRulesList.Add(new SearchRule("f", "g"));
                //SearchRulesList.Add(new SearchRule("g", "f"));
                //SearchRulesList.Add(new SearchRule("h", "g"));
                //SearchRulesList.Add(new SearchRule("g", "h"));
                //SearchRulesList.Add(new SearchRule("t", "g"));
                //SearchRulesList.Add(new SearchRule("g", "t"));
            }
        }

        class SearchRule
        {
            public string Source { get; set; }
            public string Target { get; set; }
            public SearchRule(string source, string target)
            {
                Source = source;
                Target = target;
            }
        }
    }
}
