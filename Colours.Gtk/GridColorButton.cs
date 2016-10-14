using System;
namespace Colours
{
	public class GridColorButton : global::Gtk.ColorButton
	{
		// HACK: translate Windows version with least amount of added mechanics
		// doesn't matter if generic, its special
		public PaletteColor Tag { get; set; }

		public GridColorButton(Gdk.Color c) : base(c) { }
	}
}
