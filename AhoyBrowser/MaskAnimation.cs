using System;

namespace AhoyBrowser
{
    class MaskAnimation
    {
        private static int MaskAnimationCounter;
        private static int MaskAnimationTotalCount = 10; // Set animation length here
        public static string GetMaskString()
        {
            #region Counter
            if (MaskAnimationCounter == MaskAnimationTotalCount)
            {
                MaskAnimationCounter = 1;
            }
            else
            {
                MaskAnimationCounter += 1;
            }
            #endregion

            return Animation(MaskAnimationCounter);
        }

        private static void SetCounter(int index)
        { 
            MaskAnimationCounter = index;
        }

        private static string Animation(int index)
        {
                // The cup animation will runn on index above 100
                switch (index)
                {
                    case 1:
                        return
                        "  ·    " + Environment.NewLine +
                        "  •    " + Environment.NewLine +
                        "  °    " + Environment.NewLine +
                        " °○    " + Environment.NewLine +
                        " zZz   " + Environment.NewLine +
                        "(zZz)  " + Environment.NewLine +
                        " )zz)  " + Environment.NewLine +
                        " (z(   " + Environment.NewLine +
                        "  ))   " + Environment.NewLine +
                        " ((    " + Environment.NewLine +
                        "█████▄ " + Environment.NewLine +
                        "█████▄▌" + Environment.NewLine +
                        "▀███▀  ";
                    case 2:
                        return
                        "  •    " + Environment.NewLine +
                        "  °    " + Environment.NewLine +
                        "  ○    " + Environment.NewLine +
                        "       " + Environment.NewLine +
                        "  Z    " + Environment.NewLine +
                        " zzz)  " + Environment.NewLine +
                        "(zz(   " + Environment.NewLine +
                        " )z)   " + Environment.NewLine +
                        " ((    " + Environment.NewLine +
                        "  ))   " + Environment.NewLine +
                        "█████▄ " + Environment.NewLine +
                        "█████▄▌" + Environment.NewLine +
                        "▀███▀  ";
                    case 3:
                        return
                        "       " + Environment.NewLine +
                        "  •    " + Environment.NewLine +
                        "       " + Environment.NewLine +
                        "  ○    " + Environment.NewLine +
                        "(zZz   " + Environment.NewLine +
                        " zzz)  " + Environment.NewLine +
                        " )zz)  " + Environment.NewLine +
                        " (Z(   " + Environment.NewLine +
                        "  ))   " + Environment.NewLine +
                        " ((    " + Environment.NewLine +
                        "█████▄ " + Environment.NewLine +
                        "█████▄▌" + Environment.NewLine +
                        "▀███▀  ";
                    case 4:
                        return
                        "  ·    " + Environment.NewLine +
                        "       " + Environment.NewLine +
                        "  °    " + Environment.NewLine +
                        " z     " + Environment.NewLine +
                        " Zzz)  " + Environment.NewLine +
                        "(zzz   " + Environment.NewLine +
                        "(ZZ(   " + Environment.NewLine +
                        " )z)   " + Environment.NewLine +
                        " ((    " + Environment.NewLine +
                        "  ))   " + Environment.NewLine +
                        "█████▄ " + Environment.NewLine +
                        "█████▄▌" + Environment.NewLine +
                        "▀███▀  ";
                    case 5:
                        return
                        "       " + Environment.NewLine +
                        "  •    " + Environment.NewLine +
                        " Z     " + Environment.NewLine +
                        "  ○    " + Environment.NewLine +
                        "(zZz   " + Environment.NewLine +
                        " ZZZ)  " + Environment.NewLine +
                        " )zz)  " + Environment.NewLine +
                        " (z(   " + Environment.NewLine +
                        "  ))   " + Environment.NewLine +
                        " ((    " + Environment.NewLine +
                        "█████▄ " + Environment.NewLine +
                        "█████▄▌" + Environment.NewLine +
                        "▀███▀  ";
                    case 6:
                        return
                        "  ·    " + Environment.NewLine +
                        " z     " + Environment.NewLine +
                        "  °    " + Environment.NewLine +
                        "   z   " + Environment.NewLine +
                        " zZZ)  " + Environment.NewLine +
                        "(zzz   " + Environment.NewLine +
                        "(zz(   " + Environment.NewLine +
                        " ) )   " + Environment.NewLine +
                        " ((    " + Environment.NewLine +
                        "  ))   " + Environment.NewLine +
                        "█████▄ " + Environment.NewLine +
                        "█████▄▌" + Environment.NewLine +
                        "▀███▀  ";
                    case 7:
                        return
                        "       " + Environment.NewLine +
                        "  •    " + Environment.NewLine +
                        "   ·   " + Environment.NewLine +
                        "       " + Environment.NewLine +
                        "  z    " + Environment.NewLine +
                        " (z    " + Environment.NewLine +
                        "  Z)   " + Environment.NewLine +
                        " (z    " + Environment.NewLine +
                        "  ))   " + Environment.NewLine +
                        "  (    " + Environment.NewLine +
                        "█████▄ " + Environment.NewLine +
                        "█████▄▌" + Environment.NewLine +
                        "▀███▀  ";
                    case 8:
                        return
                        "  ·    " + Environment.NewLine +
                        "       " + Environment.NewLine +
                        "       " + Environment.NewLine +
                        "       " + Environment.NewLine +
                        "       " + Environment.NewLine +
                        "   )   " + Environment.NewLine +
                        "  (    " + Environment.NewLine +
                        "  ))   " + Environment.NewLine +
                        "  (    " + Environment.NewLine +
                        "   )   " + Environment.NewLine +
                        "█████▄ " + Environment.NewLine +
                        "█████▄▌" + Environment.NewLine +
                        "▀███▀  ";
                    case 9:
                        return
                        "       " + Environment.NewLine +
                        "       " + Environment.NewLine +
                        "       " + Environment.NewLine +
                        "       " + Environment.NewLine +
                        "       " + Environment.NewLine +
                        "  (    " + Environment.NewLine +
                        "  ))   " + Environment.NewLine +
                        " (z(   " + Environment.NewLine +
                        " )))   " + Environment.NewLine +
                        " ((    " + Environment.NewLine +
                        "█████▄ " + Environment.NewLine +
                        "█████▄▌" + Environment.NewLine +
                        "▀███▀  ";
                    case 10:
                        // Reset counter to keep displaying cup animation
                        SetCounter(0);
                        return
                        "       " + Environment.NewLine +
                        "       " + Environment.NewLine +
                        "       " + Environment.NewLine +
                        "  z    " + Environment.NewLine +
                        " (Zz   " + Environment.NewLine +
                        " )zz)  " + Environment.NewLine +
                        "(ZZ(   " + Environment.NewLine +
                        " )z)   " + Environment.NewLine +
                        " ((    " + Environment.NewLine +
                        "  ))   " + Environment.NewLine +
                        "█████▄ " + Environment.NewLine +
                        "█████▄▌" + Environment.NewLine +
                        "▀███▀  ";
                    default:
                        return "";
                }
        }

        public static void ResetAnimation()
        {
            MaskAnimationCounter = 0;
        }
    }
}
