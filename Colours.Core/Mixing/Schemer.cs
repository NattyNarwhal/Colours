using System.Linq;
using System.Collections.Generic;

namespace Colours
{
    /// <summary>
    /// This enum corresponds to functions in the <see cref="ColorSchemer"/> object.
    /// </summary>
    public enum SchemeType
    {
        /// <summary>
        /// The color on the other side of the color wheel.
        /// </summary>
        Complement,
        /// <summary>
        /// The colors adjacent to the other side of the color wheel.
        /// </summary>
        SplitComplements,
        /// <summary>
        /// Two colors equally distant from each other.
        /// </summary>
        Triads,
        /// <summary>
        /// Three colors equally distant from each other.
        /// </summary>
        Tetrads,
        /// <summary>
        /// Three colors, one opposite of the color, and two adjacent to the color and its opposite.
        /// </summary>
        RectangleTetrads,
        /// <summary>
        /// Two colors adjcanent.
        /// </summary>
        Analogous,
        /// <summary>
        /// Two colors, darker and lighter.
        /// </summary>
        Monochromatic
    }

    /// <summary>
    /// Represents a colour scheme with type and name, frontends with human
    /// readable descriptions.
    /// </summary>
    /// <remarks>
    /// Primarily used for System.Windows.Forms combo boxes.
    /// </remarks>
    // TODO: does this belong in Core, or in each frontend?
    public class Scheme
    {
        /// <summary>
        /// The type of scheme this is.
        /// </summary>
        public SchemeType Type { get; set; }
        /// <summary>
        /// The name of the scheme.
        /// </summary>
        public string Name { get; set; }

        internal Scheme(SchemeType type, string name)
        {
            Type = type;
            Name = name;
        }

        /// <summary>
        /// Prints the name.
        /// </summary>
        /// <returns>The name</returns>
        /// <remarks>
        /// Again, mainly for ComboBox.
        /// </remarks>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Gets a list of available schemes.
        /// </summary>
        /// <returns>The list of all schemes supported.</returns>
        public static IEnumerable<Scheme> GetSchemes()
        {
            foreach (var i in SchemeNames)
            {
                yield return new Scheme(i.Key, i.Value);
            }
        }

        static Dictionary<SchemeType, string> SchemeNames =
            new Dictionary<SchemeType, string>()
            {
                // TODO: localizability here?
                { SchemeType.Complement, "Complement" },
                { SchemeType.SplitComplements, "Split Complements" },
                { SchemeType.Triads, "Triads" },
                { SchemeType.Tetrads, "Square Tetrads" },
                { SchemeType.RectangleTetrads, "Rectangular Tetrads" },
                { SchemeType.Analogous, "Analogous" },
                { SchemeType.Monochromatic, "Monochromatic" },
            };
    }

    /// <summary>
    /// This object contains functions to generate colour schemes. For
    /// application developers, the <see cref="App.AppController"/> object
    /// will call this for you as needed, using the <see cref="SchemeType"/>
    /// enum to choose which object is called.
    /// </summary>
    public static class ColorSchemer
    {
        

        /// <summary>
        /// Gets the color on the other side of the color wheel.
        /// </summary>
        /// <param name="c">The colour to generate the scheme from.</param>
        /// <returns>The results of the scheme, including the previous color.</returns>
        public static List<HsvColor> Complement(HsvColor c)
        {
            return new List<HsvColor>() { c, new HsvColor(c.Hue + (360 / 2), c.Saturation, c.Value) };
        }

        /// <summary>
        /// Gets 2 colors close to the opposite side of the color wheel.
        /// </summary>
        /// <param name="c">The colour to generate the scheme from.</param>
        /// <returns>The results of the scheme, including the previous color.</returns>
        public static List<HsvColor> SplitComplement(HsvColor c)
        {
            const double offset = 360 / 15;

            HsvColor c1 = new HsvColor(c.Hue + (360 / 2) - offset, c.Saturation, c.Value);
            HsvColor c2 = new HsvColor(c.Hue + (360 / 2) + offset, c.Saturation, c.Value);

            return new List<HsvColor>() { c, c1, c2 };
        }

