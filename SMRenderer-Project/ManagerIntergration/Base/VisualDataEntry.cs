using System.Windows;
using System.Windows.Controls;

namespace SMRenderer.ManagerIntergration.Base
{
    /// <summary>
    ///     Used by SMManager to show and create a field of that type.
    /// </summary>
    public abstract class VisualDataEntry
    {
        /// <summary>
        ///     Creates the panel
        /// </summary>
        /// <returns>The panel that will be used</returns>
        public abstract UIElement CreatePanel();

        /// <summary>
        /// Returns the data from the panel.
        /// </summary>
        /// <returns>The data</returns>
        public abstract object GetValue();
    }
}