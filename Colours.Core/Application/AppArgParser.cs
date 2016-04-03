using System;

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
            Normal, Scheme, Color
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
        /// <returns>The application state the frontend will load.</returns>
        public static AppState ParseArgs(string[] args, HsvColor defaultColor, SchemeType defaultScheme)
        {
            HsvColor c = defaultColor;
            SchemeType t = defaultScheme;

            ParsingMode p = ParsingMode.Normal;
            foreach (string a in args)
            {
                switch (p)
                {
                    case ParsingMode.Color:
                        c = ColorUtils.FromString(a);
                        p = ParsingMode.Normal;
                        break;
                    case ParsingMode.Scheme:
                        Enum.TryParse(a, out t);
                        p = ParsingMode.Normal;
                        break;
                    default:
                        // find flags
                        if (a == "-t") p = ParsingMode.Scheme;
                        if (a == "-c") p = ParsingMode.Color;
                        break;
                }
            }

            return new AppState(c, t);
        }
    }
}
