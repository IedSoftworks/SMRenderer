using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using OpenTK.Input;
using SMRenderer.Data;

namespace SMRenderer.Input
{
    /// <summary>
    /// Contains keys and mouse buttons to create a keybind.
    /// </summary>
    public class Keybind
    {
        /// <summary>
        /// The keyboard keys for the keybind
        /// </summary>
        private IEnumerable<Key>_keyboardKeys = new List<Key>();
        /// <summary>
        /// The mouse button for the keybind
        /// </summary>
        private IEnumerable<MouseButton> _mouseKeys = new List<MouseButton>();

        public bool OnlyFocused = true;

        /// <summary>
        /// Event if the CheckAndExecute-method is called
        /// </summary>
        public event Action<IEnumerable<Key>, IEnumerable<MouseButton>> Checked;
        /// <summary>
        /// returns true, if the keybind is pressed.
        /// </summary>
        public bool IsPressed => Check();
        
        /// <summary>
        /// Constructor, for only keyboard keys
        /// </summary>
        /// <param name="keyboard"></param>
        public Keybind(params Key[] keyboard)
        {
            _keyboardKeys = keyboard;
        }

        /// <summary>
        /// Constructor, for mouse and keyboards
        /// </summary>
        /// <param name="mouseKeys"></param>
        /// <param name="keyboardKeys"></param>
        public Keybind(MouseButton[] mouseKeys, Key[] keyboardKeys)
        {
            _mouseKeys = mouseKeys;
            _keyboardKeys = keyboardKeys;
        }
        /// <summary>
        /// returns true, if the keybind is pressed.
        /// </summary>
        private bool Check()
        {
            return _mouseKeys.All(a => SMGl.mouseState[a]) && _keyboardKeys.All(a => SMGl.keyboardState[a]);
        }

        /// <summary>
        /// Checkes the keybind and execute the checked-event.
        /// </summary>
        /// <returns></returns>
        public bool CheckAndExecute()
        {
            bool checking = Check();

            if (OnlyFocused && !SMGlobals.focused) return checking; 

            if (checking)
                Checked?.Invoke(_keyboardKeys, _mouseKeys);

            return checking;
        }
    }
}