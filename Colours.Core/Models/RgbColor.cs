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
    [Serializable]
    public class RgbColor
    {
        /// <summary>
        /// The red channel of the color. Values from 0-65535.
        /// </summary>
        [DataMember]
        public ushort R { get; set; }
        /// <summary>
        /// The green channel of the color. Values from 0-65535.
        /// </summary>
        [DataMember]
        public ushort G { get; set; }
        /// <summary>
        /// The blue channel of the color. Values from 0-66535.
        /// </summary>
        [DataMember]
        public ushort B { get; set; }

        /// <summary>
        /// The red channel of the color. Values from 0-255.
        /// </summary>
        // Adobe rounds 0x<FF>00 to 0xFE. Multiply by 0x101 instead of
        // bit shifting. Do GDK and Cocoa do this too?
        public byte R8
        {
            get
            {
                return (byte)(R >> 8);
            }
            set
            {
                R = (ushort)(value * 0x101);
            }
        }
        /// <summary>
        /// The green channel of the color. Values from 0-255.
        /// </summary>
        public byte G8
        {
            get
            {
                return (byte)(G >> 8);
            }
            set
            {
                G = (ushort)(value * 0x101);
            }
        }
        /// <summary>
        /// The blue channel of the color. Values from 0-255.
        /// </summary>
        public byte B8
        {
            get
            {
                return (byte)(B >> 8);
            }
            set
            {
                B = (ushort)(value * 0x101);
            }
        }

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
            R8 = r;
            G8 = g;
            B8 = b;
        }

        /// <summary>
        /// Creates a new color from the numbered values.
        /// </summary>
        /// <param name="r">The red channel of the color.</param>
        /// <param name="g">The green channel of the color.</param>
        /// <param name="b">The blue channel of the color.</param>
        public RgbColor(ushort r, ushort g, ushort b)
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
        /// <param name="depth">The bit depth of the colour, 8 or 16.</param>
        public RgbColor(int r, int g, int b, int depth)
        {
            if (depth == 8)
            {
                // clamp to handle negative values (XYZ conversions can)
                R8 = (byte)r.Clamp(0, 255);
                G8 = (byte)g.Clamp(0, 255);
                B8 = (byte)b.Clamp(0, 255);
            }
            else if (depth == 16)
            {
                R = (ushort)r.Clamp(0, ushort.MaxValue);
                G = (ushort)g.Clamp(0, ushort.MaxValue);
                B = (ushort)b.Clamp(0, ushort.MaxValue);
            }
            else
                throw new ArgumentOutOfRangeException("Invalid bit depth.");
        }

        /// <summary>
        /// Inverts the current color.
        /// </summary>
        /// <returns>The inverted color.</returns>
        public RgbColor Invert()
        {
            return new RgbColor(
                ushort.MaxValue - R,
                ushort.MaxValue - G,
                ushort.MaxValue - B,
                16);
        }

        // imports of Mono's System.Drawing

        /// <summary>
        /// Gets how bright the colour is, in the HSL form.
        /// </summary>
        /// <returns>A number from 0 to 1.</returns>
        public float GetBrightness()
        {
            // TODO: adapt to 16-bit values (the constant looks wrong for that)
            var minval = Math.Min(R8, Math.Min(G8, B8));
            var maxval = Math.Max(R8, Math.Max(G8, B8));

            return (float)(maxval + minval) / 510;
        }

        /// <summary>
        /// Gets how saturated the colour is, in the HSL form.
        /// </summary>
        /// <returns>A number from 0 to 1.</returns>
        public float GetSaturation()
        {
            var minval = Math.Min(R8, Math.Min(G8, B8));
            var maxval = Math.Max(R8, Math.Max(G8, B8));

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
            var minval = Math.Min(R, Math.Min(G, B));
            var maxval = Math.Max(R, Math.Max(G, B));

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
            return string.Format("#{0:X2}{1:X2}{2:X2}", R8, G8, B8);
        }

        /// <summary>
        /// Prints a representation of the color.
        /// </summary>
        /// <returns>The color, in a "RgbColor [R=0, G=0, B=0]" format."</returns>
        public override string ToString()
        {
            return string.Format("RgbColor [R={0}, G={1}, B={2}]", R8, G8, B8);
        }

        /// <summary>
        /// Converts the colour into an HSV representation.
        /// </summary>
        /// <returns>The HSV representation of the colour.</returns>
        public HsvColor ToHsv()
        {
            int max = Math.Max(R, Math.Max(G, B));
            int min = Math.Min(R, Math.Min(G, B));

            var hue = GetHue();
            var saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            var value = max / 65535d;

            return new HsvColor(hue, saturation, value);
        }

        /// <summary>
        /// Converts the colour into an XYZ representation.
        /// </summary>
        /// <returns>The XYZ representation of the colour.</returns>
        public XyzColor ToXyz()
        {
            // normalize red, green, blue values
            double rLinear = R / 65535.0;
            double gLinear = G / 65535.0;
            double bLinear = B / 65535.0;

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
            double c = (double)(65535 - R) / 65535;
            double m = (double)(65535 - G) / 65535;
            double y = (double)(65535 - B) / 65535;

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
