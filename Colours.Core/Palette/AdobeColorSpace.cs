using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// The color space values that <see cref="AcbPalette"/> uses.
    /// </summary>
    public enum AdobeColorSpaceAcbSubset
    {
        /// <summary>
        /// Represents a color in the Red/Green/Blue space.
        /// </summary>
        /// <remarks>
        /// Uses 3 channels.
        /// </remarks>
        Rgb = 0,
        /// <summary>
        /// Represents a color in the Cyan/Yellow/Magenta space.
        /// </summary>
        /// <remarks>
        /// Uses 4 channels.
        /// </remarks>
        Cmyk = 2,
        /// <summary>
        /// Represents a color in the Lightness/A Chroma/B Chroma space.
        /// </summary>
        /// <remarks>
        /// Uses 3 channels.
        /// </remarks>
        Lab = 7,
    }

    /// <summary>
    /// The color space values that Adobe file formats use.
    /// </summary>
    public enum AdobeColorSpace
    {
        /// <summary>
        /// Represents a color in the Red/Green/Blue space.
        /// </summary>
        /// <remarks>
        /// Uses 3 channels.
        /// </remarks>
        Rgb = 0,
        /// <summary>
        /// Represents a color in the Hue/Saturation/Value space.
        /// </summary>
        /// <remarks>
        /// Uses 3 channels.
        /// </remarks>
        Hsv = 1,
        /// <summary>
        /// Represents a color in the Cyan/Yellow/Magenta space.
        /// </summary>
        /// <remarks>
        /// Uses 4 channels.
        /// </remarks>
        Cmyk = 2,
        /// <summary>
        /// Represents a color in the Pantone custom space. Undocumented.
        /// </summary>
        Pantone = 3,
        /// <summary>
        /// Represents a color in the Focoltone custom space. Undocumented.
        /// </summary>
        Focoltone = 4,
        /// <summary>
        /// Represents a color in the Trumatch custom space. Undocumented.
        /// </summary>
        Trumatch = 5,
        /// <summary>
        /// Represents a color in the Toyo 88 Colorfinder 1050 custom space. Undocumented.
        /// </summary>
        Toyo88Colorfinder1050 = 6,
        /// <summary>
        /// Represents a color in the Lightness/A Chroma/B Chroma space.
        /// </summary>
        /// <remarks>
        /// Uses 3 channels.
        /// </remarks>
        Lab = 7,
        /// <summary>
        /// Represents a greyscale color.
        /// </summary>
        /// <remarks>
        /// Uses 1 channel.
        /// </remarks>
        Grey = 8,
        /// <summary>
        /// Represents a color in the HKS custom space. Undocumented.
        /// </summary>
        HKS = 10
    }
}
