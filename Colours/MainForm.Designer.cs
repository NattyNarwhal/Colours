namespace Colours
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.schemeBox = new System.Windows.Forms.ComboBox();
            this.resultsTable = new System.Windows.Forms.TableLayoutPanel();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.colorContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyHexContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyHslContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyHsvContextToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveAsHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saturateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desaturateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.invertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsHtmlDialog = new System.Windows.Forms.SaveFileDialog();
            this.backToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.acquireToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteAcquireToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eyedropperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.paletteListImages = new System.Windows.Forms.ImageList(this.components);
            this.paletteList = new System.Windows.Forms.ListView();
            this.nameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colorHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.savePaletteDialog = new System.Windows.Forms.SaveFileDialog();
            this.openPaletteDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paletteContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySubmenuHexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySubmenuHslToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySubmenuHsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorContextMenu.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.paletteContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // schemeBox
            // 
            this.schemeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.schemeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.schemeBox.FormattingEnabled = true;
            this.schemeBox.Items.AddRange(new object[] {
            "Complement",
            "Split Complements",
            "Triads",
            "Tetrads",
            "Analogous",
            "Monochromatic"});
            this.schemeBox.Location = new System.Drawing.Point(12, 3);
            this.schemeBox.Name = "schemeBox";
            this.schemeBox.Size = new System.Drawing.Size(324, 21);
            this.schemeBox.TabIndex = 2;
            this.schemeBox.SelectionChangeCommitted += new System.EventHandler(this.schemeBox_SelectionChangeCommitted);
            // 
            // resultsTable
            // 
            this.resultsTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultsTable.ColumnCount = 4;
            this.resultsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.resultsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.resultsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.resultsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.resultsTable.Location = new System.Drawing.Point(12, 30);
            this.resultsTable.Name = "resultsTable";
            this.resultsTable.RowCount = 1;
            this.resultsTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.resultsTable.Size = new System.Drawing.Size(324, 108);
            this.resultsTable.TabIndex = 3;
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            this.colorDialog.FullOpen = true;
            // 
            // colorContextMenu
            // 
            this.colorContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyHexContextToolStripMenuItem,
            this.copyHslContextToolStripMenuItem,
            this.copyHsvContextToolStripMenuItem1,
            this.toolStripMenuItem4,
            this.addToolStripMenuItem});
            this.colorContextMenu.Name = "contextMenuStrip1";
            this.colorContextMenu.Size = new System.Drawing.Size(128, 98);
            // 
            // copyHexContextToolStripMenuItem
            // 
            this.copyHexContextToolStripMenuItem.Name = "copyHexContextToolStripMenuItem";
            this.copyHexContextToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.copyHexContextToolStripMenuItem.Text = "Copy He&x";
            this.copyHexContextToolStripMenuItem.Click += new System.EventHandler(this.copyHexContextToolStripMenuItem_Click);
            // 
            // copyHslContextToolStripMenuItem
            // 
            this.copyHslContextToolStripMenuItem.Name = "copyHslContextToolStripMenuItem";
            this.copyHslContextToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.copyHslContextToolStripMenuItem.Text = "Copy HS&L";
            this.copyHslContextToolStripMenuItem.Click += new System.EventHandler(this.copyHslContextToolStripMenuItem_Click);
            // 
            // copyHsvContextToolStripMenuItem1
            // 
            this.copyHsvContextToolStripMenuItem1.Name = "copyHsvContextToolStripMenuItem1";
            this.copyHsvContextToolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.copyHsvContextToolStripMenuItem1.Text = "Copy HS&V";
            this.copyHsvContextToolStripMenuItem1.Click += new System.EventHandler(this.copyHsvContextToolStripMenuItem_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.acquireToolStripMenuItem,
            this.colorToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(348, 24);
            this.menuStrip.TabIndex = 5;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.saveAsHTMLToolStripMenuItem,
            this.toolStripMenuItem5,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(226, 6);
            // 
            // saveAsHTMLToolStripMenuItem
            // 
            this.saveAsHTMLToolStripMenuItem.Name = "saveAsHTMLToolStripMenuItem";
            this.saveAsHTMLToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsHTMLToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.saveAsHTMLToolStripMenuItem.Text = "&Export to HTML";
            this.saveAsHTMLToolStripMenuItem.Click += new System.EventHandler(this.saveAsHTMLToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(226, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.quitToolStripMenuItem.Text = "&Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripMenuItem2,
            this.pasteToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // colorToolStripMenuItem
            // 
            this.colorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backToolStripMenuItem,
            this.forwardToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyToolStripMenuItem,
            this.toolStripMenuItem6,
            this.brightenToolStripMenuItem,
            this.darkenToolStripMenuItem,
            this.saturateToolStripMenuItem,
            this.desaturateToolStripMenuItem,
            this.toolStripMenuItem3,
            this.invertToolStripMenuItem});
            this.colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            this.colorToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.colorToolStripMenuItem.Text = "&Color";
            // 
            // brightenToolStripMenuItem
            // 
            this.brightenToolStripMenuItem.Name = "brightenToolStripMenuItem";
            this.brightenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.brightenToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.brightenToolStripMenuItem.Text = "B&righten";
            this.brightenToolStripMenuItem.Click += new System.EventHandler(this.brightenToolStripMenuItem_Click);
            // 
            // darkenToolStripMenuItem
            // 
            this.darkenToolStripMenuItem.Name = "darkenToolStripMenuItem";
            this.darkenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.darkenToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.darkenToolStripMenuItem.Text = "Dar&ken";
            this.darkenToolStripMenuItem.Click += new System.EventHandler(this.darkenToolStripMenuItem_Click);
            // 
            // saturateToolStripMenuItem
            // 
            this.saturateToolStripMenuItem.Name = "saturateToolStripMenuItem";
            this.saturateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.saturateToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.saturateToolStripMenuItem.Text = "&Saturate";
            this.saturateToolStripMenuItem.Click += new System.EventHandler(this.saturateToolStripMenuItem_Click);
            // 
            // desaturateToolStripMenuItem
            // 
            this.desaturateToolStripMenuItem.Name = "desaturateToolStripMenuItem";
            this.desaturateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.desaturateToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.desaturateToolStripMenuItem.Text = "&Desaturate";
            this.desaturateToolStripMenuItem.Click += new System.EventHandler(this.desaturateToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(187, 6);
            // 
            // invertToolStripMenuItem
            // 
            this.invertToolStripMenuItem.Name = "invertToolStripMenuItem";
            this.invertToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.invertToolStripMenuItem.Text = "&Invert";
            this.invertToolStripMenuItem.Click += new System.EventHandler(this.invertToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // saveAsHtmlDialog
            // 
            this.saveAsHtmlDialog.DefaultExt = "html";
            this.saveAsHtmlDialog.Filter = "HTML|*.html|All files|*.*";
            this.saveAsHtmlDialog.Title = "Save as HTML";
            // 
            // backToolStripMenuItem
            // 
            this.backToolStripMenuItem.Name = "backToolStripMenuItem";
            this.backToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.backToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.backToolStripMenuItem.Text = "&Back";
            this.backToolStripMenuItem.Click += new System.EventHandler(this.backToolStripMenuItem_Click);
            // 
            // forwardToolStripMenuItem
            // 
            this.forwardToolStripMenuItem.Name = "forwardToolStripMenuItem";
            this.forwardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Y)));
            this.forwardToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.forwardToolStripMenuItem.Text = "&Forward";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(187, 6);
            // 
            // acquireToolStripMenuItem
            // 
            this.acquireToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eyedropperToolStripMenuItem,
            this.randomToolStripMenuItem,
            this.pasteAcquireToolStripMenuItem});
            this.acquireToolStripMenuItem.Name = "acquireToolStripMenuItem";
            this.acquireToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.acquireToolStripMenuItem.Text = "&Acquire";
            // 
            // pasteAcquireToolStripMenuItem
            // 
            this.pasteAcquireToolStripMenuItem.Name = "pasteAcquireToolStripMenuItem";
            this.pasteAcquireToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteAcquireToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.pasteAcquireToolStripMenuItem.Text = "From &Clipboard";
            this.pasteAcquireToolStripMenuItem.Click += new System.EventHandler(this.pasteAcquireToolStripMenuItem_Click);
            // 
            // eyedropperToolStripMenuItem
            // 
            this.eyedropperToolStripMenuItem.Name = "eyedropperToolStripMenuItem";
            this.eyedropperToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.eyedropperToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.eyedropperToolStripMenuItem.Text = "&Eyedropper...";
            this.eyedropperToolStripMenuItem.Click += new System.EventHandler(this.eyedropperToolStripMenuItem_Click);
            // 
            // randomToolStripMenuItem
            // 
            this.randomToolStripMenuItem.Name = "randomToolStripMenuItem";
            this.randomToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.randomToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.randomToolStripMenuItem.Text = "&Random";
            this.randomToolStripMenuItem.Click += new System.EventHandler(this.randomButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.schemeBox);
            this.splitContainer1.Panel1.Controls.Add(this.resultsTable);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.paletteList);
            this.splitContainer1.Size = new System.Drawing.Size(348, 283);
            this.splitContainer1.SplitterDistance = 141;
            this.splitContainer1.TabIndex = 6;
            // 
            // paletteListImages
            // 
            this.paletteListImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.paletteListImages.ImageSize = new System.Drawing.Size(16, 16);
            this.paletteListImages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // paletteList
            // 
            this.paletteList.AllowDrop = true;
            this.paletteList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paletteList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameHeader,
            this.colorHeader});
            this.paletteList.ContextMenuStrip = this.paletteContextMenu;
            this.paletteList.LabelEdit = true;
            this.paletteList.Location = new System.Drawing.Point(12, 3);
            this.paletteList.Name = "paletteList";
            this.paletteList.Size = new System.Drawing.Size(324, 123);
            this.paletteList.SmallImageList = this.paletteListImages;
            this.paletteList.TabIndex = 0;
            this.paletteList.UseCompatibleStateImageBehavior = false;
            this.paletteList.View = System.Windows.Forms.View.Details;
            this.paletteList.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.paletteList_AfterLabelEdit);
            this.paletteList.ItemActivate += new System.EventHandler(this.paletteList_ItemActivate);
            this.paletteList.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.paletteList_ItemDrag);
            this.paletteList.DragDrop += new System.Windows.Forms.DragEventHandler(this.paletteList_DragDrop);
            this.paletteList.DragEnter += new System.Windows.Forms.DragEventHandler(this.paletteList_DragEnter);
            // 
            // nameHeader
            // 
            this.nameHeader.Text = "Name";
            this.nameHeader.Width = 100;
            // 
            // colorHeader
            // 
            this.colorHeader.Text = "Color";
            this.colorHeader.Width = 120;
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.undoToolStripMenuItem.Text = "&Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.redoToolStripMenuItem.Text = "&Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // savePaletteDialog
            // 
            this.savePaletteDialog.DefaultExt = "gpl";
            this.savePaletteDialog.Filter = "GIMP Palette|*.gpl|All files|*.*";
            this.savePaletteDialog.Title = "Save";
            // 
            // openPaletteDialog
            // 
            this.openPaletteDialog.DefaultExt = "gpl";
            this.openPaletteDialog.Filter = "GIMP Palette|*.gpl|All files|*.*";
            this.openPaletteDialog.Title = "Open";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(124, 6);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.addToolStripMenuItem.Text = "&Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // paletteContextMenu
            // 
            this.paletteContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.paletteContextMenu.Name = "paletteContextMenu";
            this.paletteContextMenu.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copySubmenuHexToolStripMenuItem,
            this.copySubmenuHslToolStripMenuItem,
            this.copySubmenuHsvToolStripMenuItem});
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            // 
            // copySubmenuHexToolStripMenuItem
            // 
            this.copySubmenuHexToolStripMenuItem.Name = "copySubmenuHexToolStripMenuItem";
            this.copySubmenuHexToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copySubmenuHexToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.copySubmenuHexToolStripMenuItem.Text = "Copy He&x";
            this.copySubmenuHexToolStripMenuItem.Click += new System.EventHandler(this.copySubmenuHexToolStripMenuItem_Click);
            // 
            // copySubmenuHslToolStripMenuItem
            // 
            this.copySubmenuHslToolStripMenuItem.Name = "copySubmenuHslToolStripMenuItem";
            this.copySubmenuHslToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.copySubmenuHslToolStripMenuItem.Text = "Copy HS&L";
            this.copySubmenuHslToolStripMenuItem.Click += new System.EventHandler(this.copySubmenuHslToolStripMenuItem_Click);
            // 
            // copySubmenuHsvToolStripMenuItem
            // 
            this.copySubmenuHsvToolStripMenuItem.Name = "copySubmenuHsvToolStripMenuItem";
            this.copySubmenuHsvToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.copySubmenuHsvToolStripMenuItem.Text = "Copy HS&V";
            this.copySubmenuHsvToolStripMenuItem.Click += new System.EventHandler(this.copySubmenuHsvToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(187, 6);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 307);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Colours";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.colorContextMenu.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.paletteContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox schemeBox;
        private System.Windows.Forms.TableLayoutPanel resultsTable;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ContextMenuStrip colorContextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyHexContextToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem copyHslContextToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyHsvContextToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsHTMLToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveAsHtmlDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brightenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saturateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desaturateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem invertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acquireToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteAcquireToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forwardToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem eyedropperToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView paletteList;
        private System.Windows.Forms.ColumnHeader nameHeader;
        private System.Windows.Forms.ColumnHeader colorHeader;
        private System.Windows.Forms.ImageList paletteListImages;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.SaveFileDialog savePaletteDialog;
        private System.Windows.Forms.OpenFileDialog openPaletteDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip paletteContextMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySubmenuHexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySubmenuHslToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySubmenuHsvToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
    }
}

