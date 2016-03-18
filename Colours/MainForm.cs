using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colours
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            comboBox1.SelectedItem = comboBox1.Items[0];
            UpdateScheme();
        }

        public ColorButton FirstColorButton()
        {
            return ((ColorButton)tableLayoutPanel1.GetControlFromPosition(0, 0));
        }

        public void UpdateScheme()
        {
            UpdateScheme(FirstColorButton()?.HsvColor ??
                new HsvColor(Properties.Settings.Default.LastColor));
        }

        public void UpdateScheme(Color color)
        {
            UpdateScheme(new HsvColor(color));
        }

        public void UpdateScheme(HsvColor color)
        {
            Text = (string)comboBox1.SelectedItem;

            HsvColor c = color;
            List<HsvColor> lc;
            switch ((string)comboBox1.SelectedItem)
            {
                case "Complement":
                    lc = ColorSchemer.Complement(c);
                    break;
                case "Split Complements":
                    lc = ColorSchemer.SplitComplement(c);
                    break;
                case "Triads":
                    lc = ColorSchemer.Triads(c);
                    break;
                case "Tetrads":
                    lc = ColorSchemer.Tetrads(c);
                    break;
                case "Analogous":
                    lc = ColorSchemer.Analogous(c);
                    break;
                case "Monochromatic":
                    lc = ColorSchemer.Monochromatic(c);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Not a valid type of scheme.");
            }

            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnCount = lc.Count;
            int i = 0;
            foreach (HsvColor next in lc)
            {
                tableLayoutPanel1.ColumnStyles[i].SizeType = SizeType.Percent;
                tableLayoutPanel1.ColumnStyles[i].Width = 100 / lc.Count;

                ColorButton cb = new ColorButton(next);
                cb.Text = String.Format("R {0} H {3:F0}\r\nG {1} S {4:F0}\r\nB {2} V {5:F0}",
                    cb.Color.R, cb.Color.G, cb.Color.B,
                    next.Hue, next.Saturation * 100, next.Value * 100);
                toolTip1.SetToolTip(cb, String.Format("{0}\r\n{1}\r\n{2}",
                    ColorTranslator.ToHtml(cb.Color), cb.Color.ToRgbString(), cb.Color.ToHslString()));
                cb.ContextMenuStrip = contextMenuStrip1;
                cb.Dock = DockStyle.Fill;
                cb.Click += SchemeColor_Click;

                tableLayoutPanel1.Controls.Add(cb, i++, 0);
            }

            EnableItems();
        }

        public void EnableItems()
        {
            HsvColor c = FirstColorButton().HsvColor;

            brightenToolStripMenuItem.Enabled = (c.Value + 0.05d < 1d);
            darkenToolStripMenuItem.Enabled = (c.Value - 0.05d > 0d);
            saturateToolStripMenuItem.Enabled = (c.Value + 0.05d < 1d);
            desaturateToolStripMenuItem.Enabled = (c.Value - 0.05d > 0d);
        }

        private void SchemeColor_Click(object sender, EventArgs e)
        {
            ColorButton cb = (ColorButton)sender;
            colorDialog1.Color = cb.Color;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                UpdateScheme(colorDialog1.Color);
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateScheme();
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.LastColor = FirstColorButton().Color;
            Properties.Settings.Default.Save();
        }

        private void randomButton_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            UpdateScheme(Color.FromArgb(r.Next(255), r.Next(255), r.Next(255)));
        }

        private void brightenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HsvColor c = FirstColorButton().HsvColor;
            if (c.Value < 0.95d)
            {
                c.Value += 0.05d;
                FirstColorButton().HsvColor = c;
            }
            UpdateScheme();
        }

        private void darkenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HsvColor c = FirstColorButton().HsvColor;
            if (c.Value > 0.05d)
            {
                c.Value -= 0.05d;
                FirstColorButton().HsvColor = c;
            }
            UpdateScheme();
        }

        private void saturateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HsvColor c = FirstColorButton().HsvColor;
            if (c.Saturation < 0.95d)
            {
                c.Saturation += 0.05d;
                FirstColorButton().HsvColor = c;
            }
            UpdateScheme();
        }

        private void desaturateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HsvColor c = FirstColorButton().HsvColor;
            if (c.Saturation > 0.05d)
            {
                c.Saturation -= 0.05d;
                FirstColorButton().HsvColor = c;
            }
            UpdateScheme();
        }
    }
}
