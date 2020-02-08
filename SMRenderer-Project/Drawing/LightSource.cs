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
        public Vector2 Position = new Vector2(500);
        public Color4 Color = Color4.Yellow;
        public float Intensity = 100;
        public float Height = 3f;
    }
}
