using System;

namespace Colours
{
    public static class AppArgParser
    {
        private enum ParsingMode
        {
            Normal, Scheme, Color
        }

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
