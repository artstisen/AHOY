using System.Drawing;

namespace AhoyBrowser
{
    class Design
    {
        private const string Font1 = "Consolas"; // Since Windows Vista, included with Visual Studio 2010 etc...
        private const string Font2 = "Courier New"; // All versions of Windows
        public static int CurrentFont { get; set; }
        public static bool CurrentListFontWeight { get; set; }
        public static bool EnforceScrollbars { get; set; }

        private static FontStyle GetListFontWeight()
        {
            if (CurrentListFontWeight)
            {
                return FontStyle.Bold;
            }
            else
            {
                return FontStyle.Regular;
            }
        }

        #region STATUS COLORS
        public static Color ReturnStatusBackgroundColor(int color)
        {
            switch (color)
            {
                case 1:
                    return AhoyColors.CurrentColorTheme()[12];
                case 2:
                    return AhoyColors.CurrentColorTheme()[13];
                case 3:
                    return AhoyColors.CurrentColorTheme()[14];
                case 4:
                    return AhoyColors.CurrentColorTheme()[15];
                case 5:
                    return AhoyColors.CurrentColorTheme()[16];
                case 6:
                    return AhoyColors.CurrentColorTheme()[17];
                case 7:
                    return AhoyColors.CurrentColorTheme()[18];
                case 8:
                    return AhoyColors.CurrentColorTheme()[19];
                default:
                    return AhoyColors.CurrentColorTheme()[2];
            }
        }

        public static Color ReturnStatusForegroundColor(int color)
        {
            switch (color)
            {
                case 1:
                    return AhoyColors.CurrentColorTheme()[20];
                case 2:
                    return AhoyColors.CurrentColorTheme()[21];
                case 3:
                    return AhoyColors.CurrentColorTheme()[22];
                case 4:
                    return AhoyColors.CurrentColorTheme()[23];
                case 5:
                    return AhoyColors.CurrentColorTheme()[24];
                case 6:
                    return AhoyColors.CurrentColorTheme()[25];
                case 7:
                    return AhoyColors.CurrentColorTheme()[26];
                case 8:
                    return AhoyColors.CurrentColorTheme()[27];
                default:
                    return AhoyColors.CurrentColorTheme()[2];
            }
        }


        public static Color ReturnCustomBackgroundColor(int color)
        {
            switch (color)
            {
                case 10:
                    return Color.FromArgb(255, 126, 000);
                case 11:
                    return Color.FromArgb(242, 097, 000);
                case 12:
                    return Color.FromArgb(234, 083, 000);
                case 13:
                    return Color.FromArgb(225, 064, 000);
                case 14:
                    return Color.FromArgb(125, 018, 000);
                case 15:
                    return Color.FromArgb(142, 013, 000);
                case 16:
                    return Color.FromArgb(174, 016, 000);
                case 17:
                    return Color.FromArgb(215, 000, 000);
                case 18:
                    return Color.FromArgb(134, 000, 255);
                case 19:
                    return Color.FromArgb(102, 000, 194);
                case 20:
                    return Color.FromArgb(087, 000, 165);
                case 21:
                    return Color.FromArgb(072, 000, 137);
                case 22:
                    return Color.FromArgb(000, 046, 121);
                case 23:
                    return Color.FromArgb(000, 054, 143);
                case 24:
                    return Color.FromArgb(000, 070, 186);
                case 25:
                    return Color.FromArgb(000, 096, 255);
                case 26:
                    return Color.FromArgb(001, 133, 000);
                case 27:
                    return Color.FromArgb(001, 101, 000);
                case 28:
                    return Color.FromArgb(001, 087, 000);
                case 29:
                    return Color.FromArgb(001, 069, 000);
                default:
                    return Color.FromArgb(0, 0, 0);
            }
        }

