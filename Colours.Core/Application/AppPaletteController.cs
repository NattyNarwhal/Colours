using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours
{
    /// <summary>
    /// Represents the backend of the application, for managing color
    /// palettes.
    /// </summary>
    public class AppPaletteController
    {
        /// <summary>
        /// Represents the application's palette. Use the setter to change this.
        /// </summary>
        public Palette Palette { get; set; }
        /// <summary>
        /// The undo history. This will be set for you by the functions in this object.
        /// </summary>
        public Stack<Palette> UndoHistory { get; private set; }
        /// <summary>
        /// The redo history. This will be set for you by the functions in this object.
        /// </summary>
        public Stack<Palette> RedoHistory { get; private set; }
        /// <summary>
        /// Gets the file name of the loaded file
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Gets if the file should be saved.
        /// </summary>
        public bool Dirty { get; set; }

        /// <summary>
        /// This event is fired whenever the result is changed as a
        /// result of a SetColor/SetScheme/Undo/Redo call that allows
        /// event firing.
        /// </summary>
        public event EventHandler<EventArgs> PaletteChanged;

        /// <summary>
        /// This is a wrapper for the event <see cref="PaletteChanged"/>.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnPaletteChanged(EventArgs e)
        {
            PaletteChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Initializes a new controller.
        /// </summary>
        public AppPaletteController()
        {
            New();
        }

        private void ResetState()
        {
            UndoHistory = new Stack<Palette>();
            RedoHistory = new Stack<Palette>();
        }

        /// <summary>
        /// Creates a new palette and resets the state of the controller.
        /// </summary>
        public void New()
        {
            Palette = new Palette();
            ResetState();
            FileName = null;
            Dirty = true;
            OnPaletteChanged(new EventArgs());
        }

        /// <summary>
        /// Creates a new application state with an existing palette.
        /// </summary>
        /// <param name="p">The existing palette to use.</param>
        /// <param name="fileName">The name of the file.</param>
        public void NewFromPalette(Palette p, string fileName = null)
        {
            Palette = p;
            FileName = fileName;
            ResetState();
            Dirty = false;
            OnPaletteChanged(new EventArgs());
        }

        /// <summary>
        /// Sets the color palette, with the following parameters.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        public void SetPalette(Palette p, bool keepHistory = true, bool fireEvent = true)
        {
            if (keepHistory && Palette != p)
                PushUndo();
            Palette = p;
            Dirty = true;
            if (fireEvent)
                OnPaletteChanged(new EventArgs());
        }

        /// <summary>
        /// Adds a color to the palette.
        /// </summary>
        /// <param name="c">The color to append.</param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        public void AppendColor(RgbColor c, bool keepHistory = true, bool fireEvent = true)
        {
            AppendColor(new PaletteColor(c), keepHistory, fireEvent);
        }

        /// <summary>
        /// Adds a color to the palette.
        /// </summary>
        /// <param name="pc">The color to append.</param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        public void AppendColor(PaletteColor pc, bool keepHistory = true, bool fireEvent = true)
        {
            if (keepHistory)
                PushUndo();
            Palette = new Palette(Palette);
            Palette.Colors.Add(pc);
            Dirty = true;
            if (fireEvent)
                OnPaletteChanged(new EventArgs());
        }

        /// <summary>
        /// Deletes a color from the palette.
        /// </summary>
        /// <param name="pc">The color to remove.</param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        public void DeleteColor(PaletteColor pc, bool keepHistory = true, bool fireEvent = true)
        {
            if (keepHistory)
                PushUndo();
            Palette = new Palette(Palette);
            Palette.Colors.Remove(pc);
            Dirty = true;
            if (fireEvent)
                OnPaletteChanged(new EventArgs());
        }


        /// <summary>
        /// Deletes a number of colors.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        public void DeleteColors(IEnumerable<PaletteColor> l, bool keepHistory = true, bool fireEvent = true)
        {
            if (keepHistory)
                PushUndo();
            foreach (var pc in l)
                DeleteColor(pc, false, false);
            if (fireEvent)
                OnPaletteChanged(new EventArgs());
        }

        /// <summary>
        /// Pushes the current state of the application to the undo stack, and purges the redo stack.
        /// </summary>
        private void PushUndo()
        {
            UndoHistory.Push(Palette);
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
                RedoHistory.Push(Palette);
                var p = UndoHistory.Pop();
                SetPalette(p, false, false);

                // because we didn't use the setters to fire events
                // because we'd be wastefully firing two otherwise
                OnPaletteChanged(new EventArgs());
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
                UndoHistory.Push(Palette);
                var p = RedoHistory.Pop();
                SetPalette(p, false, false);

                // because we didn't use the setters to fire events
                // because we'd be wastefully firing two otherwise
                OnPaletteChanged(new EventArgs());
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
    }
}
