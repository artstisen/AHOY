using System;
using System.Collections.Generic;
using System.Drawing;

namespace AhoyBrowser
{
    class AhoyColors
    {
        public static int ColorTheme { get; set; }
        public static int PreviewColorTheme { get; set; }
        public static string CustomColorThemeTitle { get; set; }
        private static List<Color> currentColors { get; set; }
        private static List<string> customColorsHex { get; set; }
        private static List<string> customColorsHexPreview { get; set; }
        private static List<Settings> ThemeCollection { get; set; }


        public static List<Color> CurrentColorTheme()
        {
            // Initialize colors list
            if (currentColors == null || currentColors.Count == 0)
            {
                currentColors = new List<Color>();
            }

            // Get theme colors
            SetTheme(ColorTheme);

            // Replace with custom colors
            // If any of the custom colors are set
            // Run through and replace in List
            try
            {
                if (HasCustomColors())
                {
                    for (int i = 0; i < customColorsHex.Count; i++)
                    {
                        if (customColorsHex[i] != Settings.DefaultCustomColors)
                        {
                            currentColors[i] = ConvertFromHex(customColorsHex[i], false);
                        }
                    }
                }
            }
            catch
            {
                // Do nothing
            }

            return currentColors; // 31 colors noted
        }


        private static Color dimmedColor;
        public static Color GetDimmedColor(Color c, int value, bool invert)
        {
            try
            {
               
                if (invert)
                {
                    dimmedColor = Color.FromArgb(255, FlattenValue(c.R + value), FlattenValue(c.G + value), FlattenValue(c.B + value));
                }
                else
                {
                    dimmedColor = Color.FromArgb(255, FlattenValue(c.R - value), FlattenValue(c.G - value), FlattenValue(c.B - value));
                }

                return dimmedColor;
            }
            catch
            {
                return Color.Black;
            }
        }

        private static int FlattenValue(int value)
        {
            if (value < 0)
            {
                return 0;
            }
            else if (value > 255)
            {
                return 255;
            }
            else
            {
                return value;
            }
        }

        public static List<Color> CurrentColorThemeWithoutCustomColors()
        {
            // Initialize colors list
            if (currentColors == null || currentColors.Count == 0)
            {
                currentColors = new List<Color>();
            }
            // Get theme colors
            SetTheme(ColorTheme);
            return currentColors; // 31 colors noted
        }


        public static void SubmitCustomColorPreview()
        {
            InitializeCustomColors();
            try
            {
                if (IsCustomColorsPreviewValid())
                {
                    for (int i = 0; i < customColorsHexPreview.Count; i++)
                    {
                        if (IsValidHexCode(customColorsHexPreview[i], true))
                        {
                            customColorsHex[i] = customColorsHexPreview[i];
                        }
                    }
                }
            }
            catch
            {
                // Do nothing
            }
        }


        private static bool IsCustomColorsPreviewValid()
        {
            if (customColorsHexPreview != null && customColorsHexPreview.Count > 0)
            {
                if (customColorsHexPreview.Count == customColorsHex.Count)
                {
                    return true;
                }
            }
            return false;
        }

        public static void RetrieveCustomColorsAsPreview(bool clearCurrentPreview)
        {
            InitializeCustomColors();
            if (clearCurrentPreview)
            {
                if (HasCustomColorsPreview())
                {
                    ResetAllCustomPreviewColors();
                }
            }

            if (HasCustomColors())
            {
                for (int i = 0; i < customColorsHex.Count; i++)
                {
                    if (customColorsHex[i] != Settings.DefaultCustomColors)
                    {
                        customColorsHexPreview[i] = customColorsHex[i];
                    }
                }
            }
        }

