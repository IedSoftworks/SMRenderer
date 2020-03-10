using System.Windows;
using System.Windows.Controls;

namespace SMRenderer.ManagerIntergration.Base
{
    public abstract class VisualDataEntry
    {
        /// <summary>
        ///     Creates the panel
        /// </summary>
        /// <returns>The panel that will be used</returns>
        public abstract UIElement CreatePanel();

        public abstract object GetValue();
    }
}