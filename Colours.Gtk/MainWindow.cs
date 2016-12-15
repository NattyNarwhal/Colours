using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Gtk;
using Colours;
using Colours.App;

public partial class MainWindow : Gtk.Window
{
	public AppController app;
	public AppPaletteController appPal;

	private static Gdk.Atom clipAtom = Gdk.Atom.Intern("CLIPBOARD", false);
	Clipboard clipboard = Clipboard.Get(clipAtom);

	ListStore schemes = new ListStore(typeof(Scheme));
	ListStore ls = new ListStore(typeof(PaletteColor));

	public MainWindow() : base(Gtk.WindowType.Toplevel)
	{
		Build();
		// don't use this for app init, only for base init

		// init the combobox
		schemeBox.Model = schemes;
		foreach (var s in Scheme.GetSchemes())
			schemes.AppendValues(s);

		var schemeRender = new CellRendererText();
		schemeBox.PackStart(schemeRender, true);
		schemeBox.SetCellDataFunc(schemeRender, new CellLayoutDataFunc((cl, c, m, i) =>
		{
			((CellRendererText)c).Text = ((Scheme)schemes.GetValue(i, 0)).Name;
		}));
		//schemeBox.AddAttribute(schemeRender, "text", 0);

		// init the pallete list view
		treeview1.Model = ls;

		var pcIconRender = new CellRendererPixbuf();
		var pcIconCol = new TreeViewColumn("Icon", pcIconRender);
		pcIconCol.PackStart(pcIconRender, true);
		pcIconCol.SetCellDataFunc(pcIconRender, new TreeCellDataFunc((tc, c, m, i) =>
		{
			Gdk.Pixbuf buf = new Gdk.Pixbuf(Gdk.Colorspace.Rgb, false, 8, 16, 16);

			buf.Fill(GetItemFromIter(i).Color.ToGdkPixel());

			((CellRendererPixbuf)c).Pixbuf = buf;
		}));

		var pcColorRender = new CellRendererText();
		var pcColorCol = new TreeViewColumn("Color", pcColorRender);
		pcColorCol.PackStart(pcColorRender, true);
		pcColorCol.SetCellDataFunc(pcColorRender, new TreeCellDataFunc((tc, c, m, i) =>
		{
			((CellRendererText)c).Text = GetItemFromIter(i).Color.ToHtml();
		}));

		var pcNameRender = new CellRendererText();
		pcNameRender.Editable = true;
		pcNameRender.Edited += pcNameRender_Edited;
		var pcNameCol = new TreeViewColumn("Name", pcNameRender);
		pcNameCol.PackStart(pcNameRender, true);
		pcNameCol.SetCellDataFunc(pcNameRender, new TreeCellDataFunc((tc, c, m, i) =>
		{
			((CellRendererText)c).Text = GetItemFromIter(i).Name;
		}));

		treeview1.AppendColumn(pcIconCol);
		treeview1.AppendColumn(pcColorCol);
		treeview1.AppendColumn(pcNameCol);

	}

	public MainWindow(AppInitState state) : this()
	{
		app = new AppController(state.MixerState);
		appPal = new AppPaletteController();
		if (!String.IsNullOrWhiteSpace(state.PaletteFileName))
		{

		}
		app.ResultChanged += SyncAppViewState;
		appPal.PaletteChanged += SyncAppPalViewState;
		treeview1.Selection.Changed += (o, e) =>
		{
			UpdateUI();
		};
		SyncAppViewState(this, new EventArgs());
		SyncAppPalViewState(this, new EventArgs());
	}

	// replaces var pc = (PaletteColor)treeview1.Model.GetValue (i, 2);
	public PaletteColor GetItemFromIter(TreeIter i)
	{
		const int colColumn = 0;
		return (PaletteColor)treeview1.Model.GetValue(i, colColumn);
	}

	public bool GridView => colorgridwidget1.Sensitive;

