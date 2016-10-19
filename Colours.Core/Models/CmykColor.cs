using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// A color in Cyan/Magenta/Yellow/Key (black) form.
    /// </summary>
    [DataContract]
    public class CmykColor
    {
        double _c, _m, _y, _k;

        /// <summary>
        /// The cyan channel.
        /// </summary>
        [DataMember]
        public double Cyan
        {
            get
            {
                return _c;
            }
            set
            {
                _c = value.Clamp(0, 1.0);
            }
        }

        /// <summary>
        /// The magenta channel.
        /// </summary>
        [DataMember]
        public double Magenta
        {
            get
            {
                return _m;
            }
            set
            {
                _m = value.Clamp(0, 1.0);
            }
        }
        /// <summary>
        /// The yellow channel.
        /// </summary>
        [DataMember]
        public double Yellow
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value.Clamp(0, 1.0);
            }
        }
        /// <summary>
        /// The key/black channel.
        /// </summary>
        [DataMember]
        public double Key
        {
            get
            {
                return _k;
            }
            set
            {
                _k = value.Clamp(0, 1.0);
            }
        }

        /// <summary>
        /// Creates a new color in the CMYK space.
        /// </summary>
        /// <param name="c">The cyan channel.</param>
        /// <param name="m">The magenta channel.</param>
        /// <param name="y">The yellow channel.</param>
        /// <param name="k">The key/black channel.</param>
        public CmykColor(double c, double m, double y, double k)
        {
            Cyan = c;
            Magenta = m;
            Yellow = y;
            Key = k;
        }

        /// <summary>
        /// Converts the colour into an RGB representation.
        /// </summary>
        /// <returns>The RGB representation of the colour.</returns>
        public RgbColor ToRgb()
        {
            int red = Convert.ToInt32((1 - Cyan) * (1 - Key) * 65535.0);
            int green = Convert.ToInt32((1 - Magenta) * (1 - Key) * 65535.0);
            int blue = Convert.ToInt32((1 - Yellow) * (1 - Key) * 65535.0);

            return new RgbColor(red, green, blue, 16);
        }
    }
}