        /// <summary>
        /// Gets 3 colors equally distant from each other on the color wheel.
        /// </summary>
        /// <param name="c">The colour to generate the scheme from.</param>
        /// <returns>The results of the scheme, including the previous color.</returns>
        public static List<HsvColor> Tetrads(HsvColor c)
        {
            const double offset = 360 / 4;

            HsvColor c1 = new HsvColor(c.Hue + offset, c.Saturation, c.Value);
            HsvColor c2 = new HsvColor(c.Hue + 360 / 2, c.Saturation, c.Value);
            HsvColor c3 = new HsvColor(c.Hue + 360 / 2 + offset, c.Saturation, c.Value);

            return new List<HsvColor>() { c, c1, c2, c3 };
        }

        /// <summary>
        /// Gets 2 colors close by to the color on the color wheel.
        /// </summary>
        /// <param name="c">The colour to generate the scheme from.</param>
        /// <returns>The results of the scheme, including the previous color.</returns>
        public static List<HsvColor> Analogous(HsvColor c)
        {
            const double offset = 360 / 12;

            HsvColor c1 = new HsvColor(c.Hue - offset, c.Saturation, c.Value);
            HsvColor c2 = new HsvColor(c.Hue + offset, c.Saturation, c.Value);

            return new List<HsvColor>() { c, c1, c2 };
        }

        /// <summary>
        /// Gets 2 colors equally distant from each other on the color wheel.
        /// </summary>
        /// <param name="c">The colour to generate the scheme from.</param>
        /// <returns>The results of the scheme, including the previous color.</returns>
        public static List<HsvColor> Triads(HsvColor c)
        {
            const double offset = 360 / 3;

            HsvColor c1 = new HsvColor(c.Hue - offset, c.Saturation, c.Value);
            HsvColor c2 = new HsvColor(c.Hue + offset, c.Saturation, c.Value);

            return new List<HsvColor>() { c, c1, c2 };
        }

        /// <summary>
        /// Gets 3 colors, one opposite of the color, and two adjacent to the color and its opposite.
        /// </summary>
        /// <param name="c">The colour to generate the scheme from.</param>
        /// <returns>The results of the scheme, including the previous color.</returns>
        public static List<HsvColor> RectangleTetrads(HsvColor c)
        {
            HsvColor c1 = new HsvColor(c.Hue + (360 / 2), c.Saturation, c.Value);

            const double offset = 360 / 3;

            HsvColor c2 = new HsvColor(c.Hue - offset, c.Saturation, c.Value);
            HsvColor c3 = new HsvColor(c1.Hue - offset, c.Saturation, c.Value);

            return new List<HsvColor>() { c, c1, c2, c3 };
        }

        /// <summary>
        /// Gets 2 colors with offset saturation or values, rather than
        /// offset hues, and then sorts them by their luminance.
        /// </summary>
        /// <param name="c">The colour to generate the scheme from.</param>
        /// <returns>The results of the scheme, including the previous color.</returns>
        public static List<HsvColor> Monochromatic(HsvColor c)
        {
            HsvColor c1, c2;

            if (c.Saturation < 0.1d)
            {
                c1 = new HsvColor(c.Hue, (c.Saturation + (1d / 3d)) % 1d, c.Value);
                c2 = new HsvColor(c.Hue, (c.Saturation + 2d * (1d / 3d)) % 1d, c.Value);
            }
            else
            {
                c1 = new HsvColor(c.Hue, c.Saturation, (c.Value + (1d / 3d)) % 1d);
                c2 = new HsvColor(c.Hue, c.Saturation, (c.Value + 2d * (1d / 3d)) % 1d);
            }

            return new List<HsvColor>() { c, c1, c2 }
                .OrderBy(x => x.Value).Reverse().ToList();
        }
    }
}
