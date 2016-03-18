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
            UpdateScheme(FirstColorButton()?.Color ?? Properties.Settings.Default.LastColor);
        }

        public void UpdateScheme(Color color)
        {
            Text = (string)comboBox1.SelectedItem;

            HsvColor c = new HsvColor(color);
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

                ColorButton cb = new ColorButton(next.ToRgb());
                cb.Text = String.Format("R {0}\r\nG {0}\r\nB {0}",
                    cb.Color.R, cb.Color.G, cb.Color.B);
                toolTip1.SetToolTip(cb, String.Format("{0}\r\n{1}",
                    ColorTranslator.ToHtml(cb.Color), cb.Color.ToRgbString()));
                cb.ContextMenuStrip = contextMenuStrip1;
                cb.Dock = DockStyle.Fill;
                cb.Click += SchemeColor_Click;

                tableLayoutPanel1.Controls.Add(cb, i++, 0);
            }
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
    }
}
