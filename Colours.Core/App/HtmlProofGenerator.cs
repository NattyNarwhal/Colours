using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colours.App
{
    // TODO: refactor to make it more powerful

    /// <summary>
    /// A class containing functions to generate a page containing a
    /// colour scheme.
    /// </summary>
    public static class HtmlProofGenerator
    {
        const string page = "<!doctype html><html>"
            + "<head><title>{0}</title> <meta name=\"generator\""
            + "content=\"https://github.com/NattyNarwhal/Colours\" /></head>"
            + "<body>{1}</body></html>";

        const string table = "<table><tr>"
            + "<td rowspan=\"4\" style=\"width: 1em; background-color: {0}\"><td>{0}</td></tr>"
            + "<tr><td>{1}</td></tr>"
            + "<tr><td>{2}</td></tr></table>";

        /// <summary>
        /// Creates tables from a color in a palette, including the
        /// various string-based forms.
        /// </summary>
        /// <param name="pc">The color.</param>
        /// <returns>The HTML tables.</returns>
        public static string GenerateTable(PaletteColor pc)
        {
            return String.Format("<h1>{0}</h1>{1}", pc.Name,
                String.Format(table,
                    pc.Color.ToHtml(),
                    pc.Color.ToHslString(),
                    new HsvColor(pc.Color).ToString()
                )
            );
        }

        /// <summary>
        /// Creates tables from a list of colours, including the
        /// various string-based forms.
        /// </summary>
        /// <param name="colors">The colors to make the tables from</param>
        /// <returns>The HTML tables.</returns>
        public static string GenerateTable(List<HsvColor> colors)
        {
            return String.Join("", colors.Select(c =>
                String.Format(table,
                    c.ToRgb().ToHtml(),
                    c.ToRgb().ToHslString(),
                    c.ToString()
                    )
                )
            );
        }

        /// <summary>
        /// Generates the page the frontend can export.
        /// </summary>
        /// <param name="title">The title of the page.</param>
        /// <param name="body">
        /// The body of the page, usually tables from
        /// <see cref="GenerateTable(List{HsvColor})"/>.
        /// </param>
        /// <returns>The HTML page.</returns>
        public static string GeneratePage(string title, string body)
        {
            return String.Format(page, title, body);
        }

        /// <summary>
        /// Generates the page the frontend can export from a palette.
        /// </summary>
        /// <param name="pal">
        /// The palette to use as a body.
        /// </param>
        /// <returns>The HTML page.</returns>
        public static string GeneratePage(Palette pal)
        {
            var comments = String.Format("<pre>{0}</pre>",
                String.Join(Environment.NewLine, pal.Comments));

            var tables = new StringBuilder();
            if (pal.Columns == 0)
                foreach (var pc in pal.Colors)
                    tables.Append(GenerateTable(pc));
            else
            {
                var pos = 0;
                tables.AppendLine("<table>");
                while (pal.Colors.Count > pos)
                {
                    tables.AppendLine("<tr>");
                    var row = pal.Colors.Skip(pos).Take(pal.Columns);
                    pos += row.Count();
                    foreach (var pc in row)
                    {
                        tables.AppendLine("<td>");
                        tables.Append(GenerateTable(pc));
                        tables.AppendLine("</td>");
                    }
                    tables.AppendLine("</tr>");
                }
                tables.AppendLine("</table>");
            }

            return String.Format(page, pal.Name, comments + tables);
        }
    }
}
