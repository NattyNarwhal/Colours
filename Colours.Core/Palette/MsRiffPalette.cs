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
    /// Represents a palette in RIFF format, used by legacy Microsoft software.
    /// </summary>
    [DataContract]
    public class MsRiffPalette : IPalette
    {
        /// <summary>
        /// Gets or sets a list of colors.
        /// </summary>
        [DataMember]
        public List<PaletteColor> Colors { get; set; }

        MsRiffPalette()
        {
            Colors = new List<PaletteColor>();
        }

        /// <summary>
        /// Creates a RIFF palette from an existing palette.
        /// </summary>
        /// <param name="p">The palette to convert from.</param>
        public MsRiffPalette(IPalette p) : this()
        {
            foreach (var pc in p.Colors)
                Colors.Add(pc);
        }

        /// <summary>
        /// Creates a RIFF palette from a file.
        /// </summary>
        /// <param name="file">The file to convert from.</param>
        public MsRiffPalette(byte[] file) : this()
        {
            using (var ms = new MemoryStream(file))
            {
                using (var sr = new BinaryReader(ms))
                {
                    // magic
                    if (new string(sr.ReadChars(4)) != "RIFF")
                        throw new PaletteException("Not a valid RIFF palette file.");

                    // primitive RIFF reader
                    
                    var chunkSize = sr.ReadUInt32LE();
                    var chunkName = new string(sr.ReadChars(8));
                    if (chunkName == "PAL data")
                    {
                        var subChunkSize = sr.ReadUInt32LE();
                        var version = sr.ReadUInt16LE();
                        var count = sr.ReadUInt16LE();
                        for (int i = 0; i < count; i++)
                        {
                            var r = sr.ReadByte();
                            var g = sr.ReadByte();
                            var b = sr.ReadByte();
                            // nope
                            sr.ReadByte();

                            Colors.Add(new PaletteColor(new RgbColor(r, g, b)));
                        }
                    }
                    else
                    {
                        // skip num bytes - chunk name
                        ms.Seek(chunkSize - 8, SeekOrigin.Current);
                    }
                }
            }
        }

        /// <summary>
        /// Creates a RIFF palette file from this palette.
        /// </summary>
        /// <returns>The RIFF palette file.</returns>
        public byte[] ToFile()
        {
            if (Colors.Count > ushort.MaxValue)
                throw new PaletteException("There are too many colors in the palette for this format.");

            long chunkSizePos, subChunkSizePos, chunkSize, subChunkSize;

            using (var s = new MemoryStream())
            {
                using (var sw = new BinaryWriter(s))
                {
                    // magic + size
                    sw.Write("RIFF".ToCharArray());
                    // we put 0 in here for now, we'll put the real sizes later
                    chunkSizePos = s.Position;
                    sw.WriteUInt32LE(0);
                    // subchunk magic + size (same type of placeholder as
                    // the chunk size before)
                    sw.Write("PAL data".ToCharArray());
                    subChunkSizePos = s.Position;
                    sw.WriteUInt32LE(0);
                    // structure version
                    sw.WriteUInt16LE(768);
                    // count
                    sw.WriteUInt16LE(Convert.ToUInt16(Colors.Count));

                    foreach (var pc in Colors)
                    {
                        sw.Write(pc.Color.ToRgb().R8);
                        sw.Write(pc.Color.ToRgb().G8);
                        sw.Write(pc.Color.ToRgb().B8);
                        sw.Write((byte)0);
                    }
                    
                    // now overwrite the null sizes we put w/ real ones
                    chunkSize = s.Length - 8;
                    subChunkSize = s.Length - 20;
                    s.Position = chunkSizePos;
                    sw.WriteUInt32LE(Convert.ToUInt32(chunkSize));
                    s.Position = subChunkSizePos;
                    sw.WriteUInt32LE(Convert.ToUInt32(subChunkSize));

                    s.Position = 0;
                    using (var sr = new BinaryReader(s))
                    {
                        return sr.ReadBytes((int)s.Length);
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
            var p = new MsRiffPalette();
            p.Colors = new List<PaletteColor>();
            foreach (var c in Colors)
                p.Colors.Add(c);
            return p;
        }
    }
}
