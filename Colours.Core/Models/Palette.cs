using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Represents a color in a color palette.
    /// </summary>
    public class PaletteColor
    {
        /// <summary>
        /// Gets or sets the color itself.
        /// </summary>
        public RgbColor Color { get; set; }
        /// <summary>
        /// Gets or sets the name of the color.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Creates a new color for use in a palette.
        /// </summary>
        /// <param name="color">The color itself.</param>
        /// <param name="name">The name of the color.</param>
        public PaletteColor(RgbColor color, string name)
        {
            Color = color;
            Name = name;
        }

        /// <summary>
        /// Creates a new color for use in a palette.
        /// </summary>
        /// <param name="color">The color itself.</param>
        public PaletteColor(RgbColor color)
        {
            Color = color;
            Name = "Untitled";
        }

        /// <summary>
        /// Returns the color in the form it would be in a file.
        /// </summary>
        /// <returns>The color in a "R G B[tab]Name" form.</returns>
        public override string ToString()
        {
            return String.Format("{0}\t{1}\t{2}\t{3}", Color.R, Color.G, Color.B, Name);
        }
    }

    /// <summary>
    /// Represents a color palette, using the GIMP's format as the backend.
    /// </summary>
    public class Palette
    {
        const string magic = "GIMP Palette";

        /// <summary>
        /// Gets or sets the name of the palette.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets how many columns will the palette have if it is
        /// being displayed in a grid. If the amount of columns are 0, then
        /// the grid's logic will choose how many columns it wants.
        /// </summary>
        public int Columns { get; set; }
        /// <summary>
        /// Gets or sets a list of colors.
        /// </summary>
        public List<PaletteColor> Colors { get; set; }

        /// <summary>
        /// Turns a string array into a color.
        /// </summary>
        /// <param name="channels">The channels of the color, as strings.</param>
        /// <returns>The color from string array.</returns>
        protected static RgbColor ParseColorStringSplit(string[] channels)
        {
            return new RgbColor(byte.Parse(channels[0]),
                byte.Parse(channels[1]), byte.Parse(channels[2]));
        }

        /// <summary>
        /// Creates an empty palette.
        /// </summary>
        public Palette()
        {
            Colors = new List<PaletteColor>();
            Name = "Untitled";
            Columns = 0;
        }

        /// <summary>
        /// Creates a new color palette from a GIMP palette file.
        /// </summary>
        /// <param name="file">The file itself.</param>
        public Palette(string file) :
            this(file.Split(Environment.NewLine.ToCharArray()))
        { }

        /// <summary>
        /// Creates a new color palette from a GIMP palette file.
        /// </summary>
        /// <param name="file">The file itself, as lines.</param>
        public Palette(string[] file)
        {
            if (file[0] != magic)
                throw new Exception("Palette is not in a valid format.");

            Colors = new List<PaletteColor>();

            foreach (string l in file)
            {
                if (l == magic || l.StartsWith("#")) continue;
                else if (l.StartsWith("Columns: "))
                {
                    int i = 0; // a default
                    int.TryParse(l.Remove(0, "Columns: ".Length), out i);
                    Columns = i;
                }
                else if (l.StartsWith("Name: "))
                {
                    Name = l.Remove(0, "Name: ".Length);
                }
                else
                {
                    // i guess we'll try to coax some colours out of it

                    // sometimes, tabs seperate the color from its name
                    if (l.Contains("\t"))
                    {
                        var split = l.Split("\t".ToCharArray());
                        var colorSplit = split[0].Split(" ".ToCharArray(),
                            StringSplitOptions.RemoveEmptyEntries);
                        Colors.Add(new PaletteColor(
                            ParseColorStringSplit(colorSplit), split[1]));

                    }
                    // then we'll split from spaces
                    else
                    {
                        var split = l.Split(" ".ToCharArray(), 4,
                            StringSplitOptions.RemoveEmptyEntries);
                        if (split.Length == 4) // we have a name
                            Colors.Add(new PaletteColor(
                                ParseColorStringSplit(split.Take(3).ToArray()),
                                split[3]));
                        if (split.Length == 3) // we don't have a name
                            Colors.Add(new PaletteColor(
                                ParseColorStringSplit(split.Take(3).ToArray()),
                                "Untitled"));
                        else
                            throw new Exception("Palette is not in a valid format.");
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new color palette from scratch.
        /// </summary>
        /// <param name="name">The name of the palette.</param>
        /// <param name="columns">The amount of columns.</param>
        /// <param name="colors">The list of colors.</param>
        public Palette(string name, int columns, IEnumerable<RgbColor> colors)
        {
            Name = name;
            Columns = columns;
            Colors = colors
                        .Select(c => new PaletteColor(c, "Untitled"))
                        .ToList();
        }

        /// <summary>
        /// Gets the palette as a GIMP palette file.
        /// </summary>
        /// <returns>The contents of the file.</returns>
        public override string ToString()
        {
            // make the header
            string result = String.Format("{0}{3}Name: {1}{3}Columns: {2}{3}#{3}",
                magic, Name, Columns, Environment.NewLine);
            foreach (PaletteColor c in Colors)
            {
                result += String.Format("{0}{1}",
                    c.ToString(), Environment.NewLine);
            }

            return result;
        }
    }
}
