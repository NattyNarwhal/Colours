using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Represents the state needed to initialize a fully-featured application.
    /// </summary>
    public class AppInitState
    {
        /// <summary>
        /// Represents the state of the mixer, with a scheme and colour.
        /// </summary>
        public AppState MixerState { get; set; }
        /// <summary>
        /// The filename of the palette to load on initial startup.
        /// </summary>
        public string PaletteFileName { get; set; }
        /// <summary>
        /// A list of unparsed arguments for a frontend to handle.
        /// </summary>
        public List<string> UnparsedArgs { get; set; }
    }
}
