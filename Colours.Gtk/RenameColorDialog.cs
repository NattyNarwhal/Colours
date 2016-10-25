using System;
namespace Colours
{
	public partial class RenameColorDialog : global::Gtk.Dialog
	{
		public RenameColorDialog()
		{
			this.Build();
		}

		public RenameColorDialog(RgbColor c, string t, string title = "Rename") : this()
		{
			Gdk.Pixbuf buf = new Gdk.Pixbuf(Gdk.Colorspace.Rgb, false, 8, 16, 16);
			buf.Fill(c.ToGdkPixel());
			image1.Pixbuf = buf;
			colorLabel.Text = c.ToHtml();

			textEntry.Text = t;
			Title = title;
		}

		public string NewText => textEntry.Text;
	}
}
