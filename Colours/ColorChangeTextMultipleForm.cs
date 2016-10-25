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
    public partial class ColorChangeTextMultipleForm : Form
    {
        public ColorChangeTextMultipleForm()
        {
            InitializeComponent();
        }

        public ColorChangeTextMultipleForm(string title, bool allowNumbered = true) : this()
        {
            Text = title;
        }

        public string NewText => valueBox.Text;
        public bool Numbered => numberedBox.Checked;
    }
}
