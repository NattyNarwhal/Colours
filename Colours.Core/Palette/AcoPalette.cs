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
    /// Represents a color palette, using the Adobe's format as the backend.
    /// </summary>
    /// <remarks>
    /// See Adobe's documentation: 
    /// http://www.adobe.com/devnet-apps/photoshop/fileformatashtml/#50577411_pgfId-1055819
    /// </remarks>
    [DataContract]
    public class AcoPalette : IPalette
    {
        /// <summary>
        /// Gets or sets a list of colors.
        /// </summary>
        [DataMember]
        public List<PaletteColor> Colors { get; set; }

        enum ParseState
        {
            Version, Count1, Count2, Color1, Color2, Ending
        }

        static RgbColor FromPhotoshopColorV1(BinaryReader br)
        {
            var space = (AdobeColorSpace)br.ReadUInt16BE();
            
            switch (space)
            {
                case AdobeColorSpace.Rgb:
                    var red = br.ReadUInt16BE();
                    var green = br.ReadUInt16BE();
                    var blue = br.ReadUInt16BE();
                    br.ReadUInt16BE(); // nop channel
                    return new RgbColor(red, green, blue);
                case AdobeColorSpace.Lab:
                    // 0 - 10000 -> 0 - 100
                    var l = br.ReadUInt16BE() / 100;
                    // -12800 - 12700 -> -128 - 127
                    var a = br.ReadInt16BE() / 100;
                    var b = br.ReadInt16BE() / 100;
                    br.ReadUInt16BE(); // nop channel
                    return new LabColor(l, a, b).ToXyz().ToRgb();
                case AdobeColorSpace.Cmyk:
                    var cyan = 1d - br.ReadUInt16BE() / 65535d;
                    var magenta = 1d - br.ReadUInt16BE() / 65535d;
                    var yellow = 1d - br.ReadUInt16BE() / 65535d;
                    var key = 1d - br.ReadUInt16BE() / 65535d;
                    return new CmykColor(cyan, magenta, yellow, key).ToRgb();
                case AdobeColorSpace.Hsv:
                    // don't ask how I came up with this constant
                    var hue = br.ReadUInt16BE() / 182.0399d;
                    var saturation = br.ReadUInt16BE() / 65535d;
                    var value = br.ReadUInt16BE() / 65535d;
                    br.ReadUInt16BE();
                    return new HsvColor(hue, saturation, value).ToRgb();
                default:
                    throw new PaletteException(
                        string.Format("The colourspace ({0}) is unsupported.", space));
            }
        }

        private AcoPalette()
        {
            Colors = new List<PaletteColor>();
        }

        /// <summary>
        /// Creates a palette from a Photoshop swatch file.
        /// </summary>
        /// <param name="file">The file to convert from.</param>
        public AcoPalette(byte[] file)
        {
            // use two different palettes for v1 and v2 palettes,
            // and only use the winner
            var pal1 = new List<PaletteColor>();
            var pal2 = new List<PaletteColor>();

            // Photoshop's smallest column size is 16, and some
            // palettes like Visibone do use the column view.
            // pal.Columns = 16;

            ushort version = 0;
            ushort count = 0;
            var state = ParseState.Version;
            
            ushort colorPos = 0;

            using (var ms = new MemoryStream(file))
            {
                using (var sr = new BinaryReader(ms))
                {
                    while (state != ParseState.Ending)
                    {
                        switch (state)
                        {
                            case ParseState.Version:
                                version = sr.ReadUInt16BE();
                                if (version == 1)
                                    state = ParseState.Count1;
                                else if (version == 2)
                                    state = ParseState.Count2;
                                else
                                    throw new PaletteException("The version is unsupported.");
                                break;
                            case ParseState.Count1:
                                count = sr.ReadUInt16BE();
                                state = ParseState.Color1;
                                colorPos = 0;
                                break;
                            case ParseState.Color1:
                                if (colorPos++ < count)
                                {
                                    var c = FromPhotoshopColorV1(sr);
                                    pal1.Add(new PaletteColor(c));
                                }
                                else state = file.Length > ms.Position ? ParseState.Version : ParseState.Ending;
                                break;
                            case ParseState.Count2:
                                count = sr.ReadUInt16BE();
                                state = ParseState.Color2;
                                colorPos = 0;
                                break;
                            case ParseState.Color2:
                                if (colorPos++ < count)
                                {
                                    var c = FromPhotoshopColorV1(sr);
                                    var strLen = sr.ReadUInt32BE();
                                    var name = sr.ReadStringBE(Convert.ToInt32(strLen), true);
                                    pal2.Add(new PaletteColor(c, name));
                                }
                                else state = ParseState.Ending;
                                break;
                        }
                    }
                }
            }

            // because palettes with names have both v1 and v2, but v2
            // has names, only export v1 if it has more colours
            Colors = pal1.Count > pal2.Count ? pal1 : pal2;
        }

        static void ToPhotoshopColorV1(BinaryWriter bw, PaletteColor pc)
        {
            // 10 bytes: 1 ushort for type, 4 ushorts for channels

            // type
            bw.WriteUInt16BE((ushort)AdobeColorSpace.Rgb);
            // red channel
            bw.WriteUInt16BE(pc.Color.R);
            // green channel
            bw.WriteUInt16BE(pc.Color.G);
            // blue channel
            bw.WriteUInt16BE(pc.Color.B);
            // no need for fourth channel, so write 0
            bw.WriteUInt16BE(0);
        }

        static void ToPhotoshopColorV2(BinaryWriter bw, PaletteColor pc)
        {
            // write the V1 color, then the string
            ToPhotoshopColorV1(bw, pc);

            var strWithNull = pc.Name + '\0';
            // length, and then string
            bw.WriteUInt32BE(Convert.ToUInt32(strWithNull.Length));
            bw.WriteStringBE(strWithNull);
        }

        /// <summary>
        /// Creates a Photoshop palette file from this palette.
        /// </summary>
        /// <returns>The Photoshop palette file.</returns>
        public byte[] ToFile()
        {
            if (Colors.Count > ushort.MaxValue)
                throw new ArgumentOutOfRangeException("There are too many colors in the palette.");

            using (var s = new MemoryStream())
            {
                using (var sw = new BinaryWriter(s))
                {
                    // write both v1 and v2 palettes
                    var count = Convert.ToUInt16(Colors.Count);

                    // v1
                    sw.WriteUInt16BE(1);
                    sw.WriteUInt16BE(count);
                    foreach (var pc in Colors)
                        ToPhotoshopColorV1(sw, pc);

                    // v2
                    sw.WriteUInt16BE(2);
                    sw.WriteUInt16BE(count);
                    foreach (var pc in Colors)
                        ToPhotoshopColorV2(sw, pc);
                    
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
            var p = new AcoPalette();
            p.Colors = new List<PaletteColor>();
            foreach (var c in Colors)
                p.Colors.Add(c);
            return p;
        }
    }
}
