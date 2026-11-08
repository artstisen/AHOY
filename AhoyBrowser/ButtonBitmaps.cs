using System.Drawing;

namespace AhoyBrowser
{
    class ButtonBitmaps
    {
        // This class will supply the correct button bitmaps
        // based on the current ButtonBitmapVersion
        public static int ButtonBitmapVersion { get; set; }
        public static int UserSelectedBitmapVersion { get; set; }

        #region SLEEP BUTTON
        public static Bitmap Sleep()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.sleep;
                    case 1:
                        return Properties.Resources.sleep2;
                    case 2:
                        return Properties.Resources.sleep3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.sleep;
                    case 2:
                        return Properties.Resources.sleep2;
                    case 3:
                        return Properties.Resources.sleep3;
                    default:
                        return null;
                }
            }
        }


        public static Bitmap Gear()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.gear1;
                    case 1:
                        return Properties.Resources.gear2;
                    case 2:
                        return Properties.Resources.gear3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.gear1;
                    case 2:
                        return Properties.Resources.gear2;
                    case 3:
                        return Properties.Resources.gear3;
                    default:
                        return null;
                }
            }
        }


        public static Bitmap Adjust()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.adjust1;
                    case 1:
                        return Properties.Resources.adjust2;
                    case 2:
                        return Properties.Resources.adjust3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.adjust1;
                    case 2:
                        return Properties.Resources.adjust2;
                    case 3:
                        return Properties.Resources.adjust3;
                    default:
                        return null;
                }
            }
        }


        public static Bitmap SleepHighlighted()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.sleephighlight;
                    case 1:
                        return Properties.Resources.sleephighlight2;
                    case 2:
                        return Properties.Resources.sleephighlight3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.sleephighlight;
                    case 2:
                        return Properties.Resources.sleephighlight2;
                    case 3:
                        return Properties.Resources.sleephighlight3;
                    default:
                        return null;
                }
            }
        }
        #endregion


        #region PINLIST BUTTONS
        public static Bitmap Pinlist()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.pinlist;
                    case 1:
                        return Properties.Resources.pinlist2;
                    case 2:
                        return Properties.Resources.pinlist3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.pinlist;
                    case 2:
                        return Properties.Resources.pinlist2;
                    case 3:
                        return Properties.Resources.pinlist3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap PinlistActive()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.pinlistactive;
                    case 1:
                        return Properties.Resources.pinlistactive2;
                    case 2:
                        return Properties.Resources.pinlistactive3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.pinlistactive;
                    case 2:
                        return Properties.Resources.pinlistactive2;
                    case 3:
                        return Properties.Resources.pinlistactive3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap PinlistHighlight()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.pinlisthighlight;
                    case 1:
                        return Properties.Resources.pinlisthighlight2;
                    case 2:
                        return Properties.Resources.pinlisthighlight3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.pinlisthighlight;
                    case 2:
                        return Properties.Resources.pinlisthighlight2;
                    case 3:
                        return Properties.Resources.pinlisthighlight3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap UpHighlight()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.uphighlight;
                    case 1:
                        return Properties.Resources.uphighlight2;
                    case 2:
                        return Properties.Resources.uphighlight3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.uphighlight;
                    case 2:
                        return Properties.Resources.uphighlight2;
                    case 3:
                        return Properties.Resources.uphighlight3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap DownHighlight()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.downhighlight;
                    case 1:
                        return Properties.Resources.downhighlight2;
                    case 2:
                        return Properties.Resources.downhighlight3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.downhighlight;
                    case 2:
                        return Properties.Resources.downhighlight2;
                    case 3:
                        return Properties.Resources.downhighlight3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap TopHighlight()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.tophighlight;
                    case 1:
                        return Properties.Resources.tophighlight2;
                    case 2:
                        return Properties.Resources.tophighlight3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.tophighlight;
                    case 2:
                        return Properties.Resources.tophighlight2;
                    case 3:
                        return Properties.Resources.tophighlight3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap BottomHighlight()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.bottomhighlight;
                    case 1:
                        return Properties.Resources.bottomhighlight2;
                    case 2:
                        return Properties.Resources.bottomhighlight3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.bottomhighlight;
                    case 2:
                        return Properties.Resources.bottomhighlight2;
                    case 3:
                        return Properties.Resources.bottomhighlight3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap Up()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.up;
                    case 1:
                        return Properties.Resources.up2;
                    case 2:
                        return Properties.Resources.up3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.up;
                    case 2:
                        return Properties.Resources.up2;
                    case 3:
                        return Properties.Resources.up3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap Down()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.down;
                    case 1:
                        return Properties.Resources.down2;
                    case 2:
                        return Properties.Resources.down3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.down;
                    case 2:
                        return Properties.Resources.down2;
                    case 3:
                        return Properties.Resources.down3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap Top()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.top;
                    case 1:
                        return Properties.Resources.top2;
                    case 2:
                        return Properties.Resources.top3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.top;
                    case 2:
                        return Properties.Resources.top2;
                    case 3:
                        return Properties.Resources.top3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap Bottom()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.bottom;
                    case 1:
                        return Properties.Resources.bottom2;
                    case 2:
                        return Properties.Resources.bottom3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.bottom;
                    case 2:
                        return Properties.Resources.bottom2;
                    case 3:
                        return Properties.Resources.bottom3;
                    default:
                        return null;
                }
            }
        }

        public static Bitmap Plus()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.plus1;
                    case 1:
                        return Properties.Resources.plus2;
                    case 2:
                        return Properties.Resources.plus3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.plus1;
                    case 2:
                        return Properties.Resources.plus2;
                    case 3:
                        return Properties.Resources.plus3;
                    default:
                        return null;
                }
            }
        }
        #endregion

        #region MEMO BUTTONS
        public static Bitmap MemoHighlight()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.memohighlight;
                    case 1:
                        return Properties.Resources.memohighlight2;
                    case 2:
                        return Properties.Resources.memohighlight3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.memohighlight;
                    case 2:
                        return Properties.Resources.memohighlight2;
                    case 3:
                        return Properties.Resources.memohighlight3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap MemoActive()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.memoactive;
                    case 1:
                        return Properties.Resources.memoactive2;
                    case 2:
                        return Properties.Resources.memoactive3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.memoactive;
                    case 2:
                        return Properties.Resources.memoactive2;
                    case 3:
                        return Properties.Resources.memoactive3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap Memo()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.memo;
                    case 1:
                        return Properties.Resources.memo2;
                    case 2:
                        return Properties.Resources.memo3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.memo;
                    case 2:
                        return Properties.Resources.memo2;
                    case 3:
                        return Properties.Resources.memo3;
                    default:
                        return null;
                }
            }
        }
        #endregion


        #region NAVIGATION BUTTONS
        public static Bitmap LeftHighlight()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.lefthighlight;
                    case 1:
                        return Properties.Resources.lefthighlight2;
                    case 2:
                        return Properties.Resources.lefthighlight3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.lefthighlight;
                    case 2:
                        return Properties.Resources.lefthighlight2;
                    case 3:
                        return Properties.Resources.lefthighlight3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap Left()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.left;
                    case 1:
                        return Properties.Resources.left2;
                    case 2:
                        return Properties.Resources.left3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.left;
                    case 2:
                        return Properties.Resources.left2;
                    case 3:
                        return Properties.Resources.left3;
                    default:
                        return null;
                }
            }
        }

        // Used for preview
        public static Bitmap Left(int index)
        {
            switch (index)
            {
                case 0:
                    return Left();
                case 1:
                    return Properties.Resources.left;
                case 2:
                    return Properties.Resources.left2;
                case 3:
                    return Properties.Resources.left3;
                default:
                    return null;
            }
        }

        public static Bitmap RightHighlight()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.righthighlight;
                    case 1:
                        return Properties.Resources.righthighlight2;
                    case 2:
                        return Properties.Resources.righthighlight3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.righthighlight;
                    case 2:
                        return Properties.Resources.righthighlight2;
                    case 3:
                        return Properties.Resources.righthighlight3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap Right()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.right;
                    case 1:
                        return Properties.Resources.right2;
                    case 2:
                        return Properties.Resources.right3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.right;
                    case 2:
                        return Properties.Resources.right2;
                    case 3:
                        return Properties.Resources.right3;
                    default:
                        return null;
                }
            }
        }

        // Used for preview
        public static Bitmap Right(int index)
        {
            switch (index)
            {
                case 0:
                    return Right();
                case 1:
                    return Properties.Resources.right;
                case 2:
                    return Properties.Resources.right2;
                case 3:
                    return Properties.Resources.right3;
                default:
                    return null;
            }
        }
        #endregion


        #region CLEAR BUTTON
        public static Bitmap Clear(bool highlight)
        {
            if (highlight)
            {
                return Properties.Resources.clear2;
            }

            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.clear;
                    case 1:
                        return Properties.Resources.clear2;
                    case 2:
                        return Properties.Resources.clear31;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.clear;
                    case 2:
                        return Properties.Resources.clear2;
                    case 3:
                        return Properties.Resources.clear31;
                    default:
                        return null;
                }
            }
        }

        // Used for preview
        public static Bitmap Clear(int index)
        {
            switch (index)
            {
                case 0:
                    return Clear(false);
                case 1:
                    return Properties.Resources.clear;
                case 2:
                    return Properties.Resources.clear2;
                case 3:
                    return Properties.Resources.clear31;
                default:
                    return null;
            }
        }

        public static Bitmap ClearHighlight()
        {
            return Properties.Resources.clear2;
        }

        public static Bitmap Clear()
        {
            return Properties.Resources.clear;
        }

        public static Bitmap ClearGold()
        {
            return Properties.Resources.clear3;
        }
        #endregion

        #region TYPEWRITER BUTTON
        #region Local typewriter bitmaps
        #endregion
        #endregion

        #region STICKY BUTTONS
        public static Bitmap Sticky(bool highlight)
        {
            if (highlight)
            {
                return Properties.Resources.sticky2;
            }

            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.sticky1;
                    case 1:
                        return Properties.Resources.sticky2;
                    case 2:
                        return Properties.Resources.sticky3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.sticky1;
                    case 2:
                        return Properties.Resources.sticky2;
                    case 3:
                        return Properties.Resources.sticky3;
                    default:
                        return null;
                }
            }
        }

        public static Bitmap Sticky()
        {
            return Properties.Resources.sticky1;
        }


        public static Bitmap OpenEye(bool highlight)
        {
            if (highlight)
            {
                return Properties.Resources.hide2;
            }

            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.hide1;
                    case 1:
                        return Properties.Resources.hide2;
                    case 2:
                        return Properties.Resources.hide7;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.hide1;
                    case 2:
                        return Properties.Resources.hide2;
                    case 3:
                        return Properties.Resources.hide7;
                    default:
                        return null;
                }
            }
        }

        public static Bitmap OpenEye()
        {
            return Properties.Resources.hide1;
        }






        public static Bitmap HalfOpenEye(bool highlight)
        {
            if (highlight)
            {
                return Properties.Resources.hide4;
            }

            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.hide3;
                    case 1:
                        return Properties.Resources.hide4;
                    case 2:
                        return Properties.Resources.hide8;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.hide3;
                    case 2:
                        return Properties.Resources.hide4;
                    case 3:
                        return Properties.Resources.hide8;
                    default:
                        return null;
                }
            }
        }

        public static Bitmap HalfOpenEye()
        {
            return Properties.Resources.hide3;
        }



        public static Bitmap ShiningEye(bool highlight)
        {
            if (highlight)
            {
                return Properties.Resources.hide6;
            }

            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.hide5;
                    case 1:
                        return Properties.Resources.hide6;
                    case 2:
                        return Properties.Resources.hide9;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.hide5;
                    case 2:
                        return Properties.Resources.hide6;
                    case 3:
                        return Properties.Resources.hide9;
                    default:
                        return null;
                }
            }
        }

        public static Bitmap ShiningEye()
        {
            return Properties.Resources.hide5;
        }





        public static Bitmap Move(bool highlight)
        {
            if (highlight)
            {
                return Properties.Resources.move2;
            }

            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.move1;
                    case 1:
                        return Properties.Resources.move2;
                    case 2:
                        return Properties.Resources.move3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.move1;
                    case 2:
                        return Properties.Resources.move2;
                    case 3:
                        return Properties.Resources.move3;
                    default:
                        return null;
                }
            }

        }

        public static Bitmap Move()
        {
            return Properties.Resources.move1;
        }


        public static Bitmap Resize(bool highlight)
        {
            if (highlight)
            {
                return Properties.Resources.resize2;
            }


            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.resize1;
                    case 1:
                        return Properties.Resources.resize2;
                    case 2:
                        return Properties.Resources.resize3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.resize1;
                    case 2:
                        return Properties.Resources.resize2;
                    case 3:
                        return Properties.Resources.resize3;
                    default:
                        return null;
                }
            }
        }

        public static Bitmap Resize()
        {
            return Properties.Resources.resize1;
        }

        public static Bitmap Theme(bool highlight)
        {
            if (highlight)
            {
                return Properties.Resources.brush2;
            }

            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.brush1;
                    case 1:
                        return Properties.Resources.brush2;
                    case 2:
                        return Properties.Resources.brush3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.brush1;
                    case 2:
                        return Properties.Resources.brush2;
                    case 3:
                        return Properties.Resources.brush3;
                    default:
                        return null;
                }
            }
        }

        public static Bitmap Theme()
        {
            return Properties.Resources.brush1;
        }
        #endregion

        #region NOTIFICATION BUTTONS
        public static Bitmap GetTitle()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.gtitle;
                    case 1:
                        return Properties.Resources.gtitle2;
                    case 2:
                        return Properties.Resources.gtitle3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.gtitle;
                    case 2:
                        return Properties.Resources.gtitle2;
                    case 3:
                        return Properties.Resources.gtitle3;
                    default:
                        return null;
                }
            }
        }

        public static Bitmap GetContent()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.gcontent;
                    case 1:
                        return Properties.Resources.gcontent2;
                    case 2:
                        return Properties.Resources.gcontent3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.gcontent;
                    case 2:
                        return Properties.Resources.gcontent2;
                    case 3:
                        return Properties.Resources.gcontent3;
                    default:
                        return null;
                }
            }
        }

        public static Bitmap GetScrollToCaretOff()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.scrolloff1;
                    case 1:
                        return Properties.Resources.scrolloff2;
                    case 2:
                        return Properties.Resources.scrolloff3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.scrolloff1;
                    case 2:
                        return Properties.Resources.scrolloff2;
                    case 3:
                        return Properties.Resources.scrolloff3;
                    default:
                        return null;
                }
            }
        }

        public static Bitmap GetScrollToCaretOn()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.scrollon1;
                    case 1:
                        return Properties.Resources.scrollon2;
                    case 2:
                        return Properties.Resources.scrollon3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.scrollon1;
                    case 2:
                        return Properties.Resources.scrollon2;
                    case 3:
                        return Properties.Resources.scrollon3;
                    default:
                        return null;
                }
            }
        }
        #endregion

        public static Bitmap HelpButtonHighlight()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.help;
                    case 1:
                        return Properties.Resources.help2;
                    case 2:
                        return Properties.Resources.help3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.help;
                    case 2:
                        return Properties.Resources.help2;
                    case 3:
                        return Properties.Resources.help3;
                    default:
                        return null;
                }
            }
        }

        #region OTHER BUTTONS
        public static Bitmap Pinned()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.pinned;
                    case 1:
                        return Properties.Resources.pinned2;
                    case 2:
                        return Properties.Resources.pinned3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.pinned;
                    case 2:
                        return Properties.Resources.pinned2;
                    case 3:
                        return Properties.Resources.pinned3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap Unpinned()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.unpinned;
                    case 1:
                        return Properties.Resources.unpinned2;
                    case 2:
                        return Properties.Resources.unpinned3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.unpinned;
                    case 2:
                        return Properties.Resources.unpinned2;
                    case 3:
                        return Properties.Resources.unpinned3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap Locked()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.locked;
                    case 1:
                        return Properties.Resources.locked2;
                    case 2:
                        return Properties.Resources.locked3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.locked;
                    case 2:
                        return Properties.Resources.locked2;
                    case 3:
                        return Properties.Resources.locked3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap Unlocked()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.unlocked;
                    case 1:
                        return Properties.Resources.unlocked2;
                    case 2:
                        return Properties.Resources.unlocked3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.unlocked;
                    case 2:
                        return Properties.Resources.unlocked2;
                    case 3:
                        return Properties.Resources.unlocked3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap NoBookmark()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.bookmark;
                    case 1:
                        return Properties.Resources.bookmark2;
                    case 2:
                        return Properties.Resources.bookmark3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.bookmark;
                    case 2:
                        return Properties.Resources.bookmark2;
                    case 3:
                        return Properties.Resources.bookmark3;
                    default:
                        return null;
                }
            }
        }
        public static Bitmap BookmarkSet()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.bookmarkhighlight;
                    case 1:
                        return Properties.Resources.bookmarkhighlight2;
                    case 2:
                        return Properties.Resources.bookmarkhighlight3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.bookmarkhighlight;
                    case 2:
                        return Properties.Resources.bookmarkhighlight2;
                    case 3:
                        return Properties.Resources.bookmarkhighlight3;
                    default:
                        return null;
                }
            }
        }

        // Used by the colorpicker
        public static Bitmap Flip()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.flip1;
                    case 1:
                        return Properties.Resources.flip2;
                    case 2:
                        return Properties.Resources.flip3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.flip1;
                    case 2:
                        return Properties.Resources.flip2;
                    case 3:
                        return Properties.Resources.flip3;
                    default:
                        return null;
                }
            }
        }

        public static Bitmap Refresh()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.refresh1;
                    case 1:
                        return Properties.Resources.refresh2;
                    case 2:
                        return Properties.Resources.refresh3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.refresh1;
                    case 2:
                        return Properties.Resources.refresh2;
                    case 3:
                        return Properties.Resources.refresh3;
                    default:
                        return null;
                }
            }
        }
        #endregion


        #region Plus/Minus buttons
        public static Bitmap Expand()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.expand1;
                    case 1:
                        return Properties.Resources.expand2;
                    case 2:
                        return Properties.Resources.expand3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.expand1;
                    case 2:
                        return Properties.Resources.expand2;
                    case 3:
                        return Properties.Resources.expand3;
                    default:
                        return null;
                }
            }
        }

        public static Bitmap Collapse()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.collapse1;
                    case 1:
                        return Properties.Resources.collapse2;
                    case 2:
                        return Properties.Resources.collapse3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.collapse1;
                    case 2:
                        return Properties.Resources.collapse2;
                    case 3:
                        return Properties.Resources.collapse3;
                    default:
                        return null;
                }
            }
        }
        #endregion

        #region Repeat button
        public static Bitmap Repeat()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return Properties.Resources.repeat1;
                    case 1:
                        return Properties.Resources.repeat2;
                    case 2:
                        return Properties.Resources.repeat3;
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return Properties.Resources.repeat1;
                    case 2:
                        return Properties.Resources.repeat2;
                    case 3:
                        return Properties.Resources.repeat3;
                    default:
                        return null;
                }
            }
        }
        #endregion


    }
}
