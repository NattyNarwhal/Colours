using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Contains functions to render 
    /// </summary>
    public static class RenderColorIcon
    {
        /// <summary>
        /// Renders an icon, representing a square of the specified color with a black border.
        /// </summary>
        /// <param name="c">The color to use.</param>
        /// <param name="w">The width of the icon. By default, 16.</param>
        /// <param name="h">The height of the icon. By default, 16.</param>
        /// <returns>The icon.</returns>
        public static Bitmap RenderIcon(RgbColor c, int w = 16, int h = 16)
        {
            return RenderIcon(c.ToGdiColor(), w, h);
        }

        /// <summary>
        /// Renders an icon, representing a square of the specified color with a black border.
        /// </summary>
        /// <param name="c">The color to use.</param>
        /// <param name="w">The width of the icon. By default, 16.</param>
        /// <param name="h">The height of the icon. By default, 16.</param>
        /// <returns>The icon.</returns>
        public static Bitmap RenderIcon(Color c, int w = 16, int h = 16)
        {
            var b = new Bitmap(16, 16);
            using (var g = Graphics.FromImage(b))
            {
                g.FillRectangle(new SolidBrush(c), 0, 0, w, h);
                g.DrawRectangle(Pens.Black, 0, 0, w - 1, h - 1);
            }
            return b;
        }
    }
}
