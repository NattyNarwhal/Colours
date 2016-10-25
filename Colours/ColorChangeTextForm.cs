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
    public partial class ColorChangeTextForm : Form
    {
        public ColorChangeTextForm()
        {
            InitializeComponent();
        }

        public ColorChangeTextForm(string title, string text, PaletteColor pc) : this()
        {
            Text = title;
            valueBox.Text = text;
            colorIconBox.Image = RenderColorIcon.RenderIcon(pc.Color);
            colorLabel.Text = string.Format("{0} ({1})", pc.Name, pc.Color.ToHtml());
        }

        public string NewValue => valueBox.Text;
    }
}
