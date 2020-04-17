using System.Drawing;
using OpenTK;

namespace SMRenderer.TypeExtensions
{
    /// <summary>
    /// Extensions to the Point-class
    /// </summary>
    public static class PointExt
    {
        /// <summary>
        /// Convert a Point to a Vector2
        /// </summary>
        /// <param name="point">The size</param>
        /// <returns></returns>
        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        /// <summary>
        /// Convert a Size to a Vector2
        /// </summary>
        /// <param name="size">The Size</param>
        /// <returns></returns>
        public static Vector2 ToVector2(this Size size)
        {
            return new Vector2(size.Width, size.Height);
        }
    }
}