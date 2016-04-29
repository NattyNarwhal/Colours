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
    public partial class PalettePropertiesForm : Form
    {
        public string PaletteTitle
        {
            get
            {
                return titleBox.Text;
            }
            set
            {
                titleBox.Text = value;
            }
        }

        public int PaletteColumns
        {
            get
            {
                return (int)columnsBox.Value;
            }
            set
            {
                columnsBox.Value = value;
            }
        }

        public List<string> PaletteComments
        {
            get
            {
                return commentsBox.Lines.ToList();
            }
            set
            {
                commentsBox.Lines = value.ToArray();
            }
        }

        public PalettePropertiesForm()
        {
            InitializeComponent();
        }
    }
}
