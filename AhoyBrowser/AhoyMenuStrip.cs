using System.Drawing;
using System.Windows.Forms;

namespace AhoyBrowser
{
    class AhoyMenuStrip : MenuStrip
    {
        public AhoyMenuStrip()
        {
            this.Renderer = new AhoyMenuStripRenderer(new AhoyMenuStripColorTable());
        }
    }

    public class AhoyMenuStripRenderer : ToolStripProfessionalRenderer
    {
        //public ProfessionalColorTable ProfessionalColorTable { get; set; }
        public AhoyMenuStripRenderer(AhoyMenuStripColorTable c) : base(c)
        {
            // Do nothing
        }

        // Fix checkmark
        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemCheck(e);
            using (SolidBrush brush = new SolidBrush(AhoyColors.CurrentColorTheme()[6]))
            {
                e.Graphics.FillRectangle(brush, new Rectangle(3, 1, 20, 20));
            }
            e.Graphics.DrawImage(e.Image, new Point(5, 3));
        }


        // Colorize background of "tagged" items
        private Color tempRenderMenuBackColor;
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            // Colorize on tag info(used by pinned list context menu)
            if (e.Item.Tag != null)
            {
                if (e.Item.Tag.ToString().Length >= 1 && e.Item.Tag.ToString().Length <= 2) // Only render background colors where tag is up to 2 char long
                {
                    switch (e.Item.Tag.ToString())
                    {
                        case "0":
                            tempRenderMenuBackColor = Design.ReturnStatusBackgroundColor(1);
                            break;
                        case "1":
                            tempRenderMenuBackColor = Design.ReturnStatusBackgroundColor(2);
                            break;
                        case "2":
                            tempRenderMenuBackColor = Design.ReturnStatusBackgroundColor(3);
                            break;
                        case "3":
                            tempRenderMenuBackColor = Design.ReturnStatusBackgroundColor(4);
                            break;
                        case "4":
                            tempRenderMenuBackColor = Design.ReturnStatusBackgroundColor(5);
                            break;
                        case "5":
                            tempRenderMenuBackColor = Design.ReturnStatusBackgroundColor(6);
                            break;
                        case "6":
                            tempRenderMenuBackColor = Design.ReturnStatusBackgroundColor(7);
                            break;
                        case "7":
                            tempRenderMenuBackColor = Design.ReturnStatusBackgroundColor(8);
                            break;
                        case "10":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(10);
                            break;
                        case "11":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(11);
                            break;
                        case "12":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(12);
                            break;
                        case "13":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(13);
                            break;
                        case "14":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(14);
                            break;
                        case "15":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(15);
                            break;
                        case "16":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(16);
                            break;
                        case "17":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(17);
                            break;
                        case "18":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(18);
                            break;
                        case "19":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(19);
                            break;
                        case "20":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(20);
                            break;
                        case "21":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(21);
                            break;
                        case "22":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(22);
                            break;
                        case "23":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(23);
                            break;
                        case "24":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(24);
                            break;
                        case "25":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(25);
                            break;
                        case "26":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(26);
                            break;
                        case "27":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(27);
                            break;
                        case "28":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(28);
                            break;
                        case "29":
                            tempRenderMenuBackColor = Design.ReturnCustomBackgroundColor(29);
                            break;
                        case "30":
                            tempRenderMenuBackColor = AhoyColors.CurrentColorTheme()[6];
                            break;
                    }

                    Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                    using (SolidBrush brush = new SolidBrush(tempRenderMenuBackColor))
                        e.Graphics.FillRectangle(brush, rc);
                }
            }

