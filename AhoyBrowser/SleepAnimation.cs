using System;

namespace AhoyBrowser
{

    class SleepAnimation
    {
        private static int SleepAnimationCounter;
        private static int SleepAnimationTotalCount = 17; // Set animation length here
        public static string GetSleepString()
        {
            #region Counter
            if (SleepAnimationCounter == SleepAnimationTotalCount)
            {
                SleepAnimationCounter = 1;
            }
            else
            {
                SleepAnimationCounter += 1;
            }
            #endregion

            return Animation(SleepAnimationCounter);
        }

        private static string Animation(int index)
        {
            switch (index)
            {
                case 1:
                    return
                    "       " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "├───═─┤" + Environment.NewLine +
                    "│ zZz │" + Environment.NewLine +
                    "└─────┘";
                case 2:
                    return
                    "       " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "    ·  " + Environment.NewLine +
                    "├─────┤" + Environment.NewLine +
                    "│ ZzZ │" + Environment.NewLine +
                    "└─────┘";
                case 3:
                    return
                    "       " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "    •  " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "├─────┤" + Environment.NewLine +
                    "│ zZz │" + Environment.NewLine +
                    "└─────┘";
                case 4:
                    return
                    "       " + Environment.NewLine +
                    "    °  " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "├─────┤" + Environment.NewLine +
                    "│ ZzZ │" + Environment.NewLine +
                    "└─────┘";
                case 5:
                    return
                    "    ○  " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "├═────┤" + Environment.NewLine +
                    "│ zZz │" + Environment.NewLine +
                    "└─────┘";
                case 6:
                    return
                    "       " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    " ·     " + Environment.NewLine +
                    "├─────┤" + Environment.NewLine +
                    "│ ZzZ │" + Environment.NewLine +
                    "└─────┘";
                case 7:
                    return
                    "       " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    " •     " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "├───═─┤" + Environment.NewLine +
                    "│ zZz │" + Environment.NewLine +
                    "└─────┘";
                case 8:
                    return
                    "       " + Environment.NewLine +
                    " °     " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "    ·  " + Environment.NewLine +
                    "├─────┤" + Environment.NewLine +
                    "│ ZzZ │" + Environment.NewLine +
                    "└─────┘";
                case 9:
                    return
                    " ○     " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "    •  " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "├─═───┤" + Environment.NewLine +
                    "│ zZz │" + Environment.NewLine +
                    "└─────┘";
                case 10:
                    return
                    "       " + Environment.NewLine +
                    "    °  " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "  ·    " + Environment.NewLine +
                    "├─────┤" + Environment.NewLine +
                    "│ ZzZ │" + Environment.NewLine +
                    "└─────┘";
                case 11:
                    return
                    "    ○  " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "  •    " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "├────═┤" + Environment.NewLine +
                    "│ zZz │" + Environment.NewLine +
                    "└─────┘";
                case 12:
                    return
                    "       " + Environment.NewLine +
                    "  °    " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "     · " + Environment.NewLine +
                    "├─────┤" + Environment.NewLine +
                    "│ ZzZ │" + Environment.NewLine +
                    "└─────┘";
                case 13:
                    return
                    "  ○    " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "     • " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "├──═──┤" + Environment.NewLine +
                    "│ zZz │" + Environment.NewLine +
                    "└─────┘";
                case 14:
                    return
                    "       " + Environment.NewLine +
                    "     ° " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "   ·   " + Environment.NewLine +
                    "├─────┤" + Environment.NewLine +
                    "│ ZzZ │" + Environment.NewLine +
                    "└─────┘";
                case 15:
                    return
                    "     ○ " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "   •   " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "├─────┤" + Environment.NewLine +
                    "│ zZz │" + Environment.NewLine +
                    "└─────┘";
                case 16:
                    return
                    "       " + Environment.NewLine +
                    "   °   " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "├─────┤" + Environment.NewLine +
                    "│ ZzZ │" + Environment.NewLine +
                    "└─────┘";
                case 17:
                    return
                    "   ○   " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "       " + Environment.NewLine +
                    "├─────┤" + Environment.NewLine +
                    "│ zzz │" + Environment.NewLine +
                    "└─────┘";
                default:
                    return "";
            }
        }
    }
}
