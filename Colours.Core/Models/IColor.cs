using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Represents a color that can be converted to a common format.
    /// </summary>
    public interface IColor
    {
        /// <summary>
        /// Converts the color to an <see cref="RgbColor"/>.
        /// </summary>
        /// <returns>The colour in <see cref="RgbColor"/> form.</returns>
        RgbColor ToRgb();
    }
}
