using System;
using System.Collections.Generic;
using global::Gtk;

namespace Colours
{
	public partial class BlendDialog : global::Gtk.Dialog
	{
		ListStore ls = new ListStore(typeof(RgbColor));

		public IEnumerable<RgbColor> SelectedItems
		{
			get
			{
				// HACK: we can't LINQ or use yield in lambdas
				var toReturn = new List<RgbColor>();
				nodeview1.Selection.SelectedForeach((m, p, i) =>
				{
					var c = GetItemFromIter(i);
					toReturn.Add(c);
				});
				return toReturn;
			}
		}

		public BlendDialog()
		{
			this.Build();

			nodeview1.Model = ls;
			nodeview1.Selection.Mode = SelectionMode.Multiple;

			var iconRender = new CellRendererPixbuf();
			var iconCol = new TreeViewColumn("Icon", iconRender);
			iconCol.PackStart(iconRender, true);
			iconCol.SetCellDataFunc(iconRender, new TreeCellDataFunc((tc, c, m, i) =>
			{
				Gdk.Pixbuf buf = new Gdk.Pixbuf(Gdk.Colorspace.Rgb, false, 8, 16, 16);

				buf.Fill(GetItemFromIter(i).ToGdkPixel());

				((CellRendererPixbuf)c).Pixbuf = buf;
			}));

			var colorRender = new CellRendererText();
			var colorCol = new TreeViewColumn("Color", colorRender);
			colorCol.PackStart(colorRender, true);
			colorCol.SetCellDataFunc(colorRender, new TreeCellDataFunc((tc, c, m, i) =>
			{
				((CellRendererText)c).Text = GetItemFromIter(i).ToHtml();
			}));

			nodeview1.AppendColumn(iconCol);
			nodeview1.AppendColumn(colorCol);
		}

		public BlendDialog(RgbColor c1, RgbColor c2) : this()
		{
			colorbutton1.Color = c1.ToGdkColor();
			colorbutton2.Color = c2.ToGdkColor();
			UpdateUI();
		}

		public RgbColor GetItemFromIter(TreeIter i)
		{
			const int colColumn = 0;
			return (RgbColor)nodeview1.Model.GetValue(i, colColumn);
		}

		public void UpdateUI()
		{
			ls.Clear();

			foreach (var c in Blend.BlendColours(colorbutton1.Color.ToRgbColor(),
												 colorbutton2.Color.ToRgbColor(),
												 (int)spinbutton1.Value))
			{
				ls.AppendValues(c);
			}
		}

		protected void UIUpdateEvent(object sender, EventArgs e)
		{
			UpdateUI();
		}
	}
}

