using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