	public IEnumerable<PaletteColor> SelectedItems
	{
		get
		{
			if (GridView)
			{
				if (colorgridwidget1.FocusedColor != null)
					return new List<PaletteColor> { colorgridwidget1.FocusedColor };
				else return new List<PaletteColor>();
			}
			else
			{
				return GetListSelectedItems();
			}
		}
	}

	public IEnumerable<Scheme> GetSchemeList()
	{
		// Welcome to the hell that is ListStores and LINQ
		return schemes.Cast<object[]>().Select(x => x[0]).Cast<Scheme>();
	}

	public Scheme GetSelectedScheme()
	{
		return GetSchemeList().ToList()[schemeBox.Active];
	}

	public IEnumerable<PaletteColor> GetListSelectedItems()
	{
		var toReturn = new List<PaletteColor>();
		treeview1.Selection.SelectedForeach((m, p, i) =>
		{
			toReturn.Add(GetItemFromIter(i));
		});
		return toReturn;
	}

	public void SyncAppViewState(object sender, EventArgs e)
	{
		schemeBox.Active = GetSchemeList().ToList()
			.FindIndex(x => x.Type == app.SchemeType);

		foreach (ColorButton cb in colorBox.Children)
			colorBox.Remove(cb);
		foreach (HsvColor c in app.Results)
		{
			ColorButton cb = new ColorButton(c.ToGdkColor());
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

		colorBox.ShowAll();
		UpdateUI();
	}

	public void SyncAppPalViewState(object sender, EventArgs e)
	{
		colorgridwidget1.Palette = appPal.Palette;

		ls.Clear();

		foreach (PaletteColor pc in appPal.Palette.Colors)
			ls.AppendValues(pc);

		UpdateUI();
	}

	public void UpdateUI()
	{
		Title = string.Format("{0}{1} ({2} for {3})", appPal.PaletteName,
			appPal.Dirty ? "*" : "", GetSelectedScheme().Name,
			app.Color.ToHtml());

		var selected = treeview1.Selection.CountSelectedRows() > 0;
		var hasItems = appPal.Palette.Colors.Count > 0;

		goBackAction.Sensitive = app.CanUndo();
		goForwardAction.Sensitive = app.CanRedo();
		BrightenAction.Sensitive = app.CanBrighten();
		DarkenAction.Sensitive = app.CanDarken();
		SaturateAction.Sensitive = app.CanSaturate();
		DesaturateAction.Sensitive = app.CanDesaturate();

		undoAction.Sensitive = appPal.CanUndo();
		redoAction.Sensitive = appPal.CanRedo();
		undoAction.Label = appPal.CanUndo() ?
			"Undo " + appPal.UndoHistory.Peek().Name : "Can't Undo";
		redoAction.Label = appPal.CanRedo() ?
			"Redo " + appPal.RedoHistory.Peek().Name : "Can't Redo";

		cutAction.Sensitive = selected;
		copyAction.Sensitive = selected;
		deleteAction.Sensitive = selected;
		RenameAction.Sensitive = selected;
		ChangeMetadataAction.Sensitive = selected;

		saveAction.Sensitive = hasItems;
		saveAsAction.Sensitive = hasItems;
	}

	/// <summary>
	/// Asks to save changes
	/// </summary>
	/// <returns>If it's OK to close.</returns>
	public bool DirtyPrompt()
	{
		if (appPal.Dirty)
		{
			MessageDialog md = new MessageDialog(this, DialogFlags.Modal,
				MessageType.Question, ButtonsType.None,
				"There are unsaved changes. Do you want to save before you close this palette?");
			md.AddButton("Save", ResponseType.Yes);
			md.AddButton("Discard", ResponseType.No);
			md.AddButton("Cancel", ResponseType.Cancel);
			var retVal = (ResponseType)md.Run();
			md.Destroy();
			switch (retVal)
			{
				case ResponseType.Yes:
					return SavePalette(false);
				case ResponseType.No:
					return true;
				default:
					return false;
			};
		}
		else
			return true;
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		if (DirtyPrompt())
		{
			ConfigParser.SaveConfig(app.Color, app.SchemeType);
			Application.Quit();
		}
		a.RetVal = true;
	}

	[GLib.ConnectBeforeAttribute]
	protected void HandleButtonPopupMenu(object sender, ButtonPressEventArgs e)
	{
		if (e.Event.Button == 3)
		{ // right mouse button
			ColorButton cb = (ColorButton)sender;

			Menu m = new Menu();
			MenuItem hexPopupItem = new MenuItem("Copy He_x");
			MenuItem hslPopupItem = new MenuItem("Copy HS_L");
			MenuItem hsvPopupItem = new MenuItem("Copy HS_V");
			MenuItem addPopupItem = new MenuItem("_Add");
			hexPopupItem.Activated += (o, a) =>
			{
				clipboard.Text = cb.Color.ToRgbColor().ToHtml();
			};
			hslPopupItem.Activated += (o, a) =>
			{
				clipboard.Text = cb.Color.ToRgbColor().ToHslString();
			};
			hsvPopupItem.Activated += (o, a) =>
			{
				clipboard.Text = cb.Color.ToHsvColor().ToString();
			};
			addPopupItem.Activated += (o, a) =>
			{
				appPal.AppendColor(cb.Color.ToRgbColor());
			};
			m.Add(hexPopupItem);
			m.Add(hslPopupItem);
			m.Add(hsvPopupItem);
			m.Add(addPopupItem);
			m.ShowAll();
			m.Popup();
		}
	}

	protected void OnColorChooserColorChanged(object sender, EventArgs e)
	{
		ColorButton cb = (ColorButton)sender;
		app.SetColor(cb.Color.ToRgbColor());
	}

	protected void OnSchemeBoxChanged(object sender, EventArgs e)
	{
		app.SetSchemeType(GetSelectedScheme().Type, true);
	}

	protected void OnUndoActionActivated(object sender, EventArgs e)
	{
		app.Undo();
	}

	protected void OnInvertActionActivated(object sender, EventArgs e)
	{
		app.SetColor(app.Color.Invert());
	}

	protected void OnDesaturateActionActivated(object sender, EventArgs e)
	{
		app.Desaturate();
	}

	protected void OnSaturateActionActivated(object sender, EventArgs e)
	{
		app.Saturate();
	}

	protected void OnDarkenActionActivated(object sender, EventArgs e)
	{
		app.Darken();
	}

	protected void OnBrightenActionActivated(object sender, EventArgs e)
	{
		app.Brighten();
	}

	protected void OnRandomActionActivated(object sender, EventArgs e)
	{
		Random r = new Random();
		app.SetColor(new RgbColor(
			r.Next(ushort.MaxValue),
			r.Next(ushort.MaxValue),
			r.Next(ushort.MaxValue),
			16));
	}

	protected void OnPasteAcquireActionActivated(object sender, EventArgs e)
	{
		clipboard.RequestText((c, s) =>
		{
			try
			{
				if (s.StartsWith("pc"))
				{
					foreach (var pc in Regex.Split(s, Environment.NewLine))
					{
						if (pc == "pc" || String.IsNullOrWhiteSpace(pc))
							continue;
						app.SetColor(new PaletteColor(pc).Color);
						return;
					}
				}
				else
					app.SetColor(ColorUtils.FromString(s));
			}
			catch (Exception) { } // it doesn't matter
		});
	}

	protected void OnCopyHSVActionActivated(object sender, EventArgs e)
	{
		clipboard.Text = app.HsvColor.ToString();
	}

	protected void OnCopyHSLActionActivated(object sender, EventArgs e)
	{
		clipboard.Text = app.Color.ToHslString();
	}

	protected void OnCopyHexActionActivated(object sender, EventArgs e)
	{
		clipboard.Text = app.Color.ToHtml();
	}

	protected void OnRedoActionActivated(object sender, EventArgs e)
	{
		app.Redo();
	}

	protected void OnSaveAsHTMLColorActionActivated(object sender, EventArgs e)
	{
		FileChooserDialog fd = new FileChooserDialog("Save as HTML", this,
			FileChooserAction.Save, "Cancel", ResponseType.Cancel, "OK", ResponseType.Ok);
		FileFilter ff = new FileFilter();
		ff.Name = "HTML";
		ff.AddMimeType("text/html");
		ff.AddPattern("*.html");
		fd.AddFilter(ff);
		if (fd.Run() == (int)ResponseType.Ok)
		{
			File.WriteAllText(fd.Filename,
				HtmlProofGenerator.GeneratePage(
					String.Format("{0} for {1}", app.SchemeType,
						app.Color.ToHtml()),
					HtmlProofGenerator.GenerateTable(app.Results)
				)
			);
		}
		fd.Destroy();
	}

	protected void OnQuitActionActivated(object sender, EventArgs e)
	{
		Application.Quit();
	}

	protected void OnAboutActionActivated(object sender, EventArgs e)
	{
		AboutDialog ad = new AboutDialog()
		{
			Website = "https://github.com/NattyNarwhal/Colours",
			Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
			ProgramName = "Colours",
			License = "This software is licensed under the MIT License." +
				" Portions of this code were borrowed from the Mono Project" +
				", under the MIT license.",
			WrapLicense = true
		};
		ad.Run();
		ad.Destroy();
	}

	public void OpenPalette(string filename)
	{
		if (filename.EndsWith(".aco"))
		{
			var p = new AcoPalette(
				File.ReadAllBytes(filename));
			appPal.NewFromPalette(p, filename);
		}
		else if (filename.EndsWith(".ase"))
		{
			var p = new AsePalette(
				File.ReadAllBytes(filename));
			appPal.NewFromPalette(p, filename);
		}
		else if (filename.EndsWith(".act"))
		{
			var p = new ActPalette(
				File.ReadAllBytes(filename));
			appPal.NewFromPalette(p, filename);
		}
		else if (filename.EndsWith(".acb"))
		{
			var p = new AcbPalette(
				File.ReadAllBytes(filename));
			appPal.NewFromPalette(p, filename);
		}
		else {
			appPal.NewFromPalette(new GimpPalette(File.ReadAllText(filename)), filename);
		}
	}

	public bool SavePalette(bool forceDialog)
	{
		var freshFile = forceDialog || string.IsNullOrEmpty(appPal.FileName);
		var fileName = freshFile ? "" : appPal.FileName;

		if (forceDialog || string.IsNullOrEmpty(fileName))
		{
			FileChooserDialog fd = new FileChooserDialog("Save palette as", this,
				FileChooserAction.Save, "Cancel", ResponseType.Cancel, "OK", ResponseType.Ok);
			FileFilter ffGimp = new FileFilter();
			ffGimp.Name = "GIMP Palette";
			ffGimp.AddPattern("*.gpl");
			fd.AddFilter(ffGimp);
			FileFilter ffAco = new FileFilter();
			ffAco.Name = "Photoshop Palette";
			ffAco.AddPattern("*.aco");
			fd.AddFilter(ffAco);
			FileFilter ffAct = new FileFilter();
			ffAct.Name = "Photoshop Colour Table";
			ffAct.AddPattern("*.act");
			fd.AddFilter(ffAct);
			FileFilter ffAse = new FileFilter();
			ffAse.Name = "Adobe Swatch Exchange";
			ffAse.AddPattern("*.ase");
			fd.AddFilter(ffAse);
			if (fd.Run() == (int)ResponseType.Ok)
			{
				fileName = fd.Filename;
				fd.Destroy();
			}
			else {
				fd.Destroy();
				return false;
			}
		}

		appPal.ConvertPalette(fileName);
		File.WriteAllBytes(fileName, appPal.Palette.ToFile());

		appPal.Dirty = false;
		appPal.FileName = fileName;
		UpdateUI();
		return true;
	}

	protected void OnNewActionActivated(object sender, EventArgs e)
	{
		if (DirtyPrompt())
			appPal.NewFromPalette(new GimpPalette());
	}

	protected void OnSaveAsActionActivated(object sender, EventArgs e)
	{
		SavePalette(true);
	}

	protected void OnSaveActionActivated(object sender, EventArgs e)
	{
		SavePalette(false);
	}

	protected void OnOpenActionActivated(object sender, EventArgs e)
	{
		if (DirtyPrompt())
		{
			FileChooserDialog fd = new FileChooserDialog("Open palette", this,
				FileChooserAction.Open, "Cancel", ResponseType.Cancel, "OK", ResponseType.Ok);
			FileFilter ffGimp = new FileFilter();
			ffGimp.Name = "GIMP Palette";
			ffGimp.AddPattern("*.gpl");
			fd.AddFilter(ffGimp);
			FileFilter ffAco = new FileFilter();
			ffAco.Name = "Photoshop Palette";
			ffAco.AddPattern("*.aco");
			fd.AddFilter(ffAco);
			FileFilter ffAct = new FileFilter();
			ffAct.Name = "Photoshop Colour Table";
			ffAct.AddPattern("*.act");
			fd.AddFilter(ffAct);
			FileFilter ffAcb = new FileFilter();
			ffAcb.Name = "Photoshop Color Book";
			ffAcb.AddPattern("*.acb");
			fd.AddFilter(ffAcb);
			FileFilter ffAse = new FileFilter();
			ffAse.Name = "Adobe Swatch Exchange";
			ffAse.AddPattern("*.ase");
			fd.AddFilter(ffAse);
			if (fd.Run() == (int)ResponseType.Ok)
			{
				OpenPalette(fd.Filename);
			}
			fd.Destroy();
		}
	}

	protected void OnPaletteUndoActionActivated(object sender, EventArgs e)
	{
		appPal.Undo();
	}

	protected void OnPaletteRedoActionActivated(object sender, EventArgs e)
	{
		appPal.Redo();
	}

	protected void pcNameRender_Edited(object o, EditedArgs args)
	{
		TreeIter iter;
		if (treeview1.Model.GetIterFromString(out iter, args.Path))
		{
			// get the index
			var pc = GetItemFromIter(iter);
			if (pc.Name != args.NewText)
				appPal.RenameColor(appPal.Palette.Colors.IndexOf(pc), args.NewText);
		}
	}

	protected void OnTreeview1RowActivated(object o, RowActivatedArgs args)
	{
		TreeIter iter;
		if (treeview1.Model.GetIter(out iter, args.Path))
		{
			var pc = GetItemFromIter(iter);
			app.SetColor(pc.Color);
		}
	}

	protected void OnAddActionActivated(object sender, EventArgs e)
	{
		appPal.AppendColor(app.Color);
	}

	public void DeleteSelection()
	{
		// TODO: wire to delete key?
		appPal.DeleteColors(SelectedItems);
	}

	protected void OnDeleteActionActivated(object sender, EventArgs e)
	{
		DeleteSelection();
	}

	protected void OnAddAllActionActivated(object sender, EventArgs e)
	{
		appPal.AppendColors(app.Results.Select(x => x.ToRgb()));
	}

	public void CopySelection()
	{
		// HACK: ideally, we'd just send a PaletteColor or List of
		// those, except that won't work. ContainsData(type) says
		// true, GetData(type) says null.
		var sb = new StringBuilder("pc" + Environment.NewLine);
		if (treeview1.Selection.CountSelectedRows() > 0)
		{
			foreach (var pc in SelectedItems)
				sb.AppendLine(pc.ToString());
			clipboard.Text = sb.ToString();
		}
	}

	protected void OnCutActionActivated(object sender, EventArgs e)
	{
		CopySelection();
		DeleteSelection();
	}

	protected void OnCopyActionActivated(object sender, EventArgs e)
	{
		CopySelection();
	}

	protected void OnPasteActionActivated(object sender, EventArgs e)
	{
		clipboard.RequestText((c, s) =>
		{
			try
			{
				if (s.StartsWith("pc"))
				{
					var toAdd = new List<PaletteColor>();
					foreach (var pc in Regex.Split(s, Environment.NewLine))
					{
						if (pc == "pc" || String.IsNullOrWhiteSpace(pc))
							continue;
						toAdd.Add(new PaletteColor(pc));
					}
					appPal.AppendColors(toAdd);
				}
				else
					appPal.AppendColor(ColorUtils.FromString(s).ToRgb());
			}
			catch (Exception) { } // it doesn't matter
		});
	}

	protected void OnExportHTMLActionActivated(object sender, EventArgs e)
	{
		FileChooserDialog fd = new FileChooserDialog("Save as HTML", this,
			FileChooserAction.Save, "Cancel", ResponseType.Cancel, "OK", ResponseType.Ok);
		FileFilter ff = new FileFilter();
		ff.Name = "HTML";
		ff.AddMimeType("text/html");
		ff.AddPattern("*.html");
		fd.AddFilter(ff);
		if (fd.Run() == (int)ResponseType.Ok)
		{
			File.WriteAllText(fd.Filename,
				HtmlProofGenerator.GeneratePage(
					appPal.Palette
				)
			);
		}
		fd.Destroy();
	}


	protected void OnPropertiesActionActivated(object sender, EventArgs e)
	{
		if (appPal.Palette is GimpPalette)
		{
			var unboxed = appPal.Palette as GimpPalette;
			var pd = new PropertiesDialog();
			pd.PaletteTitle = unboxed.Name;
			pd.PaletteColumns = unboxed.BucketSize;
			pd.PaletteComments = unboxed.Comments;
			if (pd.Run() == (int)ResponseType.Ok)
			{
				var p = (GimpPalette)unboxed.Clone();
				p.Name = pd.PaletteTitle;
				p.BucketSize = pd.PaletteColumns;
				p.Comments = pd.PaletteComments;
				appPal.SetPalette(p, action: "Properties Change");
			}
			pd.Destroy();
		}
	}

	protected void OnTreeview1DragEnd(object o, DragEndArgs args)
	{
		// HACK: GTK thinks it's special and has its own model
		// instead of our own. Resync changes made to the GTK
		// model, manually.

		var newPal = appPal.Palette.Clone();

		// HACK: we don't have .Select on models
		var newList = new List<PaletteColor>();
		ls.Foreach((m, p, i) =>
		{
			var pc = GetItemFromIter(i);
			newList.Add(pc);
			return false; // .Foreach needs this to continue walking
		});

		// because thse are different lists, but containing the same
		// objects, do this
		if (!newPal.Colors.SequenceEqual(newList))
		{
			newPal.Colors = newList;
			appPal.SetPalette(newPal, action: "Move Colour");
		}
	}

	[GLib.ConnectBefore]
	protected void OnTreeview1ButtonPressEvent(object o, ButtonPressEventArgs args)
	{
		if (args.Event.Button == 3 && SelectedItems.Count() > 0)
		{ // right mouse button
			Menu menu = new Menu();

			MenuItem cutPopupItem = new MenuItem("Cu_t");
			MenuItem copyPopupItem = new MenuItem("_Copy");
			MenuItem delPopupItem = new MenuItem("_Remove");
			MenuItem renPopupItem = new MenuItem("Re_name");
			MenuItem metPopupItem = new MenuItem("Change _Metadata");
			cutPopupItem.Activated += (s, a) =>
			{
				CopySelection();
				DeleteSelection();
			};
			copyPopupItem.Activated += (s, a) =>
			{
				CopySelection();
			};
			delPopupItem.Activated += (s, a) =>
			{
				DeleteSelection();
			};
			renPopupItem.Activated += (s, a) =>
			{
				RenameSelected();
			};
			metPopupItem.Activated += (s, a) =>
			{
				ChangeMetadataSelection();
			};
			menu.Add(cutPopupItem);
			menu.Add(copyPopupItem);
			menu.Add(delPopupItem);
			menu.Add(renPopupItem);
			menu.Add(metPopupItem);
			menu.ShowAll();
			menu.Popup();
		}
	}

	protected void OnBlendActionActivated(object sender, EventArgs e)
	{
		var bd = new BlendDialog(app.Color, treeview1.Selection.CountSelectedRows() > 0 ?
								 SelectedItems.First().Color : app.Color);
		if (bd.Run() == (int)ResponseType.Ok)
		{
			appPal.AppendColors(bd.SelectedItems);
		}
		bd.Destroy();
	}

	protected void OnListActionToggled(object sender, EventArgs e)
	{
		var c = ListAction.Active;

		var selection = SelectedItems.FirstOrDefault();

		// the treeview has an implied ScrollView parent, as stetic states
		treeview1.Sensitive = c;
		treeview1.Visible = c;
		treeview1.Parent.Sensitive = c;
		treeview1.Parent.Visible = c;
		eitherBox.SetChildPacking(treeview1.Parent, c, c, 0, PackType.Start);
		if (c && selection != null)
		{
			ls.Foreach((m, p, i) =>
			{
				var pc = GetItemFromIter(i);
				if (pc == selection)
				{
					treeview1.Selection.SelectPath(p);
					return true;
				}
				return false;
			});
		}

		colorgridwidget1.Sensitive = !c;
		colorgridwidget1.Visible = !c;
		// HACK: doesn't seem to show properly otherwise?
		if (!c)
			colorgridwidget1.ShowAll();
		eitherBox.SetChildPacking(colorgridwidget1, !c, !c, 0, PackType.Start);
		if (!c && selection != null)
			colorgridwidget1.FocusedColor = selection;
	}

	protected void OnColorgridwidget1FocusedColorChange(object sender, EventArgs e)
	{
		UpdateUI();
	}

	protected void OnColorgridwidget1ColorChange(object sender, ColorGridChangeEventArgs e)
	{
		appPal.ChangeColor(e.Color, e.NewColor);
	}

	public void RenameSelected()
	{
		if (SelectedItems.Count() > 0)
		{
			var pc = SelectedItems.First();
			var rd = new RenameColorDialog(pc.Color, pc.Name);
			if (rd.Run() == (int)ResponseType.Ok)
			{
				appPal.RenameColor(pc, rd.NewText);
			}
			rd.Destroy();
		}
	}

	protected void OnRenameActionActivated(object sender, EventArgs e)
	{
		RenameSelected();
	}

	protected void OnSortActionActivated(object sender, EventArgs e)
	{
		var sd = new SortDialog();
		if (sd.Run() == (int)ResponseType.Ok)
		{
			appPal.SortColors(sd.SortBy, sd.Ascending);
		}
		sd.Destroy();
	}

	public void ChangeMetadataSelection()
	{
		var pc = SelectedItems.First();
		var rd = new RenameColorDialog(pc.Color, pc.Metadata, "Change Metadata");
		if (rd.Run() == (int)ResponseType.Ok)
		{
			appPal.ChangeMetadata(pc, rd.NewText);
		}
		rd.Destroy();
	}

	protected void OnChangeMetadataActionActivated(object sender, EventArgs e)
	{
		ChangeMetadataSelection();
	}
}
