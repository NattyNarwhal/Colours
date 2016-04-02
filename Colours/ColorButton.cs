using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colours
{
    /// <summary>
    /// A button that takes the appearance of the colour it represents.
    /// </summary>
    public class ColorButton : Button
    {
        // TODO: perhaps use BackColor as the backing instead
        private Color _color;
        private HsvColor _hsv;

        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                BackColor = _color;
                ForeColor = _color.GetBrightness() > 0.5 ? Color.Black : Color.White;
            }
        }

        /// <summary>
        /// A tag value for the HSV colour variant. Set this as you do Color.
        /// </summary>
        public HsvColor HsvColor {
            get
            {
                return _hsv;
            }
            set
            {
                _hsv = value;
                if (value != null)
                    Color = _hsv.ToRgb().ToGdiColor();
            }
        }

        public ColorButton() : base()
        {
            FlatStyle = FlatStyle.Popup;
            Color = SystemColors.ButtonFace;
        }

        public ColorButton(Color c) : this()
        {
            Color = c;
        }

        public ColorButton(HsvColor c) : this()
        {
            HsvColor = c;
        }

        public ColorButton(Color c, HsvColor h) : this(c)
        {
            HsvColor = h;
        }
    }
}
