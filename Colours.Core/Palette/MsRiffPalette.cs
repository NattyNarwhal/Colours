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

        /// <summary>
        /// Creates a RIFF palette from a file.
        /// </summary>
        /// <param name="file">The file to convert from.</param>
        public MsRiffPalette(byte[] file)
        {
            Colors = new List<PaletteColor>();

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
