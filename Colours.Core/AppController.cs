using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Colours
{
    public class AppController
    {
        public HsvColor HsvColor { get; private set; }
        public Color Color { get; private set; }

        public SchemeType SchemeType { get; set; }

        public Stack<HsvColor> UndoHistory { get; private set; }
        public Stack<HsvColor> RedoHistory { get; private set; }

        public List<HsvColor> Results { get; private set; }

        public AppController(HsvColor c, SchemeType scheme)
        {
            UndoHistory = new Stack<HsvColor>();
            RedoHistory = new Stack<HsvColor>();

            SchemeType = scheme;
            SetColor(c, false);
            GetSchemeResults();
        }

        public void SetColor(HsvColor c, bool keepHistory)
        {
            if (keepHistory && c.ToString() != HsvColor.ToString())
            {
                UndoHistory.Push(HsvColor);
                RedoHistory.Clear();
            }
            HsvColor = c;
            Color = c.ToRgb();
            GetSchemeResults();
        }
        
        public void SetColor(Color c, bool keepHistory)
        {
            if (keepHistory && c.ToString() != Color.ToString())
            {
                UndoHistory.Push(HsvColor);
                RedoHistory.Clear();
            }
            Color = c;
            HsvColor = new HsvColor(c);
            GetSchemeResults();
        }

        public void Brighten()
        {
            if (CanBrighten())
            {
                SetColor(new HsvColor(HsvColor.Hue,
                    HsvColor.Saturation, HsvColor.Value + 0.05d), true);
            }
        }

        public void Darken()
        {
            if (CanDarken())
            {
                SetColor(new HsvColor(HsvColor.Hue,
                    HsvColor.Saturation, HsvColor.Value - 0.05d), true);
            }
        }

        public void Saturate()
        {
            if (CanSaturate())
            {
                SetColor(new HsvColor(HsvColor.Hue,
                    HsvColor.Saturation + 0.05d, HsvColor.Value), true);
            }
        }

        public void Desaturate()
        {
            if (CanDesaturate())
            {
                SetColor(new HsvColor(HsvColor.Hue,
                    HsvColor.Saturation - 0.05d, HsvColor.Value), true);
            }
        }

        public bool CanBrighten()
        {
            return HsvColor.Value + 0.05d < 1d;
        }

        public bool CanDarken()
        {
            return HsvColor.Value - 0.05d > 0d;
        }

        public bool CanSaturate()
        {
            return HsvColor.Saturation + 0.05d < 1d;
        }

        public bool CanDesaturate()
        {
            return HsvColor.Saturation - 0.05d > 0d;
        }

        public void GetSchemeResults()
        {
            switch (SchemeType)
            {
                case SchemeType.Complement:
                    Results = ColorSchemer.Complement(HsvColor);
                    break;
                case SchemeType.SplitComplements:
                    Results = ColorSchemer.SplitComplement(HsvColor);
                    break;
                case SchemeType.Triads:
                    Results = ColorSchemer.Triads(HsvColor);
                    break;
                case SchemeType.Tetrads:
                    Results = ColorSchemer.Tetrads(HsvColor);
                    break;
                case SchemeType.Analogous:
                    Results = ColorSchemer.Analogous(HsvColor);
                    break;
                case SchemeType.Monochromatic:
                    Results = ColorSchemer.Monochromatic(HsvColor);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Not a valid type of scheme.");
            }
        }

        public void Undo()
        {
            if (CanUndo())
            {
                RedoHistory.Push(HsvColor);
                SetColor(UndoHistory.Pop(), false);
            }
        }

        public void Redo()
        {
            if (CanRedo())
            {
                UndoHistory.Push(HsvColor);
                SetColor(RedoHistory.Pop(), false);
            }
        }

        public bool CanUndo()
        {
            return UndoHistory.Count > 0;
        }

        public bool CanRedo()
        {
            return RedoHistory.Count > 0;
        }
    }
}
