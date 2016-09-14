using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Represents the colour scheme of an X resources file.
    /// </summary>
    public class XResources
    {
        #region Colors
        const string xResources =
@"{0}*foreground: {1}
{0}*background: {2}
{0}*color0: {3}
{0}*color1: {4}
{0}*color2: {5}
{0}*color3: {6}
{0}*color4: {7}
{0}*color5: {8}
{0}*color6: {9}
{0}*color7: {10}
{0}*color8: {11}
{0}*color9: {12}
{0}*color10: {13}
{0}*color11: {14}
{0}*color12: {15}
{0}*color13: {16}
{0}*color14: {17}
{0}*color15: {18}";

        /// <summary>
        /// Black.
        /// </summary>
        public RgbColor Color0 { get; set; }
        /// <summary>
        /// Dark Red.
        /// </summary>
        public RgbColor Color1 { get; set; }
        /// <summary>
        /// Dark Green.
        /// </summary>
        public RgbColor Color2 { get; set; }
        /// <summary>
        /// Dark Yellow.
        /// </summary>
        public RgbColor Color3 { get; set; }
        /// <summary>
        /// Dark Blue.
        /// </summary>
        public RgbColor Color4 { get; set; }
        /// <summary>
        /// Dark Magenta.
        /// </summary>
        public RgbColor Color5 { get; set; }
        /// <summary>
        /// Dark Cyan.
        /// </summary>
        public RgbColor Color6 { get; set; }
        /// <summary>
        /// Light Grey.
        /// </summary>
        public RgbColor Color7 { get; set; }
        /// <summary>
        /// Dark Grey.
        /// </summary>
        public RgbColor Color8 { get; set; }
        /// <summary>
        /// Red.
        /// </summary>
        public RgbColor Color9 { get; set; }
        /// <summary>
        /// Green.
        /// </summary>
        public RgbColor Color10 { get; set; }
        /// <summary>
        /// Yellow.
        /// </summary>
        public RgbColor Color11 { get; set; }
        /// <summary>
        /// Blue
        /// </summary>
        public RgbColor Color12 { get; set; }
        /// <summary>
        /// Magenta.
        /// </summary>
        public RgbColor Color13 { get; set; }
        /// <summary>
        /// Cyan.
        /// </summary>
        public RgbColor Color14 { get; set; }
        /// <summary>
        /// White.
        /// </summary>
        public RgbColor Color15 { get; set; }
        /// <summary>
        /// The colour of text and various controls.
        /// </summary>
        public RgbColor Foreground { get; set; }
        /// <summary>
        /// The colour behind text and controls.
        /// </summary>
        public RgbColor Background { get; set; }
        #endregion

        /// <summary>
        /// When outputting, the prefix to give to properties.
        /// </summary>
        /// <remarks>
        /// xterm uses Xterm, urxvt uses URxvt.
        /// </remarks>
        public string Prefix { get; set; }

        /// <summary>
        /// Initialize an example.
        /// </summary>
        public XResources()
        {
            // Using what Arch wiki suggests as default:
            // https://wiki.archlinux.org/index.php/Color_output_in_console#X_window_system
            Color0 = new RgbColor(0, 0, 0);             // Black
            Color1 = new RgbColor(0xFF, 0x65, 0x65);    // Dark red
            Color2 = new RgbColor(0x93, 0xD4, 0x4F);    // Dark green
            Color3 = new RgbColor(0xEA, 0xB9, 0x3D);    // Dark yellow
            Color4 = new RgbColor(0x20, 0x4A, 0x87);    // Dark blue
            Color5 = new RgbColor(0xCE, 0x5C, 0x00);    // Dark magenta
            Color6 = new RgbColor(0x89, 0xB6, 0xE2);    // Dark cyan
            Color7 = new RgbColor(0xCC, 0xCC, 0xCC);    // Light grey

            Color8 = new RgbColor(0x55, 0x57, 0x53);    // Dark grey
            Color9 = new RgbColor(0xFF, 0x8D, 0x8D);    // Red
            Color10 = new RgbColor(0xC8, 0xE7, 0xA8);   // Green
            Color11 = new RgbColor(0xFF, 0xC1, 0x23);   // Yellow
            Color12 = new RgbColor(0x34, 0x65, 0xA4);   // Blue
            Color13 = new RgbColor(0xF5, 0x79, 0x00);   // Magenta
            Color14 = new RgbColor(0x46, 0xA4, 0xFF);   // Cyan
            Color15 = new RgbColor(0xFF, 0xFF, 0xFF);   // White

            Foreground = new RgbColor(0, 0, 0);
            Background = new RgbColor(0xFF, 0xFF, 0xFF);
        }

        /// <summary>
        /// Creates a segment of an X resources file with the colours used.
        /// </summary>
        /// <returns>The segment of an X resources file.</returns>
        public override string ToString()
        {
            return string.Format(xResources, Prefix, Foreground, Background,
                Color0, Color1, Color2, Color3, Color4, Color5, Color6,
                Color7, Color8, Color9, Color10, Color11, Color12,
                Color13, Color14, Color15);
        }
    }
}
