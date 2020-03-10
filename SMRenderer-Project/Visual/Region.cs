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
        public static Region zero = new Region {Position = new Vector2(0), Rotation = 0};

        /// <summary>
        ///     The region will always follow the anchor; This value will make all other values irrelevant;
        /// </summary>
        public DrawItem anchor = null;

        public Vector2 Position = new Vector2(0, 0);
        public float Rotation;

        public Vector2 GetPosition()
        {
            return anchor?.Position ?? Position;
        }

        public float GetRotation()
        {
            return anchor?.Rotation ?? Rotation;
        }
    }
}