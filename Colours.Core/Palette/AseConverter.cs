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

        // be explicit with write types

        /// <summary>
        /// Writes an unsigned 16-bit integer to the stream, guaranteeing big
        /// endian format.
        /// </summary>
        /// <param name="bw">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="u16">The ushort to write as big endian.</param>
        private static void WriteUInt16BE(this BinaryWriter bw, ushort u16)
        {
            var b = BitConverter.GetBytes(u16);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(b);
            bw.Write(b);
        }

        /// <summary>
        /// Writes an unsigned 32-bit integer to the stream, guaranteeing big
        /// endian format.
        /// </summary>
        /// <param name="bw">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="u32">The uint to write as big endian.</param>
        private static void WriteUInt32BE(this BinaryWriter bw, uint u32)
        {
            var b = BitConverter.GetBytes(u32);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(b);
            bw.Write(b);
        }

        /// <summary>
        /// Writes a 32-bit float to the stream, guaranteeing big endian format.
        /// </summary>
        /// <param name="bw">The <see cref="BinaryWriter"/> to write to.</param>
        /// <param name="f32">The float to write as big endian.</param>
        private static void WriteSingleBE(this BinaryWriter bw, float f32)
        {
            var b = BitConverter.GetBytes(f32);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(b);
            bw.Write(b);
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

                            // .net strings don't have nulls at the end
                            if (name.EndsWith("\0"))
                                name = name.TrimEnd('\0');

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
        
        /// <summary>
        /// Creates an Adobe Swatch Exchange file from a palette.
        /// </summary>
        /// <param name="p">The palette to convert.</param>
        /// <returns>The ASE file, as a byte array.</returns>
        public static byte[] ToAse(Palette p)
        {
            using (var ms = new MemoryStream())
            {
                using (var sw = new BinaryWriter(ms))
                {
                    // magic
                    // don't write as a string, Write has too much magic
                    sw.Write(new char[] { 'A', 'S', 'E', 'F' });

                    // version
                    sw.Write(new byte[] { 0, 1, 0, 0 });

                    // count
                    sw.WriteUInt32BE(Convert.ToUInt32(p.Colors.Count));
                    foreach (var pc in p.Colors)
                    {
                        // block is color
                        sw.WriteUInt16BE(1);

                        // as the length must be written first, make the
                        // bytestream and then write its length, then it

                        // length can only be a ushort once written
                        using (var cms = new MemoryStream())
                        {
                            using (var csw = new BinaryWriter(cms))
                            {
                                // write length, then name
                                // names need to be null terminated
                                // lengths by 16-bit chars, not bytes
                                var name = pc.Name + '\0';
                                csw.WriteUInt16BE(Convert.ToUInt16(name.Length));
                                csw.Write(Encoding.BigEndianUnicode.GetBytes(name));

                                // write RGB color
                                csw.Write(new char[] { 'R', 'G', 'B', ' ' });
                                // the FP precision seems to be different than
                                // Adobe's, but for 8-bit values it generates
                                // close enough to be the same
                                csw.WriteSingleBE(pc.Color.R / 255f);
                                csw.WriteSingleBE(pc.Color.G / 255f);
                                csw.WriteSingleBE(pc.Color.B / 255f);

                                // write length then chunk
                                long len = cms.Length;
                                sw.WriteUInt32BE(Convert.ToUInt32(len));
                                cms.Position = 0;
                                using (var csr = new BinaryReader(cms))
                                {
                                    var chunk = csr.ReadBytes(Convert.ToInt32(len));
                                    sw.Write(chunk);
                                }
                            }
                        }
                        
                        // color type (don't know what this is for?)
                        sw.WriteUInt16BE(0);
                    }

                    ms.Position = 0;
                    using (var sr = new BinaryReader(ms))
                    {
                        return sr.ReadBytes((int)ms.Length);
                    }
                }
            }
        }
    }
}
