﻿using System;
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
            Text = (string)comboBox1.SelectedItem;

            HsvColor c = new HsvColor(FirstColorButton()?.Color ?? Color.Red);
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
                FirstColorButton().Color = colorDialog1.Color;
                UpdateScheme();
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateScheme();
        }
    }
}
