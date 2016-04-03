﻿using System;
using System.Collections.Generic;

namespace Colours
{
    /// <summary>
    /// Represents the minimum amount of application state.
    /// This is also used for undo/redo.
    /// </summary>
    public class AppState
    {
        /// <summary>
        /// The colour of the state.
        /// </summary>
        public HsvColor Color { get; set; }
        /// <summary>
        /// The scheme of the state.
        /// </summary>
        public SchemeType SchemeType { get; set; }

        /// <summary>
        /// Creates a new application state representation.
        /// </summary>
        /// <param name="c">The color to use.</param>
        /// <param name="t">The scheme to use.</param>
        public AppState(HsvColor c, SchemeType t)
        {
            Color = c;
            SchemeType = t;
        }

        /// <summary>
        /// Gets a string representation of the current state.
        /// </summary>
        /// <returns>The stringified state, for example, "Tetrads of #123456."</returns>
        public override string ToString()
        {
            return String.Format("{0} of {1}", SchemeType.ToString(),
                Color.ToRgb().ToHtml());
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
        /// <see cref="Colours.AppController.SetColor(HsvColor, bool)"/>
        /// to change the color.
        /// </summary>
        public HsvColor HsvColor { get; private set; }
        /// <summary>
        /// The current color, in RGB form. Use
        /// <see cref="Colours.AppController.SetColor(RgbColor, bool)"/>
        /// to change the color.
        /// </summary>
        public RgbColor Color
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
        /// <summary>
        /// This event is fired whenever the result is changed as a
        /// result of a SetColor/SetScheme/Undo/Redo call that allows
        /// event firing.
        /// </summary>
        public event EventHandler<EventArgs> ResultChanged;

        /// <summary>
        /// This is a wrapper for the event <see cref="ResultChanged"/>.
        /// </summary>
        /// <param name="e">The event arguments.</param>
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
        /// Set the current colour (in RGB form) and generate the Results in the
        /// Results property, and fire an event.
        /// </summary>
        /// <param name="c">The new colour.</param>
        /// <param name="keepHistory">
        /// If you want to update the undo stack. Note that it will only update it if
        /// the current colour and new colour are different.
        /// </param>
        public void SetColor(RgbColor c, bool keepHistory)
        {
            SetColor(new HsvColor(c), keepHistory, true);
        }

        /// <summary>
        /// Set the current colour (in RGB form) and generate the Results in the
        /// Results property, and fire an event.
        /// </summary>
        /// <param name="c">The new colour.</param>
        /// <param name="keepHistory">
        /// If you want to update the undo stack. Note that it will only update it if
        /// the current colour and new colour are different.
        /// </param>
        /// <param name="fireEvent">
        /// If you want to fire the event.
        /// </param>
        public void SetColor(RgbColor c, bool keepHistory, bool fireEvent)
        {
            SetColor(new HsvColor(c), keepHistory, fireEvent);
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
        #endregion

        #region adjustment functions
        /// <summary>
        /// Increases the value of the color, if possible.
        /// </summary>
        public void Brighten()
        {
            if (CanBrighten())
            {
                SetColor(new HsvColor(HsvColor.Hue,
                    HsvColor.Saturation, HsvColor.Value + 0.05d), true);
            }
        }

        /// <summary>
        /// Decreases the value of the color, if possible.
        /// </summary>
        public void Darken()
        {
            if (CanDarken())
            {
                SetColor(new HsvColor(HsvColor.Hue,
                    HsvColor.Saturation, HsvColor.Value - 0.05d), true);
            }
        }

        /// <summary>
        /// Increases the saturation of the color, if possible.
        /// </summary>
        public void Saturate()
        {
            if (CanSaturate())
            {
                SetColor(new HsvColor(HsvColor.Hue,
                    HsvColor.Saturation + 0.05d, HsvColor.Value), true);
            }
        }

        /// <summary>
        /// Decreases the saturation of the color, if possible.
        /// </summary>
        public void Desaturate()
        {
            if (CanDesaturate())
            {
                SetColor(new HsvColor(HsvColor.Hue,
                    HsvColor.Saturation - 0.05d, HsvColor.Value), true);
            }
        }

        /// <summary>
        /// Checks to see if the color can be brightened. Note that
        /// the function will call this itself - this is primarily
        /// useful for toggling this in your frontend.
        /// </summary>
        /// <returns>If the color can be brightened.</returns>
        public bool CanBrighten()
        {
            return HsvColor.Value + 0.05d < 1d;
        }

        /// <summary>
        /// Checks to see if the color can be darkened. Note that
        /// the function will call this itself - this is primarily
        /// useful for toggling this in your frontend.
        /// </summary>
        /// <returns>If the color can be darkened.</returns>
        public bool CanDarken()
        {
            return HsvColor.Value - 0.05d > 0d;
        }

        /// <summary>
        /// Checks to see if the color can be saturated. Note that
        /// the function will call this itself - this is primarily
        /// useful for toggling this in your frontend.
        /// </summary>
        /// <returns>If the color can be desaturated.</returns>
        public bool CanSaturate()
        {
            return HsvColor.Saturation + 0.05d < 1d;
        }

        /// <summary>
        /// Checks to see if the color can be desaturated. Note that
        /// the function will call this itself - this is primarily
        /// useful for toggling this in your frontend.
        /// </summary>
        /// <returns>If the color can be desaturated.</returns>
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

        /// <summary>
        /// Undoes the last change made and pushes it to the redo stack,
        /// if possible.
        /// </summary>
        public void Undo()
        {
            if (CanUndo())
            {
                RedoHistory.Push(new AppState(HsvColor, SchemeType));
                AppState s = UndoHistory.Pop();
                SetColor(s.Color, false, false);
                SetSchemeType(s.SchemeType, false, false);

                // because we didn't use the setters to fire events
                // because we'd be wastefully firing two otherwise
                OnResultChanged(new EventArgs());
            }
        }

        /// <summary>
        /// Redos the last change made and pushes it to the undo stack,
        /// if possible.
        /// </summary>
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

        /// <summary>
        /// Checks to see if there are changes able to be undone.
        /// </summary>
        /// <returns>If changes can be undone.</returns>
        public bool CanUndo()
        {
            return UndoHistory.Count > 0;
        }

        /// <summary>
        /// Checks to see if there are changes able to be redone.
        /// </summary>
        /// <returns>If changes can be redone.</returns>
        public bool CanRedo()
        {
            return RedoHistory.Count > 0;
        }
        #endregion
    }
}
