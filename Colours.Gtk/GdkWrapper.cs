using System;

namespace Colours
{
	public static class GdkWrapper
	{
		public static RgbColor ToRgbColor(this Gdk.Color c)
		{
			// GDK colour channels are ushorts, not bytes
			// except the constructor DOES take bytes
			return new RgbColor (c.Red >> 8, c.Green >> 8, c.Blue >> 8);
		}

		public static HsvColor ToHsvColor(this Gdk.Color c)
		{
			return new HsvColor(ToRgbColor(c));
		}

		public static Gdk.Color ToGdkColor(this RgbColor c)
		{
			return new Gdk.Color(c.R, c.G, c.B);
		}

		public static Gdk.Color ToGdkColor(this HsvColor c)
		{
			return ToGdkColor(c.ToRgb());
		}
	}
}

