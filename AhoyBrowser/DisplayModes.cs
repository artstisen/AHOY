using System.Collections.Generic;
using System.Windows.Forms;

namespace AhoyBrowser
{
    class DisplayModes
    {
        public static void ChangeDisplayMode(bool visible, List<Control> controls)
        {

            // Run through controls and set visibility
            if (controls.Count > 0)
            {
                for (int i = 0; i < controls.Count; i++)
                {
                    controls[i].Visible = visible;
                }
            }
        }
    }
}
