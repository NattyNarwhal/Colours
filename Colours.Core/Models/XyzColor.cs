using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Represents a colour in the CIEXYZ colour space.
    /// </summary>
    [DataContract]
    public class XyzColor
    {
        /// <summary>
        /// The D65 (white) colour.
        /// </summary>
        public static readonly XyzColor D65 = new XyzColor(0.9505, 1.0, 1.0890);

        double _x, _y, _z;
        /// <summary>
        /// Represents the "red" channel. (0 to 0.9505)
        /// </summary>
        [DataMember]
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value.Clamp(0, 0.9505);
            }
        }
        /// <summary>
        /// Represents the "green" channel. (0 to 1)
        /// </summary>
        [DataMember]
        public double Y
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
        /// Represents the "blue" channel. (0 to 1.089)
        /// </summary>
        [DataMember]
        public double Z
        {
            get
            {
                return _z;
            }
            set
            {
                _z = value.Clamp(0, 1.0890);
            }
        }

        /// <summary>
        /// Creates a new colour in the XYZ space.
        /// </summary>
        /// <param name="x">
        /// Represents the "red" channel. (0 to 0.9505)
        /// </param>
        /// <param name="y">
        /// Represents the "green" channel. (0 to 1)
        /// </param>
        /// <param name="z">
        /// Represents the "blue" channel. (0 to 1.089)
        /// </param>
        public XyzColor(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Converts the colour into an RGB representation.
        /// </summary>
        /// <returns>The RGB representation of the colour.</returns>
        public RgbColor ToRgb()
        {
            double[] Clinear = new double[3];
            Clinear[0] = X * 3.2410 - Y * 1.5374 - Z * 0.4986; // red
            Clinear[1] = -X * 0.9692 + Y * 1.8760 + Z * 0.0416; // green
            Clinear[2] = X * 0.0556 - Y * 0.2040 + Z * 1.0570; // blue

            for (int i = 0; i < 3; i++)
            {
                Clinear[i] = (Clinear[i] <= 0.0031308) ? 12.92 * Clinear[i] : (
                    1 + 0.055) * Math.Pow(Clinear[i], (1.0 / 2.4)) - 0.055;
            }

            return new RgbColor(
                Convert.ToInt32(double.Parse(string.Format("{0:0.00}",
                    Clinear[0] * 65535.0))),
                Convert.ToInt32(double.Parse(string.Format("{0:0.00}",
                    Clinear[1] * 65535.0))),
                Convert.ToInt32(double.Parse(string.Format("{0:0.00}",
                    Clinear[2] * 65535.0))),
                16);
        }

        static double Fxyz(double t)
        {
            return ((t > 0.008856) ? Math.Pow(t, (1.0 / 3.0)) : (7.787 * t + 16.0 / 116.0));
        }

        /// <summary>
        /// Converts the colour into a L*a*b* representation.
        /// </summary>
        /// <returns>The L*a*b* representation of the colour.</returns>
        public LabColor ToLab()
        {
            var lab = new LabColor(0, 0, 0);

            lab.L = 116.0 * Fxyz(Y / D65.Y) - 16;
            lab.A = 500.0 * (Fxyz(X / D65.X) - Fxyz(Y / D65.Y));
            lab.B = 200.0 * (Fxyz(Y / D65.Y) - Fxyz(Z / D65.Z));

            return lab;
        }
    }
}
