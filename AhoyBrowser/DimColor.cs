using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AhoyBrowser
{
    class DimColor
    {
        private static List<Button> ButtonList;
        public static void DimColors(Button btn)
        {
            try
            {
                // New timer
                if (DimColorTimer == null)
                {
                    DimColorTimer = new Timer();
                    DimColorTimer.Interval = 10; // Tick speed
                    DimColorTimer.Tick += DimColorTimer_Tick;
                }

                // New button list
                if (ButtonList == null)
                {
                    ButtonList = new List<Button>();
                }
                // Add button to queue
                ButtonList.Add(btn);

                // Start timer
                StartDimTimer();
            }
            catch
            {
                // Do nothing
            }
        }

        private static void StartDimTimer()
        {
            if (ButtonList.Count != 0 && !DimColorTimer.Enabled)
            {
                try
                {
                    DimColorTimerCounter = 0;
                    DimButtonRef = ButtonList[0];
                    DimColorTimer.Enabled = true;
                }
                catch
                {
                    // Do nothing
                }
            }
        }

        private static void StopDimTimer()
        {
            DimColorTimer.Enabled = false;
            ButtonList.Clear();
        }

        //private List
        private static Timer DimColorTimer;
        private static Button DimButtonRef;
        private static int DimColorTimerCounter = 0;
        private static void DimColorTimer_Tick(object sender, EventArgs e)
        {
            if (DimColorTimerCounter >= 20) //  Steps to dim
            {
                StopDimTimer();
            }
            else
            {
                DimColorTimerCounter += 1;
                try
                {
                    // Set color values
                    DimButtonRef.ForeColor = AhoyColors.GetDimmedColor(DimButtonRef.ForeColor, 5, false); // Is dimming 5 pr. step
                    DimButtonRef.BackColor = AhoyColors.GetDimmedColor(DimButtonRef.BackColor, 5, false);
                    DimButtonRef.FlatAppearance.MouseOverBackColor = DimButtonRef.BackColor;

                    foreach (Button b in ButtonList)
                    {
                        // Update buttons with current color values
                        b.ForeColor = DimButtonRef.ForeColor;
                        b.BackColor = DimButtonRef.BackColor;
                        b.FlatAppearance.MouseOverBackColor = DimButtonRef.FlatAppearance.MouseOverBackColor;
                    }
                }
                catch
                {
                    // Do nothing
                }
            }
        }






        //private static List<Button> ButtonList;
        //private static bool InvertColor;
        //private static bool Loopback;
        //private static bool HasLoopedBack;
        //public static void DimColors(Button btn, bool invert, bool loopback) // Loopback will not land on correct color if any of the RGB values exceeds 255 og goes below 0
        //{
        //    try
        //    {
        //        // New timer
        //        if (DimColorTimer == null)
        //        {
        //            DimColorTimer = new Timer();
        //            DimColorTimer.Interval = 10; // Tick speed
        //            DimColorTimer.Tick += DimColorTimer_Tick;
        //        }

        //        // New button list
        //        if (ButtonList == null)
        //        {
        //            ButtonList = new List<Button>();
        //        }

        //        InvertColor = invert;
        //        Loopback = loopback;

        //        // Add button to queue
        //        ButtonList.Add(btn);

        //        // Start timer
        //        StartDimTimer();
        //    }
        //    catch
        //    {
        //        // Do nothing
        //    }
        //}

        //private static void StartDimTimer()
        //{
        //    if (ButtonList.Count != 0 && !DimColorTimer.Enabled)
        //    {
        //        try
        //        {
        //            DimColorTimerCounter = 0;
        //            DimButtonRef = ButtonList[0];
        //            HasLoopedBack = false;
        //            DimColorTimer.Enabled = true;
        //        }
        //        catch
        //        {
        //            // Do nothing
        //        }
        //    }
        //}

        //private static void StopDimTimer()
        //{
        //    DimColorTimer.Enabled = false;
        //    if (Loopback && !HasLoopedBack)
        //    {
        //        HasLoopedBack = true;
        //        DimColorTimerCounter = 0;
        //        DimColorTimer.Enabled = true;
        //    }
        //    else
        //    {
        //        ButtonList.Clear();
        //    }
        //}

        ////private List
        //private static Timer DimColorTimer;
        //private static Button DimButtonRef;
        //private static int DimColorTimerCounter = 0;
        //private static void DimColorTimer_Tick(object sender, EventArgs e)
        //{
        //    if (DimColorTimerCounter >= 20) //  Steps to dim
        //    {
        //        StopDimTimer();
        //    }
        //    else
        //    {
        //        DimColorTimerCounter += 1;
        //        try
        //        {
        //            if (Loopback)
        //            {
        //                if (!HasLoopedBack)
        //                {
        //                    // Set color values
        //                    DimButtonRef.ForeColor = AhoyColors.GetDimmedColor(DimButtonRef.ForeColor, 5, false); // Is dimming 5 pr. step
        //                    DimButtonRef.BackColor = AhoyColors.GetDimmedColor(DimButtonRef.BackColor, 5, false);
        //                    DimButtonRef.FlatAppearance.MouseOverBackColor = AhoyColors.GetDimmedColor(DimButtonRef.FlatAppearance.MouseOverBackColor, 5, false);
        //                }
        //                else
        //                {
        //                    // Set color values
        //                    DimButtonRef.ForeColor = AhoyColors.GetDimmedColor(DimButtonRef.ForeColor, 5, true); // Is dimming 5 pr. step
        //                    DimButtonRef.BackColor = AhoyColors.GetDimmedColor(DimButtonRef.BackColor, 5, true);
        //                    DimButtonRef.FlatAppearance.MouseOverBackColor = AhoyColors.GetDimmedColor(DimButtonRef.FlatAppearance.MouseOverBackColor, 5, true);
        //                }
        //            }
        //            else
        //            {
        //                // Set color values
        //                DimButtonRef.ForeColor = AhoyColors.GetDimmedColor(DimButtonRef.ForeColor, 5, InvertColor); // Is dimming 5 pr. step
        //                DimButtonRef.BackColor = AhoyColors.GetDimmedColor(DimButtonRef.BackColor, 5, InvertColor);
        //                DimButtonRef.FlatAppearance.MouseOverBackColor = DimButtonRef.BackColor;
        //            }

        //            foreach (Button b in ButtonList)
        //            {
        //                // Update buttons with current color values
        //                b.ForeColor = DimButtonRef.ForeColor;
        //                b.BackColor = DimButtonRef.BackColor;
        //                b.FlatAppearance.MouseOverBackColor = DimButtonRef.FlatAppearance.MouseOverBackColor;
        //            }
        //        }
        //        catch
        //        {
        //            // Do nothing
        //        }
        //    }
        //}
    }
}
