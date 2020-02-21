using OpenTK;
using SMRenderer.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Animations
{
    [Serializable]
    public class SFPresets
    {
        /// <summary>
        /// Animation function for position
        /// </summary>
        public static Action<Value2Animation, Vector2> DrawItem_Position = (a, b) => ((DrawItem)a.Object).Position = b;
        /// <summary>
        /// Animation function for rotation
        /// </summary>
        public static Action<Value1Animation, double> DrawItem_Rotation = (a, b) => ((DrawItem)a.Object).Rotation = (float)b;

    }
}
