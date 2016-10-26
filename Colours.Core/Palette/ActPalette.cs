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
    /// Represents a color palette, using the Adobe's color tables format as
    /// the backend.
    /// </summary>
    /// <remarks>
    /// See Adobe's documentation: 
    /// http://www.adobe.com/devnet-apps/photoshop/fileformatashtml/#50577411_pgfId-1070626
    /// </remarks>
    [DataContract]
    public class ActPalette : IPalette
    {
        /// <summary>
        /// Gets or sets a list of colors.
        /// </summary>
        [DataMember]
        public List<PaletteColor> Colors { get; set; }

        // TODO: Transparency index support

        ActPalette()
        {
            Colors = new List<PaletteColor>();
        }

        /// <summary>
        /// Creates a palette from a colour table.
        /// </summary>
        /// <param name="file">The file as a byte array.</param>
        /// <remarks>
        /// Note that the resulting color can be truncated by a directive
        /// after the first 768 bytes.
        /// </remarks>
        public ActPalette(byte[] file) : this()
        {
            if (file.Length > 772 || file.Length < 768)
                throw new PaletteException("Not a valid colour table length.");

            var metadata = file.Length == 772;
            using (var s = new MemoryStream(file))
            {
                using (var sr = new BinaryReader(s))
                {
                    for (int i = 0; i < 256; i++)
                        Colors.Add(
                            new PaletteColor(
                                new RgbColor(sr.ReadByte(), sr.ReadByte(), sr.ReadByte())
                                )
                                );

                    if (metadata)
                    {
                        var truncateTo = sr.ReadUInt16BE();

                        Colors = Colors.Take(truncateTo).ToList();

                        // we don't support transparency indices
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new palette from an existing one.
        /// </summary>
        /// <param name="p">The palette to convert from.</param>
        public ActPalette(IPalette p) : this()
        {
            foreach (var pc in p.Colors)
                Colors.Add(pc);
        }

        /// <summary>
        /// Creates a colour table from a palette.
        /// </summary>
        /// <returns>The colour table, as a byte stream.</returns>
        /// <remarks>
        /// If the length of the palette is exactly 256, then no metadata will
        /// be appended.
        /// </remarks>
        public byte[] ToFile()
        {
            using (var s = new MemoryStream())
            {
                using (var sw = new BinaryWriter(s))
                {
                    var l = Colors.Take(256).Select(x => x.Color).ToArray();

                    const byte defaultChannel = 0xFF;
                    for (int i = 0; i < 256; i++)
                    {
                        if (l.Length <= i)
                        {
                            // when in doubt, white?
                            sw.Write(defaultChannel);
                            sw.Write(defaultChannel);
                            sw.Write(defaultChannel);
                        }
                        else
                        {
                            sw.Write(l[i].R8);
                            sw.Write(l[i].G8);
                            sw.Write(l[i].B8);
                        }
                    }

                    // if there's less than 256, truncate
                    if (l.Length != 256)
                    {
                        sw.WriteUInt16BE(Convert.ToUInt16(l.Length));

                        // we also need to worry about transparency, so give
                        // it the last one?
                        sw.Write(defaultChannel);
                        sw.Write(defaultChannel);
                    }

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
            var p = new ActPalette();
            p.Colors = new List<PaletteColor>();
            foreach (var c in Colors)
                p.Colors.Add(c);
            return p;
        }
    }
}
