using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Colours
{
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
    }
}
