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
    public partial class BlendForm : Form
    {
        public RgbColor Color1
        {
            get
            {
                return colorButton1.Color.ToRgb();
            }
        }

        public RgbColor Color2
        {
            get
            {
                return colorButton2.Color.ToRgb();
            }
        }

        public int Steps
        {
            get
            {
                return (int)stepsBox.Value;
            }
        }

        public IEnumerable<RgbColor> SelectedItems
        {
            get
            {
                return colorListView1.CheckedColors.Cast<RgbColor>();
            }
        }

        public BlendForm()
        {
            InitializeComponent();
        }

        public BlendForm(RgbColor c1, RgbColor c2) : this()
        {
            colorButton1.Color = c1;
            colorButton2.Color = c2;
            UpdateUI();
        }

        public void UpdateUI()
        {
            colorListView1.ClearColors();

            // TODO: refactor this form to support multiple colours being blended at once?
            foreach (var c in Blend.BlendColours(Color1, Color2, Steps))
            {
                colorListView1.AddColor(c);
            }
        }

        private void colorButton1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorButton1.Color = colorDialog1.Color.ToRgbColor();
                UpdateUI();
            }
        }

        private void colorButton2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorButton2.Color = colorDialog1.Color.ToRgbColor();
                UpdateUI();
            }
        }

        private void stepsBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
