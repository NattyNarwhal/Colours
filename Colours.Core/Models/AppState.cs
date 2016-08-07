using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Represents the minimum amount of application state for the.
    /// colour schemeing component. This is also used for undo/redo.
    /// </summary>
    public class AppState
    {
        /// <summary>
        /// The colour of the state.
        /// </summary>
        public HsvColor Color { get; set; }
        /// <summary>
        /// The scheme of the state.
        /// </summary>
        public SchemeType SchemeType { get; set; }

        /// <summary>
        /// Creates a new application state representation.
        /// </summary>
        /// <param name="c">The color to use.</param>
        /// <param name="t">The scheme to use.</param>
        public AppState(HsvColor c, SchemeType t)
        {
            Color = c;
            SchemeType = t;
        }

        /// <summary>
        /// Gets a string representation of the current state.
        /// </summary>
        /// <returns>The stringified state, for example, "Tetrads of #123456."</returns>
        public override string ToString()
        {
            return String.Format("{0} of {1}", SchemeType.ToString(),
                Color.ToRgb().ToHtml());
        }
    }

    /// <summary>
    /// Represents the application state to initialize an application,
    /// including palette information.
    /// </summary>
    // TODO: this is a poor abstraction (maybe more contain an AppState)
    public class InitialAppState : AppState
    {
        /// <summary>
        /// The file name of the palette, if the frontend supports it.
        /// </summary>
        public string PaletteFileName { get; set; }

        /// <summary>
        /// The arguments that were not parsed by the universal parser,
        /// for parsing yourself.
        /// </summary>
        public List<string> UnparsedArgs { get; set; }

        /// <summary>
        /// Creates a new initial application state representation.
        /// </summary>
        /// <param name="c">The color to use.</param>
        /// <param name="t">The scheme to use.</param>
        /// <param name="p">The filename of the palette to use.</param>
        /// <param name="u">The list of unparsed arguments.</param>
        public InitialAppState(HsvColor c, SchemeType t, string p, List<string> u = null)
            : base(c, t)
        {
            PaletteFileName = p;
        }
    }
}
