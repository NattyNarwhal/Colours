
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	
	private global::Gtk.Action EditAction;
	
	private global::Gtk.Action ColorAction;
	
	private global::Gtk.Action goBackAction;
	
	private global::Gtk.Action goForwardAction;
	
	private global::Gtk.Action CopyHexColorAction;
	
	private global::Gtk.Action CopyHSLColorAction;
	
	private global::Gtk.Action CopyHSVColorAction;
	
	private global::Gtk.Action PasteAcquireAction;
	
	private global::Gtk.Action refreshAction;
	
	private global::Gtk.Action BrightenAction;
	
	private global::Gtk.Action DarkenAction;
	
	private global::Gtk.Action SaturateAction;
	
	private global::Gtk.Action DesaturateAction;
	
	private global::Gtk.Action InvertAction;
	
	private global::Gtk.Action FileAction;
	
	private global::Gtk.Action SaveAsHTMLColorAction;
	
	private global::Gtk.Action quitAction;
	
	private global::Gtk.Action HelpAction;
	
	private global::Gtk.Action executeAction;
	
	private global::Gtk.Action aboutAction;
	
	private global::Gtk.Action AcquireAction;
	
	private global::Gtk.Action newAction;
	
	private global::Gtk.Action openAction;
	
	private global::Gtk.Action saveAction;
	
	private global::Gtk.Action saveAsAction;
	
	private global::Gtk.Action undoAction;
	
	private global::Gtk.Action redoAction;
	
	private global::Gtk.Action addAction;
	
	private global::Gtk.Action deleteAction;
	
	private global::Gtk.Action AddAllAction;
	
	private global::Gtk.Action cutAction;
	
	private global::Gtk.Action copyAction;
	
	private global::Gtk.Action pasteAction;
	
	private global::Gtk.Action ImportPhotoshopPaletteAction;
	
	private global::Gtk.Action ExportPhotoshopPaletteAction;
	
	private global::Gtk.Action ExportHTMLAction;
	
	private global::Gtk.Action propertiesAction;
	
	private global::Gtk.VBox mainVbox;
	
	private global::Gtk.MenuBar menubar1;
	
	private global::Gtk.VPaned vpaned1;
	
	private global::Gtk.VBox paddedBox;
	
	private global::Gtk.ComboBox schemeBox;
	
	private global::Gtk.HBox colorBox;
	
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	
	private global::Gtk.TreeView treeview1;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.EditAction = new global::Gtk.Action ("EditAction", global::Mono.Unix.Catalog.GetString ("_Edit"), null, null);
		this.EditAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Edit");
		w1.Add (this.EditAction, null);
		this.ColorAction = new global::Gtk.Action ("ColorAction", global::Mono.Unix.Catalog.GetString ("_Color"), null, null);
		this.ColorAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Color");
		w1.Add (this.ColorAction, null);
		this.goBackAction = new global::Gtk.Action ("goBackAction", global::Mono.Unix.Catalog.GetString ("_Back"), null, "gtk-go-back");
		this.goBackAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Undo");
		w1.Add (this.goBackAction, null);
		this.goForwardAction = new global::Gtk.Action ("goForwardAction", global::Mono.Unix.Catalog.GetString ("_Forward"), null, "gtk-go-forward");
		this.goForwardAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Redo");
		w1.Add (this.goForwardAction, null);
		this.CopyHexColorAction = new global::Gtk.Action ("CopyHexColorAction", global::Mono.Unix.Catalog.GetString ("Copy He_x"), null, null);
		this.CopyHexColorAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Copy He_x");
		w1.Add (this.CopyHexColorAction, null);
		this.CopyHSLColorAction = new global::Gtk.Action ("CopyHSLColorAction", global::Mono.Unix.Catalog.GetString ("Copy HS_L"), null, null);
		this.CopyHSLColorAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Copy HS_L");
		w1.Add (this.CopyHSLColorAction, null);
		this.CopyHSVColorAction = new global::Gtk.Action ("CopyHSVColorAction", global::Mono.Unix.Catalog.GetString ("Copy HS_V"), null, null);
		this.CopyHSVColorAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Copy HS_V");
		w1.Add (this.CopyHSVColorAction, null);
		this.PasteAcquireAction = new global::Gtk.Action ("PasteAcquireAction", global::Mono.Unix.Catalog.GetString ("_Paste"), null, null);
		this.PasteAcquireAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Paste");
		w1.Add (this.PasteAcquireAction, null);
		this.refreshAction = new global::Gtk.Action ("refreshAction", global::Mono.Unix.Catalog.GetString ("_Random"), null, "gtk-refresh");
		this.refreshAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Rando_m");
		w1.Add (this.refreshAction, "<Primary>r");
		this.BrightenAction = new global::Gtk.Action ("BrightenAction", global::Mono.Unix.Catalog.GetString ("_Brighten"), null, null);
		this.BrightenAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Brighten");
		w1.Add (this.BrightenAction, "<Primary>k");
		this.DarkenAction = new global::Gtk.Action ("DarkenAction", global::Mono.Unix.Catalog.GetString ("Dar_ken"), null, null);
		this.DarkenAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Dar_ken");
		w1.Add (this.DarkenAction, "<Primary>j");
		this.SaturateAction = new global::Gtk.Action ("SaturateAction", global::Mono.Unix.Catalog.GetString ("_Saturate"), null, null);
		this.SaturateAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Saturate");
		w1.Add (this.SaturateAction, "<Primary>l");
		this.DesaturateAction = new global::Gtk.Action ("DesaturateAction", global::Mono.Unix.Catalog.GetString ("_Desaturate"), null, null);
		this.DesaturateAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Desaturate");
		w1.Add (this.DesaturateAction, "<Primary>h");
		this.InvertAction = new global::Gtk.Action ("InvertAction", global::Mono.Unix.Catalog.GetString ("_Invert"), null, null);
		this.InvertAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Invert");
		w1.Add (this.InvertAction, null);
		this.FileAction = new global::Gtk.Action ("FileAction", global::Mono.Unix.Catalog.GetString ("_File"), null, null);
		this.FileAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_File");
		w1.Add (this.FileAction, null);
		this.SaveAsHTMLColorAction = new global::Gtk.Action ("SaveAsHTMLColorAction", global::Mono.Unix.Catalog.GetString ("_Save as HTML"), null, null);
		this.SaveAsHTMLColorAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Save as HTML");
		w1.Add (this.SaveAsHTMLColorAction, null);
		this.quitAction = new global::Gtk.Action ("quitAction", global::Mono.Unix.Catalog.GetString ("_Quit"), null, "gtk-quit");
		this.quitAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Quit");
		w1.Add (this.quitAction, null);
		this.HelpAction = new global::Gtk.Action ("HelpAction", global::Mono.Unix.Catalog.GetString ("_Help"), null, null);
		this.HelpAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Help");
		w1.Add (this.HelpAction, null);
		this.executeAction = new global::Gtk.Action ("executeAction", global::Mono.Unix.Catalog.GetString ("_Execute"), null, "gtk-execute");
		this.executeAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Execute");
		w1.Add (this.executeAction, null);
		this.aboutAction = new global::Gtk.Action ("aboutAction", global::Mono.Unix.Catalog.GetString ("_About"), null, "gtk-about");
		this.aboutAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_About");
		w1.Add (this.aboutAction, null);
		this.AcquireAction = new global::Gtk.Action ("AcquireAction", global::Mono.Unix.Catalog.GetString ("_Acquire"), null, null);
		this.AcquireAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Acquire");
		w1.Add (this.AcquireAction, null);
		this.newAction = new global::Gtk.Action ("newAction", global::Mono.Unix.Catalog.GetString ("_New"), null, "gtk-new");
		this.newAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_New");
		w1.Add (this.newAction, null);
		this.openAction = new global::Gtk.Action ("openAction", global::Mono.Unix.Catalog.GetString ("_Open"), null, "gtk-open");
		this.openAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Open");
		w1.Add (this.openAction, null);
		this.saveAction = new global::Gtk.Action ("saveAction", global::Mono.Unix.Catalog.GetString ("_Save"), null, "gtk-save");
		this.saveAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Save");
		w1.Add (this.saveAction, null);
		this.saveAsAction = new global::Gtk.Action ("saveAsAction", global::Mono.Unix.Catalog.GetString ("Save _As"), null, "gtk-save-as");
		this.saveAsAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Save _As");
		w1.Add (this.saveAsAction, null);
		this.undoAction = new global::Gtk.Action ("undoAction", global::Mono.Unix.Catalog.GetString ("_Undo"), null, "gtk-undo");
		this.undoAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Undo");
		w1.Add (this.undoAction, null);
		this.redoAction = new global::Gtk.Action ("redoAction", global::Mono.Unix.Catalog.GetString ("_Redo"), null, "gtk-redo");
		this.redoAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Redo");
		w1.Add (this.redoAction, null);
		this.addAction = new global::Gtk.Action ("addAction", global::Mono.Unix.Catalog.GetString ("_Add"), null, "gtk-add");
		this.addAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Add");
		w1.Add (this.addAction, null);
		this.deleteAction = new global::Gtk.Action ("deleteAction", global::Mono.Unix.Catalog.GetString ("_Delete"), null, "gtk-delete");
		this.deleteAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Delete");
		w1.Add (this.deleteAction, null);
		this.AddAllAction = new global::Gtk.Action ("AddAllAction", global::Mono.Unix.Catalog.GetString ("Add A_ll"), null, null);
		this.AddAllAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Add A_ll");
		w1.Add (this.AddAllAction, null);
		this.cutAction = new global::Gtk.Action ("cutAction", global::Mono.Unix.Catalog.GetString ("Cu_t"), null, "gtk-cut");
		this.cutAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Cu_t");
		w1.Add (this.cutAction, null);
		this.copyAction = new global::Gtk.Action ("copyAction", global::Mono.Unix.Catalog.GetString ("_Copy"), null, "gtk-copy");
		this.copyAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Copy");
		w1.Add (this.copyAction, null);
		this.pasteAction = new global::Gtk.Action ("pasteAction", global::Mono.Unix.Catalog.GetString ("_Paste"), null, "gtk-paste");
		this.pasteAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Paste");
		w1.Add (this.pasteAction, null);
		this.ImportPhotoshopPaletteAction = new global::Gtk.Action ("ImportPhotoshopPaletteAction", global::Mono.Unix.Catalog.GetString ("Import Photoshop Palette"), null, null);
		this.ImportPhotoshopPaletteAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Import Photoshop Palette");
		w1.Add (this.ImportPhotoshopPaletteAction, null);
		this.ExportPhotoshopPaletteAction = new global::Gtk.Action ("ExportPhotoshopPaletteAction", global::Mono.Unix.Catalog.GetString ("Export Photoshop Palette"), null, null);
		this.ExportPhotoshopPaletteAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Export Photoshop Palette");
		w1.Add (this.ExportPhotoshopPaletteAction, null);
		this.ExportHTMLAction = new global::Gtk.Action ("ExportHTMLAction", global::Mono.Unix.Catalog.GetString ("Export HTML"), null, null);
		this.ExportHTMLAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Export HTML");
		w1.Add (this.ExportHTMLAction, null);
		this.propertiesAction = new global::Gtk.Action ("propertiesAction", global::Mono.Unix.Catalog.GetString ("_Properties"), null, "gtk-properties");
		this.propertiesAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Properties");
		w1.Add (this.propertiesAction, null);
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.mainVbox = new global::Gtk.VBox ();
		this.mainVbox.Name = "mainVbox";
		this.mainVbox.Spacing = 6;
		// Container child mainVbox.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><menubar name=\'menubar1\'><menu name=\'FileAction\' action=\'FileAction\'><menuite" +
		"m name=\'newAction\' action=\'newAction\'/><menuitem name=\'openAction\' action=\'openA" +
		"ction\'/><menuitem name=\'saveAction\' action=\'saveAction\'/><menuitem name=\'saveAsA" +
		"ction\' action=\'saveAsAction\'/><separator/><menuitem name=\'propertiesAction\' acti" +
		"on=\'propertiesAction\'/><separator/><menuitem name=\'ImportPhotoshopPaletteAction\'" +
		" action=\'ImportPhotoshopPaletteAction\'/><menuitem name=\'ExportPhotoshopPaletteAc" +
		"tion\' action=\'ExportPhotoshopPaletteAction\'/><menuitem name=\'ExportHTMLAction\' a" +
		"ction=\'ExportHTMLAction\'/><separator/><menuitem name=\'quitAction\' action=\'quitAc" +
		"tion\'/></menu><menu name=\'EditAction\' action=\'EditAction\'><menuitem name=\'undoAc" +
		"tion\' action=\'undoAction\'/><menuitem name=\'redoAction\' action=\'redoAction\'/><sep" +
		"arator/><menuitem name=\'cutAction\' action=\'cutAction\'/><menuitem name=\'copyActio" +
		"n\' action=\'copyAction\'/><menuitem name=\'pasteAction\' action=\'pasteAction\'/><sepa" +
		"rator/><menuitem name=\'addAction\' action=\'addAction\'/><menuitem name=\'AddAllActi" +
		"on\' action=\'AddAllAction\'/><menuitem name=\'deleteAction\' action=\'deleteAction\'/>" +
		"</menu><menu name=\'AcquireAction\' action=\'AcquireAction\'><menuitem name=\'PasteAc" +
		"quireAction\' action=\'PasteAcquireAction\'/><menuitem name=\'refreshAction\' action=" +
		"\'refreshAction\'/></menu><menu name=\'ColorAction\' action=\'ColorAction\'><menuitem " +
		"name=\'goBackAction\' action=\'goBackAction\'/><menuitem name=\'goForwardAction\' acti" +
		"on=\'goForwardAction\'/><separator/><menuitem name=\'CopyHexColorAction\' action=\'Co" +
		"pyHexColorAction\'/><menuitem name=\'CopyHSLColorAction\' action=\'CopyHSLColorActio" +
		"n\'/><menuitem name=\'CopyHSVColorAction\' action=\'CopyHSVColorAction\'/><menuitem n" +
		"ame=\'SaveAsHTMLColorAction\' action=\'SaveAsHTMLColorAction\'/><separator/><menuite" +
		"m name=\'BrightenAction\' action=\'BrightenAction\'/><menuitem name=\'DarkenAction\' a" +
		"ction=\'DarkenAction\'/><separator/><menuitem name=\'SaturateAction\' action=\'Satura" +
		"teAction\'/><menuitem name=\'DesaturateAction\' action=\'DesaturateAction\'/><separat" +
		"or/><menuitem name=\'InvertAction\' action=\'InvertAction\'/></menu><menu name=\'Help" +
		"Action\' action=\'HelpAction\'><menuitem name=\'aboutAction\' action=\'aboutAction\'/><" +
		"/menu></menubar></ui>");
		this.menubar1 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubar1")));
		this.menubar1.Name = "menubar1";
		this.mainVbox.Add (this.menubar1);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.mainVbox [this.menubar1]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child mainVbox.Gtk.Box+BoxChild
		this.vpaned1 = new global::Gtk.VPaned ();
		this.vpaned1.CanFocus = true;
		this.vpaned1.Name = "vpaned1";
		this.vpaned1.Position = 100;
		// Container child vpaned1.Gtk.Paned+PanedChild
		this.paddedBox = new global::Gtk.VBox ();
		this.paddedBox.Name = "paddedBox";
		this.paddedBox.Spacing = 6;
		this.paddedBox.BorderWidth = ((uint)(6));
		// Container child paddedBox.Gtk.Box+BoxChild
		this.schemeBox = global::Gtk.ComboBox.NewText ();
		this.schemeBox.AppendText (global::Mono.Unix.Catalog.GetString ("Complement"));
		this.schemeBox.AppendText (global::Mono.Unix.Catalog.GetString ("Split Complements"));
		this.schemeBox.AppendText (global::Mono.Unix.Catalog.GetString ("Triads"));
		this.schemeBox.AppendText (global::Mono.Unix.Catalog.GetString ("Tetrads"));
		this.schemeBox.AppendText (global::Mono.Unix.Catalog.GetString ("Analogous"));
		this.schemeBox.AppendText (global::Mono.Unix.Catalog.GetString ("Monochromatic"));
		this.schemeBox.Name = "schemeBox";
		this.schemeBox.Active = 0;
		this.paddedBox.Add (this.schemeBox);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.paddedBox [this.schemeBox]));
		w3.Position = 0;
		w3.Expand = false;
		w3.Fill = false;
		// Container child paddedBox.Gtk.Box+BoxChild
		this.colorBox = new global::Gtk.HBox ();
		this.colorBox.Name = "colorBox";
		this.colorBox.Homogeneous = true;
		this.colorBox.Spacing = 6;
		this.paddedBox.Add (this.colorBox);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.paddedBox [this.colorBox]));
		w4.Position = 1;
		this.vpaned1.Add (this.paddedBox);
		global::Gtk.Paned.PanedChild w5 = ((global::Gtk.Paned.PanedChild)(this.vpaned1 [this.paddedBox]));
		w5.Resize = false;
		// Container child vpaned1.Gtk.Paned+PanedChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.treeview1 = new global::Gtk.TreeView ();
		this.treeview1.CanFocus = true;
		this.treeview1.Name = "treeview1";
		this.treeview1.Reorderable = true;
		this.GtkScrolledWindow.Add (this.treeview1);
		this.vpaned1.Add (this.GtkScrolledWindow);
		this.mainVbox.Add (this.vpaned1);
		global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.mainVbox [this.vpaned1]));
		w8.Position = 1;
		this.Add (this.mainVbox);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 473;
		this.DefaultHeight = 316;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.goBackAction.Activated += new global::System.EventHandler (this.OnUndoActionActivated);
		this.goForwardAction.Activated += new global::System.EventHandler (this.OnRedoActionActivated);
		this.CopyHexColorAction.Activated += new global::System.EventHandler (this.OnCopyHexActionActivated);
		this.CopyHSLColorAction.Activated += new global::System.EventHandler (this.OnCopyHSLActionActivated);
		this.CopyHSVColorAction.Activated += new global::System.EventHandler (this.OnCopyHSVActionActivated);
		this.PasteAcquireAction.Activated += new global::System.EventHandler (this.OnPasteAcquireActionActivated);
		this.refreshAction.Activated += new global::System.EventHandler (this.OnRandomActionActivated);
		this.BrightenAction.Activated += new global::System.EventHandler (this.OnBrightenActionActivated);
		this.DarkenAction.Activated += new global::System.EventHandler (this.OnDarkenActionActivated);
		this.SaturateAction.Activated += new global::System.EventHandler (this.OnSaturateActionActivated);
		this.DesaturateAction.Activated += new global::System.EventHandler (this.OnDesaturateActionActivated);
		this.InvertAction.Activated += new global::System.EventHandler (this.OnInvertActionActivated);
		this.SaveAsHTMLColorAction.Activated += new global::System.EventHandler (this.OnSaveAsHTMLColorActionActivated);
		this.quitAction.Activated += new global::System.EventHandler (this.OnQuitActionActivated);
		this.aboutAction.Activated += new global::System.EventHandler (this.OnAboutActionActivated);
		this.newAction.Activated += new global::System.EventHandler (this.OnNewActionActivated);
		this.openAction.Activated += new global::System.EventHandler (this.OnOpenActionActivated);
		this.saveAction.Activated += new global::System.EventHandler (this.OnSaveActionActivated);
		this.saveAsAction.Activated += new global::System.EventHandler (this.OnSaveAsActionActivated);
		this.undoAction.Activated += new global::System.EventHandler (this.OnPaletteUndoActionActivated);
		this.redoAction.Activated += new global::System.EventHandler (this.OnPaletteRedoActionActivated);
		this.addAction.Activated += new global::System.EventHandler (this.OnAddActionActivated);
		this.deleteAction.Activated += new global::System.EventHandler (this.OnDeleteActionActivated);
		this.AddAllAction.Activated += new global::System.EventHandler (this.OnAddAllActionActivated);
		this.cutAction.Activated += new global::System.EventHandler (this.OnCutActionActivated);
		this.copyAction.Activated += new global::System.EventHandler (this.OnCopyActionActivated);
		this.pasteAction.Activated += new global::System.EventHandler (this.OnPasteActionActivated);
		this.ImportPhotoshopPaletteAction.Activated += new global::System.EventHandler (this.OnImportPhotoshopPaletteActionActivated);
		this.ExportPhotoshopPaletteAction.Activated += new global::System.EventHandler (this.OnExportPhotoshopPaletteActionActivated);
		this.ExportHTMLAction.Activated += new global::System.EventHandler (this.OnExportHTMLActionActivated);
		this.propertiesAction.Activated += new global::System.EventHandler (this.OnPropertiesActionActivated);
		this.schemeBox.Changed += new global::System.EventHandler (this.OnSchemeBoxChanged);
		this.treeview1.RowActivated += new global::Gtk.RowActivatedHandler (this.OnTreeview1RowActivated);
	}
}
