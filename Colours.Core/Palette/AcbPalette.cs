using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
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
        const string magic = "8BCB";
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
        [DataMember]
        public AdobeColorSpaceAcbSubset ColorSpace { get; set; }
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

        // strings use literal ^C/^R as an escaped form of the copyright and
        // registered symbols
        static string UnescapeString(string raw)
        {
            return Regex.Replace(raw, "\\^[CR]", m => m.Value == "^R" ? "®" : "©");
        }

        static string EscapeString(string raw)
        {
            return Regex.Replace(raw, "[®©]", m => m.Value == "®" ? "^R" : "^C");
        }

        // color books have some metadata inside of the metadata, but it can be
        // harmlessly truncated
        static string GetValue(string raw)
        {
            return raw.StartsWith("$$$/colorbook/") ? raw.Split('=')[1] : raw;
        }

        AcbPalette()
        {
            Colors = new List<PaletteColor>();
            Name = string.Empty;
            Prefix = string.Empty;
            Postfix = string.Empty;
            Description = string.Empty;
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
                    if (new string(sr.ReadChars(4)) != magic)
                        throw new PaletteException("Not a valid color book.");
                    var version = sr.ReadUInt16BE();
                    ID = sr.ReadUInt16BE();

                    Name = UnescapeString(GetValue(sr.ReadStringBE(false)));
                    Prefix = GetValue(sr.ReadStringBE(false));
                    Postfix = GetValue(sr.ReadStringBE(false));
                    Description = UnescapeString(GetValue(sr.ReadStringBE(false)));

                    var count = sr.ReadUInt16BE();
                    BucketSize = sr.ReadUInt16BE();
                    DefaultSelection = sr.ReadUInt16BE();
                    ColorSpace = (AdobeColorSpaceAcbSubset)sr.ReadUInt16BE();

                    for (int i = 0; i < count; i++)
                    {
                        var name = sr.ReadStringBE(true);

                        // TODO: this needs to be associated with the PaletteColor
                        var cid = new string(sr.ReadChars(6));
#if DEBUG
                        System.Diagnostics.Debug.WriteLine("{0}{1}{2}:{3}",
                            Prefix, name, Postfix, cid);
#endif

                        IColor color;
                        switch (ColorSpace)
                        {
                            case AdobeColorSpaceAcbSubset.Rgb:
                                color = new RgbColor(sr.ReadByte(),
                                    sr.ReadByte(), sr.ReadByte());
                                break;
                            // For lab and cmyk, should we add 0.5?
                            // (seems to be not a good idea)
                            case AdobeColorSpaceAcbSubset.Lab:
                                var l = sr.ReadByte() / 2.55d;
                                var a = sr.ReadByte() - 128;
                                var b = sr.ReadByte() - 128;
                                color = new LabColor(l, a, b);
                                break;
                            case AdobeColorSpaceAcbSubset.Cmyk:
                                var c = 1 - sr.ReadByte() / 255d;
                                var m = 1 - sr.ReadByte() / 255d;
                                var y = 1 - sr.ReadByte() / 255d;
                                var k = 1 - sr.ReadByte() / 255d;
                                color = new CmykColor(c, m, y, k);
                                break;
                            default:
                                throw new PaletteException(string.Format(
                                    "Invalid colorspace. {0}", ColorSpace));
                        }

                        Colors.Add(new PaletteColor(color, name, cid));
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
        /// Creates a new palette from an existing one.
        /// </summary>
        /// <param name="p">The palette to convert from.</param>
        public AcbPalette(IPalette p) : this()
        {
            foreach (var pc in p.Colors)
                Colors.Add(pc);
            if (p is INamedPalette)
                Name = ((INamedPalette)p).Name;
            if (p is IBucketedPalette)
                BucketSize = ((IBucketedPalette)p).BucketSize;
        }

        /// <summary>
        /// Outputs a byte array representing the file that can be written to.
        /// </summary>
        /// <returns>The palette as a writeable byte array.</returns>
        public byte[] ToFile()
        {
            using (var s = new MemoryStream())
            {
                using (var sw = new BinaryWriter(s))
                {
                    sw.Write(magic.ToCharArray());
                    sw.WriteUInt16BE(1); // version

                    sw.WriteUInt16BE(ID);

                    sw.WriteUInt32BE(Convert.ToUInt32(Name.Length));
                    sw.WriteStringBE(Name);
                    sw.WriteUInt32BE(Convert.ToUInt32(Prefix.Length));
                    sw.WriteStringBE(Prefix);
                    sw.WriteUInt32BE(Convert.ToUInt32(Postfix.Length));
                    sw.WriteStringBE(Postfix);
                    sw.WriteUInt32BE(Convert.ToUInt32(Description.Length));
                    sw.WriteStringBE(Description);

                    sw.WriteUInt16BE(Convert.ToUInt16(Colors.Count));
                    sw.WriteUInt16BE(Convert.ToUInt16(BucketSize));
                    sw.WriteUInt16BE(DefaultSelection);
                    sw.WriteUInt16BE((ushort)ColorSpace);

                    foreach (var pc in Colors)
                    {
                        sw.WriteUInt32BE(Convert.ToUInt32(pc.Name.Length));
                        sw.WriteStringBE(pc.Name);
                        // name needs to be exactly 6 chars, so pad if less,
                        // truncate if more
                        if (pc.Metadata.Length == 6)
                        {
                            sw.Write(pc.Metadata.ToCharArray());
                        }
                        else if (pc.Metadata.Length > 6)
                        {
                            sw.Write(pc.Metadata.Substring(0, 6).ToCharArray());
                        }
                        else if (pc.Metadata.Length < 6)
                        {
                            sw.Write(pc.Metadata.PadLeft(6 - pc.Metadata.Length).ToCharArray());
                        }

                        switch (ColorSpace)
                        {
                            case AdobeColorSpaceAcbSubset.Rgb:
                                var rgb = pc.Color is RgbColor ?
                                    (RgbColor)pc.Color : pc.Color.ToRgb();
                                sw.Write(rgb.R8);
                                sw.Write(rgb.G8);
                                sw.Write(rgb.B8);
                                break;
                            case AdobeColorSpaceAcbSubset.Lab:
                                var lab = pc.Color is LabColor ?
                                    (LabColor)pc.Color : pc.Color.ToRgb().ToXyz().ToLab();
                                sw.Write(Convert.ToByte(lab.L * 2.55d));
                                sw.Write(Convert.ToByte(lab.A + 128));
                                sw.Write(Convert.ToByte(lab.B + 128));
                                break;
                            case AdobeColorSpaceAcbSubset.Cmyk:
                                var cmyk = pc.Color is CmykColor ?
                                    (CmykColor)pc.Color : pc.Color.ToRgb().ToCmyk();
                                sw.Write(Convert.ToByte(255 - (cmyk.Cyan * 255d)));
                                sw.Write(Convert.ToByte(255 - (cmyk.Magenta * 255d)));
                                sw.Write(Convert.ToByte(255 - (cmyk.Yellow * 255d)));
                                sw.Write(Convert.ToByte(255 - (cmyk.Key * 255d)));
                                break;
                            default:
                                throw new PaletteException(string.Format(
                                    "Invalid colorspace. {0}", ColorSpace));
                        }
                    }

                    switch (Purpose)
                    {
                        case AcbPurpose.Process:
                            sw.Write(proc.ToCharArray());
                            break;
                        case AcbPurpose.Spot:
                            sw.Write(spot.ToCharArray());
                            break;
                        default: break;
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
