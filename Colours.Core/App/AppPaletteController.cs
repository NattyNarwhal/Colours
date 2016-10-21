using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colours.App
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
        public IPalette Palette { get; set; }
        /// <summary>
        /// The undo history. This will be set for you by the functions in this object.
        /// </summary>
        public Stack<AppPalUndo> UndoHistory { get; private set; } // should the user be able to peek at it?
        /// <summary>
        /// The redo history. This will be set for you by the functions in this object.
        /// </summary>
        public Stack<AppPalUndo> RedoHistory { get; private set; }
        /// <summary>
        /// Gets the file name of the loaded file.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Gets if the file should be saved.
        /// </summary>
        public bool Dirty { get; set; }

        /// <summary>
        /// Gets the name of the palette, using the file name as a fallback.
        /// </summary>
        public string PaletteName
        {
            get
            {
                if (Palette is INamedPalette)
                    return ((INamedPalette)Palette).Name;
                else if (!string.IsNullOrWhiteSpace(FileName))
                    return Path.GetFileNameWithoutExtension(FileName);
                else
                    return string.Empty;
            }
        }

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
            UndoHistory = new Stack<AppPalUndo>();
            RedoHistory = new Stack<AppPalUndo>();
        }

        /// <summary>
        /// Creates a new palette and resets the state of the controller.
        /// </summary>
        public void New()
        {
            NewFromPalette(new GimpPalette());
        }

        /// <summary>
        /// Creates a new application state with an existing palette.
        /// </summary>
        /// <param name="p">The existing palette to use.</param>
        /// <param name="fileName">The name of the file.</param>
        public void NewFromPalette(IPalette p, string fileName = null)
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
        /// <param name="action">If the undo is added, the action it is described as.</param>
        public void SetPalette(IPalette p, bool keepHistory = true, bool fireEvent = true, string action = null)
        {
            if (keepHistory && Palette != p)
                PushUndo(action ?? "Palette Change");
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
        /// <param name="action">If the undo is added, the action it is described as.</param>
        public void AppendColor(RgbColor c, bool keepHistory = true, bool fireEvent = true, string action = null)
        {
            AppendColor(new PaletteColor(c), keepHistory, fireEvent);
        }

        /// <summary>
        /// Adds a color to the palette.
        /// </summary>
        /// <param name="pc">The color to append.</param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        /// <param name="action">If the undo is added, the action it is described as.</param>
        public void AppendColor(PaletteColor pc, bool keepHistory = true, bool fireEvent = true, string action = null)
        {
            if (keepHistory)
                PushUndo(action ?? "Add Colour"); // TODO: should we announce the colour we're acting on? same for other funcs
            Palette = Palette.Clone();
            Palette.Colors.Add(pc);
            Dirty = true;
            if (fireEvent)
                OnPaletteChanged(new EventArgs());
        }

        /// <summary>
        /// Adds a list of colors to the palette.
        /// </summary>
        /// <param name="lc">A list of colors to add.</param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        /// <param name="action">If the undo is added, the action it is described as.</param>
        public void AppendColors(IEnumerable<RgbColor> lc, bool keepHistory = true, bool fireEvent = true, string action = null)
        {
            AppendColors(lc.Select(x => new PaletteColor(x)), keepHistory, fireEvent);
        }

        /// <summary>
        /// Adds a list of colors to the palette.
        /// </summary>
        /// <param name="lc">A list of colors to add.</param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        /// <param name="action">If the undo is added, the action it is described as.</param>
        public void AppendColors(IEnumerable<PaletteColor> lc, bool keepHistory = true, bool fireEvent = true, string action = null)
        {
            if (keepHistory)
                PushUndo(action ?? "Add Colours");
            foreach (var pc in lc)
                AppendColor(pc, false, false);
            if (fireEvent)
                OnPaletteChanged(new EventArgs());
        }

        /// <summary>
        /// Deletes a color from the palette.
        /// </summary>
        /// <param name="pc">The color to remove.</param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        /// <param name="action">If the undo is added, the action it is described as.</param>
        public void DeleteColor(PaletteColor pc, bool keepHistory = true, bool fireEvent = true, string action = null)
        {
            if (keepHistory)
                PushUndo(action ?? "Delete Colour");
            Palette = Palette.Clone();
            Palette.Colors.Remove(pc);
            Dirty = true;
            if (fireEvent)
                OnPaletteChanged(new EventArgs());
        }


        /// <summary>
        /// Deletes a number of colors.
        /// </summary>
        /// <param name="l">A list of colors to remove</param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        /// <param name="action">If the undo is added, the action it is described as.</param>
        public void DeleteColors(IEnumerable<PaletteColor> l, bool keepHistory = true, bool fireEvent = true, string action = null)
        {
            if (keepHistory)
                PushUndo(action ?? "Delete Colours");
            foreach (var pc in l)
                DeleteColor(pc, false, false);
            if (fireEvent)
                OnPaletteChanged(new EventArgs());
        }

        /// <summary>
        /// Renames a color.
        /// </summary>
        /// <param name="index">The location of the color.</param>
        /// <param name="newName">The new name of the color.</param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        /// <param name="action">If the undo is added, the action it is described as.</param>
        public void RenameColor(int index, string newName, bool keepHistory = true, bool fireEvent = true, string action = null)
        {
            if (keepHistory)
                PushUndo(action ?? "Rename Colour");
            Palette = Palette.Clone();
            Palette.Colors[index] =
                new PaletteColor(Palette.Colors[index].Color, newName);
            Dirty = true;
            if (fireEvent)
                OnPaletteChanged(new EventArgs());
        }

        /// <summary>
        /// Renames a color.
        /// </summary>
        /// <param name="pc">The color.</param>
        /// <param name="newName">The new name of the color.</param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        /// <param name="action">If the undo is added, the action it is described as.</param>
        public void RenameColor(PaletteColor pc, string newName, bool keepHistory = true, bool fireEvent = true, string action = null)
        {
            if (Palette.Colors.Contains(pc))
                RenameColor(Palette.Colors.IndexOf(pc), newName, keepHistory, fireEvent, action);
            else throw new ArgumentException("The colour is not in the palette.");
        }

        /// <summary>
        /// Renames several colors.
        /// </summary>
        /// <param name="newNames">The color and their new names, as a dictionary.</param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        /// <param name="action">If the undo is added, the action it is described as.</param>
        public void RenameColors(IDictionary<PaletteColor, string> newNames, bool keepHistory = true, bool fireEvent = true, string action = null)
        {
            if (keepHistory)
                PushUndo(action ?? "Rename Colours");
            Palette = Palette.Clone();
            foreach (var i in newNames)
            {
                var index = Palette.Colors.IndexOf(i.Key);
                if (index == -1)
                    throw new ArgumentException("The colour is not in the palette."); ;
                Palette.Colors[index] =
                    new PaletteColor(Palette.Colors[index].Color, i.Value);
            }
            Dirty = true;
            if (fireEvent)
                OnPaletteChanged(new EventArgs());
        }

        /// <summary>
        /// Moves a color's position.
        /// </summary>
        /// <param name="pc">The color to move.</param>
        /// <param name="newIndex">The new location of the color.</param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        /// <param name="action">If the undo is added, the action it is described as.</param>
        public void MoveColor(PaletteColor pc, int newIndex, bool keepHistory = true, bool fireEvent = true, string action = null)
        {
            if (keepHistory)
                PushUndo(action ?? "Move Colour");
            Palette = Palette.Clone();
            Palette.Colors.Remove(pc);
            Palette.Colors.Insert(newIndex, pc);
            Dirty = true;
            if (fireEvent)
                OnPaletteChanged(new EventArgs());
        }

        /// <summary>
        /// Moves a color's position.
        /// </summary>
        /// <param name="pc">The color to move.</param>
        /// <param name="targetPC">The new location of the color.</param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        /// <param name="action">If the undo is added, the action it is described as.</param>
        public void MoveColor(PaletteColor pc, PaletteColor targetPC, bool keepHistory = true, bool fireEvent = true, string action = null)
        {
            MoveColor(pc, Palette.Colors.IndexOf(targetPC), keepHistory, fireEvent, action);
        }

        /// <summary>
        /// Moves the position of several colors.
        /// </summary>
        /// <param name="lpc">The list of colors to move.</param>
        /// <param name="targetPC">
        /// The new location of the color. If null, it'll be the end of the list.
        /// </param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        /// <param name="action">If the undo is added, the action it is described as.</param>
        public void MoveColors(IEnumerable<PaletteColor> lpc, PaletteColor targetPC, bool keepHistory = true, bool fireEvent = true, string action = null)
        {
            if (keepHistory)
                PushUndo(action ?? "Move Colours");
            Palette = Palette.Clone();
            Palette.Colors.RemoveAll(x => lpc.Contains(x));
            // ugly stuff to handle moves gracefully
            if (targetPC != null)
                Palette.Colors.InsertRange(Palette.Colors.IndexOf(targetPC), lpc);
            else
                Palette.Colors.AddRange(lpc);
            Dirty = true;
            if (fireEvent)
                OnPaletteChanged(new EventArgs());
        }

        /// <summary>
        /// Changes a color in place.
        /// </summary>
        /// <param name="pc">The color to change.</param>
        /// <param name="newColor">The new color.</param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        /// <param name="action">If the undo is added, the action it is described as.</param>
        public void ChangeColor(PaletteColor pc, RgbColor newColor, bool keepHistory = true, bool fireEvent = true, string action = null)
        {
            if (keepHistory)
                PushUndo(action ?? "Change Colour");
            Palette = Palette.Clone();
            if (Palette.Colors.Contains(pc))
                pc.Color = newColor;
            Dirty = true;
            if (fireEvent)
                OnPaletteChanged(new EventArgs());
        }

        /// <summary>
        /// Changes several colors in place.
        /// </summary>
        /// <param name="lpc">The list of colors to change.</param>
        /// <param name="newColor">The new color for all.</param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        /// <param name="action">If the undo is added, the action it is described as.</param>
        public void ChangeColors(IEnumerable<PaletteColor> lpc, RgbColor newColor, bool keepHistory = true, bool fireEvent = true, string action = null)
        {
            if (keepHistory)
                PushUndo(action ?? "Change Colours");
            Palette = Palette.Clone();
            foreach (var pc in lpc)
                if (Palette.Colors.Contains(pc))
                    pc.Color = newColor;
            Dirty = true;
            if (fireEvent)
                OnPaletteChanged(new EventArgs());
        }

        /// <summary>
        /// Sorts the palette by some criteria.
        /// </summary>
        /// <param name="sortBy">The value to sort the list by.</param>
        /// <param name="ascending">If the list should be in ascending order.</param>
        /// <param name="keepHistory">If undo should have been added.</param>
        /// <param name="fireEvent">If the event should fire.</param>
        /// <param name="action">If the undo is added, the action it is described as.</param>
        public void SortColors(PaletteSortBy sortBy, bool ascending, bool keepHistory = true, bool fireEvent = true, string action = null)
        {
            if (keepHistory)
                PushUndo(action ?? "Sort Colours");
            Palette = Palette.Clone();
            switch (sortBy)
            {
                case PaletteSortBy.Name:
                    Palette.Colors = ascending ? Palette.Colors.OrderBy(x => x.Name).ToList()
                        : Palette.Colors.OrderByDescending(x => x.Name).ToList();
                    break;
                case PaletteSortBy.Hue:
                    Palette.Colors = ascending ? Palette.Colors.OrderBy(x => x.Color.ToHsv().Hue).ToList()
                        : Palette.Colors.OrderByDescending(x => x.Color.ToHsv().Hue).ToList();
                    break;
                case PaletteSortBy.Saturation:
                    Palette.Colors = ascending ? Palette.Colors.OrderBy(x => x.Color.ToHsv().Saturation).ToList()
                        : Palette.Colors.OrderByDescending(x => x.Color.ToHsv().Saturation).ToList();
                    break;
                case PaletteSortBy.Brightness:
                    Palette.Colors = ascending ? Palette.Colors.OrderBy(x => x.Color.ToHsv().Value).ToList()
                        : Palette.Colors.OrderByDescending(x => x.Color.ToHsv().Value).ToList();
                    break;
            }
            Dirty = true;
            if (fireEvent)
                OnPaletteChanged(new EventArgs());
        }

        /// <summary>
        /// Pushes the current state of the application to the undo stack, and purges the redo stack.
        /// </summary>
        private void PushUndo(string action)
        {
            UndoHistory.Push(new AppPalUndo(Palette, action));
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
                RedoHistory.Push(new AppPalUndo(Palette, UndoHistory.Peek().Name));
                var state = UndoHistory.Pop();
                SetPalette(state.Palette, false);
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
                UndoHistory.Push(new AppPalUndo(Palette, RedoHistory.Peek().Name));
                var state = RedoHistory.Pop();
                SetPalette(state.Palette, false);
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
