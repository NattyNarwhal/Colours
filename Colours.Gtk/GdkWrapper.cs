using System;

namespace Colours
{
	public static class GdkWrapper
	{
		public static uint ToGdkPixel(this RgbColor c)
		{
			// HACK: Gdk.Color.Pixel doesn't work well, even
			// with colormap alloc
			return (uint)((c.R8 << 24) | (c.G8 << 16) | (c.B8 << 8) | 0xFF);
		}

		public static RgbColor ToRgbColor(this Gdk.Color c)
		{
			return new RgbColor (c.Red, c.Green, c.Blue);
		}

		public static HsvColor ToHsvColor(this Gdk.Color c)
		{
			return new HsvColor(ToRgbColor(c));
		}

		public static Gdk.Color ToGdkColor(this RgbColor c)
		{
			var gc = new Gdk.Color();
			gc.Red = c.R;
			gc.Green = c.G;
			gc.Blue = c.B;
			return gc;
		}

		public static Gdk.Color ToGdkColor(this HsvColor c)
		{
			return ToGdkColor(c.ToRgb());
		}
	}
}