        public static Color ReturnCustomForegroundColor(int color)
        {
            switch (color)
            {
                case 10:
                    return Color.FromArgb(255, 226, 202);
                case 11:
                    return Color.FromArgb(255, 226, 202);
                case 12:
                    return Color.FromArgb(255, 226, 202);
                case 13:
                    return Color.FromArgb(255, 226, 202);
                case 14:
                    return Color.FromArgb(255, 255, 255);
                case 15:
                    return Color.FromArgb(255, 255, 255);
                case 16:
                    return Color.FromArgb(255, 255, 255);
                case 17:
                    return Color.FromArgb(255, 255, 255);
                case 18:
                    return Color.FromArgb(230, 202, 255);
                case 19:
                    return Color.FromArgb(230, 202, 255);
                case 20:
                    return Color.FromArgb(230, 202, 255);
                case 21:
                    return Color.FromArgb(230, 202, 255);
                case 22:
                    return Color.FromArgb(000, 255, 255);
                case 23:
                    return Color.FromArgb(000, 255, 255);
                case 24:
                    return Color.FromArgb(000, 255, 255);
                case 25:
                    return Color.FromArgb(000, 255, 255);
                case 26:
                    return Color.FromArgb(108, 255, 107);
                case 27:
                    return Color.FromArgb(108, 255, 107);
                case 28:
                    return Color.FromArgb(108, 255, 107);
                case 29:
                    return Color.FromArgb(108, 255, 107);
                default:
                    return Color.FromArgb(255, 255, 255);
            }
        }
        #endregion

        public static string GetFontName()
        {
            switch (CurrentFont)
            {
                case 0:
                    return Font1;
                case 1:
                    return Font2;
                default:
                    return Font1;
            }
        }

        public static string GetFontNameByIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return Font1;
                case 1:
                    return Font2;
                default:
                    return Font1;
            }
        }

        #region FONTS
        public static Font MenuFont()
        {
            return new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
        }

        public static Font MenuFontBold()
        {
            return new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
        }

        public static Font SearchFieldFont()
        {
            // Search fields
            return new Font(GetFontName(), 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        public static Font PasswordFieldFont()
        {
            // Password
            return new Font(GetFontName(), 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
        }

        public static Font TitleFieldFont()
        {
            // Entry title
            return new Font(GetFontName(), 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        public static Font ContentFieldFont()
        {
            // Entry content
            return new Font(GetFontName(), 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }
        public static Font FieldFont(int Zoom)
        {
            // Fields
            switch (Zoom)
            {
                case 0:
                    return new Font(GetFontName(), 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
                case 1:
                    return new Font(GetFontName(), 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
                case 2: // Purposely different font size
                    return new Font(GetFontName(), 15.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
                default:
                    return new Font(GetFontName(), 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
        }

        public static Font ListBoxFont(int Zoom)
        {
            // Listboxes
            switch (Zoom)
            {
                case 0:
                    return new Font(GetFontName(), 9F, GetListFontWeight(), GraphicsUnit.Point, 0);
                case 1:
                    return new Font(GetFontName(), 12F, GetListFontWeight(), GraphicsUnit.Point, 0);
                case 2:
                    return new Font(GetFontName(), 14.25F, GetListFontWeight(), GraphicsUnit.Point, 0);
                default:
                    return new Font(GetFontName(), 12F, GetListFontWeight(), GraphicsUnit.Point, 0);
            }
        }

        public static Font MessageboxTinyFont()
        {
            // Messagebox small font
            return new Font(GetFontName(), 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        public static Font MessageboxXSmallFont()
        {
            // Messagebox small font
            return new Font(GetFontName(), 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        public static Font MessageboxSmallFont()
        {
            // Messagebox small font
            return new Font(GetFontName(), 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        public static Font MessageboxSmallFontBold()
        {
            // Messagebox small font
            return new Font(GetFontName(), 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
        }

        public static Font MessageboxMediumSmallFont()
        {
            // Messagebox small font
            return new Font(GetFontName(), 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        public static Font MessageboxLargeFont()
        {
            // Messagebox large font
            return new Font(GetFontName(), 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        public static Font MessageboxLargeFontBold()
        {
            // Messagebox large font
            return new Font(GetFontName(), 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
        }

        public static Font MessageboxXXLFont()
        {
            // Messagebox large font
            return new Font(GetFontName(), 44F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        public static Font SleepFont()
        {
            // Messagebox large font
            return new Font(GetFontName(), 32F, FontStyle.Bold, GraphicsUnit.Point, 0);
        }

        public static Font SplashFont()
        {
            // Messagebox large font
            return new Font(GetFontName(), 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }
        public static Font StyleEditorXXSSmallFont()
        {
            // Messagebox small font
            return new Font(GetFontName(), 7F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        public static Font StyleEditorXSSmallFont()
        {
            // Messagebox small font
            return new Font(GetFontName(), 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        public static Font LogMonitorFont()
        {
            // Messagebox small font
            return new Font(GetFontName(), 7.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        public static Font TypermaxXSSmallFont()
        {
            // Messagebox small font
            return new Font(GetFontName(), 7.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }
        #endregion
    }
}
