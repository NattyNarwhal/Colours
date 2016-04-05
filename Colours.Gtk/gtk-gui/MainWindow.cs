
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	
	private global::Gtk.Action EditAction;
	
	private global::Gtk.Action ColorAction;
	
	private global::Gtk.Action goBackAction;
	
	private global::Gtk.Action goForwardAction;
	
	private global::Gtk.Action copyAction;
	
	private global::Gtk.Action CopyRGBAction;
	
	private global::Gtk.Action CopyHSLAction;
	
	private global::Gtk.Action CopyHSVAction;
	
	private global::Gtk.Action pasteAction;
	
	private global::Gtk.Action refreshAction;
	
	private global::Gtk.Action BrightenAction;
	
	private global::Gtk.Action DarkenAction;
	
	private global::Gtk.Action SaturateAction;
	
	private global::Gtk.Action DesaturateAction;
	
	private global::Gtk.Action InvertAction;
	
	private global::Gtk.Action FileAction;
	
	private global::Gtk.Action saveAction;
	
	private global::Gtk.Action quitAction;
	
	private global::Gtk.VBox mainVbox;
	
	private global::Gtk.MenuBar menubar1;
	
	private global::Gtk.VBox paddedBox;
	
	private global::Gtk.ComboBox schemeBox;

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
		w1.Add (this.goBackAction, "<Primary>z");
		this.goForwardAction = new global::Gtk.Action ("goForwardAction", global::Mono.Unix.Catalog.GetString ("_Forward"), null, "gtk-go-forward");
		this.goForwardAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Redo");
		w1.Add (this.goForwardAction, "<Primary>y");
		this.copyAction = new global::Gtk.Action ("copyAction", global::Mono.Unix.Catalog.GetString ("Copy He_x"), null, "gtk-copy");
		this.copyAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Copy He_x");
		w1.Add (this.copyAction, null);
		this.CopyRGBAction = new global::Gtk.Action ("CopyRGBAction", global::Mono.Unix.Catalog.GetString ("Copy _RGB"), null, null);
		this.CopyRGBAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Copy _RGB");
		w1.Add (this.CopyRGBAction, null);
		this.CopyHSLAction = new global::Gtk.Action ("CopyHSLAction", global::Mono.Unix.Catalog.GetString ("Copy HS_L"), null, null);
		this.CopyHSLAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Copy HS_L");
		w1.Add (this.CopyHSLAction, null);
		this.CopyHSVAction = new global::Gtk.Action ("CopyHSVAction", global::Mono.Unix.Catalog.GetString ("Copy HS_V"), null, null);
		this.CopyHSVAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Copy HS_V");
		w1.Add (this.CopyHSVAction, null);
		this.pasteAction = new global::Gtk.Action ("pasteAction", global::Mono.Unix.Catalog.GetString ("_Paste"), null, "gtk-paste");
		this.pasteAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Paste");
		w1.Add (this.pasteAction, null);
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
		this.saveAction = new global::Gtk.Action ("saveAction", global::Mono.Unix.Catalog.GetString ("_Save as HTML"), null, "gtk-save");
		this.saveAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Save as HTML");
		w1.Add (this.saveAction, null);
		this.quitAction = new global::Gtk.Action ("quitAction", global::Mono.Unix.Catalog.GetString ("_Quit"), null, "gtk-quit");
		this.quitAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Quit");
		w1.Add (this.quitAction, null);
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
		this.UIManager.AddUiFromString ("<ui><menubar name='menubar1'><menu name='FileAction' action='FileAction'><menuitem name='saveAction' action='saveAction'/><separator/><menuitem name='quitAction' action='quitAction'/></menu><menu name='EditAction' action='EditAction'><menuitem name='goBackAction' action='goBackAction'/><menuitem name='goForwardAction' action='goForwardAction'/><separator/><menuitem name='copyAction' action='copyAction'/><menuitem name='CopyHSLAction' action='CopyHSLAction'/><menuitem name='CopyHSVAction' action='CopyHSVAction'/><separator/><menuitem name='pasteAction' action='pasteAction'/><menuitem name='refreshAction' action='refreshAction'/></menu><menu name='ColorAction' action='ColorAction'><menuitem name='BrightenAction' action='BrightenAction'/><menuitem name='DarkenAction' action='DarkenAction'/><separator/><menuitem name='SaturateAction' action='SaturateAction'/><menuitem name='DesaturateAction' action='DesaturateAction'/><separator/><menuitem name='InvertAction' action='InvertAction'/></menu></menubar></ui>");
		this.menubar1 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubar1")));
		this.menubar1.Name = "menubar1";
		this.mainVbox.Add (this.menubar1);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.mainVbox [this.menubar1]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child mainVbox.Gtk.Box+BoxChild
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
		this.mainVbox.Add (this.paddedBox);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.mainVbox [this.paddedBox]));
		w4.Position = 1;
		this.Add (this.mainVbox);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 390;
		this.DefaultHeight = 164;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.goBackAction.Activated += new global::System.EventHandler (this.OnUndoActionActivated);
		this.goForwardAction.Activated += new global::System.EventHandler (this.OnRedoActionActivated);
		this.copyAction.Activated += new global::System.EventHandler (this.OnCopyHexActionActivated);
		this.CopyHSLAction.Activated += new global::System.EventHandler (this.OnCopyHSLActionActivated);
		this.CopyHSVAction.Activated += new global::System.EventHandler (this.OnCopyHSVActionActivated);
		this.pasteAction.Activated += new global::System.EventHandler (this.OnPasteActionActivated);
		this.refreshAction.Activated += new global::System.EventHandler (this.OnRandomActionActivated);
		this.BrightenAction.Activated += new global::System.EventHandler (this.OnBrightenActionActivated);
		this.DarkenAction.Activated += new global::System.EventHandler (this.OnDarkenActionActivated);
		this.SaturateAction.Activated += new global::System.EventHandler (this.OnSaturateActionActivated);
		this.DesaturateAction.Activated += new global::System.EventHandler (this.OnDesaturateActionActivated);
		this.InvertAction.Activated += new global::System.EventHandler (this.OnInvertActionActivated);
		this.saveAction.Activated += new global::System.EventHandler (this.OnSaveActionActivated);
		this.quitAction.Activated += new global::System.EventHandler (this.OnQuitActionActivated);
		this.schemeBox.Changed += new global::System.EventHandler (this.OnSchemeBoxChanged);
	}
}
