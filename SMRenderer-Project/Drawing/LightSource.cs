using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Drawing
{
    public class LightSource
    {
        public Vector2 Position = Vector2.One;
        public Color4 Color = Color4.White;
        public float Intensity = 1;
    }
}
