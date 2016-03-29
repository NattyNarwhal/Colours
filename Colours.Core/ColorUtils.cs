using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

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
        /// <returns>The string, in an "rgb(0, 0, 0)" format.</returns>
        public static string ToRgbString(this Color c)
        {
            return String.Format("rgb({0}, {1}, {2})",
                c.R, c.G, c.B);
        }

        /// <summary>
        /// Prints a human-readable color triplet for use in things like CSS.
        /// </summary>
        /// <param name="c">The color to encode.</param>
        /// <returns>The string, in an "hsl(0, 0%, 0%)" format.</returns>
        public static string ToHslString(this Color c)
        {
            CultureInfo cssCulture = new CultureInfo(CultureInfo.InvariantCulture.LCID);
            cssCulture.NumberFormat.PercentDecimalDigits = 0;
            cssCulture.NumberFormat.PercentPositivePattern = 1;
            return String.Format(cssCulture,
                "hsl({0:F0}, {1:P}, {2:P})",
                c.GetHue(), c.GetSaturation(), c.GetBrightness());
        }

        /// <summary>
        /// Attempt to create a color from a string, in a variety of formats.
        /// </summary>
        /// <param name="colorString"></param>
        /// <returns></returns>
        public static HsvColor FromString(string colorString)
        {
            const string rgbRegex = @"rgb\(\s*(\d{1,3}),\s*(\d{1,3}),\s*(\d{1,3})\s*\)";
            const string hsvRegex = @"hsv\(\s*(\d{1,3}),\s*(\d{1,3})%,\s*(\d{1,3})%\s*\)";

            colorString.Trim();

            try
            {
                return new HsvColor(ColorTranslator.FromHtml(colorString));
            }
            catch (Exception) // we tried to do HTML, which does a good job normally
            {
                if (colorString.StartsWith("rgb("))
                {
                    Match m = Regex.Match(colorString, rgbRegex);
                    if (!m.Success) goto fail;
                    var ints = m.Groups.Cast<Group>().Skip(1)
                        .Select(g => int.Parse(g.Value)).ToList();
                    if (ints.Count() == 3)
                        return new HsvColor(Color.FromArgb
                            (ints[0], ints[1], ints[2]));
                }
                // TODO: hsl
                else if (colorString.StartsWith("hsv("))
                {
                    Match m = Regex.Match(colorString, hsvRegex);
                    if (!m.Success) goto fail;
                    var dubs = m.Groups.Cast<Group>().Skip(1)
                        .Select(g => double.Parse(g.Value)).ToList();
                    if (dubs.Count() == 3)
                        return new HsvColor(
                            dubs[0], dubs[1] / 100d, dubs[2] / 100d);
                }
                fail:
                throw new ArgumentException("The string could not be converted.");
            }
        }
    }
}
