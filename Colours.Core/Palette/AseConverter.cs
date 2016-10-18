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
                            var nameLen = sr.ReadUInt16BE();
                            var name = sr.ReadStringBE(nameLen, true);

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
                                csw.WriteStringBE(name);

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
