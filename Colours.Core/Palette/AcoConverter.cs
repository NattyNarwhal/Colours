using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Converts to and from <see cref="Palette"/> and Photoshop
    /// swatches. (ACO files)
    /// </summary>
    /// <remarks>
    /// See Adobe's documentation: 
    /// http://www.adobe.com/devnet-apps/photoshop/fileformatashtml/#50577411_pgfId-1055819
    /// </remarks>
    public static class AcoConverter
    {
        const int colorStructLen = 10;

        enum ColorSpace
        {
            /// <summary>
            /// Represents a color in the Red/Green/Blue space.
            /// </summary>
            Rgb = 0,
            /// <summary>
            /// Represents a color in the Hue/Saturation/Value space.
            /// </summary>
            Hsv = 1,
            /// <summary>
            /// Represents a color in the Cyan/Yellow/Magenta space.
            /// </summary>
            Cmyk = 2,
            /// <summary>
            /// Represents a color in the Lightness/A Chroma/B Chroma space.
            /// </summary>
            Lab = 7,
            /// <summary>
            /// Represents a greyscale color.
            /// </summary>
            Grey = 8,
        }

        enum ParseState
        {
            Version, Count1, Count2, Color1, Color2, Ending
        }

        static byte ShortToByte(int s)
        {
            return (byte)(s >> 8);
        }

        static byte[] GetPartBE(byte[] b, int length, int position = 0)
        {
            return BitConverter.IsLittleEndian ?
                b.Skip(position).Take(length).Reverse().ToArray() :
                b.Skip(position).Take(length).ToArray();
        }

        static ushort GetUShortBE(byte[] b, int position = 0)
        {
            return BitConverter.ToUInt16(GetPartBE(b, 2, position), 0);
        }

        static uint GetUIntBE(byte[] b, int position = 0)
        {
            return BitConverter.ToUInt16(GetPartBE(b, 4, position), 0);
        }

        static int GetIntBE(byte[] b, int position = 0)
        {
            return BitConverter.ToInt16(GetPartBE(b, 4, position), 0);
        }

        /// <summary>
        /// Creates a palette from a Photoshop swatch file.
        /// </summary>
        /// <param name="file">The file to convert from.</param>
        /// <returns>The new palette.</returns>
        public static Palette FromPhotoshopPalette(byte[] file)
        {
            var pal = new Palette();

            // Photoshop's smallest column size is 16, and some
            // palettes like Visibone do use the column view.
            // pal.Columns = 16;

            ushort version = 0;
            ushort count = 0;
            var state = ParseState.Version;

            int pos = 0;
            ushort colorPos = 0;

            while (state != ParseState.Ending)
            {
                switch (state)
                {
                    case ParseState.Version:
                        version = GetUShortBE(file, pos);
                        pos += 2;
                        if (version == 1)
                            state = ParseState.Count1;
                        else if (version == 2)
                            state = ParseState.Count2;
                        else
                            throw new ArgumentOutOfRangeException("The version is unsupported.");
                        break;
                    case ParseState.Count1:
                        count = GetUShortBE(file, pos);
                        colorPos = 0;
                        pos += 2;
                        state = ParseState.Color1;
                        break;
                    case ParseState.Color1:
                        if (colorPos++ < count)
                        {
                            var c = FromPhotoshopColorV1(file.Skip(pos).Take(10).ToArray());
                            pal.Colors.Add(new PaletteColor(c));
                            pos += colorStructLen;
                        }
                        else state = file.Length > pos ? ParseState.Version : ParseState.Ending;
                        break;
                    case ParseState.Count2:
                        count = GetUShortBE(file, pos);
                        colorPos = 0;
                        pos += 2;
                        state = ParseState.Color2;
                        break;
                    case ParseState.Color2:
                        if (colorPos++ < count)
                        {
                            var c = FromPhotoshopColorV1(file.Skip(pos).Take(10).ToArray());
                            pos += colorStructLen;
                            int strLen = GetIntBE(file, pos) * 2;
                            pos += 4;
                            c.Name = new string(Encoding.BigEndianUnicode.GetChars(file, pos, strLen));
                            pos += strLen;
                            pal.Colors.Add(c);
                        }
                        else state = ParseState.Ending;
                        break;
                }
            }

            return pal;
        }

        static RgbColor FromPhotoshopColorV1(byte[] color)
        {
            var space = (ColorSpace)GetUShortBE(color);
            switch (space)
            {
                case ColorSpace.Rgb:
                    return new RgbColor(
                            ShortToByte(GetUShortBE(color, 2)),
                            ShortToByte(GetUShortBE(color, 4)),
                            ShortToByte(GetUShortBE(color, 6))
                        );
                default:
                    throw new NotImplementedException("The colourspace is unsupported.");
            }
        }
    }
}
