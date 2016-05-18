using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Gtk;
using Colours;

public partial class MainWindow: Gtk.Window
{
	public AppController app;
	public AppPaletteController appPal;

	private static Gdk.Atom clipAtom = Gdk.Atom.Intern("CLIPBOARD", false);
	Clipboard clipboard = Clipboard.Get (clipAtom);

	// TODO: should this be just PaletteColor, and use render funcs?
	ListStore ls = new ListStore (typeof(Gdk.Pixbuf), typeof(string), typeof(string), typeof(PaletteColor));

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		// don't use this for app init, only for base init
		treeview1.Model = ls;

		var pcIconRender = new CellRendererPixbuf ();
		var pcIconCol = new TreeViewColumn ("Icon", pcIconRender);
		pcIconCol.AddAttribute (pcIconRender, "pixbuf", 0);

		var pcNameRender = new CellRendererText ();
		pcNameRender.Editable = true;
		pcNameRender.Edited += pcNameRender_Edited;
		var pcNameCol = new TreeViewColumn ("Name", pcNameRender);
		pcNameCol.AddAttribute (pcNameRender, "text", 1);

		var pcColorRender = new CellRendererText ();
		var pcColorCol = new TreeViewColumn ("Color", pcColorRender);
		pcColorCol.AddAttribute (pcColorRender, "text", 2);

