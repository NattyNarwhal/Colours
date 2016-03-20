using System;
using Gtk;
using Colours;

public partial class MainWindow: Gtk.Window
{
	public AppController app;
	HBox newBox;

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();

		app = new AppController (new HsvColor (System.Drawing.Color.Red), GetComboBoxSelection());
		SyncAppViewState ();
	}

	public string GetComboBoxSelection()
	{
		TreeIter iter;

		if (schemeBox.GetActiveIter (out iter))
			return (string)schemeBox.Model.GetValue (iter, 0);
		else
			throw new Exception ();
	}

	public void SyncAppViewState()
	{
		mainVbox.Remove (newBox);

		newBox = new HBox (true, 6);

		int i = 0;
		foreach (HsvColor c in app.Results) {
			ColorButton cb = new ColorButton (c.ToGdkColor ());
			if (i++ == 0) {
				cb.ColorSet += OnColorChooserColorChanged;
			} else {
				cb.Sensitive = false;
			}
			cb.TooltipText = String.Format("{0}\r\n{1}\r\n{2}\r\n{3}",
				System.Drawing.ColorTranslator.ToHtml(c.ToRgb()),
				c.ToRgb().ToRgbString(),
				c.ToRgb().ToHslString(),
				c.ToString());
			newBox.PackStart(cb, true, true, 0);
		}

		mainVbox.Add (newBox);
		this.ShowAll ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnColorChooserColorChanged (object sender, EventArgs e)
	{
		ColorButton cb = (ColorButton)sender;
		app.SetColor (cb.Color.ToGDIColor (), true);
		SyncAppViewState ();
	}

	protected void OnSchemeBoxChanged (object sender, EventArgs e)
	{
		app.SchemeType = GetComboBoxSelection ();
		app.GetSchemeResults ();
		SyncAppViewState ();
	}
}
