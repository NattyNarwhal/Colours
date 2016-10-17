using System;
using System.Collections.Generic;
using System.IO;
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
            /// <remarks>
            /// Uses 3 channels.
            /// </remarks>
            Rgb = 0,
            /// <summary>
            /// Represents a color in the Hue/Saturation/Value space.
            /// </summary>
            /// <remarks>
            /// Uses 4 channels.
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
        }

        enum ParseState
        {
            Version, Count1, Count2, Color1, Color2, Ending
        }

        static byte ShortToByte(int s)
        {
            return (byte)(s >> 8);
        }

        static ushort ByteToShort(byte b)
        {
            // Adobe software wants it doubled, not shifted
            return (ushort)(b * 0x101);
        }

        static byte[] GetBytesUShortBE(ushort s)
        {
            return GetPartBE(BitConverter.GetBytes(s), 2);
        }

        static byte[] GetBytesInt32BE(int i)
        {
            return GetPartBE(BitConverter.GetBytes(i), 4);
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

        static byte[] GetColorStruct(byte[] full, int position)
        {
            return full.Skip(position).Take(colorStructLen).ToArray();
        }

        // TODO: use an actual struct?
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
                    throw new PaletteException(
                        string.Format("The colourspace ({0}) is unsupported.", space));
            }
        }

        /// <summary>
        /// Creates a palette from a Photoshop swatch file.
        /// </summary>
        /// <param name="file">The file to convert from.</param>
        /// <returns>The new palette.</returns>
        public static Palette FromPhotoshopPalette(byte[] file)
        {
            // use two different palettes for v1 and v2 palettes,
            // and only use the winner
            var pal1 = new Palette();
            var pal2 = new Palette();

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
                            throw new PaletteException("The version is unsupported.");
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
                            var c = FromPhotoshopColorV1(GetColorStruct(file, pos));
                            pal1.Colors.Add(new PaletteColor(c));
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
                            // TODO: this is pretty ugly
                            var c = FromPhotoshopColorV1(GetColorStruct(file, pos));
                            pos += colorStructLen;
                            int strLen = GetIntBE(file, pos) * 2;
                            pos += 4;
                            var name = new string(Encoding.BigEndianUnicode.GetChars(file, pos, strLen));
                            pos += strLen;
                            pal2.Colors.Add(new PaletteColor(c, name));
                        }
                        else state = ParseState.Ending;
                        break;
                }
            }

            // because palettes with names have both v1 and v2, but v2
            // has names, only export v1 if it has more colours
            return pal1.Colors.Count > pal2.Colors.Count
                ? pal1 : pal2;
        }

        static byte[] ToPhotoshopColorV1(PaletteColor pc)
        {
            var buf = new byte[colorStructLen];

            // default two bytes are 0, which means RGB color space

            // red channel
            Array.Copy(GetBytesUShortBE(ByteToShort(pc.Color.R)), 0, buf, 2, 2);
            // green channel
            Array.Copy(GetBytesUShortBE(ByteToShort(pc.Color.G)), 0, buf, 4, 2);
            // blue channel
            Array.Copy(GetBytesUShortBE(ByteToShort(pc.Color.B)), 0, buf, 6, 2);
            // no need for fourth channel

            return buf;
        }

        static byte[] ToPhotoshopColorV2(PaletteColor pc)
        {
            var strWithNull = pc.Name + '\0';
            var asBytes = Encoding.BigEndianUnicode.GetBytes(strWithNull);
            var lenBytes = GetBytesInt32BE(strWithNull.Length);

            var buf = new byte[colorStructLen + lenBytes.Length + asBytes.Length];
            Array.Copy(ToPhotoshopColorV1(pc), buf, colorStructLen);
            Array.Copy(lenBytes, 0, buf, colorStructLen, lenBytes.Length);
            Array.Copy(asBytes, 0, buf, colorStructLen + lenBytes.Length, asBytes.Length);

            return buf;
        }

        /// <summary>
        /// Creates a Photoshop palette from a <see cref="Palette"/>.
        /// </summary>
        /// <param name="p">The palette to convert.</param>
        /// <returns>The Photoshop palette file.</returns>
        public static byte[] ToPhotoshopPalette(Palette p)
        {
            if (p.Colors.Count > ushort.MaxValue)
                throw new ArgumentOutOfRangeException("There are too many colors in the palette.");

            using (var s = new MemoryStream())
            {
                using (var sw = new BinaryWriter(s))
                {
                    // write both v1 and v2 palettes
                    var countBytes = GetBytesUShortBE((ushort)p.Colors.Count);

                    // v1
                    var v1 = GetBytesUShortBE(1);
                    sw.Write(v1);
                    sw.Write(countBytes);
                    foreach (var pc in p.Colors)
                        sw.Write(ToPhotoshopColorV1(pc));

                    // v2
                    var v2 = GetBytesUShortBE(2);
                    sw.Write(v2);
                    sw.Write(countBytes);
                    foreach (var pc in p.Colors)
                        sw.Write(ToPhotoshopColorV2(pc));
                    
                    s.Position = 0;
                    using (var sr = new BinaryReader(s))
                    {
                        return sr.ReadBytes((int)s.Length);
                    }
                }
            }
        }
    }
}
