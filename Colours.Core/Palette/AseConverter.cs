using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Converts to and from the Adobe Swatch Exchange format.
    /// </summary>
    public static class AseConverter
    {
        /// <summary>
        /// Reads 4 bytes in big endian and returns an unsigned 32-bit integer.
        /// </summary>
        /// <param name="br">
        /// A <see cref="BinaryReader"/> that has a uint in big endian to read.
        /// </param>
        /// <returns>An unsigned 32-bit integer.</returns>
        private static uint ReadUInt32BE(this BinaryReader br)
        {
            var b = br.ReadBytes(4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(b);
            return BitConverter.ToUInt32(b, 0);
        }

        /// <summary>
        /// Reads 2 bytes in big endian and returns an unsigned 16-bit integer.
        /// </summary>
        /// <param name="br">
        /// A <see cref="BinaryReader"/> that has a ushort in big endian to read.
        /// </param>
        /// <returns>An unsigned 16-bit integer.</returns>
        private static ushort ReadUInt16BE(this BinaryReader br)
        {
            var b = br.ReadBytes(2);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(b);
            return BitConverter.ToUInt16(b, 0);
        }

        /// <summary>
        /// Reads 2 bytes in big endian and returns a 32-bit float.
        /// </summary>
        /// <param name="br">
        /// A <see cref="BinaryReader"/> that has a float in big endian to read.
        /// </param>
        /// <returns>A 32-bit float.</returns>
        private static float ReadSingleBE(this BinaryReader br)
        {
            var b = br.ReadBytes(4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(b);
            return BitConverter.ToSingle(b, 0);
        }

        /// <summary>
        /// Creates a palette from an Adobe Swatch Exchange file.
        /// </summary>
        /// <param name="file">The file, as an array of bytes.</param>
        /// <returns>The converted palette.</returns>
        public static Palette FromAse(byte[] file)
        {
            using (var ms = new MemoryStream(file))
            {
                using (var sr = new BinaryReader(ms))
                {
                    if (new string(sr.ReadChars(4)) != "ASEF")
                        throw new PaletteException("Not an ASE file.");
                    
                    // major, minor
                    var version = new Version(sr.ReadUInt16BE(), sr.ReadUInt16BE());
                    // should we bomb if it's not a version we accept?
                    if (version > new Version(1, 0))
                        throw new PaletteException("The version is unsupported.");

                    var count = sr.ReadUInt32BE();
                    var p = new Palette();

                    for (int i = 0; i < count; i++)
                    {
                        var type = sr.ReadUInt16BE();
                        var length = sr.ReadUInt32BE();
                        if (type == 1) // color entry
                        {
                            // UTF-16 characters are doublewide, and this only
                            // counts each character
                            var nameLen = sr.ReadUInt16BE() * 2;
                            var nameBytes = sr.ReadBytes(nameLen);
                            var name = new string(Encoding.BigEndianUnicode.GetChars(nameBytes));

                            var colorSpace = new string(sr.ReadChars(4));
                            RgbColor color;

                            switch (colorSpace)
                            {
                                case "RGB ":
                                    var r = sr.ReadSingleBE();
                                    var g = sr.ReadSingleBE();
                                    var b = sr.ReadSingleBE();

                                    var rr = Convert.ToByte(Math.Round(r * 255));
                                    var gr = Convert.ToByte(Math.Round(g * 255));
                                    var br = Convert.ToByte(Math.Round(b * 255));

                                    color = new RgbColor(rr, gr, br);
                                    break;
                                default:
                                    throw new PaletteException(
                                        string.Format("The colorspace ({0}) is unsupported.", colorSpace));
                            }

                            p.Colors.Add(new PaletteColor(color, name));

                            var colorType = sr.ReadUInt16BE();
                        }
                        else
                        {
                            // skip ahead
                            sr.ReadBytes(Convert.ToInt32(length));
                        }
                    }

                    return p;
                }
            }
        }
    }
}
