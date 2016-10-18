using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Utility function library for math.
    /// </summary>
    public static class MathUtils
    {
        /// <summary>
        /// Clamps a value to fall within the minimum or maximum.
        /// </summary>
        /// <typeparam name="T">The type of the value to clamp.</typeparam>
        /// <param name="val">The value to clamp.</param>
        /// <param name="min">The minimum boundary.</param>
        /// <param name="max">The maximum boundary.</param>
        /// <returns>The clamped value.</returns>
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
    }
}
