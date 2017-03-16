using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Represents a color palette, using the Adobe's Swatch Exchange format as
    /// the backend.
    /// </summary>
    [DataContract]
    public class AsePalette : IPalette
    {
        /// <summary>
        /// Gets or sets a list of colors.
        /// </summary>
        [DataMember]
        public List<PaletteColor> Colors { get; set; }

        AsePalette()
        {
            Colors = new List<PaletteColor>();
        }

        /// <summary>
        /// Creates a palette from an Adobe Swatch Exchange file.
        /// </summary>
        /// <param name="file">The file, as an array of bytes.</param>
        /// <returns>The converted palette.</returns>
        public AsePalette(byte[] file) : this()
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

                    for (int i = 0; i < count; i++)
                    {
                        var type = sr.ReadUInt16BE();
                        var length = sr.ReadUInt32BE();
                        if (type == 1) // color entry
                        {
                            var nameLen = sr.ReadUInt16BE();
                            var name = sr.ReadStringBE(nameLen, true);

                            var colorSpace = new string(sr.ReadChars(4));
                            IColor color;

                            switch (colorSpace)
                            {
                                case "RGB ":
                                    var r = sr.ReadSingleBE();
                                    var g = sr.ReadSingleBE();
                                    // HACK: we can't use same named variable
                                    // declarations in a switch/case block, due
                                    // to C behaviour leftovers
                                    var blue = sr.ReadSingleBE();

                                    var rr = Convert.ToUInt16(Math.Round(r * 65535));
                                    var gr = Convert.ToUInt16(Math.Round(g * 65535));
                                    var br = Convert.ToUInt16(Math.Round(blue * 65535));

                                    color = new RgbColor(rr, gr, br);
                                    break;
                                case "LAB ":
                                    var l = sr.ReadSingleBE() * 100;
                                    var a = sr.ReadSingleBE();
                                    var b = sr.ReadSingleBE();

                                    color = new LabColor(l, a, b);
                                    break;
                                default:
                                    throw new PaletteException(
                                        string.Format("The colorspace ({0}) is unsupported.", colorSpace));
                            }

                            Colors.Add(new PaletteColor(color, name));

                            var colorType = sr.ReadUInt16BE();
                        }
                        else
                        {
                            // skip ahead
                            sr.ReadBytes(Convert.ToInt32(length));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new palette from an existing one.
        /// </summary>
        /// <param name="p">The palette to convert from.</param>
        public AsePalette(IPalette p) : this()
        {
            foreach (var pc in p.Colors)
                Colors.Add(pc);
        }

        /// <summary>
        /// Creates an Adobe Swatch Exchange file from a palette.
        /// </summary>
        /// <returns>The ASE file, as a byte array.</returns>
        public byte[] ToFile()
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
                    sw.WriteUInt32BE(Convert.ToUInt32(Colors.Count));
                    foreach (var pc in Colors)
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

                                // TODO: Write based on type
                                // write RGB color
                                csw.Write(new char[] { 'R', 'G', 'B', ' ' });
                                var rgb = pc.Color.ToRgb();
                                // the FP precision seems to be different than
                                // Adobe's, but for 8-bit values it generates
                                // close enough to be the same
                                csw.WriteSingleBE(rgb.R / 65535f);
                                csw.WriteSingleBE(rgb.G / 65535f);
                                csw.WriteSingleBE(rgb.B / 65535f);

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

        /// <summary>
        /// Creates a new palette with properties identical to the old one.
        /// </summary>
        /// <remarks>
        /// This is intended for changing the properties of a palette, while
        /// preserving the old version's properties, due to changing the
        /// reference.
        /// </remarks>
        /// <returns>The new palette.</returns>
        public IPalette Clone()
        {
            var p = new AsePalette();
            p.Colors = new List<PaletteColor>();
            foreach (var c in Colors)
                p.Colors.Add(c);
            return p;
        }
    }
}
