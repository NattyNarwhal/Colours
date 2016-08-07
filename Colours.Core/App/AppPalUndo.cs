using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours.App
{
    /// <summary>
    /// Represents the undo state of <see cref="Colours.AppPaletteController"/>.
    /// </summary>
    public class AppPalUndo
    {
        /// <summary>
        /// The palette state.
        /// </summary>
        public Palette Palette { get; set; }
        /// <summary>
        /// The name of the action that was taken.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Creates a new undo state.
        /// </summary>
        /// <param name="p">The palette.</param>
        /// <param name="n">The name of the action that was taken.</param>
        public AppPalUndo(Palette p, string n)
        {
            Palette = p;
            Name = n;
        }
    }
}