using System;
using System.Drawing;
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
		foreach (HsvColor c in app.Results) {
			ColorButton cb = new ColorButton (c.ToGdkColor ());
			cb.ColorSet += OnColorChooserColorChanged;
			cb.TooltipText = String.Format("{0}\r\n{1}\r\n{2}\r\n{3}",
				System.Drawing.ColorTranslator.ToHtml(c.ToRgb()),
				c.ToRgb().ToRgbString(),
				c.ToRgb().ToHslString(),
				c.ToString());
			newBox.PackStart(cb, true, true, 0);
		}

		undoAction.Sensitive = app.CanUndo ();
		redoAction.Sensitive = app.CanRedo ();
		BrightenAction.Sensitive = app.CanBrighten ();
		DarkenAction.Sensitive = app.CanDarken ();
		SaturateAction.Sensitive = app.CanSaturate ();
		DesaturateAction.Sensitive = app.CanDesaturate ();

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

	protected void OnUndoActionActivated (object sender, EventArgs e)
	{
		app.Undo();
		SyncAppViewState();
	}	protected void OnInvertActionActivated (object sender, EventArgs e)
	{
		app.SetColor (app.Color.Invert (), true);
		SyncAppViewState ();
	}

	protected void OnDesaturateActionActivated (object sender, EventArgs e)
	{
		app.Desaturate ();
		SyncAppViewState ();
	}

	protected void OnSaturateActionActivated (object sender, EventArgs e)
	{
		app.Saturate ();
		SyncAppViewState ();
	}
	protected void OnDarkenActionActivated (object sender, EventArgs e)
	{
		app.Darken ();
		SyncAppViewState ();
	}

	protected void OnBrightenActionActivated (object sender, EventArgs e)
	{
		app.Brighten ();
		SyncAppViewState ();
	}

	protected void OnRandomActionActivated (object sender, EventArgs e)
	{
		Random r = new Random ();
		app.SetColor (Color.FromArgb (r.Next (255), r.Next (255), r.Next (255)), true);
		SyncAppViewState ();
	}

	protected void OnPasteActionActivated (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}

	protected void OnCopyHSVActionActivated (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}	

	protected void OnCopyHSLActionActivated (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}

	protected void OnCopyRGBActionActivated (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}

	protected void OnCopyHexActionActivated (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}

	protected void OnRedoActionActivated (object sender, EventArgs e)
	{
		app.Redo ();
		SyncAppViewState ();
	}
}