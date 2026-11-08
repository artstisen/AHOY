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
                        return ResourceCache.Get(nameof(Properties.Resources.sleep));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.sleep2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.sleep3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.sleep));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.sleep2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.sleep3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.adjust1));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.adjust2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.adjust3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.adjust1));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.adjust2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.adjust3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.sleephighlight));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.sleephighlight2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.sleephighlight3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.sleephighlight));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.sleephighlight2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.sleephighlight3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.pinlist));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.pinlist2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.pinlist3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.pinlist));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.pinlist2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.pinlist3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.pinlistactive));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.pinlistactive2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.pinlistactive3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.pinlistactive));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.pinlistactive2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.pinlistactive3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.pinlisthighlight));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.pinlisthighlight2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.pinlisthighlight3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.pinlisthighlight));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.pinlisthighlight2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.pinlisthighlight3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.uphighlight));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.uphighlight2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.uphighlight3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.uphighlight));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.uphighlight2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.uphighlight3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.downhighlight));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.downhighlight2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.downhighlight3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.downhighlight));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.downhighlight2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.downhighlight3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.tophighlight));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.tophighlight2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.tophighlight3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.tophighlight));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.tophighlight2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.tophighlight3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.bottomhighlight));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.bottomhighlight2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.bottomhighlight3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.bottomhighlight));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.bottomhighlight2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.bottomhighlight3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.up));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.up2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.up3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.up));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.up2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.up3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.down));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.down2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.down3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.down));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.down2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.down3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.top));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.top2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.top3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.top));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.top2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.top3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.bottom));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.bottom2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.bottom3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.bottom));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.bottom2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.bottom3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.plus1));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.plus2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.plus3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.plus1));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.plus2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.plus3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.memohighlight));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.memohighlight2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.memohighlight3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.memohighlight));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.memohighlight2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.memohighlight3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.memoactive));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.memoactive2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.memoactive3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.memoactive));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.memoactive2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.memoactive3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.memo));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.memo2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.memo3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.memo));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.memo2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.memo3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.lefthighlight));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.lefthighlight2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.lefthighlight3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.lefthighlight));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.lefthighlight2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.lefthighlight3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.left));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.left2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.left3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.left));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.left2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.left3));
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
                    return ResourceCache.Get(nameof(Properties.Resources.left));
                case 2:
                    return ResourceCache.Get(nameof(Properties.Resources.left2));
                case 3:
                    return ResourceCache.Get(nameof(Properties.Resources.left3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.righthighlight));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.righthighlight2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.righthighlight3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.righthighlight));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.righthighlight2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.righthighlight3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.right));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.right2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.right3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.right));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.right2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.right3));
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
                    return ResourceCache.Get(nameof(Properties.Resources.right));
                case 2:
                    return ResourceCache.Get(nameof(Properties.Resources.right2));
                case 3:
                    return ResourceCache.Get(nameof(Properties.Resources.right3));
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
                return ResourceCache.Get(nameof(Properties.Resources.clear2));
            }

            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return ResourceCache.Get(nameof(Properties.Resources.clear));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.clear2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.clear31));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.clear));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.clear2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.clear31));
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
                    return ResourceCache.Get(nameof(Properties.Resources.clear));
                case 2:
                    return ResourceCache.Get(nameof(Properties.Resources.clear2));
                case 3:
                    return ResourceCache.Get(nameof(Properties.Resources.clear31));
                default:
                    return null;
            }
        }
        public static Bitmap Clear()
        {
            return ResourceCache.Get(nameof(Properties.Resources.clear));
        }
        #endregion

        #region TYPEWRITER BUTTON
        #endregion

        #region STICKY BUTTONS
        public static Bitmap Sticky(bool highlight)
        {
            if (highlight)
            {
                return ResourceCache.Get(nameof(Properties.Resources.sticky2));
            }

            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return ResourceCache.Get(nameof(Properties.Resources.sticky1));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.sticky2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.sticky3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.sticky1));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.sticky2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.sticky3));
                    default:
                        return null;
                }
            }
        }

        public static Bitmap Sticky()
        {
            return ResourceCache.Get(nameof(Properties.Resources.sticky1));
        }


        public static Bitmap OpenEye(bool highlight)
        {
            if (highlight)
            {
                return ResourceCache.Get(nameof(Properties.Resources.hide2));
            }

            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return ResourceCache.Get(nameof(Properties.Resources.hide1));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.hide2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.hide7));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.hide1));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.hide2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.hide7));
                    default:
                        return null;
                }
            }
        }

        public static Bitmap OpenEye()
        {
            return ResourceCache.Get(nameof(Properties.Resources.hide1));
        }


        public static Bitmap HalfOpenEye(bool highlight)
        {
            if (highlight)
            {
                return ResourceCache.Get(nameof(Properties.Resources.hide4));
            }

            if (UserSelectedBitmapVersion == 0)
            {
                // Use default bitmap
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return ResourceCache.Get(nameof(Properties.Resources.hide3));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.hide4));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.hide8));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.hide3));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.hide4));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.hide8));
                    default:
                        return null;
                }
            }
        }

        public static Bitmap HalfOpenEye()
        {
            return ResourceCache.Get(nameof(Properties.Resources.hide3));
        }


        public static Bitmap ShiningEye(bool highlight)
        {
            if (highlight)
            {
                return ResourceCache.Get(nameof(Properties.Resources.hide6));
            }

            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return ResourceCache.Get(nameof(Properties.Resources.hide5));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.hide6));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.hide9));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.hide5));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.hide6));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.hide9));
                    default:
                        return null;
                }
            }
        }

        public static Bitmap ShiningEye()
        {
            return ResourceCache.Get(nameof(Properties.Resources.hide5));
        }


        public static Bitmap Move(bool highlight)
        {
            if (highlight)
            {
                return ResourceCache.Get(nameof(Properties.Resources.move2));
            }

            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return ResourceCache.Get(nameof(Properties.Resources.move1));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.move2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.move3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.move1));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.move2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.move3));
                    default:
                        return null;
                }
            }

        }

        public static Bitmap Move()
        {
            return ResourceCache.Get(nameof(Properties.Resources.move1));
        }


        public static Bitmap Resize(bool highlight)
        {
            if (highlight)
            {
                return ResourceCache.Get(nameof(Properties.Resources.resize2));
            }


            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return ResourceCache.Get(nameof(Properties.Resources.resize1));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.resize2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.resize3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.resize1));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.resize2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.resize3));
                    default:
                        return null;
                }
            }
        }

        public static Bitmap Resize()
        {
            return ResourceCache.Get(nameof(Properties.Resources.resize1));
        }

        public static Bitmap Theme(bool highlight)
        {
            if (highlight)
            {
                return ResourceCache.Get(nameof(Properties.Resources.brush2));
            }

            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return ResourceCache.Get(nameof(Properties.Resources.brush1));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.brush2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.brush3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.brush1));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.brush2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.brush3));
                    default:
                        return null;
                }
            }
        }

        public static Bitmap Theme()
        {
            return ResourceCache.Get(nameof(Properties.Resources.brush1));
        }
        #endregion


        public static Bitmap HelpButtonHighlight()
        {
            if (UserSelectedBitmapVersion == 0)
            {
                switch (ButtonBitmapVersion)
                {
                    case 0:
                        return ResourceCache.Get(nameof(Properties.Resources.help));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.help2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.help3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.help));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.help2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.help3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.pinned));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.pinned2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.pinned3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.pinned));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.pinned2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.pinned3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.unpinned));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.unpinned2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.unpinned3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.unpinned));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.unpinned2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.unpinned3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.locked));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.locked2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.locked3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.locked));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.locked2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.locked3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.unlocked));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.unlocked2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.unlocked3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.unlocked));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.unlocked2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.unlocked3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.bookmark));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.bookmark2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.bookmark3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.bookmark));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.bookmark2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.bookmark3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.bookmarkhighlight));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.bookmarkhighlight2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.bookmarkhighlight3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.bookmarkhighlight));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.bookmarkhighlight2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.bookmarkhighlight3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.flip1));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.flip2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.flip3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.flip1));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.flip2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.flip3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.refresh1));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.refresh2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.refresh3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.refresh1));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.refresh2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.refresh3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.expand1));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.expand2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.expand3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.expand1));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.expand2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.expand3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.collapse1));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.collapse2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.collapse3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.collapse1));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.collapse2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.collapse3));
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
                        return ResourceCache.Get(nameof(Properties.Resources.repeat1));
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.repeat2));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.repeat3));
                    default:
                        return null;
                }
            }
            else
            {
                switch (UserSelectedBitmapVersion)
                {
                    case 1:
                        return ResourceCache.Get(nameof(Properties.Resources.repeat1));
                    case 2:
                        return ResourceCache.Get(nameof(Properties.Resources.repeat2));
                    case 3:
                        return ResourceCache.Get(nameof(Properties.Resources.repeat3));
                    default:
                        return null;
                }
            }
        }
        #endregion


    }
}
