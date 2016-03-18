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

        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                BackColor = _color;
                ForeColor = _color.Invert();
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
    }
}
