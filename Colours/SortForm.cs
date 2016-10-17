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
    public partial class SortForm : Form
    {
        public SortForm()
        {
            InitializeComponent();
            foreach (var sb in Enum.GetValues(typeof(PaletteSortBy)))
                sortByBox.Items.Add(sb);
            sortByBox.SelectedIndex = 0;
        }

        public bool Ascending => orderBox.Checked;

        public PaletteSortBy SortBy => (PaletteSortBy)sortByBox.SelectedItem;
    }
}
