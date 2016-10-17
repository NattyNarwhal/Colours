using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Ways to sort a palette by.
    /// </summary>
    public enum PaletteSortBy
    {
        /// <summary>
        /// Sort colors by their names, alphabetically.
        /// </summary>
        Name,
        /// <summary>
        /// Sort colors by their position on the color wheel.
        /// </summary>
        Hue,
        /// <summary>
        /// Sort colors by their saturation.
        /// </summary>
        Saturation,
        /// <summary>
        /// Sort colors by their brightness/value.
        /// </summary>
        Brightness
    }
}
