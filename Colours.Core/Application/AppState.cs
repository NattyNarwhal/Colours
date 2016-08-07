namespace Colours
{
    /// <summary>
    /// Represents the minimum amount of application state for the.
    /// colour schemeing component. This is also used for undo/redo.
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
            return string.Format("{0} of {1}", SchemeType.ToString(),
                Color.ToRgb().ToHtml());
        }
    }
}
