using System.Drawing;
using OpenTK;

namespace SMRenderer.TypeExtensions
{
    public static class PointExt
    {
        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static Vector2 ToVector2(this Size point)
        {
            return new Vector2(point.Width, point.Height);
        }
    }
}