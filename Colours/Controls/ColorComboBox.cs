using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colours
{
    public class ColorComboBox : ComboBox
    {
        public ColorComboBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 21; // seems reasonable
            DrawItem += ColorComboBox_DrawItem;
        }

        private void ColorComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background of the item.
            e.DrawBackground();

            var rectangle = new Rectangle(3, e.Bounds.Top + 2,
                    e.Bounds.Height > 16 ? 16 : e.Bounds.Height,
                    e.Bounds.Height > 16 ? 16 : e.Bounds.Height);

            //e.Graphics.FillRectangle(new SolidBrush(color), rectangle);
            //e.Graphics.DrawRectangle(Pens.Black, rectangle);

            // Draw each string in the array, using a different size, color,
            // and font for each item.
            if (e.Index > -1 && Items[e.Index] is PaletteColor)
            {
                var pc = (PaletteColor)Items[e.Index];

                e.Graphics.DrawImage(RenderColorIcon.RenderIcon(pc.Color.ToRgb()), rectangle);
                e.Graphics.DrawString(pc.Name,
                    e.Font, new SolidBrush(e.ForeColor),
                    new RectangleF(e.Bounds.X + rectangle.Width + 4,
                        e.Bounds.Y + 4, e.Bounds.Width, e.Font.Height));
            }

            // Draw the focus rectangle if the mouse hovers over an item.
            e.DrawFocusRectangle();
        }
    }
}
