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
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            /*
            Opposite
Complement
Split Complements
Triads
Tetrads
Analogous
Monochromatic
            */
            Text = (string)comboBox1.SelectedItem;
            flowLayoutPanel1.Controls.Clear();

            HsvColor c = new HsvColor(colorButton1.Color);
            List<HsvColor> lc;
            ColorButton cb;
            switch ((string)comboBox1.SelectedItem)
            {
                case "Opposite":
                    cb = new ColorButton(ColorSchemer.Opposite(c).ToRgb());
                    flowLayoutPanel1.Controls.Add(cb);
                    break;
                case "Complement":
                    cb = new ColorButton(ColorSchemer.Complement(c).ToRgb());
                    flowLayoutPanel1.Controls.Add(cb);
                    break;
                case "Split Complements":
                    lc = ColorSchemer.SplitComplement(c);
                    foreach (HsvColor next in lc)
                    {
                        cb = new ColorButton(next.ToRgb());
                        flowLayoutPanel1.Controls.Add(cb);
                    }
                    break;
                case "Triads":
                    lc = ColorSchemer.Triads(c);
                    foreach (HsvColor next in lc)
                    {
                        cb = new ColorButton(next.ToRgb());
                        flowLayoutPanel1.Controls.Add(cb);
                    }
                    break;
                case "Tetrads":
                    lc = ColorSchemer.Tetrads(c);
                    foreach (HsvColor next in lc)
                    {
                        cb = new ColorButton(next.ToRgb());
                        flowLayoutPanel1.Controls.Add(cb);
                    }
                    break;
                case "Analogous":
                    lc = ColorSchemer.Analogous(c);
                    foreach (HsvColor next in lc)
                    {
                        cb = new ColorButton(next.ToRgb());
                        flowLayoutPanel1.Controls.Add(cb);
                    }
                    break;
                case "Monochromatic":
                    lc = ColorSchemer.Monochromatic(c);
                    foreach (HsvColor next in lc)
                    {
                        cb = new ColorButton(next.ToRgb());
                        flowLayoutPanel1.Controls.Add(cb);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Not a valid type of scheme.");
            }
        }
    }
}
