using System;
using System.Collections.Generic;
using System.Linq;

namespace Colours
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
            + "<tr><td>{2}</td></tr>"
            + "<tr><td>{3}</td></tr></table>";
        
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
                    c.ToRgb().ToRgbString(),
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
    }
}
