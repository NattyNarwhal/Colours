using System;
using System.Collections.Generic;

namespace Colours
{
    /// <summary>
    /// Provides a unniversal getopt-like, special-purpose command-line
    /// argument parser for platforms that support command line arguments.
    /// </summary>
    public static class AppArgParser
    {
        private enum ParsingMode
        {
            Normal, Scheme, Color, PaletteFile
        }

        /// <summary>
        /// Parses the command line given to it.
        /// </summary>
        /// <param name="args">
        /// The arguments given to the program. This is usually found in
        /// the Main function.
        /// </param>
        /// <param name="defaultColor">
        /// The color to use if none was found in the arguments. Note that
        /// this can be one you saved in a configuration file, for example.
        /// </param>
        /// <param name="defaultScheme">
        /// The scheme to use if none was found in the arguments. Note that
        /// this can be one you saved in a configuration file, for example.
        /// </param>
        /// <param name="initialPaletteFile">
        /// The palette file to use if none was found in the arguments. If
        /// the frontend doesn't support this, it will be ignored anyways.
        /// </param>
        /// <returns>The application state the frontend will load.</returns>
        public static InitialAppState ParseArgs(string[] args,
            HsvColor defaultColor, SchemeType defaultScheme, string initialPaletteFile = null)
        {
            HsvColor c = defaultColor;
            SchemeType t = defaultScheme;
            string p = initialPaletteFile;
            List<string> u = new List<string>();

            ParsingMode m = ParsingMode.Normal;
            foreach (string a in args)
            {
                switch (m)
                {
                    case ParsingMode.Color:
                        c = ColorUtils.FromString(a);
                        m = ParsingMode.Normal;
                        break;
                    case ParsingMode.Scheme:
                        Enum.TryParse(a, out t);
                        m = ParsingMode.Normal;
                        break;
                    case ParsingMode.PaletteFile:
                        p = a;
                        m = ParsingMode.Normal;
                        break;
                    default:
                        // find flags
                        if (a == "-t") m = ParsingMode.Scheme;
                        else if (a == "-c") m = ParsingMode.Color;
                        else if (a == "-p") m = ParsingMode.PaletteFile;
                        else u.Add(a);
                        break;
                }
            }

            return new InitialAppState(c, t, p, u);
        }
    }
}
