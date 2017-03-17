using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    public class ColorDragEventArgs : EventArgs
    {
        public PaletteColor DraggedColor { get; set; }
        public PaletteColor TargetColor { get; set; }

        public ColorDragEventArgs(PaletteColor dragged, PaletteColor target)
        {
            DraggedColor = dragged;
            TargetColor = target;
        }
    }
}
