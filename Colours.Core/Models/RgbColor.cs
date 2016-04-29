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
            R = (byte)r;
            G = (byte)g;
            B = (byte)b;
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

            float diff = (float)(maxval - minval);
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
            return String.Format(cssCulture,
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
            return String.Format("#{0:X2}{1:X2}{2:X2}", R, G, B);
        }

        /// <summary>
        /// Prints a representation of the color.
        /// </summary>
        /// <returns>The color, in a "RgbColor [R=0, G=0, B=0]" format."</returns>
        public override string ToString()
        {
            return String.Format("RgbColor [R={0}, G={1}, B={2}]", R, G, B);
        }
    }
}
