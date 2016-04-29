using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Colours
{
    /// <summary>
    /// Represents a color in a color palette.
    /// </summary>
    [DataContract]
    public class PaletteColor
    {
        // TODO: this seems to have trouble if you don't have a name on the end
        const string matchRegex = @"(\d{1,3})\s+(\d{1,3})\s+(\d{1,3})\s+(.*)";

        /// <summary>
        /// Gets or sets the color itself.
        /// </summary>
        [DataMember]
        public RgbColor Color { get; set; }
        /// <summary>
        /// Gets or sets the name of the color.
        /// </summary>
        [DataMember]
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
        /// Creates a new color for use in a palette.
        /// </summary>
        /// <param name="l">
        /// The color in a text based form, such as "0 0 0[tab]Untitled".
        /// </param>
        public PaletteColor(string l)
        {
            // TODO: use matchRegex

            // sometimes, tabs seperate the color from its name
            if (l.Contains("\t"))
            {
                var split = l.Split("\t".ToCharArray());
                var colorSplit = split[0].Split(" ".ToCharArray(),
                    StringSplitOptions.RemoveEmptyEntries);

                Color = ParseColorStringSplit(colorSplit);
                Name = split[1];

            }
            // then we'll split from spaces
            else
            {
                var split = l.Split(" ".ToCharArray(), 4,
                    StringSplitOptions.RemoveEmptyEntries);
                if (split.Length == 4) // we have a name
                {
                    Color = ParseColorStringSplit(split.Take(3).ToArray());
                    Name = split[3];
                }
                if (split.Length == 3) // we don't have a name
                {
                    Color = ParseColorStringSplit(split.Take(3).ToArray());
                    Name = "Untitled";
                }
                else
                    throw new Exception("Palette is not in a valid format.");
            }
        }

        /// <summary>
        /// Returns the color in the form it would be in a file.
        /// </summary>
        /// <returns>The color in a "R G B[tab]Name" form.</returns>
        public override string ToString()
        {
            return String.Format("{0} {1} {2}\t{3}", Color.R, Color.G, Color.B, Name);
        }

        /// <summary>
        /// Gets if a string meets the format requirement to be
        /// convertable to a PaletteColor.
        /// </summary>
        /// <param name="pc">The string to check.</param>
        /// <returns>If the string can be converted over.</returns>
        public static bool IsPaletteColorString(string pc)
        {
            return Regex.IsMatch(pc, matchRegex);
        }

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
    }
}
