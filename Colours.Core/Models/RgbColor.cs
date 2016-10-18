using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Colours
{
    /// <summary>
    /// A color in Red/Green/Blue form. This class is cross-platform -
    /// it is expected to convert your platform's native color format
    /// (like Gdk.Color, NSColor, etc.) to an RgbColor/HsvColor.
    /// </summary>
    [DataContract]
    public class RgbColor
    {
        /// <summary>
        /// The red channel of the color. Values from 0-255.
        /// </summary>
        [DataMember]
        public byte R { get; set; }
        /// <summary>
        /// The green channel of the color. Values from 0-255.
        /// </summary>
        [DataMember]
        public byte G { get; set; }
        /// <summary>
        /// The blue channel of the color. Values from 0-255.
        /// </summary>
        [DataMember]
        public byte B { get; set; }

        /// <summary>
        /// Creates a new RgbColor as the color black.
        /// </summary>
        public RgbColor()
        {
            R = 0;
            G = 0;
            B = 0;
        }

        /// <summary>
        /// Creates a new color from the numbered values.
        /// </summary>
        /// <param name="r">The red channel of the color.</param>
        /// <param name="g">The green channel of the color.</param>
        /// <param name="b">The blue channel of the color.</param>
        public RgbColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// Creates a new color from the numbered values.
        /// </summary>
        /// <param name="r">The red channel of the color.</param>
        /// <param name="g">The green channel of the color.</param>
        /// <param name="b">The blue channel of the color.</param>
        public RgbColor(int r, int g, int b)
        {
            // clamp to handle negative values (XYZ conversions can)
            R = (byte)r.Clamp(0, 255);
            G = (byte)g.Clamp(0, 255);
            B = (byte)b.Clamp(0, 255);
        }

        /// <summary>
        /// Inverts the current color.
        /// </summary>
        /// <returns>The inverted color.</returns>
        public RgbColor Invert()
        {
            return new RgbColor(
                255 - R,
                255 - G,
                255 - B);
        }

        // imports of Mono's System.Drawing

        /// <summary>
        /// Gets how bright the colour is, in the HSL form.
        /// </summary>
        /// <returns>A number from 0 to 1.</returns>
        public float GetBrightness()
        {
            byte minval = Math.Min(R, Math.Min(G, B));
            byte maxval = Math.Max(R, Math.Max(G, B));

            return (float)(maxval + minval) / 510;
        }

        /// <summary>
        /// Gets how saturated the colour is, in the HSL form.
        /// </summary>
        /// <returns>A number from 0 to 1.</returns>
        public float GetSaturation()
        {
            byte minval = Math.Min(R, Math.Min(G, B));
            byte maxval = Math.Max(R, Math.Max(G, B));

            if (maxval == minval)
                return 0.0f;

            int sum = maxval + minval;
            if (sum > 255)
                sum = 510 - sum;

            return (float)(maxval - minval) / sum;
        }

        /// <summary>
        /// Gets the hue of the color.
        /// </summary>
        /// <returns>A number from 0-360.</returns>
        public float GetHue()
        {
            byte minval = Math.Min(R, Math.Min(G, B));
            byte maxval = Math.Max(R, Math.Max(G, B));

            if (maxval == minval)
                return 0.0f;

            float diff = maxval - minval;
            float rnorm = (maxval - R) / diff;
            float gnorm = (maxval - G) / diff;
            float bnorm = (maxval - B) / diff;

            float hue = 0.0f;
            if (R == maxval)
                hue = 60.0f * (6.0f + bnorm - gnorm);
            if (G == maxval)
                hue = 60.0f * (2.0f + rnorm - bnorm);
            if (B == maxval)
                hue = 60.0f * (4.0f + gnorm - rnorm);
            if (hue > 360.0f)
                hue = hue - 360.0f;

            return hue;
        }

        /// <summary>
        /// Prints a human-readable color triplet for use in things like CSS.
        /// </summary>
        /// <returns>The string, in an "rgb(0, 0, 0)" format.</returns>
        public string ToHslString()
        {
            CultureInfo cssCulture = new CultureInfo(CultureInfo.InvariantCulture.Name);
            cssCulture.NumberFormat.PercentDecimalDigits = 0;
            cssCulture.NumberFormat.PercentPositivePattern = 1;
            return string.Format(cssCulture,
                "hsl({0:F0}, {1:P}, {2:P})",
                GetHue(), GetSaturation(), GetBrightness());
        }

        /// <summary>
        /// Prints the color in a 6 hex digit form prefixed with a #,
        /// used to represent the color in things such as CSS.
        /// </summary>
        /// <returns>The string, in a "#123456" format.</returns>
        public string ToHtml()
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", R, G, B);
        }

        /// <summary>
        /// Prints a representation of the color.
        /// </summary>
        /// <returns>The color, in a "RgbColor [R=0, G=0, B=0]" format."</returns>
        public override string ToString()
        {
            return string.Format("RgbColor [R={0}, G={1}, B={2}]", R, G, B);
        }

        /// <summary>
        /// Converts the colour into an XYZ representation.
        /// </summary>
        /// <returns>The XYZ representation of the colour.</returns>
        public XyzColor ToXyz()
        {
            // normalize red, green, blue values
            double rLinear = R / 255.0;
            double gLinear = G / 255.0;
            double bLinear = B / 255.0;

            // convert to a sRGB form
            double r = (rLinear > 0.04045) ? Math.Pow((rLinear + 0.055) / (
                1 + 0.055), 2.2) : (rLinear / 12.92);
            double g = (gLinear > 0.04045) ? Math.Pow((gLinear + 0.055) / (
                1 + 0.055), 2.2) : (gLinear / 12.92);
            double b = (bLinear > 0.04045) ? Math.Pow((bLinear + 0.055) / (
                1 + 0.055), 2.2) : (bLinear / 12.92);

            // converts
            return new XyzColor(
                (r * 0.4124 + g * 0.3576 + b * 0.1805),
                (r * 0.2126 + g * 0.7152 + b * 0.0722),
                (r * 0.0193 + g * 0.1192 + b * 0.9505)
                );
        }

        /// <summary>
        /// Converts the colour into an CMYK representation.
        /// </summary>
        /// <returns>The CMYK representation of the colour.</returns>
        public CmykColor ToCmyk()
        {
            // normalizes red, green, blue values
            double c = (double)(255 - R) / 255;
            double m = (double)(255 - G) / 255;
            double y = (double)(255 - B) / 255;

            double k = Math.Min(c, Math.Min(m, y));

            if (k == 1.0)
            {
                return new CmykColor(0, 0, 0, 1);
            }
            else
            {
                return new CmykColor((c - k) / (1 - k), (m - k) / (1 - k), (y - k) / (1 - k), k);
            }

        }
    }
}
