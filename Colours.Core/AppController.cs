﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace Colours
{
    /// <summary>
    /// Represents the minimum amount of application state.
    /// This is also used for undo/redo.
    /// </summary>
    [Serializable]
    public class AppState
    {
        public HsvColor Color { get; set; }
        public SchemeType SchemeType { get; set; }

        public AppState(HsvColor c, SchemeType t)
        {
            Color = c;
            SchemeType = t;
        }

        public override string ToString()
        {
            return String.Format("{0} of {1}", SchemeType.ToString(),
                ColorTranslator.ToHtml(Color.ToRgb()));
        }
    }

    /// <summary>
    /// Represents the backend of the application.
    /// </summary>
    public class AppController
    {
        #region properties
        /// <summary>
        /// The current color, in HSV form. Use
        /// <see cref="Colours.AppController.SetColor(Color, bool)"/>
        /// to change the color.
        /// </summary>
        public HsvColor HsvColor { get; private set; }
        /// <summary>
        /// The current color, in GDI form. Use
        /// <see cref="Colours.AppController.SetColor(HsvColor, bool)"/>
        /// to change the color.
        /// </summary>
        public Color Color
        {
            get
            {
                return HsvColor.ToRgb();
            }
        }

        /// <summary>
        /// The current colour scheme. Use
        /// <see cref="Colours.AppController.SetSchemeType(Colours.SchemeType, bool)"/>
        /// to change the scheme.
        /// </summary>
        public SchemeType SchemeType { get; private set; }

        /// <summary>
        /// The undo history. This will be set for you by the functions in this object.
        /// </summary>
        public Stack<AppState> UndoHistory { get; private set; }
        /// <summary>
        /// The redo history. This will be set for you by the functions in this object.
        /// </summary>
        public Stack<AppState> RedoHistory { get; private set; }

        /// <summary>
        /// The current resulting colour scheme. This will be generated by
        /// <see cref="Colours.AppController.GetSchemeResults"/> whenever
        /// the colour or scheme is set.
        /// </summary>
        public List<HsvColor> Results { get; private set; }
        #endregion

        #region events
        public event EventHandler<EventArgs> ResultChanged;

        protected virtual void OnResultChanged(EventArgs e)
        {
            // make temp in case of race condition
            EventHandler<EventArgs> handler = ResultChanged;

            if (handler != null)
                handler(this, e);
        }
        #endregion

        #region ctors
        /// <summary>
        /// Creates a new application controller.
        /// </summary>
        /// <param name="c">The initial colour to use.</param>
        /// <param name="scheme">The initial scheme to use.</param>
        public AppController(HsvColor c, SchemeType scheme)
        {
            UndoHistory = new Stack<AppState>();
            RedoHistory = new Stack<AppState>();

            // set this directly because SetColor will do the dirty work for us
            SchemeType = scheme;
            SetColor(c, false); // this will set Results
        }

        /// <summary>
        /// Creates a new application controller.
        /// </summary>
        /// <param name="state">The object holding the application's state.</param>
        public AppController(AppState state)
            : this(state.Color, state.SchemeType)
        { }
        #endregion

        #region setters
        /// <summary>
        /// Set the current scheme type and generate the Results in the
        /// Results property.
        /// </summary>
        /// <param name="t">The new scheme type.</param>
        /// <param name="keepHistory">
        /// If you want to update the undo stack. Note that it will only update it if
        /// the current scheme and new scheme are different.
        /// </param>
        /// <param name="fireEvent">
        /// If you want to fire the event.
        /// </param>
        public void SetSchemeType(SchemeType t, bool keepHistory, bool fireEvent)
        {
            if (keepHistory && SchemeType != t)
            {
                PushUndo();
            }
            SchemeType = t;
            GetSchemeResults();
            if (fireEvent)
                OnResultChanged(new EventArgs());
        }

        /// <summary>
        /// Set the current scheme type and generate the Results in the
        /// Results property, and fire an event.
        /// </summary>
        /// <param name="t">The new scheme type.</param>
        /// <param name="keepHistory">
        /// If you want to update the undo stack. Note that it will only update it if
        /// the current scheme and new scheme are different.
        /// </param>
        public void SetSchemeType(SchemeType t, bool keepHistory)
        {
            SetSchemeType(t, keepHistory, true);
        }

        /// <summary>
        /// Set the current colour (in HSV form) and generate the Results in the
        /// Results property, and fire an event.
        /// </summary>
        /// <param name="c">The new colour.</param>
        /// <param name="keepHistory">
        /// If you want to update the undo stack. Note that it will only update it if
        /// the current colour and new colour are different.
        /// </param>
        public void SetColor(HsvColor c, bool keepHistory)
        {
            SetColor(c, keepHistory, true);
        }

        /// <summary>
        /// Set the current colour (in HSV form) and generate the Results in the
        /// Results property.
        /// </summary>
        /// <param name="c">The new colour.</param>
        /// <param name="keepHistory">
        /// If you want to update the undo stack. Note that it will only update it if
        /// the current colour and new colour are different.
        /// </param>
        /// <param name="fireEvent">
        /// If you want to fire the event.
        /// </param>
        public void SetColor(HsvColor c, bool keepHistory, bool fireEvent)
        {
            if (keepHistory && c.ToString() != HsvColor.ToString())
            {
                PushUndo();
            }
            HsvColor = c;
            GetSchemeResults();
            if (fireEvent)
                OnResultChanged(new EventArgs());
        }

        /// <summary>
        /// Set the current colour (in GDI form) and generate the Results in the
        /// Results property, and fire an event.
        /// </summary>
        /// <param name="c">The new colour.</param>
        /// <param name="keepHistory">
        /// If you want to update the undo stack. Note that it will only update it if
        /// the current colour and new colour are different.
        /// </param>
        public void SetColor(Color c, bool keepHistory)
        {
            SetColor(c, keepHistory, true);
        }

        /// <summary>
        /// Set the current colour (in GDI form) and generate the Results in the
        /// Results property.
        /// </summary>
        /// <param name="c">The new colour.</param>
        /// <param name="keepHistory">
        /// If you want to update the undo stack. Note that it will only update it if
        /// the current colour and new colour are different.
        /// </param>
        /// <param name="fireEvent">
        /// If you want to fire the event.
        /// </param>
        public void SetColor(Color c, bool keepHistory, bool fireEvent)
        {
            SetColor(new HsvColor(c), keepHistory, fireEvent);
        }
        #endregion

        #region adjustment functions
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
        #endregion

        /// <summary>
        /// Get the results of the colour and scheme combination.
        /// This will be called by the varioous setters in the object.
        /// </summary>
        private void GetSchemeResults()
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

        #region undo functions
        /// <summary>
        /// Pushes the current state of the application to the undo stack, and purges the redo stack.
        /// </summary>
        private void PushUndo()
        {
            UndoHistory.Push(new AppState(HsvColor, SchemeType));
            RedoHistory.Clear();
        }

        public void Undo()
        {
            if (CanUndo())
            {
                RedoHistory.Push(new AppState(HsvColor, SchemeType));
                AppState s = UndoHistory.Pop();
                SetColor(s.Color, false, false);
                SetSchemeType(s.SchemeType, false);

                // because we didn't use the setters to fire events
                // because we'd be wastefully firing two otherwise
                OnResultChanged(new EventArgs());
            }
        }

        public void Redo()
        {
            if (CanRedo())
            {
                UndoHistory.Push(new AppState(HsvColor, SchemeType));
                AppState s = RedoHistory.Pop();
                SetColor(s.Color, false, false);
                SetSchemeType(s.SchemeType, false, false);

                // because we didn't use the setters to fire events
                // because we'd be wastefully firing two otherwise
                OnResultChanged(new EventArgs());
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
        #endregion
    }
}
