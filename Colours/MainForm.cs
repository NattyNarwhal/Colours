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
            if (Properties.Settings.Default.CustomColors?.Count == 16)
            {
                colorDialog.CustomColors = Properties.Settings.Default.CustomColors.ToArray();
            }
            // don't init the app with this func; init with AppState
            // this is just a base ctor
        }

        public MainForm(InitialAppState state) : this()
        {
            app = new AppController(state);
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
            schemeBox.SelectedIndex = (int)app.SchemeType;

            resultsTable.Controls.Clear();
            resultsTable.ColumnCount = app.Results.Count;
            int i = 0;
            foreach (HsvColor next in app.Results)
            {
                resultsTable.ColumnStyles[i].SizeType = SizeType.Percent;
                resultsTable.ColumnStyles[i].Width = 100 / app.Results.Count;
                
                ColorButton cb = new ColorButton(next);
                if (app.HsvColor == next)
                    cb.Font = new Font(cb.Font, FontStyle.Bold);
                cb.Text = String.Format("{0}\r\n{1}",
                    next.ToRgb().ToHtml(), next.ToString());
                toolTip.SetToolTip(cb, String.Format("{0}\r\n{1}\r\n{2}",
                    next.ToRgb().ToHtml(), next.ToRgb().ToHslString(), next.ToString()));
                cb.ContextMenuStrip = colorContextMenu;
                cb.Dock = DockStyle.Fill;
                cb.Click += SchemeColor_Click;

                resultsTable.Controls.Add(cb, i++, 0);
            }

            UpdateUI();
        }

        public void SyncAppPalState(object sender, EventArgs e)
        {
            paletteList.Items.Clear();
            paletteListImages.Images.Clear();
            var i = 0;
            foreach (PaletteColor pc in appPal.Palette.Colors)
            {
                var lvi = new ListViewItem(pc.Name);
                using (var b = new Bitmap(16, 16))
                {
                    using (var g = Graphics.FromImage(b))
                    {
                        g.FillRectangle(new SolidBrush(pc.Color.ToGdiColor()), 0, 0, 16, 16);
                        g.DrawRectangle(Pens.Black, 0, 0, 15, 15);
                    }
                    paletteListImages.Images.Add(b);
                }
                lvi.Tag = pc;
                lvi.ImageIndex = i++;
                lvi.SubItems.Add(pc.Color.ToHtml());
                paletteList.Items.Add(lvi);
            }

            UpdateUI();
        }

        public void UpdateUI()
        {
            Text = String.Format("{0}{1} ({2} for {3})",
                appPal.Palette.Name, appPal.Dirty ? "*" : "",
                (string)schemeBox.SelectedItem, app.Color.ToHtml());

            var hasAny = appPal.Palette.Colors.Count > 0;
            var selected = paletteList.SelectedIndices.Count > 0;

            saveToolStripMenuItem.Enabled = appPal.Dirty;
            saveAsHTMLToolStripMenuItem.Enabled = hasAny;
            exportPhotoshopSwatchToolStripMenuItem.Enabled = hasAny;

            undoToolStripMenuItem.Enabled = appPal.CanUndo();
            redoToolStripMenuItem.Enabled = appPal.CanRedo();
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
            useToolStripMenuItem.Enabled = selected;
        }
        
        private void SchemeColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = ((ColorButton)sender).Color.ToGdiColor();
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                app.SetColor(colorDialog.Color.ToRgbColor(), true);
            }
        }

        private void schemeBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            app.SetSchemeType((SchemeType)schemeBox.SelectedIndex, true);
        }

        private void copyHexContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorButton cb = (ColorButton)colorContextMenu.SourceControl;
            Clipboard.SetText(cb.Color.ToHtml());
        }

        private void copyHslContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorButton cb = (ColorButton)colorContextMenu.SourceControl;
            Clipboard.SetText(cb.Color.ToHslString());
        }

        private void copyHsvContextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorButton cb = (ColorButton)colorContextMenu.SourceControl;
            Clipboard.SetText(cb.HsvColor.ToString());
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
            Properties.Settings.Default.CustomColors = new ColorList(colorDialog.CustomColors);
            Properties.Settings.Default.LastColor = app.Color.ToGdiColor();
            Properties.Settings.Default.Save();
        }

        private void randomButton_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            app.SetColor(new RgbColor(r.Next(255), r.Next(255), r.Next(255)), true);
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
            try {
                if (Clipboard.ContainsText())
                {
                    var clip = Clipboard.GetText();
                    if (clip.StartsWith("pc"))
                    {
                        foreach (var pc in Regex.Split(clip, Environment.NewLine))
                        {
                            if (pc == "pc" || String.IsNullOrWhiteSpace(pc))
                                continue;
                            app.SetColor(new PaletteColor(pc).Color, true);
                            break;
                        }
                        SyncAppPalState(this, new EventArgs());
                    }
                    else
                        app.SetColor(ColorUtils.FromString(Clipboard.GetText()), true);
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
            Clipboard.SetText(app.HsvColor.ToString());
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.SetColor(app.Color.Invert(), true);
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
                app.SetColor(ef.Color.ToRgbColor(), true);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog(this);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            if (forceDialog || appPal.FileName == null)
            {
                if (savePaletteDialog.ShowDialog(this) == DialogResult.OK)
                {
                    appPal.FileName = savePaletteDialog.FileName;
                }
                else return false;
            }

            File.WriteAllText(appPal.FileName, appPal.Palette.ToString());
            appPal.Dirty = false;
            // update the titlebar's dirtiness
            UpdateUI();
            return true;
        }

        public void OpenPalette(string fileName)
        {
            appPal.NewFromPalette(new Palette(File.ReadAllLines(fileName)), fileName);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (appPal.Dirty)
                if (UnsavedPrompt()) return;
            SavePalette(false);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (appPal.Dirty)
                if (UnsavedPrompt()) return;
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
                app.SetColor(appPal.Palette.Colors[paletteList.SelectedIndices[0]].Color, true);
        }

        private void paletteList_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (appPal.Palette.Colors.Count > e.Item)
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
                ListViewItem selected = paletteList.SelectedItems[0];

                Point cp = paletteList.PointToClient(new Point(e.X, e.Y));
                ListViewItem dragToItem = paletteList.GetItemAt(cp.X, cp.Y);
                if (dragToItem != null)
                {
                    int dropIndex = dragToItem.Index;

                    var pc = (PaletteColor)selected.Tag;
                    appPal.MoveColor(pc, dropIndex);
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
            appPal.DeleteColors(paletteList.SelectedItems
                .Cast<ListViewItem>().Select(x => (PaletteColor)x.Tag));
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeletePaletteItems();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Clipboard.ContainsText())
                {
                    var clip = Clipboard.GetText();
                    if (clip.StartsWith("pc"))
                    {
                        foreach (var pc in Regex.Split(clip, Environment.NewLine))
                        {
                            if (pc == "pc" || String.IsNullOrWhiteSpace(pc))
                                continue;
                            appPal.AppendColor(new PaletteColor(pc), fireEvent: false);
                        }
                        SyncAppPalState(this, new EventArgs());
                    }
                    else
                        appPal.AppendColor(ColorUtils.FromString(clip).ToRgb());
                }
            }
            catch (ArgumentException) // these are harmless
            {

            }
        }

        public void CopyPaletteColor()
        {
            // HACK: ideally, we'd just send a PaletteColor or List of
            // those, except that won't work. ContainsData(type) says
            // true, GetData(type) says null.
            var sb = new StringBuilder("pc" + Environment.NewLine);
            if (paletteList.SelectedIndices.Count > 0)
            {
                foreach (ListViewItem i in paletteList.SelectedItems)
                    sb.AppendLine(((PaletteColor)i.Tag).ToString());
                Clipboard.SetText(sb.ToString());
            }
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
            appPal.AppendColor(app.Color);
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (paletteList.SelectedItems.Count > 0)
                paletteList.SelectedItems[0].BeginEdit();
        }

        private void saveAsHTMLProofToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveAsHtmlDialog.ShowDialog(this) == DialogResult.OK)
            {
                File.WriteAllText(saveAsHtmlDialog.FileName,
                    HtmlProofGenerator.GeneratePage(String.Format("{0} of {1}",
                        app.SchemeType, app.Color.ToHtml()),
                        HtmlProofGenerator.GenerateTable(app.Results))
                );
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pd = new PalettePropertiesForm()
            {
                PaletteTitle = appPal.Palette.Name,
                PaletteColumns = appPal.Palette.Columns,
                PaletteComments = appPal.Palette.Comments
            };
            if (pd.ShowDialog(this) == DialogResult.OK)
            {
                var p = new Palette(appPal.Palette)
                {
                    Name = pd.PaletteTitle,
                    Columns = pd.PaletteColumns,
                    Comments = pd.PaletteComments
                };
                appPal.SetPalette(p);
            }
        }

        private void paletteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // update cut/copy/paste, etc.
            UpdateUI();
        }

        private void addAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var c in app.Results)
                appPal.AppendColor(c.ToRgb());
        }

        private void importPhotoshopSwatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (importPhotoshopDialog.ShowDialog(this) == DialogResult.OK)
                {
                    var p = AcoConverter.FromPhotoshopPalette(
                        File.ReadAllBytes(importPhotoshopDialog.FileName));
                    appPal.NewFromPalette(p);
                }
            }
            catch (NotImplementedException)
            {
                MessageBox.Show(this, "The Photoshop palette has an unsupported colourspace..",
                    "Colours", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show(this, "The Photoshop palette could not be converted.",
                    "Colours", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportPhotoshopSwatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (exportPhotoshopDialog.ShowDialog(this) == DialogResult.OK)
                {
                    File.WriteAllBytes(exportPhotoshopDialog.FileName,
                        AcoConverter.ToPhotoshopPalette(appPal.Palette));
                }
            }
            catch (Exception)
            {
                MessageBox.Show(this, "The Photoshop palette could not be converted.",
                    "Colours", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
