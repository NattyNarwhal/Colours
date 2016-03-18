using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;

namespace Colours
{
    /// <summary>
    /// Represents a color in the Hue/Saturation/Value form.
    /// </summary>
    /// <seealso cref="System.Drawing.Color"/>
    public struct HsvColor
    {
        /// <summary>
        /// The hue of the colour, on a wheel from 0 to 360 degrees.
        /// </summary>
        public double Hue { get; set; }
        /// <summary>
        /// The saturation of the colour, from 0 to 1.
        /// </summary>
        public double Saturation { get; set; }
        /// <summary>
        /// The brightness of the colour, from 0 to 1.
        /// </summary>
        public double Value { get; set; }

        // Functions adapted from:
        // http://stackoverflow.com/questions/359612/how-to-change-rgb-color-to-hsv/1626175#1626175

        public HsvColor(Color c)
        {
            int max = Math.Max(c.R, Math.Max(c.G, c.B));
            int min = Math.Min(c.R, Math.Min(c.G, c.B));

            Hue = c.GetHue();
            Saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            Value = max / 255d;
        }

        public HsvColor(double h, double s, double v)
        {
            Hue = h;
            Saturation = s;
            Value = v;
        }

        public Color ToRgb()
        {
            int hi = Convert.ToInt32(Math.Floor(Hue / 60)) % 6;
            double f = Hue / 60 - Math.Floor(Hue / 60);

            double tempValue = Value * 255;
            int v = Convert.ToInt32(tempValue);
            int p = Convert.ToInt32(tempValue * (1 - Saturation));
            int q = Convert.ToInt32(tempValue * (1 - f * Saturation));
            int t = Convert.ToInt32(tempValue * (1 - (1 - f) * Saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }


        public override string ToString()
        {
            CultureInfo cssCulture = new CultureInfo(CultureInfo.InvariantCulture.LCID);
            cssCulture.NumberFormat.PercentDecimalDigits = 0;
            cssCulture.NumberFormat.PercentPositivePattern = 1;
            return String.Format(cssCulture,
                "hsv({0:F0}, {1:P}, {2:P})", Hue, Saturation, Value);
        }
    }
}
