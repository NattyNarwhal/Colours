using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    public static class ColorSchemer
    {
        public static HsvColor Opposite(HsvColor c)
        {
            return new HsvColor(360 - c.Hue, c.Saturation, c.Value);
        }

        public static HsvColor Complement(HsvColor c)
        {
            return new HsvColor(c.Hue + (360 / 2), c.Saturation, c.Value);
        }

        public static List<HsvColor> SplitComplement(HsvColor c)
        {
            const double offset = 360 / 15;

            HsvColor c1 = new HsvColor(c.Hue + (360 / 2) - offset, c.Saturation, c.Value);
            HsvColor c2 = new HsvColor(c.Hue + (360 / 2) + offset, c.Saturation, c.Value);

            return new List<HsvColor>() { c1, c2 };
        }

        public static List<HsvColor> Tetrads(HsvColor c)
        {
            const double offset = 360 / 4;

            HsvColor c1 = new HsvColor(c.Hue + offset, c.Saturation, c.Value);
            HsvColor c2 = new HsvColor(c.Hue + 360 / 2, c.Saturation, c.Value);
            HsvColor c3 = new HsvColor(c.Hue + 360 / 2 + offset, c.Saturation, c.Value);

            return new List<HsvColor>() { c1, c2, c3 };
        }

        public static List<HsvColor> Analogous(HsvColor c)
        {
            const double offset = 360 / 12;

            HsvColor c1 = new HsvColor(c.Hue - offset, c.Saturation, c.Value);
            HsvColor c2 = new HsvColor(c.Hue + offset, c.Saturation, c.Value);

            return new List<HsvColor>() { c1, c2 };
        }

        public static List<HsvColor> Triads(HsvColor c)
        {
            const double offset = 360 / 3;

            HsvColor c1 = new HsvColor(c.Hue - offset, c.Saturation, c.Value);
            HsvColor c2 = new HsvColor(c.Hue + offset, c.Saturation, c.Value);

            return new List<HsvColor>() { c1, c2 };
        }

        public static List<HsvColor> Monochromatic(HsvColor c)
        {
            HsvColor c1, c2;

            if (c.Saturation < 0.1d)
            {
                c1 = new HsvColor(c.Hue, (c.Saturation + (1d / 3d)) % 1d, c.Value);
                c2 = new HsvColor(c.Hue, (c.Saturation + 0.02d * (1d / 3d)) % 1d, c.Value);
            }
            else
            {
                c1 = new HsvColor(c.Hue, c.Saturation, (c.Value + (1d / 3d)) % 1d);
                c2 = new HsvColor(c.Hue, c.Saturation, (c.Value + 0.02d * (1d / 3d)) % 1d);
            }

            return new List<HsvColor>() { c1, c2 };
        }
    }
}
