using System.Collections.Generic;
using System.Linq;

namespace SMRenderer.Input
{
    /// <summary>
    /// Collectes Keybinds with additional features
    /// </summary>
    public class KeybindCollection : List<Keybind>
    {
        /// <summary>
        /// Keybinds in this collection will execute automaticlly.
        /// </summary>
        public static KeybindCollection AutoExecute = new KeybindCollection();

        /// <summary>
        /// If any keybind is pressed, returns true
        /// </summary>
        public bool IsPressed => Check();

        /// <summary>
        /// checks any keybind if all are pressed.
        /// </summary>
        /// <returns></returns>
        private bool Check()
        {
            return this.All(a => a.IsPressed);
        }
        /// <summary>
        /// Runs any check and execute-methods from keybinds.
        /// </summary>
        public void CheckAndExecute()
        {
            ForEach(a => a.CheckAndExecute());
        }
    }
}