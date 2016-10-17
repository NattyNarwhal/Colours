using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// A class to demarcate exceptions related to palettes, such as parsing.
    /// </summary>
    public class PaletteException : Exception
    {
        /// <summary>
        /// Creates an exception to throw.
        /// </summary>
        /// <param name="message">The message to include.</param>
        public PaletteException(string message) : base(message)
        {
        }
    }
}
