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

        public string SchemeType { get; set; }

        public Stack<HsvColor> UndoHistory { get; private set; }
        public Stack<HsvColor> RedoHistory { get; private set; }

        public List<HsvColor> Results { get; private set; }

        public AppController(HsvColor c, string scheme)
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

        /// <summary>
        /// Gets the colour scheme results for the current color. Note
        /// that you only need to call this if you change the scheme,
        /// as SetColor and the other color changing methods will do
        /// this for you.
        /// </summary>
        public void GetSchemeResults()
        {
            switch (SchemeType)
            {
                case "Complement":
                    Results = ColorSchemer.Complement(HsvColor);
                    break;
                case "Split Complements":
                    Results = ColorSchemer.SplitComplement(HsvColor);
                    break;
                case "Triads":
                    Results = ColorSchemer.Triads(HsvColor);
                    break;
                case "Tetrads":
                    Results = ColorSchemer.Tetrads(HsvColor);
                    break;
                case "Analogous":
                    Results = ColorSchemer.Analogous(HsvColor);
                    break;
                case "Monochromatic":
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
