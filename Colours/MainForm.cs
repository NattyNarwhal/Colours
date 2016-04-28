﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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

        public MainForm(AppState state) : this()
        {
            app = new AppController(state);
            appPal = new AppPaletteController();
            app.ResultChanged += SyncAppViewState;
            appPal.PaletteChanged += SyncAppPalState;
            // send an initial event manually, because the event has
            // already been fired when it was initialized,
            // but without our handler
            SyncAppViewState(this, new EventArgs());
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

            EnableItems();
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

            EnableItems();
        }

        public void EnableItems()
        {
            Text = String.Format("{0} ({1} for {2})", appPal.Palette.Name,
                (string)schemeBox.SelectedItem, app.Color.ToHtml());

            undoToolStripMenuItem.Enabled = appPal.CanUndo();
            redoToolStripMenuItem.Enabled = appPal.CanRedo();
            backToolStripMenuItem.Enabled = app.CanUndo();
            forwardToolStripMenuItem.Enabled = app.CanRedo();

            brightenToolStripMenuItem.Enabled = app.CanBrighten();
            darkenToolStripMenuItem.Enabled = app.CanDarken();
            saturateToolStripMenuItem.Enabled = app.CanSaturate();
            desaturateToolStripMenuItem.Enabled = app.CanDesaturate();
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
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
                app.SetColor(ColorUtils.FromString(Clipboard.GetText()), true);
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
                    HtmlProofGenerator.GeneratePage(appPal.Palette.Name,
                        String.Join("", appPal.Palette.Colors.Select
                            (x => HtmlProofGenerator.GenerateTable(x)))
                    )
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

        public void SavePalette()
        {
            File.WriteAllText(appPal.FileName, appPal.Palette.ToString());
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (appPal.FileName == null)
            {
                if (savePaletteDialog.ShowDialog(this) == DialogResult.OK)
                {
                    appPal.FileName = savePaletteDialog.FileName;
                    SavePalette();
                }
            }
            else
                SavePalette();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (savePaletteDialog.ShowDialog(this) == DialogResult.OK)
            {
                appPal.FileName = savePaletteDialog.FileName;
                SavePalette();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openPaletteDialog.ShowDialog(this) == DialogResult.OK)
            {
                var p = new Palette(File.ReadAllLines(openPaletteDialog.FileName));
                appPal.NewFromPalette(p, openPaletteDialog.FileName);
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
            var p = appPal.Palette;
            p.Colors[e.Item].Name = e.Label;
            appPal.SetPalette(p);
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
            else if (e.AllowedEffect == DragDropEffects.Copy)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void paletteList_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                ListViewItem selected = paletteList.SelectedItems[0];

                Point cp = paletteList.PointToClient(new Point(e.X, e.Y));
                ListViewItem dragToItem = paletteList.GetItemAt(cp.X, cp.Y);
                int dropIndex = dragToItem.Index;

                var pal = appPal.Palette;

                pal.Colors.Remove((PaletteColor)selected.Tag);
                pal.Colors.Insert(dropIndex, (PaletteColor)selected.Tag);

                appPal.SetPalette(pal);
            }
            else if (e.Effect == DragDropEffects.Copy
                && e.Data.GetDataPresent(typeof(Color)))
            {
                Color c = (Color)e.Data.GetData(typeof(Color));

                var pal = appPal.Palette;

                pal.Colors.Add(new PaletteColor(c.ToRgbColor()));

                appPal.SetPalette(pal);
            }

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cb = (ColorButton)colorContextMenu.SourceControl;
            var p = appPal.Palette;
            p.Colors.Add(new PaletteColor(cb.Color));
            appPal.SetPalette(p);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var p = appPal.Palette;
            foreach (ListViewItem i in paletteList.SelectedItems)
                p.Colors.Remove((PaletteColor)i.Tag);
            appPal.SetPalette(p);
        }
    }
}
