using System;
using OpenTK.Input;

namespace SMRenderer.Data
{
    /// <summary>
    /// Shortcut to SMGlobals
    /// </summary>
    public class SMGl : SMGlobals
    {
    }
    /// <summary>
    /// Contains globals used for the renderer
    /// </summary>
    public class SMGlobals
    {
        /// <summary>
        /// The current delta time with scaling and anything.
        /// </summary>
        public static double currentDeltaTime = 0;
        /// <summary>
        /// The current delta time without scaling and anything.
        /// </summary>
        public static double currentDeltaTimeUnscaled = 0;

        public static bool focused;

        /// <summary>
        /// A global randomizer.
        /// </summary>
        public static Random random = new Random();

        public static KeyboardState keyboardState;
        public static MouseState mouseState;
    }
}