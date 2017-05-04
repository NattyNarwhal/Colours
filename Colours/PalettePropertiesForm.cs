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
    public partial class PalettePropertiesForm : Form
    {
        private IPalette _palette;

        public string PaletteTitle
        {
            get
            {
                return titleBox.Text;
            }
            set
            {
                titleBox.Text = value;
            }
        }

        public int PaletteBucket
        {
            get
            {
                return (int)columnsBox.Value;
            }
            set
            {
                columnsBox.Value = value;
            }
        }

        public List<string> GimpPaletteComments
        {
            get
            {
                return commentsBox.Lines.ToList();
            }
            set
            {
                commentsBox.Lines = value.ToArray();
            }
        }

        public ushort AcbId
        {
            get
            {
                return Convert.ToUInt16(idBox.Value);
            }
            set
            {
                idBox.Value = value;
            }
        }

        public ushort AcbDefaultColor
        {
            get
            {
                return Convert.ToUInt16(defaultColorBox.Value);
            }
            set
            {
                defaultColorBox.Value = value;
            }
        }

        public string AcbPrefix
        {
            get
            {
                return prefixBox.Text;
            }
            set
            {
                prefixBox.Text = value;
            }
        }

        public string AcbPostfix
        {
            get
            {
                return postfixBox.Text;
            }
            set
            {
                postfixBox.Text = value;
            }
        }

        public string AcbDescription
        {
            get
            {
                return descriptionBox.Text;
            }
            set
            {
                descriptionBox.Text = value;
            }
        }

        public AdobeColorSpaceAcbSubset AcbColorSpace
        {
            get
            {
                return (AdobeColorSpaceAcbSubset)colorspaceBox.SelectedItem;
            }
            set
            {
                colorspaceBox.SelectedItem = value;
            }
        }

        public AcbPurpose AcbSpotProcess
        {
            get
            {
                return (AcbPurpose)spotProcessBox.SelectedItem;
            }
            set
            {
                spotProcessBox.SelectedItem = value;
            }
        }

        public ushort? ActTransparencyIndex
        {
            get
            {
                return transparencyEnabledBox.Checked ?
                    (ushort?)Convert.ToUInt16(transparencyIndexBox.Value) : null;
            }
            set
            {
                transparencyEnabledBox.Checked = value != null;
                if (value != null)
                    transparencyIndexBox.Value = (ushort)value;
            }
        }

        public PalettePropertiesForm()
        {
            InitializeComponent();
            // HACK: AddRange is shitty, Enum.GetValues is shitty
            foreach (var s in Enum.GetValues(typeof(AdobeColorSpaceAcbSubset)))
                colorspaceBox.Items.Add(s);
            foreach (var p in Enum.GetValues(typeof(AcbPurpose)))
                spotProcessBox.Items.Add(p);
        }

        public PalettePropertiesForm(IPalette initial) : this()
        {
            _palette = initial;
            if (initial is IBucketedPalette)
            {
                PaletteBucket = ((IBucketedPalette)initial).BucketSize;
            }
            if (initial is INamedPalette)
            {
                PaletteTitle = ((INamedPalette)initial).Name;
            }
            if (initial is GimpPalette)
            {
                tabControl1.TabPages.Remove(acbTab);
                tabControl1.TabPages.Remove(actTab);

                GimpPaletteComments = ((GimpPalette)initial).Comments;
            }
            if (initial is AcbPalette)
            {
                tabControl1.TabPages.Remove(gimpTab);
                tabControl1.TabPages.Remove(actTab);

                var unboxed = (AcbPalette)initial;

                AcbId = unboxed.ID;
                AcbDefaultColor = unboxed.DefaultSelection;
                AcbPrefix = unboxed.Prefix;
                AcbPostfix = unboxed.Postfix;
                AcbDescription = unboxed.Description;
                AcbColorSpace = unboxed.ColorSpace;
                AcbSpotProcess = unboxed.Purpose;
            }
            if (initial is ActPalette)
            {
                tabControl1.TabPages.Remove(commonTab);
                tabControl1.TabPages.Remove(gimpTab);
                tabControl1.TabPages.Remove(acbTab);

                transparencyEnabledBox.CheckedChanged += (o, e) =>
                {
                    transparencyIndexBox.Enabled = transparencyEnabledBox.Checked;
                    UpdateTransparencyIndex();
                };

                ActTransparencyIndex = ((ActPalette)initial).TransparentIndex;
                UpdateTransparencyIndex();
            }
            if (initial is NativePalette)
            {
                tabControl1.TabPages.Remove(gimpTab);
                tabControl1.TabPages.Remove(acbTab);
                tabControl1.TabPages.Remove(actTab);
            }
        }

        private void transparencyIndexBox_ValueChanged(object sender, EventArgs e)
        {
            UpdateTransparencyIndex();
        }

        public void UpdateTransparencyIndex()
        {
            var color = _palette?.Colors.Skip((int)transparencyIndexBox.Value).FirstOrDefault();
            // that's a bit too much, should cut down
            if (_palette != null &&
                _palette is ActPalette &&
                ((ActPalette)_palette).TransparentIndex != null
                && color != null)
            {
                transparencyRenderBox.Image = RenderColorIcon.RenderBitmap(color.Color.ToRgb(),
                    transparencyRenderBox.Width,
                    transparencyRenderBox.Width);
                transparencyRenderLabel.Text = string.Format("{0} ({1})",
                    color.Name, color.Color.ToRgb().ToHtml());
            }
            else
            {
                transparencyRenderBox.Image = null;
                transparencyRenderLabel.Text = string.Empty;
            }
        }
    }
}
