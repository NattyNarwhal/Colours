using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        public MainForm()
        {
            InitializeComponent();
            comboBox1.SelectedItem = Properties.Settings.Default.SchemeType
                ?? (string)comboBox1.Items[0];
            HsvColor initial = new HsvColor(Properties.Settings.Default.LastColor);
            app = new AppController(initial, (SchemeType)comboBox1.SelectedIndex);

            if (Properties.Settings.Default.CustomColors?.Count == 16)
            {
                colorDialog1.CustomColors = Properties.Settings.Default.CustomColors.ToArray();
            }

            SyncAppViewState();
        }

        public void SyncAppViewState()
        {
            Text = String.Format("{0} for {1}", app.SchemeType,
                ColorTranslator.ToHtml(app.Color));

            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnCount = app.Results.Count;
            int i = 0;
            foreach (HsvColor next in app.Results)
            {
                tableLayoutPanel1.ColumnStyles[i].SizeType = SizeType.Percent;
                tableLayoutPanel1.ColumnStyles[i].Width = 100 / app.Results.Count;

                ColorButton cb = new ColorButton(next);
                if (i == 0)
                    cb.Font = new Font(cb.Font, FontStyle.Bold);
                cb.Text = String.Format("{0}\r\n{1}",
                    ColorTranslator.ToHtml(cb.Color), next.ToString());
                toolTip1.SetToolTip(cb, String.Format("{0}\r\n{1}\r\n{2}\r\n{3}",
                    ColorTranslator.ToHtml(cb.Color), cb.Color.ToRgbString(),
                    cb.Color.ToHslString(), next.ToString()));
                cb.ContextMenuStrip = contextMenuStrip1;
                cb.Dock = DockStyle.Fill;
                cb.Click += SchemeColor_Click;

                tableLayoutPanel1.Controls.Add(cb, i++, 0);
            }

            EnableItems();
        }

        public void EnableItems()
        {
            undoToolStripMenuItem.Enabled = app.CanUndo();
            redoToolStripMenuItem.Enabled = app.CanRedo();

            brightenToolStripMenuItem.Enabled = app.CanBrighten();
            darkenToolStripMenuItem.Enabled = app.CanDarken();
            saturateToolStripMenuItem.Enabled = app.CanSaturate();
            desaturateToolStripMenuItem.Enabled = app.CanDesaturate();
        }
        
        private void SchemeColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = ((ColorButton)sender).Color;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                app.SetColor(colorDialog1.Color, true);
                SyncAppViewState();
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            app.SchemeType = (SchemeType)comboBox1.SelectedIndex;
            app.GetSchemeResults();
            SyncAppViewState();
        }

        private void copyHexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorButton cb = (ColorButton)contextMenuStrip1.SourceControl;
            Clipboard.SetText(ColorTranslator.ToHtml(cb.Color));
        }

        private void copyCSSRGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorButton cb = (ColorButton)contextMenuStrip1.SourceControl;
            Clipboard.SetText(cb.Color.ToRgbString());
        }

        private void copyCSSHSLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorButton cb = (ColorButton)contextMenuStrip1.SourceControl;
            Clipboard.SetText(cb.Color.ToHslString());
        }

        private void copyCSSHSVToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColorButton cb = (ColorButton)contextMenuStrip1.SourceControl;
            Clipboard.SetText(cb.HsvColor.ToString());
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // save settings
            Properties.Settings.Default.SchemeType = (string)comboBox1.SelectedItem;
            Properties.Settings.Default.CustomColors = new ColorList(colorDialog1.CustomColors);
            Properties.Settings.Default.LastColor = app.Color;
            Properties.Settings.Default.Save();
        }

        private void randomButton_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            app.SetColor(Color.FromArgb(r.Next(255), r.Next(255), r.Next(255)), true);
            SyncAppViewState();
        }

        private void brightenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.Brighten();
            SyncAppViewState();
        }

        private void darkenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.Darken();
            SyncAppViewState();
        }

        private void saturateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.Saturate();
            SyncAppViewState();
        }

        private void desaturateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.Desaturate();
            SyncAppViewState();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                app.SetColor(ColorTranslator.FromHtml(Clipboard.GetText()), true);
                SyncAppViewState();
            }
            catch (Exception)
            {

            }
        }

        private void copyHexToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(ColorTranslator.ToHtml(app.Color));
        }

        private void copyCSSRGBToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(app.Color.ToRgbString());
        }

        private void copyCSSHSLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(app.Color.ToHslString());
        }

        private void copyCSSHSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(app.HsvColor.ToString());
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.SetColor(app.Color.Invert(), true);
            SyncAppViewState();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.Undo();
            SyncAppViewState();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.Redo();
            SyncAppViewState();
        }

        private void saveAsHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveAsHtmlDialog.ShowDialog(this) == DialogResult.OK)
            {
                File.WriteAllText(saveAsHtmlDialog.FileName,
                    HtmlProofGenerator.GeneratePage(
                        String.Format("{0} for {1}", app.SchemeType,
                            ColorTranslator.ToHtml(app.Color)),
                        HtmlProofGenerator.GenerateTable(app.Results)
                    )
                );
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