		treeview1.AppendColumn (pcIconCol);
		treeview1.AppendColumn (pcNameCol);
		treeview1.AppendColumn (pcColorCol);

	}

	public MainWindow(InitialAppState state) : this()
	{
		app = new AppController (state);
		appPal = new AppPaletteController ();
		if (!String.IsNullOrWhiteSpace (state.PaletteFileName)) {

		}
		app.ResultChanged += SyncAppViewState;
		appPal.PaletteChanged += SyncAppPalViewState;
		treeview1.Selection.Changed += (o, e) => {
			UpdateUI();
		};
		SyncAppViewState (this, new EventArgs());
		SyncAppPalViewState (this, new EventArgs ());
	}

	// replaces var pc = (PaletteColor)treeview1.Model.GetValue (i, 2);
	public PaletteColor GetItemFromIter(TreeIter i)
	{
		const int colColumn = 3;
		return (PaletteColor)treeview1.Model.GetValue (i, colColumn);
	}

	public void SyncAppViewState(object sender, EventArgs e)
	{
		schemeBox.Active = (int)app.SchemeType;

		foreach (ColorButton cb in colorBox.Children)
			colorBox.Remove (cb);
		foreach (HsvColor c in app.Results) {
			ColorButton cb = new ColorButton (c.ToGdkColor ());
			cb.UseAlpha = false;
			cb.ColorSet += OnColorChooserColorChanged;
			cb.TooltipText = String.Format("{0}\r\n{1}\r\n{2}",
				c.ToRgb().ToHtml(),
				c.ToRgb().ToHslString(),
				c.ToString());

			// bind the menu too
			cb.ButtonPressEvent += HandleButtonPopupMenu;

			colorBox.PackStart(cb, true, true, 0);
		}

		this.ShowAll ();
		UpdateUI ();
	}

	public void SyncAppPalViewState(object sender, EventArgs e) 
	{
		ls.Clear ();

		foreach (PaletteColor pc in appPal.Palette.Colors) {
			Gdk.Pixbuf buf = new Gdk.Pixbuf (Gdk.Colorspace.Rgb, false, 8, 16, 16);

			buf.Fill (pc.Color.ToGdkPixel());

			ls.AppendValues (buf, pc.Name, pc.Color.ToHtml(), pc);
		}

		UpdateUI ();
	}

	public void UpdateUI()
	{
		Title = String.Format ("{0}{1} ({2} for {3})", appPal.Palette.Name,
			appPal.Dirty ? "*" : "", schemeBox.ActiveText,
			app.Color.ToHtml());

		var selected = treeview1.Selection.CountSelectedRows() > 0;
		var hasItems = appPal.Palette.Colors.Count > 0;

		goBackAction.Sensitive = app.CanUndo ();
		goForwardAction.Sensitive = app.CanRedo ();
		BrightenAction.Sensitive = app.CanBrighten ();
		DarkenAction.Sensitive = app.CanDarken ();
		SaturateAction.Sensitive = app.CanSaturate ();
		DesaturateAction.Sensitive = app.CanDesaturate ();

		undoAction.Sensitive = appPal.CanUndo ();
		redoAction.Sensitive = appPal.CanRedo ();

		cutAction.Sensitive = selected;
		copyAction.Sensitive = selected;
		deleteAction.Sensitive = selected;

		saveAction.Sensitive = hasItems;
		saveAsAction.Sensitive = hasItems;
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
			MenuItem hslPopupItem = new MenuItem ("Copy HS_L");
			MenuItem hsvPopupItem = new MenuItem ("Copy HS_V");
			MenuItem addPopupItem = new MenuItem ("_Add");
			hexPopupItem.Activated += (o, a) => {
				clipboard.Text = cb.Color.ToRgbColor().ToHtml();
			};
			hslPopupItem.Activated += (o, a) => {
				clipboard.Text = cb.Color.ToRgbColor().ToHslString();
			};
			hsvPopupItem.Activated += (o, a) => {
				clipboard.Text = cb.Color.ToHsvColor().ToString();
			};
			addPopupItem.Activated += (o, a) => {
				appPal.AppendColor(cb.Color.ToRgbColor());
			};
			m.Add (hexPopupItem);
			m.Add (hslPopupItem);
			m.Add (hsvPopupItem);
			m.Add (addPopupItem);
			m.ShowAll ();
			m.Popup();
		}
	}

	protected void OnColorChooserColorChanged (object sender, EventArgs e)
	{
		ColorButton cb = (ColorButton)sender;
		app.SetColor (cb.Color.ToRgbColor (), true);
	}

	protected void OnSchemeBoxChanged (object sender, EventArgs e)
	{
		app.SetSchemeType ((SchemeType)schemeBox.Active, true);
	}

	protected void OnUndoActionActivated (object sender, EventArgs e)
	{
		app.Undo();
	}	protected void OnInvertActionActivated (object sender, EventArgs e)
	{
		app.SetColor (app.Color.Invert (), true);
	}

	protected void OnDesaturateActionActivated (object sender, EventArgs e)
	{
		app.Desaturate ();
	}

	protected void OnSaturateActionActivated (object sender, EventArgs e)
	{
		app.Saturate ();
	}
	protected void OnDarkenActionActivated (object sender, EventArgs e)
	{
		app.Darken ();
	}

	protected void OnBrightenActionActivated (object sender, EventArgs e)
	{
		app.Brighten ();
	}

	protected void OnRandomActionActivated (object sender, EventArgs e)
	{
		Random r = new Random ();
		app.SetColor (new RgbColor (r.Next (255), r.Next (255), r.Next (255)), true);
	}

	protected void OnPasteAcquireActionActivated (object sender, EventArgs e)
	{
		clipboard.RequestText ((c, s) => {
			try {
				if (s.StartsWith("pc"))
				{
					foreach (var pc in Regex.Split(s, Environment.NewLine))
					{
						if (pc == "pc" || String.IsNullOrWhiteSpace(pc))
							continue;
						app.SetColor(new PaletteColor(pc).Color, true);
						return;
					}
				}
				else
					app.SetColor (ColorUtils.FromString (s), true);
			}
			catch (Exception) {} // it doesn't matter
		});
	}

	protected void OnCopyHSVActionActivated (object sender, EventArgs e)
	{
		clipboard.Text = app.HsvColor.ToString ();
	}	

	protected void OnCopyHSLActionActivated (object sender, EventArgs e)
	{
		clipboard.Text = app.Color.ToHslString ();
	}

	protected void OnCopyHexActionActivated (object sender, EventArgs e)
	{
		clipboard.Text = app.Color.ToHtml();
	}

	protected void OnRedoActionActivated (object sender, EventArgs e)
	{
		app.Redo ();
	}

	protected void OnSaveAsHTMLColorActionActivated (object sender, EventArgs e)
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
						app.Color.ToHtml()),
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
	protected void OnAboutActionActivated (object sender, EventArgs e)
	{
		AboutDialog ad = new AboutDialog (){
			Website = "https://github.com/NattyNarwhal/Colours",
			Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
			ProgramName = "Colours",
			License = "This software is licensed under the MIT License." +
				" Portions of this code were borrowed from the Mono Project" +
				", under the MIT license.",
			WrapLicense = true
		};
		ad.Run ();
		ad.Destroy ();
	}

	public void OpenPalette(string filename)
	{
		appPal.NewFromPalette (new Palette (File.ReadAllText (filename)), filename);
	}

	public bool SavePalette(bool forceDialog)
	{
		if (forceDialog || appPal.FileName == null) {
			FileChooserDialog fd = new FileChooserDialog ("Save palette as", this,
				FileChooserAction.Save, "Cancel", ResponseType.Cancel, "OK", ResponseType.Ok);
			FileFilter ff = new FileFilter ();
			ff.Name = "GIMP Palette";
			ff.AddPattern ("*.gpl");
			fd.AddFilter (ff);
			if (fd.Run () == (int)ResponseType.Ok) {
				appPal.FileName = fd.Filename;
				fd.Destroy ();
			} else {
				fd.Destroy ();
				return false;
			}
		}
		File.WriteAllText (appPal.FileName, appPal.Palette.ToString ());
		appPal.Dirty = false;
		UpdateUI ();
		return true;
	}
	protected void OnNewActionActivated (object sender, EventArgs e)
	{
		appPal.NewFromPalette (new Palette ());
	}

	protected void OnSaveAsActionActivated (object sender, EventArgs e)
	{
		SavePalette(true);
	}

	protected void OnSaveActionActivated (object sender, EventArgs e)
	{
		SavePalette(false);
	}

	protected void OnOpenActionActivated (object sender, EventArgs e)
	{
		FileChooserDialog fd = new FileChooserDialog ("Open palette", this,
			FileChooserAction.Open, "Cancel", ResponseType.Cancel, "OK", ResponseType.Ok);
		FileFilter ff = new FileFilter ();
		ff.Name = "GIMP Palette";
		ff.AddPattern ("*.gpl");
		fd.AddFilter (ff);
		if (fd.Run () == (int)ResponseType.Ok) {
			OpenPalette (fd.Filename);
		}
		fd.Destroy ();
	}

	protected void OnPaletteUndoActionActivated (object sender, EventArgs e)
	{
		appPal.Undo ();
	}

	protected void OnPaletteRedoActionActivated (object sender, EventArgs e)
	{
		appPal.Redo ();
	}

	protected void pcNameRender_Edited (object o, EditedArgs args)
	{
		TreeIter iter;
		if (treeview1.Model.GetIterFromString(out iter, args.Path)) {
			// get the index
			var pc = GetItemFromIter(iter);
			if (pc.Name != args.NewText)
				appPal.RenameColor (appPal.Palette.Colors.IndexOf (pc), args.NewText);
		}
	}

	protected void OnTreeview1RowActivated (object o, RowActivatedArgs args)
	{
		TreeIter iter;
		if (treeview1.Model.GetIter(out iter, args.Path)) {
			var pc = GetItemFromIter (iter);
			app.SetColor (pc.Color, true);
		}
	}
	protected void OnAddActionActivated (object sender, EventArgs e)
	{
		appPal.AppendColor(app.Color);
	}

	public void DeleteSelection()
	{
		// TODO: wire to delete key?
		List<PaletteColor> l = new List<PaletteColor> ();
		treeview1.Selection.SelectedForeach ((m, p, i) => {
			var pc = GetItemFromIter(i);
			l.Add(pc);
		});
		appPal.DeleteColors (l);
	}
	protected void OnDeleteActionActivated (object sender, EventArgs e)
	{
		DeleteSelection ();
	}

	protected void OnAddAllActionActivated (object sender, EventArgs e)
	{
		foreach (var c in app.Results)
			appPal.AppendColor (c.ToRgb ());
 	}
	public void CopySelection()
	{
		// HACK: ideally, we'd just send a PaletteColor or List of
		// those, except that won't work. ContainsData(type) says
		// true, GetData(type) says null.
		var sb = new StringBuilder("pc" + Environment.NewLine);
		if (treeview1.Selection.CountSelectedRows() > 0)
		{
			treeview1.Selection.SelectedForeach ((m, p, i) => {
				var pc = GetItemFromIter(i);
				sb.AppendLine(pc.ToString());
			});
			clipboard.Text = sb.ToString ();
		}
	}

	protected void OnCutActionActivated (object sender, EventArgs e)
	{
		CopySelection ();
		DeleteSelection ();
	}

	protected void OnCopyActionActivated (object sender, EventArgs e)
	{
		CopySelection ();
	}

	protected void OnPasteActionActivated (object sender, EventArgs e)
	{
		clipboard.RequestText ((c, s) => {
			try {
				if (s.StartsWith("pc"))
				{
					foreach (var pc in Regex.Split(s, Environment.NewLine))
					{
						if (pc == "pc" || String.IsNullOrWhiteSpace(pc))
							continue;
						appPal.AppendColor(new PaletteColor(pc));
					}
				}
				else
					appPal.AppendColor(ColorUtils.FromString(s).ToRgb());
			}
			catch (Exception) {} // it doesn't matter
		});
	}

	protected void OnImportPhotoshopPaletteActionActivated (object sender, EventArgs e)
	{
		FileChooserDialog fd = new FileChooserDialog ("Open palette", this,
			FileChooserAction.Open, "Cancel", ResponseType.Cancel, "OK", ResponseType.Ok);
		FileFilter ff = new FileFilter ();
		ff.Name = "Photoshop Palette";
		ff.AddPattern ("*.aco");
		fd.AddFilter (ff);
		if (fd.Run () == (int)ResponseType.Ok) {
			appPal.NewFromPalette (
				AcoConverter.FromPhotoshopPalette (
					File.ReadAllBytes (fd.Filename)));
		}
		fd.Destroy ();
	}

	protected void OnExportPhotoshopPaletteActionActivated (object sender, EventArgs e)
	{
		FileChooserDialog fd = new FileChooserDialog ("Save palette as", this,
			FileChooserAction.Save, "Cancel", ResponseType.Cancel, "OK", ResponseType.Ok);
		FileFilter ff = new FileFilter ();
		ff.Name = "GIMP Palette";
		ff.AddPattern ("*.gpl");
		fd.AddFilter (ff);
		if (fd.Run () == (int)ResponseType.Ok) {
			File.WriteAllBytes (fd.Filename,
				AcoConverter.ToPhotoshopPalette (appPal.Palette));
		}
		fd.Destroy ();
	}

	protected void OnExportHTMLActionActivated (object sender, EventArgs e)
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
					appPal.Palette
				)
			);
		}
		fd.Destroy ();
	}

	protected void OnPropertiesActionActivated (object sender, EventArgs e)
	{
		var pd = new PropertiesDialog();
		pd.PaletteTitle = appPal.Palette.Name;
		pd.PaletteColumns = appPal.Palette.Columns;
		pd.PaletteComments = appPal.Palette.Comments;
		if (pd.Run () == (int)ResponseType.Ok) {
			var p = new Palette(appPal.Palette)
			{
				Name = pd.PaletteTitle,
				Columns = pd.PaletteColumns,
				Comments = pd.PaletteComments
			};
			appPal.SetPalette(p);
		}
		pd.Destroy ();
	}
}