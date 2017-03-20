using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Represents a Paint Shop Pro palette.
    /// </summary>
    [DataContract]
    public class PspPalette : IPalette
    {
        const string magic = "JASC-PAL";
        const string itemRegex = @"(\d{1,3})\s(\d{1,3})\s(\d{1,3})";

        /// <summary>
        /// Gets or sets a list of colors.
        /// </summary>
        [DataMember]
        public List<PaletteColor> Colors { get; set; }

        PspPalette()
        {
            Colors = new List<PaletteColor>();
        }

        /// <summary>
        /// Creates a new Paint Shop Pro palette from a file.
        /// </summary>
        /// <param name="file">The file, split up into lines.</param>
        public PspPalette(string[] file) : this()
        {
            if (file[0] != magic && file[1] != "0100")
                throw new PaletteException("Palette is not in a valid format.");
            //skip count (item 2)
            for (int i = 3; i < file.Length; i++)
            {
                if (Regex.IsMatch(file[i], itemRegex))
                {
                    var matches = Regex.Match(file[i], itemRegex);
                    int r, g, b;
                    int.TryParse(matches.Groups[1].Value, out r);
                    int.TryParse(matches.Groups[2].Value, out g);
                    int.TryParse(matches.Groups[3].Value, out b);
                    Colors.Add(new PaletteColor(new RgbColor(r, g, b, 8)));
                }
            }
        }

        /// <summary>
        /// Creates a new Paint Shop Pro palette from a file.
        /// </summary>
        /// <param name="file">The file itself.</param>
        public PspPalette(string file) :
            this(Regex.Split(file, "\r?\n"))
        { }

        /// <summary>
        /// Creates a new Paint Shop Pro palette from an existing palette.
        /// </summary>
        /// <param name="p">The palette to convert.</param>
        public PspPalette(IPalette p) : this()
        {
            foreach (var pc in Colors)
                p.Colors.Add(pc);
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
            var p = new PspPalette();
            foreach (var pc in Colors)
                p.Colors.Add(pc);
            return p;
        }

        /// <summary>
        /// Gets the palette as a Paint Shop pro palette file.
        /// </summary>
        /// <returns>The contents of the file as a byte array.</returns>
        public byte[] ToFile()
        {
            var sb = new StringBuilder();
            sb.AppendLine(magic); //magic
            sb.AppendLine("0100"); // version?
            sb.AppendLine(Colors.Count.ToString()); // count?
            foreach (var pc in Colors)
            {
                var rgb = pc.Color.ToRgb();
                sb.AppendFormat("{0} {1} {3}{4}",
                    rgb.R8, rgb.G8, rgb.B8, Environment.NewLine);
            }
            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}
