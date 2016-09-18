using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MoreLinq;

namespace Colours
{
    public partial class ColorGrid : UserControl
    {
        Palette _palette;

        // We can't let VS see this as it wants to serialize Palette, which it can't
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Palette Palette
        {
            get
            {
                return _palette;
            }
            set
            {
                _palette = value;

                // onto the gridification
                UpdateGrid();
            }
        }

        public ContextMenuStrip ColorContextMenuStrip { get; set; }

        public ColorGrid()
        {
            InitializeComponent();

            Palette = new Palette();
            table.Resize += (o, e) =>
            {
                if (table.ColumnCount > 1)
                    for (int i = 0; i < table.RowCount; i++)
                    {
                        if (table.GetControlFromPosition(table.ColumnCount - 1, i) != null)
                            ((ColorButton)table.GetControlFromPosition(table.ColumnCount - 1, i)).Width =
                                ((ColorButton)table.GetControlFromPosition(table.ColumnCount - 2, i)).Width;
                    }
            };
        }

        void UpdateGrid()
        {
            table.Controls.Clear();

            // nothing to do
            if (Palette.Colors.Count == 0)
                return;

            int cols = Palette.Columns > 0 ? Palette.Columns :
                (Palette.Colors.Count < 16 ? Palette.Colors.Count : 16);

            table.ColumnCount = cols;
            foreach (ColumnStyle s in table.ColumnStyles)
            {
                s.SizeType = SizeType.Percent;
                s.Width = 100f / cols;
            }
            while (table.ColumnStyles.Count < cols)
                table.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 100f / cols));

            var batches = Palette.Colors.Batch(cols);
            var rows = batches.Count();
            table.RowCount = rows;
            // style
            foreach (RowStyle s in table.RowStyles)
                s.SizeType = SizeType.AutoSize;

            for (int r = 0; r < rows; r++)
            {
                var ra = batches.ToArray()[r];
                for (int c = 0; c < ra.Count(); c++)
                {
                    var pc = ra.ToArray()[c];
                    var cb = new ColorButton()
                    {
                        Width = 32,
                        Height = 32,
                        Dock = DockStyle.Fill,
                        Margin = new Padding(1),
                        Color = pc.Color,
                        Tag = pc,
                        ContextMenuStrip = ColorContextMenuStrip
                    };
                    
                    // we can't use MouseClick for right clicks on buttons
                    cb.MouseUp += (o, e) =>
                    {
                        if (e.Button == MouseButtons.Right)
                            ((ColorButton)o).Focus();
                    };

                    if (cols > 1 && c == ra.Count() - 1 && c == cols - 1)
                    {
                        cb.Dock = DockStyle.None;
                        cb.Width = ((ColorButton)table.GetControlFromPosition(c - 1, r)).Width;
                    }

                    toolTip1.SetToolTip(cb, string.Format("{0}\r\n{1}\r\n{2}",
                        pc.Name, pc.Color.ToHtml(), new HsvColor(pc.Color).ToString()));
                    table.Controls.Add(cb);
                    table.SetCellPosition(cb, new TableLayoutPanelCellPosition(c, r));
                }
            }
        }

        public PaletteColor GetPaletteColor(int col, int row)
        {
            return (PaletteColor)((ColorButton)table
                .GetControlFromPosition(col, row)).Tag;
        }

        public PaletteColor FocusedColor
        {
            get
            {
                return (PaletteColor)table.Controls.Cast<ColorButton>()
                    .Where(x => x.Focused).FirstOrDefault()?.Tag;
            }
            set
            {
                table.Controls.Cast<ColorButton>().Where(x => x.Tag == value)
                    .FirstOrDefault()?.Focus();
            }
        }
    }
}
