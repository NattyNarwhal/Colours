using System;
using System.Globalization;

namespace Colours
{
    /// <summary>
    /// Represents a color in the Hue/Saturation/Value form.
    /// </summary>
    /// <seealso cref="System.Drawing.Color"/>
    public class HsvColor
    {
        private double _hue;

        /// <summary>
        /// The hue of the colour, on a wheel from 0 to 360 degrees.
        /// </summary>
        public double Hue
        {
            get
            {
                return _hue;
            }
            set
            {
                if (value > 360)
                    _hue = value - 360;
                else if (value < 0)
                    _hue = value + 360;
                else
                    _hue = value;
            }
        }
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

        /// <summary>
        /// Creates a new HsvColor from an existing RgbColor.
        /// </summary>
        /// <param name="c">The RgbColor to create from.</param>
        public HsvColor(RgbColor c)
        {
            int max = Math.Max(c.R, Math.Max(c.G, c.B));
            int min = Math.Min(c.R, Math.Min(c.G, c.B));

            Hue = c.GetHue();
            Saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            Value = max / 255d;
        }

        /// <summary>
        /// Creates a new HsvColor from the numbered values.
        /// </summary>
        /// <param name="h">The hue of the color.</param>
        /// <param name="s">The saturation of the color.</param>
        /// <param name="v">The value of the color.</param>
        public HsvColor(double h, double s, double v)
        {
            Hue = h;
            Saturation = s;
            Value = v;
        }

        /// <summary>
        /// Creates a new RgbColor from the existing HsvColor.
        /// </summary>
        /// <returns>The new RgbColor.</returns>
        public RgbColor ToRgb()
        {
            int hi = Convert.ToInt32(Math.Floor(Hue / 60)) % 6;
            double f = Hue / 60 - Math.Floor(Hue / 60);

            double tempValue = Value * 255;
            int v = Convert.ToInt32(tempValue);
            int p = Convert.ToInt32(tempValue * (1 - Saturation));
            int q = Convert.ToInt32(tempValue * (1 - f * Saturation));
            int t = Convert.ToInt32(tempValue * (1 - (1 - f) * Saturation));

            if (hi == 0)
                return new RgbColor(v, t, p);
            else if (hi == 1)
                return new RgbColor(q, v, p);
            else if (hi == 2)
                return new RgbColor(p, v, t);
            else if (hi == 3)
                return new RgbColor(p, q, v);
            else if (hi == 4)
                return new RgbColor(t, p, v);
            else
                return new RgbColor(v, p, q);
        }

        /// <summary>
        /// Outputs a string representation of the color.
        /// </summary>
        /// <returns>A string in "hsv(0, 0%, 0%)" format.</returns>
        public override string ToString()
        {
            CultureInfo cssCulture = new CultureInfo(CultureInfo.InvariantCulture.Name);
            cssCulture.NumberFormat.PercentDecimalDigits = 0;
            cssCulture.NumberFormat.PercentPositivePattern = 1;
            return String.Format(cssCulture,
                "hsv({0:F0}, {1:P}, {2:P})", Hue, Saturation, Value);
        }
    }
}
