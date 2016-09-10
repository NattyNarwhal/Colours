using System;

namespace Colours
{
	public static class GdkWrapper
	{
		public static uint ToGdkPixel(this RgbColor c)
		{
			// HACK: Gdk.Color.Pixel doesn't work well, even
			// with colormap alloc
			return (uint)((c.R << 24) | (c.G << 16) | (c.B << 8) | 0xFF);
		}

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

