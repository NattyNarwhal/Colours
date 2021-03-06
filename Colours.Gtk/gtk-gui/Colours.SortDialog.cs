
// This file has been generated by the GUI designer. Do not modify.
namespace Colours
{
	public partial class SortDialog
	{
		private global::Gtk.HBox hbox1;

		private global::Gtk.Label sortByLabel;

		private global::Gtk.ComboBox sortByBox;

		private global::Gtk.CheckButton orderBox;

		private global::Gtk.Button buttonCancel;

		private global::Gtk.Button buttonOk;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Colours.SortDialog
			this.Name = "Colours.SortDialog";
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child Colours.SortDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.sortByLabel = new global::Gtk.Label();
			this.sortByLabel.Name = "sortByLabel";
			this.sortByLabel.LabelProp = global::Mono.Unix.Catalog.GetString("_Sort by:");
			this.sortByLabel.UseUnderline = true;
			this.hbox1.Add(this.sortByLabel);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.sortByLabel]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.sortByBox = global::Gtk.ComboBox.NewText();
			this.sortByBox.Name = "sortByBox";
			this.hbox1.Add(this.sortByBox);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.sortByBox]));
			w3.Position = 1;
			// Container child hbox1.Gtk.Box+BoxChild
			this.orderBox = new global::Gtk.CheckButton();
			this.orderBox.CanFocus = true;
			this.orderBox.Name = "orderBox";
			this.orderBox.Label = global::Mono.Unix.Catalog.GetString("_Ascending");
			this.orderBox.DrawIndicator = true;
			this.orderBox.UseUnderline = true;
			this.hbox1.Add(this.orderBox);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.orderBox]));
			w4.Position = 2;
			w4.Expand = false;
			w1.Add(this.hbox1);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(w1[this.hbox1]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Internal child Colours.SortDialog.ActionArea
			global::Gtk.HButtonBox w6 = this.ActionArea;
			w6.Name = "dialog1_ActionArea";
			w6.Spacing = 10;
			w6.BorderWidth = ((uint)(5));
			w6.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget(this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w7 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w6[this.buttonCancel]));
			w7.Expand = false;
			w7.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget(this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w8 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w6[this.buttonOk]));
			w8.Position = 1;
			w8.Expand = false;
			w8.Fill = false;
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 128;
			this.sortByLabel.MnemonicWidget = this.sortByBox;
			this.Show();
		}
	}
}
