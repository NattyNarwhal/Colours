using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Converts to and from <see cref="Palette"/> and Photoshop tables.
    /// (ACT files)
    /// </summary>
    /// <remarks>
    /// See Adobe's documentation: 
    /// http://www.adobe.com/devnet-apps/photoshop/fileformatashtml/#50577411_pgfId-1070626
    /// </remarks>
    public static class ActConverter
    {
        /// <summary>
        /// Creates a palette from a colour table.
        /// </summary>
        /// <param name="file">The file as a bytestream.</param>
        /// <returns>A palette from the table</returns>
        /// <remarks>
        /// Note that the resulting color can be truncated by a directive
        /// after the first 768 bytes.
        /// </remarks>
        public static Palette FromTable(byte[] file)
        {
            var p = new Palette() { Name = "Imported from Photoshop" };

            if (file.Length > 772 || file.Length < 768)
                throw new InvalidDataException("Not a valid colour table length.");

            var metadata = file.Length == 772;
            using (var s = new MemoryStream(file))
            {
                using (var sr = new BinaryReader(s))
                {
                    for (int i = 0; i < 256; i++)
                        p.Colors.Add(
                            new PaletteColor(
                                new RgbColor(sr.ReadByte(), sr.ReadByte(), sr.ReadByte())
                                )
                                );

                    if (metadata)
                    {
                        var truncateRaw = sr.ReadBytes(2);
                        // Photoshop values are BE
                        if (BitConverter.IsLittleEndian)
                            truncateRaw = truncateRaw.Reverse().ToArray();
                        var truncateTo = BitConverter.ToUInt16(truncateRaw, 0);

                        p.Colors = p.Colors.Take(truncateTo).ToList();

                        // we don't support transparency indices
                    }
                }
            }

            return p;
        }
    }
}
