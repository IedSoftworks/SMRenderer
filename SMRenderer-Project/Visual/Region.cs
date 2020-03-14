using System;
using OpenTK;
using SMRenderer.Visual.Drawing;

namespace SMRenderer.Visual
{
    /// <summary>
    ///     Creates a region to use relative values in DrawItem; All values in this class have to be absolute
    /// </summary>
    [Serializable]
    public class Region
    {
        /// <summary>
        ///     Static of a zero region.
        /// </summary>
        public static Region zero = new Region {Position = new Vector2(0), Rotation = 0};

        /// <summary>
        ///     The region will always follow the anchor; This value will make all other values irrelevant;
        /// </summary>
        public DrawItem anchor = null;
        /// <summary>
        /// The region position
        /// </summary>
        public Vector2 Position = new Vector2(0, 0);
        /// <summary>
        /// The region rotation
        /// </summary>
        public float Rotation;

        /// <summary>
        /// Returns the right position, based on current inputs.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetPosition()
        {
            return anchor?.Position ?? Position;
        }

        /// <summary>
        /// Returns the right rotation, based on current inputs.
        /// </summary>
        /// <returns></returns>
        public float GetRotation()
        {
            return anchor?.Rotation ?? Rotation;
        }
    }
}