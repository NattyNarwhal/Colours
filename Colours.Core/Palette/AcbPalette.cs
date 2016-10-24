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
    /// Represents a color palette, using the Adobe's color book format as the
    /// backend.
    /// </summary>
    /// <remarks>
    /// <para>
    /// See Adobe's documentation: 
    /// http://www.adobe.com/devnet-apps/photoshop/fileformatashtml/#50577411_pgfId-1066780
    /// </para>
    /// <para>
    /// More useful information: http://magnetiq.com/pages/acb-spec/
    /// </para>
    /// </remarks>
    [DataContract]
    public class AcbPalette : INamedPalette, IBucketedPalette
    {
        const string proc = "spflproc";
        const string spot = "spflspot";

        /// <summary>
        /// Gets or sets the unique ID of the palette.
        /// </summary>
        /// <remarks>
        /// Adobe has reserved some IDs for their own palettes. Consult the
        /// format documentation for what values you shouldn't use for your
        /// own palettes.
        /// </remarks>
        [DataMember]
        public ushort ID { get; set; }
        /// <summary>
        /// Gets or sets the name of the palette.
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the prefix prepended to colour names.
        /// </summary>
        [DataMember]
        public string Prefix { get; set; }
        /// <summary>
        /// Gets or sets the postfix appended to colour names.
        /// </summary>
        [DataMember]
        public string Postfix { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <remarks>
        /// This property usually contains copyright information.
        /// </remarks>
        [DataMember]
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets how many colours will be displays per page of the
        /// book.
        /// </summary>
        [DataMember]
        public int BucketSize { get; set; }
        /// <summary>
        /// Gets or sets the index of the default selection on a page.
        /// </summary>
        /// <remarks>
        /// <para>
        /// In the scrollbar for a color book in Photoshop, this will be the
        /// color shown for that page.
        /// </para>
        /// <para>
        /// This must be less than or equal to <see cref="BucketSize"/>.
        /// </para>
        /// </remarks>
        [DataMember]
        public ushort DefaultSelection { get; set; }
        /// <summary>
        /// Gets or sets the color space of the palette.
        /// </summary>
        /// <remarks>
        /// Only <see cref="AdobeColorSpace.Cmyk"/>,
        /// <see cref="AdobeColorSpace.Lab"/>, and
        /// <see cref="AdobeColorSpace.Rgb"/> are allowed.
        /// </remarks>
        [DataMember]
        public AdobeColorSpace ColorSpace { get; set; }
        /// <summary>
        /// Gets or sets a list of colors.
        /// </summary>
        [DataMember]
        public List<PaletteColor> Colors { get; set; }
        /// <summary>
        /// Gets or sets if this palette is for spot or process.
        /// </summary>
        [DataMember]
        public AcbPurpose Purpose { get; set; }

        /// <summary>
        /// Strings can have escape sequences for IP markings.
        /// </summary>
        /// <param name="raw">The string to unescape.</param>
        /// <returns>The unescaped string.</returns>
        static string UnescapeString(string raw)
        {
            return raw.Replace('\u00ae', '®').Replace('\u00a9', '©');
        }

        static string GetValue(string raw)
        {
            return raw.Split('=')[1];
        }

        AcbPalette()
        {
            Colors = new List<PaletteColor>();
        }

        /// <summary>
        /// Creates a palette from a Photoshop color book file.
        /// </summary>
        /// <param name="file">The file to convert from.</param>
        public AcbPalette(byte[] file) : this()
        {
            using (var ms = new MemoryStream(file))
            {
                using (var sr = new BinaryReader(ms))
                {
                    if (new string(sr.ReadChars(4)) != "8BCB")
                        throw new PaletteException("Not a valid color book.");
                    var version = sr.ReadUInt16BE();
                    ID = sr.ReadUInt16BE();

                    Name = GetValue(sr.ReadStringBE(false));
                    Prefix = GetValue(sr.ReadStringBE(false));
                    Postfix = GetValue(sr.ReadStringBE(false));
                    Description = UnescapeString(GetValue(sr.ReadStringBE(false)));

                    var count = sr.ReadUInt16BE();
                    BucketSize = sr.ReadUInt16BE();
                    DefaultSelection = sr.ReadUInt16BE();
                    ColorSpace = (AdobeColorSpace)sr.ReadUInt16BE();

                    for (int i = 0; i < count; i++)
                    {
                        var name = sr.ReadStringBE(true);

                        // TODO: this needs to be associated with the PaletteColor
                        var cid = new string(sr.ReadChars(6));

                        RgbColor color;
                        switch (ColorSpace)
                        {
                            case AdobeColorSpace.Rgb:
                                color = new RgbColor(sr.ReadByte(),
                                    sr.ReadByte(), sr.ReadByte());
                                break;
                            // For lab and cmyk, should we add 0.5?
                            case AdobeColorSpace.Lab:
                                var l = sr.ReadByte() / 2.55d + 0.5d;
                                var a = sr.ReadByte() - 128;
                                var b = sr.ReadByte() - 128;
                                color = new LabColor(l, a, b).ToXyz().ToRgb();
                                break;
                            case AdobeColorSpace.Cmyk:
                                var c = 1 - (sr.ReadByte() / 255d);
                                var m = 1 - sr.ReadByte() / 255d;
                                var y = 1 - sr.ReadByte() / 255d;
                                var k = 1 - sr.ReadByte() / 255d;
                                color = new CmykColor(c, m, y, k).ToRgb();
                                break;
                            default:
                                throw new PaletteException(string.Format(
                                    "Invalid colorspace. {0}", ColorSpace));
                        }

                        Colors.Add(new PaletteColor(color, name));
                    }

                    if (ms.Length > ms.Position)
                    {
                        var purpose = new string(sr.ReadChars(8));
                        switch (purpose)
                        {
                            case spot:
                                Purpose = AcbPurpose.Spot;
                                break;
                            case proc:
                                Purpose = AcbPurpose.Process;
                                break;
                            default:
                                Purpose = AcbPurpose.Unknown;
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Outputs a byte array representing the file that can be written to.
        /// </summary>
        /// <returns>The palette as a writeable byte array.</returns>
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
            var p = new AcbPalette();
            p.ID = ID;
            p.Name = Name;
            p.Prefix = Prefix;
            p.Postfix = Postfix;
            p.Description = Description;
            p.BucketSize = BucketSize;
            p.DefaultSelection = DefaultSelection;
            p.ColorSpace = ColorSpace;
            p.Purpose = Purpose;
            p.Colors = new List<PaletteColor>();
            // simply copying the list doesn't make it deep but a fill
            // deep copy would make the colors different; not desirable
            foreach (var pc in Colors)
                p.Colors.Add(pc);
            return p;
        }
    }
}
