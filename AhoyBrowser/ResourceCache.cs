using System.Collections.Generic;
using System.Drawing;

namespace AhoyBrowser
{
    /// <summary>
    /// Shared images from the embedded resources. Every Properties.Resources.X access creates a new
    /// Bitmap (a GDI handle); the UI asks for the same images over and over (hover, theme changes).
    /// The cached images are shared: never dispose them.
    /// </summary>
    internal static class ResourceCache
    {
        private static readonly Dictionary<string, Bitmap> Bitmaps = new Dictionary<string, Bitmap>();

        internal static Bitmap Get(string name)
        {
            lock (Bitmaps)
            {
                if (!Bitmaps.TryGetValue(name, out Bitmap bitmap))
                {
                    bitmap = (Bitmap)Properties.Resources.ResourceManager.GetObject(name, Properties.Resources.Culture);
                    Bitmaps[name] = bitmap;
                }
                return bitmap;
            }
        }
    }
}
