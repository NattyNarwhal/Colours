
// This file has been generated by the GUI designer. Do not modify.
namespace Colours
{
	public partial class ColorGridWidget
	{
		private global::Gtk.ScrolledWindow scrolledwindow1;

		private global::Gtk.Table table1;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Colours.ColorGridWidget
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Colours.ColorGridWidget";
			// Container child Colours.ColorGridWidget.Gtk.Container+ContainerChild
			this.scrolledwindow1 = new global::Gtk.ScrolledWindow();
			this.scrolledwindow1.CanFocus = true;
			this.scrolledwindow1.Name = "scrolledwindow1";
			this.scrolledwindow1.HscrollbarPolicy = ((global::Gtk.PolicyType)(2));
			// Container child scrolledwindow1.Gtk.Container+ContainerChild
			global::Gtk.Viewport w1 = new global::Gtk.Viewport();
			w1.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child GtkViewport.Gtk.Container+ContainerChild
			this.table1 = new global::Gtk.Table(((uint)(1)), ((uint)(1)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			w1.Add(this.table1);
			this.scrolledwindow1.Add(w1);
			this.Add(this.scrolledwindow1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
		}
	}
}