using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace Colours
{
    /// <summary>
    /// Represents a color palette abstraction.
    /// </summary>
    public interface IPalette
    {
        /// <summary>
        /// Gets or sets a list of colors.
        /// </summary>
        List<PaletteColor> Colors { get; set; }

        /// <summary>
        /// Outputs a byte array representing the file that can be written to.
        /// </summary>
        /// <returns>The palette as a writeable byte array.</returns>
        byte[] ToFile();
        
        /// <summary>
        /// Creates a new palette with properties identical to the old one.
        /// </summary>
        /// <remarks>
        /// This is intended for changing the properties of a palette, while
        /// preserving the old version's properties, due to changing the
        /// reference.
        /// </remarks>
        /// <returns>The new palette.</returns>
        IPalette Clone();
    }
}
