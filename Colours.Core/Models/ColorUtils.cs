using System;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Colours
{
    /// <summary>
    /// Contains utility functions for <see cref="RgbColor"/> and
    /// <see cref="HsvColor"/>.
    /// </summary>
    public static class ColorUtils
    {
        const string rgbRegex = @"rgb\(\s*(\d{1,3}),\s*(\d{1,3}),\s*(\d{1,3})\s*\)";
        const string hsvRegex = @"hsv\(\s*(\d{1,3}),\s*(\d{1,3})%,\s*(\d{1,3})%\s*\)";
        const string gdiRegex = @"[RGB]=(\d{0,3})";

        /// <summary>
        /// Creates an RGB color from a 3 or 6 hex digit number prefixed with an #.
        /// </summary>
        /// <param name="c">The string, such as "#123456".</param>
        /// <returns>The new color.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the string could not be parsed.
        /// </exception>
        public static RgbColor FromHtml(string c)
        {
            if (string.IsNullOrWhiteSpace(c) || c[0] != '#')
                throw new ArgumentException("The color is empty or invalid.");
            if (c[0] == '#' && c.Length == 4)
            {
                char r = c[1], g = c[2], b = c[3];
                c = new string(new char[] { '#', r, r, g, g, b, b });
            }

            byte R = byte.Parse(c.Substring(1, 2), NumberStyles.AllowHexSpecifier);
            byte G = byte.Parse(c.Substring(3, 2), NumberStyles.AllowHexSpecifier);
            byte B = byte.Parse(c.Substring(5, 2), NumberStyles.AllowHexSpecifier);
            return new RgbColor(R, G, B);
        }

        /// <summary>
        /// Creates an RGB color from a CSS RGB triplet.
        /// </summary>
        /// <param name="c">The string, such as "rgb(0, 0, 0)".</param>
        /// <returns>The new color.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the string could not be parsed.
        /// </exception>
        public static RgbColor FromCssRgb(string c)
        {
            if (c.StartsWith("rgb("))
            {
                Match m = Regex.Match(c, rgbRegex);
                if (!m.Success) goto fail;
                var ints = m.Groups.Cast<Group>().Skip(1)
                    .Select(g => int.Parse(g.Value)).ToList();
                if (ints.Count() == 3)
                    return new RgbColor
                        (ints[0], ints[1], ints[2], 8);
            }
            fail:
            throw new ArgumentException("The color is invalid.");
        }

        /// <summary>
        /// Creates an RGB color from the output of ToString on a
        /// System.Drawing.Color or <see cref="RgbColor"/>.
        /// </summary>
        /// <param name="c">
        /// The string, such as "Color [A=255, R=210, G=180, B=140]".
        /// </param>
        /// <returns>The new color.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the string could not be parsed.
        /// </exception>
        public static RgbColor FromGdiColorString(string c)
        {
            if (c.StartsWith("Color [")
                || c.StartsWith("RgbColor ["))
            {
                Match m = Regex.Match(c, gdiRegex);
                if (!m.Success) goto fail;
                var ints = m.Groups.Cast<Group>().Skip(1)
                    .Select(g => int.Parse(g.Value)).ToList();
                // TODO: should handle out-of-order ones if existant
                if (ints.Count() == 3)
                    return new RgbColor
                        (ints[0], ints[1], ints[2], 8);
            }
            fail:
            throw new ArgumentException("The color is invalid.");
        }

        /// <summary>
        /// Creates an HSV color from a HSV triplet.
        /// </summary>
        /// <param name="c">The string, such as "hsv(0, 0%, 0%)".</param>
        /// <returns>The new color.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the string could not be parsed.
        /// </exception>
        public static HsvColor FromCssHsv(string c)
        {
            if (c.StartsWith("hsv("))
            {
                Match m = Regex.Match(c, hsvRegex);
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

        /// <summary>
        /// Attempt to create a color from a string, in a variety of formats.
        /// </summary>
        /// <param name="colorString">
        /// The string, such as "#123456" or "hsv(180, 50%, 50%)"
        /// </param>
        /// <returns>An HsvColor from the string.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the string could not be parsed.
        /// </exception>
        public static HsvColor FromString(string colorString)
        {
            colorString.Trim();

            try
            {
                return new HsvColor(FromHtml(colorString));
            }
            catch (Exception) // we tried to do HTML, which does a good job normally
            {
                if (colorString.StartsWith("rgb("))
                {
                    return new HsvColor(FromCssRgb(colorString));
                }
                // TODO: hsl
                else if (colorString.StartsWith("hsv("))
                {
                    return FromCssHsv(colorString);
                }
                else if (colorString.StartsWith("Color [")
                    || colorString.StartsWith("RgbColor ["))
                {
                    return new HsvColor(FromGdiColorString(colorString));
                }
                throw new ArgumentException("The string could not be converted.");
            }
        }
    }
}
