using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Drawing
{
    [Serializable]
    public class LightSource
    {
        public Vector2 Position = Vector2.Zero;
        public Color4 Color = Color4.White;
        public float Intensity = 1;
        public float Height = 1f;
    }
    [Serializable]
    public class LightCollection : List<LightSource>
    {
        [NonSerialized] internal float[] shaderArgs_positions;
        [NonSerialized] internal float[] shaderArgs_colors;
        new public void Add(LightSource source)
        {
            if (Count >= 4) base.Remove(this.Last());
            base.Add(source);
        }
        public void CreateShaderArgs()
        {
            List<float> shaderArgs_pos = new List<float>();
            List<float> shaderArgs_color = new List<float>();
            foreach(LightSource light in this)
            {
                shaderArgs_pos.AddRange(new float[] { light.Position.X, light.Position.Y, light.Height, light.Intensity });
                shaderArgs_color.AddRange(new float[] { light.Color.R, light.Color.G, light.Color.B, light.Color.A });
            }
            shaderArgs_colors = shaderArgs_color.ToArray();
            shaderArgs_positions = shaderArgs_pos.ToArray();
        }
    }
}
