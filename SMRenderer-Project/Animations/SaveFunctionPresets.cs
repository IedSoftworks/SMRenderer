using OpenTK;
using SMRenderer.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Animations
{
    public class SFPresets
    {
        public static Action<Value1Animation, double> Rotation(DrawItem item)
        {
            return (a, b) => item.Rotation = (float)b;
        }
        public static Action<Value2Animation, Vector2> Position(DrawItem item)
        {
            return (a, b) => item.Position = b;
        }

    }
}
