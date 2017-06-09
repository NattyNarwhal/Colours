using Colours.App; // should we move the apps into this NS too?
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colours
{
    public partial class MainForm : Form
    {
        public AppController app;
        public AppPaletteController appPal;

        public MainForm()
        {
            InitializeComponent();
            // designer doesnt want to pick from resx
            Icon = Properties.Resources.Colours;

            schemeBox.Items.AddRange(Scheme.GetSchemes().ToArray());

            // don't init the app with this func; init with AppState
            // this is just a base ctor
        }

        public MainForm(AppInitState state) : this()
        {
            app = new AppController(state.MixerState);
            appPal = new AppPaletteController();
            if (state.PaletteFileName != null)
            {
                appPal.FileName = state.PaletteFileName;
            }

            app.ResultChanged += SyncAppViewState;
            appPal.PaletteChanged += SyncAppPalState;
            // send an initial event manually, because the event has
            // already been fired when it was initialized,
            // but without our handler
            SyncAppViewState(this, new EventArgs());
            SyncAppPalState(this, new EventArgs());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (appPal.FileName != null)
                OpenPalette(appPal.FileName);
        }

        /// <summary>
        /// Handle the controller's state updates and sync them to the view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SyncAppViewState(object sender, EventArgs e)
        {
            schemeBox.SelectedItem = schemeBox.Items.Cast<Scheme>()
                .Where(x => x.Type == app.SchemeType).FirstOrDefault();

            resultsTable.Controls.Clear();
            resultsTable.RowCount = app.Results.Count;
            int i = 0;
            foreach (HsvColor next in app.Results)
            {
                resultsTable.RowStyles[i].SizeType = SizeType.Percent;
                resultsTable.RowStyles[i].Height = 100 / app.Results.Count;

                ColorButton cb = new ColorButton(next);
                if (app.HsvColor == next)
                    cb.Font = new Font(cb.Font, FontStyle.Bold);
                cb.Text = String.Format("{0}\r\n{1}",
                    next.ToRgb().ToHtml(), next.ToCssString());
                toolTip.SetToolTip(cb, String.Format("{0}\r\n{1}\r\n{2}",
                    next.ToRgb().ToHtml(), next.ToRgb().ToHslString(), next.ToCssString()));
                cb.ContextMenuStrip = colorContextMenu;
                cb.Dock = DockStyle.Fill;
                cb.Click += SchemeColor_Click;

                resultsTable.Controls.Add(cb, 0, i++);
            }

            UpdateUI();
        }

        public void SyncAppPalState(object sender, EventArgs e)
        {
            colorGrid1.Palette = appPal.Palette;

            paletteList.SuspendLayout();
            paletteList.Items.Clear();
            paletteListImages.Images.Clear();
            var i = 0;
            foreach (PaletteColor pc in appPal.Palette.Colors)
            {
                var lvi = new ListViewItem(pc.Name);
                paletteListImages.Images.Add(RenderColorIcon.RenderBitmap(pc.Color.ToRgb()));
                lvi.Tag = pc;
                lvi.ImageIndex = i++;
                lvi.SubItems.Add(NativeFormat ?
                    pc.Color.ToString() : pc.Color.ToRgb().ToHtml());
                lvi.ToolTipText = pc.Metadata;
                paletteList.Items.Add(lvi);
            }
            paletteList.ResumeLayout(false);
            
            UpdateUI();
        }

        public void UpdateUI()
        {
            Text = string.Format("{0}{1} ({2} for {3})",
                appPal.PaletteName, appPal.Dirty ? "*" : "",
                schemeBox.SelectedItem.ToString(), app.Color.ToHtml());

            var hasAny = appPal.Palette.Colors.Count > 0;
            var hasMultiple = appPal.Palette.Colors.Count > 1;
            var selected = SelectedItems.Count() > 0;
            var canPaste = Clipboard.ContainsData("LPC") ||
                Clipboard.ContainsText();
            var supportsMetadata = appPal.Palette is GimpPalette ||
                appPal.Palette is AcbPalette || appPal.Palette is ActPalette ||
                appPal.Palette is NativePalette;
            var supportsColourMetadata = appPal.Palette is AcbPalette ||
                appPal.Palette is NativePalette;

            propertiesToolStripMenuItem.Enabled = supportsMetadata;
            propertiesToolStripMenuItem.ToolTipText = supportsMetadata ?
                "" : "This file format doesn't support metadata.";
            changeMetadataToolStripMenuItem.Enabled = selected && supportsColourMetadata;
            changeMetadataSubmenuToolStripMenuItem.Enabled = selected && supportsColourMetadata;
            changeMetadataToolStripMenuItem.ToolTipText = supportsColourMetadata ?
                "" : "This file format doesn't support metadata.";
            changeMetadataSubmenuToolStripMenuItem.ToolTipText = supportsColourMetadata ?
                "" : "This file format doesn't support metadata.";

            saveToolStripMenuItem.Enabled = appPal.Dirty;
            saveAsHTMLToolStripMenuItem.Enabled = hasAny;

            undoToolStripMenuItem.Enabled = appPal.CanUndo();
            redoToolStripMenuItem.Enabled = appPal.CanRedo();
            undoToolStripMenuItem.Text = appPal.CanUndo() ?
                "Undo " + appPal.UndoHistory.Peek().Name : "Can't Undo";
            redoToolStripMenuItem.Text = appPal.CanRedo() ?
                "Redo " + appPal.RedoHistory.Peek().Name : "Can't Redo";

            backToolStripMenuItem.Enabled = app.CanUndo();
            forwardToolStripMenuItem.Enabled = app.CanRedo();

            brightenToolStripMenuItem.Enabled = app.CanBrighten();
            darkenToolStripMenuItem.Enabled = app.CanDarken();
            saturateToolStripMenuItem.Enabled = app.CanSaturate();
            desaturateToolStripMenuItem.Enabled = app.CanDesaturate();

            cutSubmenuToolStripMenuItem.Enabled = selected;
            cutToolStripMenuItem.Enabled = selected;
            copyPCSubmenuToolStripMenuItem.Enabled = selected;
            copyPCToolStripMenuItem.Enabled = selected;
            deleteSubmenuToolStripMenuItem.Enabled = selected;
            deleteToolStripMenuItem.Enabled = selected;
            renameToolStripMenuItem.Enabled = selected;
            renameSubmenuToolStripMenuItem.Enabled = selected;
            useToolStripMenuItem.Enabled = selected;
            useSubmenuToolStripMenuItem.Enabled = selected;
            changeToolStripMenuItem.Enabled = selected;
            changeSubmenuToolStripMenuItem.Enabled = selected;
            selectAllToolStripMenuItem.Enabled = !GridView && hasAny;
            sortToolStripMenuItem.Enabled = hasMultiple;

            pasteAcquireToolStripMenuItem.Enabled = canPaste;
            pasteToolStripMenuItem.Enabled = canPaste;

            nativeFormatToolStripMenuItem.Enabled = !GridView;

            UpdateUIPaletteList();
        }

        public void UpdateUIPaletteList()
        {
            paletteList.AutoResizeColumn(1, paletteList.Items.Count > 0 ?
                ColumnHeaderAutoResizeStyle.ColumnContent :
                ColumnHeaderAutoResizeStyle.HeaderSize);

            // ClientSize will deal with only the chunk we control, not the
            // scroll bar and borders. However, sometimes shrinking resize
            // events cause a horizontal scrollbar to appear (which disappears
            // when you click it) It doesn't help if you shrink it quickly.
            nameHeader.Width = paletteList.ClientSize.Width - colorHeader.Width;
        }

        public bool GridView => colorGrid1.Visible;
        public bool NativeFormat => nativeFormatToolStripMenuItem.Checked;

        // if you need to get the LVIs, use it directly
        public IEnumerable<PaletteColor> SelectedItems
        {
            get
            {
                if (GridView)
                {
                    if (colorGrid1.FocusedColor != null)
                    {
                        return new List<PaletteColor>() { colorGrid1.FocusedColor };
                    }
                    else
                    {
                        return new List<PaletteColor>();
                    }
                }
                else
                {
                    return paletteList.SelectedItems.Cast<ListViewItem>()
                        .Select(x => (PaletteColor)x.Tag);
                }
            }
        }

        private void SchemeColor_Click(object sender, EventArgs e)
        {
            // TODO: This needs to handle
            colorDialog.Color = ((ColorButton)sender).Color.ToRgb().ToGdiColor();

            if (SelectedItems.Count() > 0)
                colorDialog.CustomColors = SelectedItems
                    .Take(16)
                    .Select(x =>
                        ColorTranslator.ToOle(x.Color.ToRgb().ToGdiColor()))
                    .ToArray();
            else
                colorDialog.CustomColors = appPal.Palette.Colors.Take(16)
                    .Select(x => ColorTranslator.ToOle(x.Color.ToRgb().ToGdiColor()))
                    .ToArray();

            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                app.SetColor(colorDialog.Color.ToRgbColor());
            }
        }

        private void schemeBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            app.SetSchemeType(((Scheme)schemeBox.SelectedItem).Type);
        }

        private void copyHexContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorButton cb = (ColorButton)colorContextMenu.SourceControl;
            Clipboard.SetText(cb.Color.ToRgb().ToHtml());
        }

        private void copyHslContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorButton cb = (ColorButton)colorContextMenu.SourceControl;
            Clipboard.SetText(cb.Color.ToRgb().ToHslString());
        }

        private void copyHsvContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorButton cb = (ColorButton)colorContextMenu.SourceControl;
            Clipboard.SetText((cb.Color is HsvColor ?
                (HsvColor)cb.Color : cb.Color.ToRgb().ToHsv()).ToCssString());
        }

        /// <summary>
        /// Gives a prompt with options on how to proceed regarding
        /// unsaved changes.
        /// </summary>
        /// <returns>If the user has canceled the actions.</returns>
        public bool UnsavedPrompt()
        {
            var r = MessageBox.Show(this,
                    "There are unsaved changes to the palette. Do you want to save?",
                    "Colours", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);
            switch (r)
            {
                case DialogResult.Yes:
                    SavePalette(false);
                    return false;
                case DialogResult.Cancel:
                    return true;
                case DialogResult.No:
                default:
                    return false;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (appPal.Dirty)
            {
                e.Cancel = UnsavedPrompt();
            }

            // save settings
            Properties.Settings.Default.SchemeType = app.SchemeType;
            Properties.Settings.Default.LastColor = app.Color.ToGdiColor();
            Properties.Settings.Default.Save();
        }

        private void randomButton_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            app.SetColor(new RgbColor(
                r.Next(ushort.MaxValue),
                r.Next(ushort.MaxValue),
                r.Next(ushort.MaxValue),
                16));
        }

        private void brightenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.Brighten();
        }

        private void darkenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.Darken();
        }

        private void saturateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.Saturate();
        }

        private void desaturateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.Desaturate();
        }

        private void pasteAcquireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Clipboard.ContainsData("LPC"))
                {
                    var o = (List<PaletteColor>)Clipboard.GetData("LPC");
                    app.SetColor(o.First().Color.ToRgb());
                }
                else if (Clipboard.ContainsText())
                {
                    var clip = Clipboard.GetText();
                    app.SetColor(ColorUtils.FromString(Clipboard.GetText()));
                }
            }
            catch (ArgumentException) // these are harmless
            {

            }
        }

        private void copySubmenuHexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(app.Color.ToHtml());
        }

        private void copySubmenuHslToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(app.Color.ToHslString());
        }

        private void copySubmenuHsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(app.HsvColor.ToCssString());
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.SetColor(app.Color.Invert());
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.Undo();
        }

        private void forwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.Redo();
        }

        private void saveAsHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(saveAsHtmlDialog.FileName))
                saveAsHtmlDialog.FileName = appPal.PaletteName;
            if (saveAsHtmlDialog.ShowDialog(this) == DialogResult.OK)
            {
                File.WriteAllText(saveAsHtmlDialog.FileName,
                    HtmlProofGenerator.GeneratePage(appPal.Palette)
                );
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void eyedropperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EyedropperForm ef = new EyedropperForm();
            if (ef.ShowDialog(this) == DialogResult.OK)
            {
                app.SetColor(ef.Color.ToRgbColor());
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog(this);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (appPal.Dirty)
                if (UnsavedPrompt()) return;
            appPal.New();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (appPal.CanUndo())
                appPal.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (appPal.CanRedo())
                appPal.Redo();
        }

        public bool SavePalette(bool forceDialog)
        {
            var freshFile = forceDialog || appPal.FileName == null;
            var fileName = freshFile ? "" : appPal.FileName;

            if (freshFile)
            {
                // if we don't have a name already, give it one based
                // on its palette title name - the save dialog will
                // handle the extension
                if (appPal.FileName == null)
                    savePaletteDialog.FileName = appPal.PaletteName;
                // set the file name later, in case more interruptions
                // will stop us from writing the file (user intervention,
                // exceptions)
                if (savePaletteDialog.ShowDialog(this) == DialogResult.OK)
                    fileName = savePaletteDialog.FileName;
                else return false;
            }

            // warn if the format doesn't support metadata
            if (!(fileName.EndsWith(".gpl") || fileName.EndsWith(".colors")
                || fileName.EndsWith(".acb")))
                MessageBox.Show(this,
                    "This format doesn't support metadata like comments. Metadata will be lost when the file is reloaded.",
                    "Colours", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            try
            {
                appPal.ConvertPalette(fileName);
                File.WriteAllBytes(fileName, appPal.Palette.ToFile());
                appPal.FileName = fileName;
                appPal.Dirty = false;
                // update the titlebar's dirtiness
                UpdateUI();
                return true;
            }
            catch (PaletteException e)
            {
                MessageBox.Show(this, e.Message, "Palette Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void OpenPalette(string fileName)
        {
            try
            {
                if (fileName.ToLower().EndsWith(".aco"))
                {
                    var p = new AcoPalette(File.ReadAllBytes(fileName));
                    appPal.NewFromPalette(p, fileName);
                }
                else if (fileName.ToLower().EndsWith(".act"))
                {
                    var p = new ActPalette(File.ReadAllBytes(fileName));
                    appPal.NewFromPalette(p, fileName);
                }
                else if (fileName.ToLower().EndsWith(".ase"))
                {
                    var p = new AsePalette(File.ReadAllBytes(fileName));
                    appPal.NewFromPalette(p, fileName);
                }
                else if (fileName.ToLower().EndsWith(".acb"))
                {
                    var p = new AcbPalette(File.ReadAllBytes(fileName));
                    appPal.NewFromPalette(p, fileName);
                }
                else if (fileName.ToLower().EndsWith(".pal"))
                {
                    var p = new MsRiffPalette(File.ReadAllBytes(fileName));
                    appPal.NewFromPalette(p, fileName);
                }
                else if (fileName.ToLower().EndsWith(".gpl"))
                {
                    var p = new GimpPalette(File.ReadAllLines(fileName));
                    appPal.NewFromPalette(p, fileName);
                }
                else if (fileName.ToLower().EndsWith(".psppalette"))
                {
                    var p = new PspPalette(File.ReadAllLines(fileName));
                    appPal.NewFromPalette(p, fileName);
                }
                // implied to be native palette
                else
                {
                    var p = NativePalette.CreateFromFile(File.ReadAllText(fileName));
                    appPal.NewFromPalette(p, fileName);
                }
            }
            catch (PaletteException e)
            {
                MessageBox.Show(this, e.Message, "Palette Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavePalette(false);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavePalette(true);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (appPal.Dirty)
                if (UnsavedPrompt()) return;
            if (openPaletteDialog.ShowDialog(this) == DialogResult.OK)
            {
                OpenPalette(openPaletteDialog.FileName);
                savePaletteDialog.FileName = openPaletteDialog.FileName;
            }
        }

        private void paletteList_ItemActivate(object sender, EventArgs e)
        {
            if (paletteList.SelectedIndices.Count > 0)
                app.SetColor(appPal.Palette.Colors[paletteList.SelectedIndices[0]].Color.ToRgb());
        }

        private void paletteList_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            // Null e.Label is from a cancelled edit, and empty/whitespace
            // items could confuse the parsers.
            if (appPal.Palette.Colors.Count > e.Item && !string.IsNullOrWhiteSpace(e.Label))
                appPal.RenameColor(e.Item, e.Label);
        }

        private void paletteList_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void paletteList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.AllowedEffect == DragDropEffects.Move)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void paletteList_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                Point cp = paletteList.PointToClient(new Point(e.X, e.Y));
                ListViewItem dragToItem = paletteList.GetItemAt(cp.X, cp.Y);
                if (!paletteList.SelectedItems.Contains(dragToItem))
                {
                    var dropIndex = dragToItem != null ?
                        appPal.Palette.Colors[dragToItem.Index] :
                        null;
                    appPal.MoveColors(SelectedItems, dropIndex);
                }
            }
        }

        private void addSubmenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cb = (ColorButton)colorContextMenu.SourceControl;
            appPal.AppendColor(new PaletteColor(cb.Color));
        }

        public void DeletePaletteItems()
        {
            appPal.DeleteColors(SelectedItems);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeletePaletteItems();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Clipboard.ContainsData("LPC"))
                {
                    var o = (List<PaletteColor>)Clipboard.GetData("LPC");
                    appPal.AppendColors(o, action: "Paste Colors");
                }
                else if (Clipboard.ContainsText())
                {
                    var clip = Clipboard.GetText();
                    appPal.AppendColor(ColorUtils.FromString(clip).ToRgb(), action: "Paste Color");
                }
            }
            catch (ArgumentException) // these are harmless
            {

            }
        }

        public void CopyPaletteColor()
        {
            Clipboard.SetData("LPC", SelectedItems.ToList());
        }

        private void copyPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyPaletteColor();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyPaletteColor();
            DeletePaletteItems();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            appPal.AppendColor(app.HsvColor);
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedItems.Count() == 1)
            {
                if (!GridView)
                {
                    paletteList.SelectedItems[0].BeginEdit();
                }
                else
                {
                    var pc = SelectedItems.First();
                    var rf = new ColorChangeTextForm(
                        string.Format("Rename \"{0}\" ({1})", pc.Name, pc.Color.ToRgb().ToHtml()),
                        pc.Name, pc);
                    if (rf.ShowDialog(this) == DialogResult.OK)
                        appPal.RenameColor(pc, rf.NewValue);
                }
            }
            else if (SelectedItems.Count() > 1)
            {
                var pc = SelectedItems.First();
                var rf = new ColorChangeTextMultipleForm("Rename");
                if (rf.ShowDialog(this) == DialogResult.OK)
                {
                    var names = Enumerable.Repeat(rf.NewText, SelectedItems.Count()).ToArray();
                    if (rf.Numbered)
                    {
                        for (int i = 0; names.Count() > i; i++)
                        {
                            names[i] = string.Format("{0} ({1})", names[i], i + 1);
                        }
                    }
                    appPal.RenameColors(SelectedItems
                        .Zip(names, (x, y) => new { Key = x, Value = y })
                        .ToDictionary(x => x.Key, x => x.Value));
                }
            }
        }

        private void saveAsHTMLProofToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var title = String.Format("{0} of {1}", app.SchemeType, app.Color.ToHtml());
            if (String.IsNullOrEmpty(saveAsHtmlDialog.FileName))
                saveAsHtmlDialog.FileName = title;
            if (saveAsHtmlDialog.ShowDialog(this) == DialogResult.OK)
            {
                File.WriteAllText(saveAsHtmlDialog.FileName,
                    HtmlProofGenerator.GeneratePage(title,
                        HtmlProofGenerator.GenerateTable(app.Results))
                );
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pd = new PalettePropertiesForm(appPal.Palette);
            if (pd.ShowDialog(this) == DialogResult.OK)
            {
                var p = appPal.Palette.Clone();
                if (p is IBucketedPalette)
                {
                    ((IBucketedPalette)p).BucketSize = pd.PaletteBucket;
                }
                if (p is INamedPalette)
                {
                    ((INamedPalette)p).Name = pd.PaletteTitle;
                }
                if (p is GimpPalette)
                {
                    ((GimpPalette)p).Comments = pd.GimpPaletteComments;
                }
                if (p is AcbPalette)
                {
                    // the ref stays the same
                    var acb = (AcbPalette)p;

                    acb.ID = pd.AcbId;
                    acb.DefaultSelection = pd.AcbDefaultColor;
                    acb.Prefix = pd.AcbPrefix;
                    acb.Postfix = pd.AcbPostfix;
                    acb.Description = pd.AcbDescription;
                    acb.ColorSpace = pd.AcbColorSpace;
                    acb.Purpose = pd.AcbSpotProcess;
                }
                if (p is ActPalette)
                {
                    var act = (ActPalette)p;

                    act.TransparentIndex = pd.ActTransparencyIndex;
                }
                appPal.SetPalette(p, action: "Properties Change");
            }
        }

        private void paletteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // update cut/copy/paste, etc.
            UpdateUI();
        }

        private void addAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            appPal.AppendColors(app.Results);
        }

        private void blendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BlendForm f = new BlendForm(app.Color, 
                paletteList.SelectedIndices.Count > 0 ?
                ((PaletteColor)paletteList.SelectedItems[0].Tag).Color.ToRgb() : app.Color);
            if (f.ShowDialog(this) == DialogResult.OK)
                appPal.AppendColors(f.SelectedItems);
        }

        private void paletteList_SizeChanged(object sender, EventArgs e)
        {
            UpdateUIPaletteList();
        }

        public void SelectAll(bool select = true)
        {
            foreach (ListViewItem i in paletteList.Items)
                i.Selected = select;
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridToolStripMenuItem.Checked = false;
            listToolStripMenuItem.Checked = true;

            var selection = SelectedItems.FirstOrDefault();

            colorGrid1.Enabled = false;
            colorGrid1.Visible = false;
            // transfer selections
            SelectAll(false);
            if (selection != null)
                paletteList.Items.Cast<ListViewItem>()
                    .Where(x => x.Tag == selection)
                    .FirstOrDefault()
                    .Selected = true;
            paletteList.Enabled = true;
            paletteList.Visible = true;
            paletteList.Focus();
            UpdateUI();
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridToolStripMenuItem.Checked = true;
            listToolStripMenuItem.Checked = false;

            var selection = SelectedItems.FirstOrDefault();

            colorGrid1.Enabled = true;
            colorGrid1.Visible = true;
            if (selection != null)
                colorGrid1.FocusedColor = selection;
            paletteList.Enabled = false;
            paletteList.Visible = false;
            UpdateUI();
        }

        private void colorGrid1_FocusedColorChange(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void useToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedItems.Count() > 0)
                app.SetColor(SelectedItems.FirstOrDefault().Color.ToRgb());
        }

        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedItems.Count() > 0)
            {
                colorDialog.Color = SelectedItems.FirstOrDefault().Color.ToRgb().ToGdiColor();
                if (colorDialog.ShowDialog(this) == DialogResult.OK)
                {
                    appPal.ChangeColors(SelectedItems, colorDialog.Color.ToRgbColor());
                }
            }
        }

        private void colorGrid1_ColorClick(object sender, EventArgs e)
        {
            if (sender is ColorButton)
            {
                app.SetColor(((ColorButton)sender).Color);
            }
        }

        private void colorGrid1_ColorDrag(object sender, ColorDragEventArgs e)
        {
            appPal.MoveColor(e.DraggedColor, e.TargetColor);
        }

        private void sortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sf = new SortForm();
            if (sf.ShowDialog(this) == DialogResult.OK)
            {
                appPal.SortColors(sf.SortBy, sf.Ascending);
            }
        }

        private void changeMetadataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedItems.Count() == 1)
            {
                var pc = SelectedItems.First();
                var rf = new ColorChangeTextForm(
                    string.Format("Metadata for \"{0}\" ({1})", pc.Name, pc.Color.ToRgb().ToHtml()),
                    pc.Metadata, pc);
                if (rf.ShowDialog(this) == DialogResult.OK)
                    appPal.ChangeMetadata(pc, rf.NewValue);
            }
            else if (SelectedItems.Count() > 1)
            {
                var pc = SelectedItems.First();
                var rf = new ColorChangeTextMultipleForm("Metadata", false);
                if (rf.ShowDialog(this) == DialogResult.OK)
                {
                    var names = Enumerable.Repeat(rf.NewText, SelectedItems.Count()).ToArray();
                    if (rf.Numbered)
                    {
                        for (int i = 0; names.Count() > i; i++)
                        {
                            names[i] = string.Format("{0} ({1})", names[i], i + 1);
                        }
                    }
                    appPal.ChangeMetadata(SelectedItems
                        .Zip(names, (x, y) => new { Key = x, Value = y })
                        .ToDictionary(x => x.Key, x => x.Value));
                }
            }
        }

        private void nativeFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: This is an expensive operation, consider putting this into
            // UpdateUIPaletteList instead?
            SyncAppPalState(nativeFormatToolStripMenuItem, e);
        }
    }
}
