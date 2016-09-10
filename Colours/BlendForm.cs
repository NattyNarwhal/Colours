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
                return colorButton1.Color;
            }
        }

        public RgbColor Color2
        {
            get
            {
                return colorButton2.Color;
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
                return listView1.SelectedItems.Cast<ListViewItem>()
                    .Select(x => (RgbColor)x.Tag);
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
            listView1.Items.Clear();
            paletteListImages.Images.Clear();

            // TODO: refactor this form to support multiple colours being blended at once?
            int i = 0; // image index
            foreach (var c in Blend.BlendColours(Color1, Color2, Steps))
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Tag = c;
                lvi.Text = c.ToHtml();

                paletteListImages.Images.Add(RenderColorIcon.RenderIcon(c));
                lvi.ImageIndex = i++;

                listView1.Items.Add(lvi);
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
