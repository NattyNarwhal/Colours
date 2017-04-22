using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colours
{
    public class ColorListView : ListView
    {
        ImageList il = new ImageList();
        ColumnHeader ch = new ColumnHeader();

        public ColorListView() : base()
        {
            il.ImageSize = new Size(16, 16);
            il.ColorDepth = ColorDepth.Depth32Bit;
            SmallImageList = il;
            // set up invisible header
            View = View.Details;
            HeaderStyle = ColumnHeaderStyle.None;
            Columns.Add(ch);
            ch.Width = ClientSize.Width;
            Resize += (o, e) => ch.Width = ClientSize.Width;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<IColor> CheckedColors
        {
            get
            {
                return CheckedItems.Cast<ListViewItem>().Select(x => (IColor)x.Tag);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<IColor> SelectedColors
        {
            get
            {
                return SelectedItems.Cast<ListViewItem>().Select(x => (IColor)x.Tag);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable<IColor> Colors
        {
            get
            {
                return Items.Cast<ListViewItem>().Select(x => (IColor)x.Tag);
            }
        }

        public void ClearColors()
        {
            il.Images.Clear();
            Items.Clear();
        }

        public void AddColor(IColor c)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Tag = c;
            // TODO: make this configurable
            lvi.Text = c.ToRgb().ToHtml();

            il.Images.Add(c.GetHashCode().ToString(), RenderColorIcon.RenderBitmap(c.ToRgb()));
            lvi.ImageKey = c.GetHashCode().ToString();

            Items.Add(lvi);
        }

        // TODO: Remove, hide inherited members from propertygrids
    }
}
