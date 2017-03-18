using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Colours
{
    /// <summary>
    /// Represents a color in the Hue/Saturation/Value form.
    /// </summary>
    [DataContract]
    [Serializable]
    public class HsvColor : IColor
    {
        private double _hue, _saturation, _value;

        /// <summary>
        /// The hue of the colour, on a wheel from 0 to 360 degrees.
        /// </summary>
        [DataMember]
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
        [DataMember]
        public double Saturation
        {
            get
            {
                return _saturation;
            }
            set
            {
                _saturation = value.Clamp(0.0, 1.0);
            }
        }
        /// <summary>
        /// The brightness of the colour, from 0 to 1.
        /// </summary>
        [DataMember]
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value.Clamp(0.0, 1.0);
            }
        }

        // Functions adapted from:
        // http://stackoverflow.com/questions/359612/how-to-change-rgb-color-to-hsv/1626175#1626175

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

            double tempValue = Value * 65535;
            int v = Convert.ToInt32(tempValue);
            int p = Convert.ToInt32(tempValue * (1 - Saturation));
            int q = Convert.ToInt32(tempValue * (1 - f * Saturation));
            int t = Convert.ToInt32(tempValue * (1 - (1 - f) * Saturation));

            if (hi == 0)
                return new RgbColor(v, t, p, 16);
            else if (hi == 1)
                return new RgbColor(q, v, p, 16);
            else if (hi == 2)
                return new RgbColor(p, v, t, 16);
            else if (hi == 3)
                return new RgbColor(p, q, v, 16);
            else if (hi == 4)
                return new RgbColor(t, p, v, 16);
            else
                return new RgbColor(v, p, q, 16);
        }

        /// <summary>
        /// Prints a representation of the color.
        /// </summary>
        /// <returns>The color, in a "HsvColor [H=0, S=0, V=0]" format."</returns>
        public override string ToString()
        {
            return string.Format("HsvColor [H={0:N}, S={1:N}, V={2:N}]", Hue, Saturation, Value);
        }

        /// <summary>
        /// Outputs a string representation of the color, in CSS form.
        /// </summary>
        /// <returns>A string in "hsv(0, 0%, 0%)" format.</returns>
        public string ToCssString()
        {
            CultureInfo cssCulture = new CultureInfo(CultureInfo.InvariantCulture.Name);
            cssCulture.NumberFormat.PercentDecimalDigits = 0;
            cssCulture.NumberFormat.PercentPositivePattern = 1;
            return String.Format(cssCulture,
                "hsv({0:F0}, {1:P}, {2:P})", Hue, Saturation, Value);
        }
    }
}
