using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GdiColor = System.Drawing.Color;

namespace Colours
{
    /// <summary>
    /// A button that takes the appearance of the colour it represents.
    /// </summary>
    public class ColorButton : Button
    {
        private IColor _color;

        /// <summary>
        /// Represents the color of the ColorButton.
        /// </summary>
        // HACK: Visual Studio tries to serialize an RgbColor and it ends badly
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IColor Color
        {
            get { return _color; }
            set
            {
                _color = value;
                BackColor = _color.ToRgb().ToGdiColor();
                ForeColor = _color.ToRgb().GetBrightness() > 0.5
                    ? GdiColor.Black : GdiColor.White;
            }
        }

        public ColorButton() : base()
        {
            FlatStyle = FlatStyle.Popup;
            Color = SystemColors.ButtonFace.ToRgbColor();
        }

        public ColorButton(IColor c) : this()
        {
            Color = c;
        }
    }
}
