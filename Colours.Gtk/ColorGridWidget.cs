using System;
using System.Linq;
using MoreLinq;
using global::Gtk;

namespace Colours
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ColorGridWidget : Bin
	{
		Palette _palette;

		public Palette Palette
		{
			get
			{
				return _palette;
			}
			set
			{
				_palette = value;
				UpdateGrid();
			}
		}

		public ColorGridWidget()
		{
			this.Build();
			Palette = new Palette();
		}

		void UpdateGrid()
		{
			table1.Foreach((widget) => { table1.Remove(widget); });;

			if (Palette.Colors.Count == 0) return;

			var cols = Convert.ToUInt32(Palette.Colors.Count > 0 ? Palette.Colors.Count :
			                            (Palette.Colors.Count < 16 ? Palette.Colors.Count : 16));

			var batches = Palette.Colors.Batch(Convert.ToInt32(cols));
			var rows = Convert.ToUInt32(batches.Count());

			table1.NRows = rows;
			table1.NColumns = cols;

			for (uint r = 0; r < rows; r++)
			{
				var ra = batches.ToArray()[r];
				for (uint c = 0; c < ra.Count(); c++)
				{
					var pc = ra.ToArray()[c];
					var cb = new GridColorButton(pc.Color.ToGdkColor());
					cb.Tag = pc;
					cb.ButtonPressEvent += ColorButton_Press;
					cb.ColorSet += (sender, e) =>
					{
						OnColorChange(new ColorGridChangeEventArgs(cb.Tag, cb.Color.ToRgbColor()), cb);
					};
					cb.FocusInEvent += (sender, e) =>
					{
						FocusedOnColorChange(e, cb);
					};
					cb.FocusOutEvent += (sender, e) =>
					{
						FocusedOnColorChange(e, cb);
					};

					table1.Attach(cb, c, c + 1, r, r + 1);
				}
			}

			if (Visible)
				table1.ShowAll();
		}

		[GLib.ConnectBefore]
		void ColorButton_Press(object sender, ButtonPressEventArgs e)
		{
			OnColorClick(e, sender);
		}

		public PaletteColor FocusedColor
		{
			get
			{
				PaletteColor pc = null;
				table1.Foreach((widget) =>
				{
					if (widget is GridColorButton && ((GridColorButton)widget).HasFocus)
					{
						pc = ((GridColorButton)widget).Tag;
					}
				});
				return pc;
			}
			set
			{
				table1.Foreach((widget) =>
				{
					if (widget is GridColorButton && ((GridColorButton)widget).Tag == value)
						((GridColorButton)widget).HasFocus = true;
				});
			}
		}

		/// <summary>
		/// A button representing a colour was pressed.
		/// </summary>
		public event ButtonPressEventHandler ColorClick;

		protected virtual void OnColorClick(ButtonPressEventArgs e, object sender)
		{
			ColorClick?.Invoke(sender, e);
		}

		/// <summary>
		/// A colour in the palette was changed by the user.
		/// </summary>
		public event EventHandler<ColorGridChangeEventArgs> ColorChange;

		protected virtual void OnColorChange(ColorGridChangeEventArgs e, object sender)
		{
			ColorChange?.Invoke(sender, e);
		}

		/// <summary>
		/// The focused colour was changed.
		/// </summary>
		public event EventHandler<EventArgs> FocusedColorChange;

		protected virtual void FocusedOnColorChange(EventArgs e, object sender)
		{
			FocusedColorChange?.Invoke(sender, e);
		}
	}
}
