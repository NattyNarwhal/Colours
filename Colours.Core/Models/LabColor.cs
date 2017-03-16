using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Represents a colour in the L*a*b* colour space.
    /// </summary>
    public class LabColor : IColor
    {
        /// <summary>
        /// The luminance of the colour.
        /// </summary>
        /// <remarks>
        /// This should be a value from 0 to 100. Some formats might represent
        /// this as a value from 0 to 1, however. In these cases, you can
        /// simply multiply by 100.
        /// </remarks>
        public double L { get; set; }
        /// <summary>
        /// The red/green colour-opponent dimension.
        /// </summary>
        public double A { get; set; }
        /// <summary>
        /// The yellow/blue colour-opponent dimension.
        /// </summary>
        public double B { get; set; }

        /// <summary>
        /// Creates a new colour in the L*a*b* space.
        /// </summary>
        /// <param name="l">
        /// The luminance of the colour.
        /// </param>
        /// <param name="a">
        /// The red/green colour-opponent dimension.
        /// </param>
        /// <param name="b">
        /// The yellow/blue colour-opponent dimension.
        /// </param>
        public LabColor(double l, double a, double b)
        {
            L = l;
            A = a;
            B = b;
        }

        /// <summary>
        /// Converts the colour into an XYZ representation.
        /// </summary>
        /// <returns>The XYZ representation of the colour.</returns>
        public XyzColor ToXyz()
        {
            double delta = 6.0 / 29.0;

            double fy = (L + 16) / 116.0;
            double fx = fy + (A / 500.0);
            double fz = fy - (B / 200.0);

            return new XyzColor(
                (fx > delta) ? XyzColor.D65.X * (fx * fx * fx) : (fx - 16.0 / 116.0) * 3 * (
                    delta * delta) * XyzColor.D65.X,
                (fy > delta) ? XyzColor.D65.Y * (fy * fy * fy) : (fy - 16.0 / 116.0) * 3 * (
                    delta * delta) * XyzColor.D65.Y,
                (fz > delta) ? XyzColor.D65.Z * (fz * fz * fz) : (fz - 16.0 / 116.0) * 3 * (
                    delta * delta) * XyzColor.D65.Z
                );

        }

        /// <summary>
        /// Converts the colour into an RGB representation.
        /// </summary>
        /// <returns>The RGB representation of the colour.</returns>
        public RgbColor ToRgb()
        {
            return ToXyz().ToRgb();
        }
    }
}
