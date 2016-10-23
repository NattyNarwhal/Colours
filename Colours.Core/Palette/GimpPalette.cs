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
    /// Represents a color palette, using the GIMP's format as the backend.
    /// </summary>
    [DataContract]
    public class GimpPalette : INamedPalette, IBucketedPalette
    {
        const string magic = "GIMP Palette";

        const string nameRegex = "^Name: ?(.*)";
        const string columnsRegex = @"^Columns: ?(\d*)";
        // Hex codes as colour names would otherwise confuse
        const string commentRegex = @"^#\s*(.*)?";

        /// <summary>
        /// Gets or sets the name of the palette.
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets how many columns will the palette have if it is
        /// being displayed in a grid. If the amount of columns are 0, then
        /// the grid's logic will choose how many columns it wants.
        /// </summary>
        [DataMember]
        public int BucketSize { get; set; }
        /// <summary>
        /// Gets or sets a list of colors.
        /// </summary>
        [DataMember]
        public List<PaletteColor> Colors { get; set; }
        /// <summary>
        /// Gets or sets comments in a palette.
        /// </summary>
        [DataMember]
        public List<string> Comments { get; set; }

        /// <summary>
        /// Creates an empty palette.
        /// </summary>
        public GimpPalette()
        {
            Colors = new List<PaletteColor>();
            Name = "Untitled";
            Comments = new List<string>();
            BucketSize = 0;
        }

        /// <summary>
        /// Creates a new color palette from a GIMP palette file.
        /// </summary>
        /// <param name="file">The file itself.</param>
        public GimpPalette(string file) :
            this(Regex.Split(file, "\r?\n"))
        { }

        /// <summary>
        /// Creates a new color palette from a GIMP palette file.
        /// </summary>
        /// <param name="file">The file itself, as lines.</param>
        public GimpPalette(string[] file)
        {
            if (file[0] != magic)
                throw new PaletteException("Palette is not in a valid format.");

            Colors = new List<PaletteColor>();
            Comments = new List<string>();

            foreach (string l in file)
            {
                if (l == magic) continue;
                if (Regex.IsMatch(l, commentRegex))
                {
                    // empty matches (like just a #) will give a string.Empty
                    Comments.Add(Regex.Match(l, commentRegex).Groups[1].Value);
                }
                else if (Regex.IsMatch(l, columnsRegex))
                {
                    int i = 0; // a default
                    int.TryParse(Regex.Match(l,
                        columnsRegex).Groups[1].Value, out i);
                    BucketSize = i;
                }
                else if (Regex.IsMatch(l, nameRegex))
                {
                    Name = Regex.Match(l, nameRegex).Groups[1].Value;
                }
                else if (PaletteColor.IsPaletteColorString(l))
                {
                    // i guess we'll try to coax some colours out of it
                    Colors.Add(new PaletteColor(l));
                }
            }
        }

        /// <summary>
        /// Creates a new color palette from scratch.
        /// </summary>
        /// <param name="name">The name of the palette.</param>
        /// <param name="columns">The amount of columns.</param>
        /// <param name="colors">The list of colors.</param>
        public GimpPalette(string name, int columns, IEnumerable<RgbColor> colors)
        {
            Name = name;
            BucketSize = columns;
            Colors = colors
                        .Select(c => new PaletteColor(c, "Untitled"))
                        .ToList();
        }

        /// <summary>
        /// Creates a new color palette from scratch.
        /// </summary>
        /// <param name="name">The name of the palette.</param>
        /// <param name="columns">The amount of columns.</param>
        /// <param name="comments">The list of comments.</param>
        /// <param name="colors">The list of colors.</param>
        public GimpPalette(string name, int columns, IEnumerable<string> comments, IEnumerable<RgbColor> colors)
            : this(name, columns, colors)
        {
            Comments = comments.ToList();
        }

        /// <summary>
        /// Gets the palette as a GIMP palette file.
        /// </summary>
        /// <returns>The contents of the file.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            // make the header
            sb.AppendLine(magic);
            sb.AppendLine(String.Format("Name: {0}", Name));
            sb.AppendLine(String.Format("Columns: {0}", BucketSize));

            if (Comments.Count > 0)
            {
                foreach (var c in Comments)
                    sb.AppendLine(String.Format("# {0}", c));
            }
            else sb.AppendLine("#"); // is this needed?

            foreach (PaletteColor c in Colors)
                sb.AppendLine(c.ToString());

            return sb.ToString();
        }

        /// <summary>
        /// Gets the palette as a GIMP palette file.
        /// </summary>
        /// <returns>The contents of the file as a byte array.</returns>
        public byte[] ToFile()
        {
            return Encoding.UTF8.GetBytes(ToString());
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
            var p = new GimpPalette();
            p.Name = Name;
            p.BucketSize = BucketSize;
            p.Colors = new List<PaletteColor>();
            p.Comments = new List<string>();
            // simply copying the list doesn't make it deep but a fill
            // deep copy would make the colors different; not desirable
            foreach (var pc in Colors)
                p.Colors.Add(pc);
            foreach (var c in Comments)
                p.Comments.Add(c);
            return p;
        }
    }
}
