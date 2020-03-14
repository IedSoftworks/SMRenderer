using System;
using OpenTK;
using SMRenderer.Visual.Drawing;

namespace SMRenderer.Animations
{
    /// <summary>
    /// Contains presets for animations-savefunctions
    /// </summary>
    [Serializable]
    public class SFPresets
    {
        /// <summary>
        ///     Animation function for position
        /// </summary>
        public static Action<Value2Animation, Vector2> DrawItemPosition = (a, b) => ((DrawItem) a.Object).Position = b;

        /// <summary>
        ///     Animation function for rotation
        /// </summary>
        public static Action<Value1Animation, double> DrawItemRotation =
            (a, b) => ((DrawItem) a.Object).Rotation = (float) b;
    }
}