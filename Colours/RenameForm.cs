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
    public partial class RenameForm : Form
    {
        public RenameForm()
        {
            InitializeComponent();
        }

        public RenameForm(PaletteColor pc) : this()
        {
            colorIconBox.Image = RenderColorIcon.RenderIcon(pc.Color);
            colorLabel.Text = string.Format("{0} ({1})", pc.Name, pc.Color.ToHtml());
            nameBox.Text = pc.Name;
        }

        public string NewName => nameBox.Text;
    }
}
