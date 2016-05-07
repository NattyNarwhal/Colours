using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Functions to blend colours togethers.
    /// </summary>
    public static class Blend
    {
        /// <summary>
        /// Blends two colours together.
        /// </summary>
        /// <param name="c1">The first colour to blend.</param>
        /// <param name="c2">The second colour to blend.</param>
        /// <param name="middle">How many colours to create.</param>
        /// <returns>A list of the colours blended.</returns>
        public static IEnumerable<RgbColor> BlendColours(RgbColor c1, RgbColor c2, int middle)
        {
            var steps = middle + 1;
            // intermediate color
            var sr = (c2.R - c1.R) / steps;
            var sg = (c2.G - c1.G) / steps;
            var sb = (c2.B - c1.B) / steps;

            for (int i = 1; i < steps; i++)
            {
                yield return new RgbColor(
                    c1.R + (sr * i),
                    c1.G + (sg * i),
                    c1.B + (sb * i)
                    );
            }
        }
    }
}
