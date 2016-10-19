using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Colours
{
    public static class GdiWrapper
    {
        public static RgbColor ToRgbColor(this Color c)
        {
            return new RgbColor(c.R, c.G, c.B);
        }

        public static Color ToGdiColor(this RgbColor c)
        {
            return Color.FromArgb(c.R8, c.G8, c.B8);
        }
    }
}