        public static List<Color> CurrentColorThemePreview()
        {
            // Initialize colors list
            if (currentColors == null || currentColors.Count == 0)
            {
                currentColors = new List<Color>();
            }

            // Get theme preview colors
            SetTheme(PreviewColorTheme);

            // Override with custom color theme
            // If any of the custom colors are set
            // Run through and replace in List
            try
            {
                if (HasCustomColorsPreview())
                {
                    for (int i = 0; i < customColorsHexPreview.Count; i++)
                    {
                        if (customColorsHexPreview[i] != Settings.DefaultCustomColors)
                        {
                            currentColors[i] = ConvertFromHex(customColorsHexPreview[i], false);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Do nothing
            }

            return currentColors;
        }


        public static void SetCustomColorPreviewByIndex(int index, string colorHex)
        {
            InitializeCustomColors();
            if ((customColorsHexPreview != null && customColorsHexPreview.Count > 0) && (index >= 0 && index < customColorsHexPreview.Count))
            {
                customColorsHexPreview[index] = colorHex;
            }
        }


        public static void ResetAllCustomColors()
        {
            InitializeCustomColors();
            if (customColorsHex != null && customColorsHex.Count > 0)
            {
                for (int i = 0; i < customColorsHex.Count; i++)
                {
                    customColorsHex[i] = Settings.DefaultCustomColors;
                    RestoreOriginalColorByIndex(i);
                }
            }
        }

        public static void ResetAllCustomPreviewColors()
        {
            InitializeCustomColors();
            if (customColorsHexPreview != null && customColorsHexPreview.Count > 0)
            {
                for (int i = 0; i < customColorsHexPreview.Count; i++)
                {
                    customColorsHexPreview[i] = Settings.DefaultCustomColors;
                }
            }
        }


        public static void ResetCustomColorPreviewByIndex(int index)
        {
            InitializeCustomColors();
            if ((customColorsHexPreview != null && customColorsHexPreview.Count > 0) && (index >= 0 && index < customColorsHexPreview.Count))
            {
                customColorsHexPreview[index] = Settings.DefaultCustomColors;
                RestoreOriginalColorByIndex(index);
            }
        }

        private static void RestoreOriginalColorByIndex(int index)
        {
            if ((currentColors != null && currentColors.Count > 0) && (index >= 0 && index < currentColors.Count))
            {
                currentColors[index] = CurrentColorThemeWithoutCustomColors()[index];
            }
        }


        public static void InitializeCustomColors()
        {
            // Initialize the custom colors list and fill it with empty colors
            if (customColorsHex == null)
            {
                customColorsHex = new List<string>();
                customColorsHexPreview = new List<string>();

            }

            if (customColorsHex.Count == 0)
            {
                for (int i = 0; i < currentColors.Count; i++)
                {
                    customColorsHex.Add(Settings.DefaultCustomColors);
                    customColorsHexPreview.Add(Settings.DefaultCustomColors);
                }
            }
        }

        // Fill custom color list
        private static List<string> tempLoadCustomColors;
        public static void LoadCustomColorsIntoList(string colorCSV)
        {
            // Initialize custom colors list
            InitializeCustomColors();

            // Initialize temp list for loading
            if (tempLoadCustomColors == null || tempLoadCustomColors.Count == 0)
            {
                tempLoadCustomColors = new List<string>();
            }

            if (colorCSV.Length > 0 && colorCSV.Contains(";"))
            {
                tempLoadCustomColors.Clear();
                try
                {
                    tempLoadCustomColors.AddRange(colorCSV.Split(';'));
                    if (tempLoadCustomColors.Count >= customColorsHex.Count && ValidateHexValues(tempLoadCustomColors, true))
                    {
                        for (int i = 0; i < customColorsHex.Count; i++)
                        {
                            customColorsHex[i] = tempLoadCustomColors[i];
                        }
                    }
                }
                catch
                {
                    // Do nothing
                    // Log error here
                    //Log.LogPreloadThemeError();
                }
            }
        }

        // Fill custom color preset list
        public static void LoadCustomPreviewColorsIntoList(string colorCSV)
        {
            // Initialize custom colors list
            InitializeCustomColors();

            // Initialize temp list for loading
            if (tempLoadCustomColors == null || tempLoadCustomColors.Count == 0)
            {
                tempLoadCustomColors = new List<string>();
            }

            if (colorCSV.Length > 0 && colorCSV.Contains(";"))
            {
                tempLoadCustomColors.Clear();
                try
                {
                    tempLoadCustomColors.AddRange(colorCSV.Split(';'));
                    if (tempLoadCustomColors.Count >= customColorsHexPreview.Count && ValidateHexValues(tempLoadCustomColors, true))
                    {
                        for (int i = 0; i < customColorsHexPreview.Count; i++)
                        {
                            customColorsHexPreview[i] = tempLoadCustomColors[i];
                        }
                    }
                }
                catch
                {
                    // Log error here
                    //Log.LogThemeError();
                }
            }
        }

        public static bool ValidateHexValues(List<string> hexValues, bool allowEmptyColorCode)
        {
            if (hexValues != null && hexValues.Count > 0)
            {
                for (int i = 0; i < hexValues.Count; i++)
                {
                    if (!IsValidHexCode(hexValues[i], allowEmptyColorCode))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }


        // Get a straight forward list of all currently used colors
        public static string GetListOfThemeColorsAsString()
        {
            try
            {
                List<Color> tempListOfColors;
                tempListOfColors = new List<Color>();
                tempCustomColors = "";

                for (int i = 0; i < CurrentColorTheme().Count; i++)
                {
                    tempCustomColors += i.ToString("00") + ": " + GetColorTableFromRGB(CurrentColorTheme()[i]) + Environment.NewLine;
                }
            }
            catch
            {
                tempCustomColors = "N/A";
            }
            return tempCustomColors;
        }


        // Get custom colors as CSV
        private static string tempCustomColors;
        public static string RetrieveCustomColorsAsCSV()
        {
            tempCustomColors = "";
            if (customColorsHex != null && customColorsHex.Count > 0)
            {
                for (int i = 0; i < customColorsHex.Count; i++)
                {
                    tempCustomColors += customColorsHex[i] + ";";
                }
            }
            return tempCustomColors;
        }

        public static string RetrieveCustomColorPreviewAsCSV()
        {
            tempCustomColors = "";
            if (customColorsHexPreview != null && customColorsHexPreview.Count > 0)
            {
                for (int i = 0; i < customColorsHexPreview.Count; i++)
                {
                    tempCustomColors += customColorsHexPreview[i] + ";";
                }
            }
            return tempCustomColors;
        }


        #region COLOR CONVERTERS
        private const string tempHexRegex = @"[#]([0-9]|[a-f]|[A-F]){6}\b";
        public static bool IsValidHexCode(string hexString, bool allowEmptyColorCode)
        {
            try
            {
                if (allowEmptyColorCode)
                {
                    if (hexString == Settings.DefaultCustomColors || hexString.Length == 0 || System.Text.RegularExpressions.Regex.IsMatch(hexString, tempHexRegex))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (System.Text.RegularExpressions.Regex.IsMatch(hexString, tempHexRegex))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool HasDualHexValue(string input)
        {
            try
            {
                if (!string.IsNullOrEmpty(input) && input.Length > 0)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(input, @"([#]([0-9]|[a-f]|[A-F]){6}\b)\;([#]([0-9]|[a-f]|[A-F]){6}\b)"))
                    {
                        return true;
                    }
                    else
                    {
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
                return false;
            }
        }

        public static Color ConvertFromHex(string hexString, bool showErrorDialog)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(hexString, @"[#]([0-9]|[a-f]|[A-F]){6}\b"))
                {
                    int red = int.Parse(hexString.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
                    int green = int.Parse(hexString.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
                    int blue = int.Parse(hexString.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);

                    return Color.FromArgb(red, green, blue);
                }
                else
                {
                    Log.LogColorPickerStatus("HEX to RGB conversion failed");
                    if (showErrorDialog)
                    {
                        AhoySimpleMessageBox msg = new AhoySimpleMessageBox("Unable to convert HEX value to RGB.\nPlease revise your input", MessageText.ErrorCaption(), false);
                        msg.ShowDialog();
                        msg.TopMost = true;
                    }
                }
            }
            catch
            {
                // Do nothing
            }
            return Color.White;
        }


        public static string ConvertToHex(Color rgb)
        {
            string tempColorHex;
            Color tempColorRGB;
            tempColorHex = "";
            try
            {
                tempColorRGB = rgb;
                tempColorHex = "#" + tempColorRGB.R.ToString("X2") + tempColorRGB.G.ToString("X2") + tempColorRGB.B.ToString("X2");
                return tempColorHex;
            }
            catch
            {
                return "N/A";
            }
        }
        #endregion


        public static string GetColorTableFromRGB(Color color1)
        {
            try
            {
                string tempOutput = ConvertToHex(color1) + " (" + color1.R.ToString("000") + ";" + color1.G.ToString("000") + ";" + color1.B.ToString("000") + ")";
                return tempOutput;
            }
            catch
            {
                return "N/A";
            }
        }

        public static string GetColorTableFromRGB(Color color1, Color color2)
        {
            try
            {
                string tempOutput = ConvertToHex(color1) + " (" + color1.R.ToString("000") + ";" + color1.G.ToString("000") + ";" + color1.B.ToString("000") + "), " +
                                    ConvertToHex(color2) + " (" + color2.R.ToString("000") + ";" + color2.G.ToString("000") + ";" + color2.B.ToString("000") + ")";
                return tempOutput;
            }
            catch
            {
                return "N/A";
            }
        }

        public static string GetColorTableFromHex(string hex1)
        {
            try
            {
                if (!string.IsNullOrEmpty(hex1))
                {
                    string tempOutput = hex1 + " (" + ConvertFromHex(hex1, false).R.ToString("000") + ";" + ConvertFromHex(hex1, false).G.ToString("000") + ";" + ConvertFromHex(hex1, false).B.ToString("000") + ")";
                    return tempOutput;
                }
                else
                {
                    return "N/A";
                }
            }
            catch
            {
                return "N/A";
            }
        }

        public static string GetColorTableFromHex(string hex1, string hex2)
        {
            try
            {
                if (!string.IsNullOrEmpty(hex1) && !string.IsNullOrEmpty(hex2))
                {
                    string tempOutput = hex1 + " (" + ConvertFromHex(hex1, false).R.ToString("000") + ";" + ConvertFromHex(hex1, false).G.ToString("000") + ";" + ConvertFromHex(hex1, false).B.ToString("000") + "), " +
                                        hex2 + " (" + ConvertFromHex(hex2, false).R.ToString("000") + ";" + ConvertFromHex(hex2, false).G.ToString("000") + ";" + ConvertFromHex(hex2, false).B.ToString("000") + ")";
                    return tempOutput;
                }
                else
                {
                    return "N/A";
                }
            }
            catch
            {
                return "N/A";
            }
        }


        public static bool HasCustomColors()
        {
            if (customColorsHex != null && customColorsHex.Count > 0)
            {
                foreach (string s in customColorsHex)
                {
                    if (IsValidHexCode(s, false))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool HasCustomColorsPreview()
        {
            if (customColorsHexPreview != null && customColorsHexPreview.Count > 0)
            {
                foreach (string s in customColorsHexPreview)
                {
                    if (IsValidHexCode(s, false))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public static void PreparePreview()
        {
            PreviewColorTheme = ColorTheme;
            RetrieveCustomColorsAsPreview(true);
        }

        // Get theme title by iterating the preloaded theme collection (settings objects)
        public static string GetThemeTitleByIndex(int index)
        {
            if (ThemeCollection == null)
            {
                InitializeThemeCollection();
            }

            if (index < ThemeCollection.Count)
            {
                return ThemeCollection[index].ThemeTitle;
            }
            else
            {
                return "N/A";
            }
        }


        // Load all color themes into memory (Called by the SetTheme method below)
        public static void InitializeThemeCollection()
        {
            try
            {
                ThemeCollection = new List<Settings>();
                // Load all themes 
                for (int i = 0; i < ThemeCount + 1; i++)
                {
                    ThemeCollection.Add(IO.ImportLocalTheme(i));
                }
            }
            catch
            {
                Log.LogPreloadThemeError();
            }
        }

        // Built-in color themes (index = value saved in Settings.xml as <colortheme>)
        internal const int ThemeDark = 0;
        internal const int ThemeLight = 1;
        internal const int ThemeBlue = 2;
        internal const int ThemeCount = 2; // Zero indexed (highest index)

        // Ahoy 1.x had 20 themes, saved as <theme>. Keep the three that still exist, map light
        // themes to Light and everything else to Dark.
        internal static int MapLegacyThemeIndex(int legacyIndex)
        {
            switch (legacyIndex)
            {
                case 1:  // Text Editor
                case 2:  // Gadget
                    return ThemeLight;
                case 10: // Blue Plastic
                case 15: // Age of Kings
                    return ThemeBlue;
                default:
                    return ThemeDark;
            }
        }

        internal static bool IsLightTheme()
        {
            return ColorTheme == ThemeLight;
        }

        // Error/attention text: the highlight color is hard to read on the light theme
        internal static Color ErrorTextColor()
        {
            return IsLightTheme() ? CurrentColorTheme()[4] : CurrentColorTheme()[6];
        }

        private static Settings ThemeSettingsObject;
        private static void SetTheme(int index)
        {
            try
            {
                // Ensure index is between 0 and ThemeCount (if set to something else manually in the settingsfile etc.)
                if (index < 0 || index > ThemeCount)
                {
                    index = 0;
                }

                // Ensure themes are available
                if (ThemeCollection == null)
                {
                    InitializeThemeCollection();
                }

                // Ready the settings object
                if (ThemeSettingsObject == null)
                {
                    ThemeSettingsObject = new Settings();
                }

                // Clear previous theme
                currentColors.Clear();

                // Load theme from index
                ThemeSettingsObject = ThemeCollection[index];
                // Set values
                if (ThemeSettingsObject.LoadSuccess)
                {
                    // Set button bitmaps
                    ButtonBitmaps.ButtonBitmapVersion = ThemeSettingsObject.ButtonBitmapOverride;
                    // Set colors
                    LoadColors(ThemeSettingsObject.CustomColors);
                }
                else
                {
                    Log.LogThemeError();
                }
            }
            catch
            {
                //Log.LogThemeError();
            }
        }

        // Will load colors from CSV into the active color palette (2019)
        // (Use the "csv" command to get the current color theme as CSV)
        public static void LoadColors(string colorCSV)
        {
            // Initialize temp list for loading
            if (tempLoadCustomColors == null || tempLoadCustomColors.Count == 0)
            {
                tempLoadCustomColors = new List<string>();
            }

            if (colorCSV.Length > 0 && colorCSV.Contains(";"))
            {
                tempLoadCustomColors.Clear();
                try
                {
                    tempLoadCustomColors.AddRange(colorCSV.Split(';'));
                    if (tempLoadCustomColors.Count > 0 && ValidateHexValues(tempLoadCustomColors, true))
                    {
                        for (int i = 0; i < tempLoadCustomColors.Count - 1; i++)
                        {
                            currentColors.Add(ConvertFromHex(tempLoadCustomColors[i], false));
                        }
                    }
                    else
                    {
                        // Log error here
                        Log.LogThemeError();
                    }
                }
                catch
                {
                    // Log error here
                    Log.LogThemeError();
                }
            }
        }
    }
}
