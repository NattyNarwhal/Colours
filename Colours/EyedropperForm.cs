using Gma.System.MouseKeyHook;
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
    public partial class EyedropperForm : Form
    {
        /// <summary>
        /// The captured color.
        /// </summary>
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                previewLabel.BackColor = _color;
                previewLabel.ForeColor = _color.GetBrightness() > 0.5
                    ? Color.Black : Color.White;
                previewLabel.Text = String.Format("{0}\r\n{1}\r\n{2}\r\n{3}",
                    value.ToRgbColor().ToHtml(),
                    value.ToRgbColor().ToRgbString(),
                    value.ToRgbColor().ToHslString(),
                    new HsvColor(value.ToRgbColor()).ToString());
            }
        }

        private Color _color;
        const string waitingText = "Press \"Drop\" to pick a color.";
        const string pickingText = "Click on the screen to pick a color.";

        IKeyboardMouseEvents events = Hook.GlobalEvents();
        bool capture = false;

        public EyedropperForm()
        {
            InitializeComponent();

            Color = Color.Black;

            events.MouseDownExt += GrabUnderCursor;
        }

        ~EyedropperForm()
        {
            events.MouseDownExt -= GrabUnderCursor;
            events.Dispose();
        }

        public void GrabUnderCursor(object sender, MouseEventExtArgs e)
        {
            if (capture)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Color = GetPixel(MousePosition);
                    e.Handled = true;
                }
                capture = false;
                stateLabel.Text = waitingText;
            }
        }

        public Color GetPixel(Point position)
        {
            using (var bitmap = new Bitmap(1, 1))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(position, new Point(0, 0), new Size(1, 1));
                }
                return bitmap.GetPixel(0, 0);
            }
        }

        private void dropButton_Click(object sender, EventArgs e)
        {
            capture = true;
            stateLabel.Text = pickingText;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
