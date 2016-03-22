using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Colours
{
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

        public static string GenerateTable(List<HsvColor> colors)
        {
            return String.Join("", colors.Select(c =>
                String.Format(table,
                    ColorTranslator.ToHtml(c.ToRgb()),
                    c.ToRgb().ToRgbString(),
                    c.ToRgb().ToHslString(),
                    c.ToString()
                    )
                )
            );
        }

        public static string GeneratePage(string title, string body)
        {
            return String.Format(page, title, body);
        }
    }
}