            base.OnRenderMenuItemBackground(e);
        }


        // Color text of "tagged" items
        // Font coloring, style and size...
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {

            // Colorize on tag info (used by pinned list context menu)
            if (e.Item.Tag != null && e.Item.Tag.ToString().Length >= 1 && !e.Item.Selected)
            {
                switch (e.Item.Tag.ToString())
                {
                    case "0":
                        e.TextColor = Design.ReturnStatusForegroundColor(1);
                        break;
                    case "1":
                        e.TextColor = Design.ReturnStatusForegroundColor(2);
                        break;
                    case "2":
                        e.TextColor = Design.ReturnStatusForegroundColor(3);
                        break;
                    case "3":
                        e.TextColor = Design.ReturnStatusForegroundColor(4);
                        break;
                    case "4":
                        e.TextColor = Design.ReturnStatusForegroundColor(5);
                        break;
                    case "5":
                        e.TextColor = Design.ReturnStatusForegroundColor(6);
                        break;
                    case "6":
                        e.TextColor = Design.ReturnStatusForegroundColor(7);
                        break;
                    case "7":
                        e.TextColor = Design.ReturnStatusForegroundColor(8);
                        break;
                    case "10":
                        e.TextColor = Design.ReturnCustomForegroundColor(10);
                        break;
                    case "11":
                        e.TextColor = Design.ReturnCustomForegroundColor(11);
                        break;
                    case "12":
                        e.TextColor = Design.ReturnCustomForegroundColor(12);
                        break;
                    case "13":
                        e.TextColor = Design.ReturnCustomForegroundColor(13);
                        break;
                    case "14":
                        e.TextColor = Design.ReturnCustomForegroundColor(14);
                        break;
                    case "15":
                        e.TextColor = Design.ReturnCustomForegroundColor(15);
                        break;
                    case "16":
                        e.TextColor = Design.ReturnCustomForegroundColor(16);
                        break;
                    case "17":
                        e.TextColor = Design.ReturnCustomForegroundColor(17);
                        break;
                    case "18":
                        e.TextColor = Design.ReturnCustomForegroundColor(18);
                        break;
                    case "19":
                        e.TextColor = Design.ReturnCustomForegroundColor(19);
                        break;
                    case "20":
                        e.TextColor = Design.ReturnCustomForegroundColor(20);
                        break;
                    case "21":
                        e.TextColor = Design.ReturnCustomForegroundColor(21);
                        break;
                    case "22":
                        e.TextColor = Design.ReturnCustomForegroundColor(22);
                        break;
                    case "23":
                        e.TextColor = Design.ReturnCustomForegroundColor(23);
                        break;
                    case "24":
                        e.TextColor = Design.ReturnCustomForegroundColor(24);
                        break;
                    case "25":
                        e.TextColor = Design.ReturnCustomForegroundColor(25);
                        break;
                    case "26":
                        e.TextColor = Design.ReturnCustomForegroundColor(26);
                        break;
                    case "27":
                        e.TextColor = Design.ReturnCustomForegroundColor(27);
                        break;
                    case "28":
                        e.TextColor = Design.ReturnCustomForegroundColor(28);
                        break;
                    case "29":
                        e.TextColor = Design.ReturnCustomForegroundColor(29);
                        break;
                    case "30":
                        e.TextColor = AhoyColors.CurrentColorTheme()[5];
                        break;
                }

                // Set font
                if (e.Item.Tag.ToString() == "bold")
                {
                    e.TextColor = AhoyColors.CurrentColorTheme()[8];
                    e.TextFont = Design.MenuFontBold();
                }
                else if (e.Item.Tag.ToString() == "high")
                {
                    e.TextColor = AhoyColors.CurrentColorTheme()[6];
                    e.TextFont = Design.MenuFont();
                }
                else
                {
                    e.TextFont = Design.MenuFont();
                }
            }
            else if (e.Item.Selected) // Text with mouse over 
            {
                e.TextColor = AhoyColors.CurrentColorTheme()[8];
                if (e.Item.Tag != null && e.Item.Tag.ToString().Length >= 1 && e.Item.Tag.ToString() == "bold")
                {
                    e.TextFont = Design.MenuFontBold();
                }
                else
                {
                    e.TextFont = Design.MenuFont();
                }
            }
            else // Normal text font and color
            {
                e.TextColor = AhoyColors.CurrentColorTheme()[8];
                e.TextFont = Design.MenuFont();
            }
            base.OnRenderItemText(e);
        }


        // Render Arrow
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            var tsMenuItem = e.Item as ToolStripMenuItem;
            if (tsMenuItem != null)
                e.ArrowColor = AhoyColors.CurrentColorTheme()[8];
            base.OnRenderArrow(e);
        }
    }

    public class AhoyMenuStripColorTable : ProfessionalColorTable
    {

        // Menu top click
        public override Color MenuItemPressedGradientBegin
        {
            get
            {
                return AhoyColors.CurrentColorTheme()[7];
            }
        }
        // Menu top click
        public override Color MenuItemPressedGradientEnd
        {
            get
            {
                return AhoyColors.CurrentColorTheme()[7];
            }
        }

        // Boarder
        public override Color MenuBorder
        {
            get
            {
                return AhoyColors.CurrentColorTheme()[0];
            }
        }
        // Separator line 
        public override Color SeparatorDark
        {
            get
            {
                return AhoyColors.CurrentColorTheme()[0];
            }
        }
        // Separator line 
        public override Color SeparatorLight
        {
            get
            {
                return AhoyColors.CurrentColorTheme()[0];
            }
        }


        // Mouse Over color
        public override Color MenuItemSelected
        {
            get
            {
                return AhoyColors.CurrentColorTheme()[10];
            }
        }


        // Menu background (not top)
        public override Color MenuStripGradientBegin
        {
            get
            {
                return AhoyColors.CurrentColorTheme()[7];
            }
        }
        // Menu background (not top)
        public override Color MenuStripGradientEnd
        {
            get
            {
                return AhoyColors.CurrentColorTheme()[7];
            }
        }
        // Menu background (not top)
        public override Color ToolStripDropDownBackground
        {
            get
            {
                return AhoyColors.CurrentColorTheme()[7];
            }
        }
        // Frame around mouse over item
        public override Color MenuItemBorder
        {
            get
            {
                return AhoyColors.CurrentColorTheme()[7];
            }
        }
        public override Color ImageMarginGradientBegin
        {
            get
            {
                return AhoyColors.CurrentColorTheme()[7];
            }
        }

        public override Color ImageMarginGradientMiddle
        {
            get
            {
                return AhoyColors.CurrentColorTheme()[7];
            }
        }

        public override Color ImageMarginGradientEnd
        {
            get
            {
                return AhoyColors.CurrentColorTheme()[7];
            }
        }

        public override Color ImageMarginRevealedGradientEnd
        {
            get
            {
                return AhoyColors.CurrentColorTheme()[7];
            }
        }

        public override Color ImageMarginRevealedGradientMiddle
        {
            get
            {
                return AhoyColors.CurrentColorTheme()[7];
            }
        }

        public override Color ImageMarginRevealedGradientBegin
        {
            get
            {
                return AhoyColors.CurrentColorTheme()[7];
            }
        }


        // Top Menu hover
        public override Color MenuItemSelectedGradientBegin
        {
            get
            {
                return AhoyColors.CurrentColorTheme()[10];
            }
        }

        // Top Menu hover
        public override Color MenuItemSelectedGradientEnd
        {
            get
            {
                return AhoyColors.CurrentColorTheme()[10];
            }
        }


    }
}
