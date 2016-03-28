﻿using System;
using System.Drawing;
using System.IO;
using Gtk;
using Colours;

public partial class MainWindow: Gtk.Window
{
	public AppController app;

	private static Gdk.Atom clipAtom = Gdk.Atom.Intern("CLIPBOARD", false);
	Clipboard clipboard = Clipboard.Get (clipAtom);
	HBox newBox;

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		// don't use this for app init, only for base init
	}

	public MainWindow(AppState state) : this()
	{
		app = new AppController (state.Color, state.SchemeType);
		SyncAppViewState ();
	}

	public void SyncAppViewState()
	{
		schemeBox.Active = (int)app.SchemeType;
		Title = String.Format ("{0} for {1}", schemeBox.ActiveText,
			ColorTranslator.ToHtml (app.Color));

		paddedBox.Remove (newBox);

		newBox = new HBox (true, 6);
		foreach (HsvColor c in app.Results) {
			ColorButton cb = new ColorButton (c.ToGdkColor ());
			cb.UseAlpha = false;
			cb.ColorSet += OnColorChooserColorChanged;
			cb.TooltipText = String.Format("{0}\r\n{1}\r\n{2}\r\n{3}",
				System.Drawing.ColorTranslator.ToHtml(c.ToRgb()),
				c.ToRgb().ToRgbString(),
				c.ToRgb().ToHslString(),
				c.ToString());

			// bind the menu too
			cb.ButtonPressEvent += HandleButtonPopupMenu;

			newBox.PackStart(cb, true, true, 0);
		}

		goBackAction.Sensitive = app.CanUndo ();
		goForwardAction.Sensitive = app.CanRedo ();
		BrightenAction.Sensitive = app.CanBrighten ();
		DarkenAction.Sensitive = app.CanDarken ();
		SaturateAction.Sensitive = app.CanSaturate ();
		DesaturateAction.Sensitive = app.CanDesaturate ();

		paddedBox.Add (newBox);
		this.ShowAll ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		ConfigParser.SaveConfig (app.Color, app.SchemeType);
		Application.Quit ();
		a.RetVal = true;
	}

	[GLib.ConnectBeforeAttribute]
	protected void HandleButtonPopupMenu (object sender, ButtonPressEventArgs e)
	{
		if (e.Event.Button == 3) { // right mouse button
			ColorButton cb = (ColorButton)sender;

			Menu m = new Menu();
			MenuItem hexPopupItem = new MenuItem ("Copy He_x");
			MenuItem rgbPopupItem = new MenuItem ("Copy _RGB");
			MenuItem hslPopupItem = new MenuItem ("Copy HS_L");
			MenuItem hsvPopupItem = new MenuItem ("Copy HS_V");
			hexPopupItem.Activated += (o, a) => {
				clipboard.Text = ColorTranslator.ToHtml(cb.Color.ToGDIColor());
			};
			rgbPopupItem.Activated += (o, a) => {
				clipboard.Text = cb.Color.ToGDIColor().ToRgbString();
			};
			hslPopupItem.Activated += (o, a) => {
				clipboard.Text = cb.Color.ToGDIColor().ToHslString();
			};
			hsvPopupItem.Activated += (o, a) => {
				clipboard.Text = cb.Color.ToHsvColor().ToString();
			};
			m.Add (hexPopupItem);
			m.Add (rgbPopupItem);
			m.Add (hslPopupItem);
			m.Add (hsvPopupItem);
			m.ShowAll ();
			m.Popup();
		}
	}

	protected void OnColorChooserColorChanged (object sender, EventArgs e)
	{
		ColorButton cb = (ColorButton)sender;
		app.SetColor (cb.Color.ToGDIColor (), true);
		SyncAppViewState ();
	}

	protected void OnSchemeBoxChanged (object sender, EventArgs e)
	{
		// we can set the combobox from config while the app
		// isn't initialized yet
		if (app != null) {
			app.SetSchemeType ((SchemeType)schemeBox.Active, true);
			SyncAppViewState ();
		}
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
		clipboard.RequestText ((c, s) => {
			try {
				app.SetColor (ColorTranslator.FromHtml (s), true);
			}
			catch (Exception) {} // it doesn't matter
		});
		SyncAppViewState ();
	}

	protected void OnCopyHSVActionActivated (object sender, EventArgs e)
	{
		clipboard.Text = app.HsvColor.ToString ();
	}	

	protected void OnCopyHSLActionActivated (object sender, EventArgs e)
	{
		clipboard.Text = app.Color.ToHslString ();
	}

	protected void OnCopyRGBActionActivated (object sender, EventArgs e)
	{
		clipboard.Text = app.Color.ToRgbString ();
	}

	protected void OnCopyHexActionActivated (object sender, EventArgs e)
	{
		clipboard.Text = ColorTranslator.ToHtml (app.Color);
	}

	protected void OnRedoActionActivated (object sender, EventArgs e)
	{
		app.Redo ();
		SyncAppViewState ();
	}

	protected void OnSaveActionActivated (object sender, EventArgs e)
	{
		FileChooserDialog fd = new FileChooserDialog ("Save as HTML", this,
			FileChooserAction.Save, "Cancel", ResponseType.Cancel, "OK", ResponseType.Ok);
		FileFilter ff = new FileFilter ();
		ff.Name = "HTML";
		ff.AddMimeType ("text/html");
		ff.AddPattern ("*.html");
		fd.AddFilter (ff);
		if (fd.Run () == (int)ResponseType.Ok) {
			File.WriteAllText(fd.Filename,
				HtmlProofGenerator.GeneratePage(
					String.Format("{0} for {1}", app.SchemeType,
						ColorTranslator.ToHtml(app.Color)),
					HtmlProofGenerator.GenerateTable(app.Results)
				)
			);
		}
		fd.Destroy ();
	}

	protected void OnQuitActionActivated (object sender, EventArgs e)
	{
		Application.Quit ();
	}
}