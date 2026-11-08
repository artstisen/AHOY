using System.ComponentModel;
using System.Windows.Forms;

namespace AhoyBrowser
{
    class AhoyContextMenuStrip : ContextMenuStrip
    {
        public AhoyContextMenuStrip(IContainer c) : base(c)
        {
            this.Renderer = new AhoyMenuStripRenderer(new AhoyMenuStripColorTable());
        }
    }
}
