using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Colours
{
    /// <summary>
    /// Represents a color palette, using the GIMP's format as the backend.
    /// </summary>
    [DataContract]
    public class Palette
    {
        const string magic = "GIMP Palette";

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
        public int Columns { get; set; }
        /// <summary>
        /// Gets or sets a list of colors.
        /// </summary>
        [DataMember]
        public List<PaletteColor> Colors { get; set; }

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
        /// Creates a new palette from an existing one.
        /// </summary>
        /// <param name="p">The palette to use.</param>
        public Palette(Palette p)
        {
            Name = p.Name;
            Columns = p.Columns;
            Colors = new List<PaletteColor>();
            foreach (var pc in p.Colors)
                Colors.Add(pc);
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
