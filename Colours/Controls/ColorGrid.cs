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
using System.Runtime.InteropServices;

namespace Colours
{
    public partial class ColorGrid : UserControl
    {
        class ColorGridButton : ColorButton
        {
            protected override bool IsInputKey(Keys keyData)
            {
                // we only need to handle up/down, we're fine with stock l/r
                switch (keyData)
                {
                    //case Keys.Right:
                    //case Keys.Left:
                    case Keys.Up:
                    case Keys.Down:
                        return true;
                    //case Keys.Shift | Keys.Right:
                    //case Keys.Shift | Keys.Left:
                    case Keys.Shift | Keys.Up:
                    case Keys.Shift | Keys.Down:
                        return true;
                }
                return base.IsInputKey(keyData);
            }
        }

        // forcing the scrollbar makes the layout far more predictable
        // looks kinda ugly though, and causes weird scrollbars at times
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.Style |= 0x00200000; // WS_VSCROLL
                return cp;
            }
        }
        
        // but we can disable it when it isn't needed
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        static extern bool EnableScrollBar(HandleRef hWnd, int nBar, int value);

        void EnableVScrollBar(bool enable)
        {
            // check if we can do this by being on windows (only NT matters)
            // 1 = vscroll
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                EnableScrollBar(new HandleRef(this, Handle), 1, enable ? 0 : 3);
        }

        IPalette _palette;
        bool initializing = true;
        // for button dnd
        bool isDragged = false;
        Point drag;

        // We can't let VS see this as it wants to serialize Palette, which it can't
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IPalette Palette
        {
            get
            {
                return _palette;
            }
            set
            {
                _palette = value;

                // onto the gridification
                if (!initializing)
                    UpdateGrid();
            }
        }

        public ContextMenuStrip ColorContextMenuStrip { get; set; }

        public ColorGrid()
        {
            InitializeComponent();

            Palette = new GimpPalette();
            Resize += (o, e) => ResizeUI();

            // HACK: the designer runs into issues... often with this control, due to init.
            initializing = false;
        }

        void UpdateGrid()
        {
            table.SuspendLayout();
            table.Controls.Clear();

            // nothing to do
            if (Palette.Colors.Count == 0)
                goto end; // a velociraptor eats you

            int cols = 16;
            if (Palette is IBucketedPalette)
                cols = ((IBucketedPalette)Palette).BucketSize > 0 ?
                    ((IBucketedPalette)Palette).BucketSize :
                    (Palette.Colors.Count < 16 ?
                        Palette.Colors.Count : 16);

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
                    var cb = new ColorGridButton()
                    {
                        //Width = 32,
                        //Height = 32,
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
                    cb.GotFocus += (o, e) => OnFocusedColorChange(new EventArgs());
                    cb.LostFocus += (o, e) =>  OnFocusedColorChange(new EventArgs());
                    cb.Click += (o, e) => OnColorClick(new EventArgs(), cb);
                    // drag and drop
                    cb.MouseDown += (o, e) => isDragged = e.Button == MouseButtons.Left;
                    cb.MouseMove += (o, e) =>
                    {
                        // e.Location is relative to the control. If we have
                        // the location of the control in its parent, and add
                        // the relative mouse position of the control, we get
                        // a workable offset.
                        if (isDragged)
                            drag = new Point(cb.Location.X + e.X, cb.Location.Y + e.Y);
                    };
                    cb.MouseUp += (o, e) =>
                    {
                        if (isDragged)
                        {
                            var cp = table.GetChildAtPoint(drag);
                            if (cp is ColorButton)
                            {
                                var dragged = (PaletteColor)cb.Tag;
                                var target = (PaletteColor)((ColorButton)cp).Tag;
                                if (dragged != target)
                                    OnColorDrag(new ColorDragEventArgs(dragged, target), cb);
                            }
                        }
                        isDragged = false;
                    };
                    cb.KeyDown += (o, e) =>
                    {
                        // we do this because up/down in the table just goes left/right
                        if (e.KeyCode == Keys.Up)
                        {
                            var button = (ColorButton)o;
                            var pos = table.GetPositionFromControl(button);
                            if (pos.Row > 0)
                                table.GetControlFromPosition(pos.Column, pos.Row - 1).Focus();

                            e.Handled = true;
                        }
                        if (e.KeyCode == Keys.Down)
                        {
                            var button = (ColorButton)o;
                            var pos = table.GetPositionFromControl(button);
                            if (pos.Row < table.RowCount - 1)
                            {
                                var lastControlPos = table.GetPositionFromControl(table.Controls[table.Controls.Count - 1]);
                                var newCol = pos.Row + 1 == table.RowCount - 1 &&
                                    lastControlPos.Column < pos.Column ?
                                        lastControlPos.Column : pos.Column;
                                table.GetControlFromPosition(newCol, pos.Row + 1).Focus();
                            }

                            e.Handled = true;
                        }
                    };

                    if (cols > 1 && c == ra.Count() - 1 && c == cols - 1)
                        cb.Dock = DockStyle.None;

                    // HACK: This also could be better post-IColor
                    toolTip1.SetToolTip(cb, string.Format("{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4}",
                        pc.Name, pc.Metadata, pc.Color.ToRgb().ToHtml(),
                        (pc.Color is HsvColor ? (HsvColor)pc.Color : pc.Color.ToRgb().ToHsv()).ToCssString(),
                        pc.Color.ToString()));
                    table.Controls.Add(cb);
                    table.SetCellPosition(cb, new TableLayoutPanelCellPosition(c, r));
                }
            }

            end:
            table.ResumeLayout();

            ResizeUI();
        }

        void ResizeUI()
        {
            // helps speed up, especially when we change height
            table.SuspendLayout();
            if (table.ColumnCount > 1)
            {
                for (int i = 0; i < table.RowCount; i++)
                {
                    ColorButton cb;
                    if ((cb = (ColorButton)table.GetControlFromPosition(table.ColumnCount - 1, i)) != null)
                    {
                        // make the width of the last column's items the
                        // same as the previous, otherwise it looks ugly
                        cb.Width = ((ColorButton)table.GetControlFromPosition(table.ColumnCount - 2, i)).Width;
                    }
                }
            }
            // make the buttons square
            foreach (ColorButton cb in table.Controls)
                cb.Height = cb.Width;
            table.ResumeLayout();
            EnableVScrollBar(table.Height > Height);
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

        public event EventHandler<EventArgs> FocusedColorChange;

        protected virtual void OnFocusedColorChange(EventArgs e)
        {
            FocusedColorChange?.Invoke(this, e);
        }

        public event EventHandler<EventArgs> ColorClick;

        protected virtual void OnColorClick(EventArgs e, ColorButton sender)
        {
            ColorClick?.Invoke(sender, e);
        }

        public event EventHandler<ColorDragEventArgs> ColorDrag;

        protected virtual void OnColorDrag(ColorDragEventArgs e, ColorButton sender)
        {
            ColorDrag?.Invoke(sender, e);
        }
    }
}
