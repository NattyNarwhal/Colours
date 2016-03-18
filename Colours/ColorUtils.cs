using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;

namespace Colours
{
    [Serializable]
    public class ColorList : List<int>
    {
        public ColorList()
        {

        }

        public ColorList(int[] win32colors)
        {
            AddRange(win32colors);
        }
    }

    public static class ColorUtils
    {
        /// <summary>
        /// Gets the inverse of an RGB color.
        /// </summary>
        /// <param name="c">The color to invert.</param>
        /// <returns>The inverted color.</returns>
        public static Color Invert(this Color c)
        {
            return Color.FromArgb(
                    255 - c.R,
                    255 - c.G,
                    255 - c.B
                );
        }
        
        /// <summary>
        /// Prints a human-readable color triplet for use in things like CSS.
        /// </summary>
        /// <param name="c">The color to encode.</param>
        /// <returns>The </returns>
        public static string ToRgbString(this Color c)
        {
            return String.Format("rgb({0}, {1}, {2})",
                c.R, c.G, c.B);
        }

        public static string ToHslString(this Color c)
        {
            CultureInfo cssCulture = new CultureInfo(CultureInfo.InvariantCulture.LCID);
            cssCulture.NumberFormat.PercentDecimalDigits = 0;
            cssCulture.NumberFormat.PercentPositivePattern = 1;
            return String.Format(cssCulture,
                "hsl({0:F0}, {1:P}, {2:P})",
                c.GetHue(), c.GetSaturation(), c.GetBrightness());
        }
    }
}
