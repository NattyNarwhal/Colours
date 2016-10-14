using System;
namespace Colours
{
	public class ColorGridChangeEventArgs : EventArgs
	{
		public PaletteColor Color { get; set; }
		public RgbColor NewColor { get; set; }

		public ColorGridChangeEventArgs(PaletteColor old, RgbColor @new)
		{
			Color = old;
			NewColor = @new;
		}
	}
}
