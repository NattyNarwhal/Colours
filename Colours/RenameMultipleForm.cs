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
    public partial class RenameMultipleForm : Form
    {
        public RenameMultipleForm()
        {
            InitializeComponent();
        }

        public string NewName => nameBox.Text;
        public bool Numbered => numberedBox.Checked;
    }
}
