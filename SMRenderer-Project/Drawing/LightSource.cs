using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Drawing
{
    /// <summary>
    /// A light source for the scene
    /// </summary>
    [Serializable]
    public class LightSource
    {
        /// <summary>
        /// Position
        /// </summary>
        public Vector2 Position = Vector2.Zero;
        /// <summary>
        /// Lightcolor
        /// </summary>
        public Color4 Color = Color4.White;
        /// <summary>
        /// Light intensity
        /// </summary>
        public float Intensity = 1;
        /// <summary>
        /// Light height
        /// </summary>
        public float Height = 1f;
    }
    [Serializable]
    public class LightCollection : List<LightSource>
    {
        /// <summary>
        /// Shader argument for the positions
        /// </summary>
        [NonSerialized] internal float[] shaderArgs_positions;
        /// <summary>
        /// Shader argument for the colors
        /// </summary>
        [NonSerialized] internal float[] shaderArgs_colors;
        /// <summary>
        /// Adds a LightSource.
        /// <para>Removes the first, if the capazity is exeded</para>
        /// </summary>
        /// <param name="source"></param>
        new public void Add(LightSource source)
        {
            if (Count >= 4) base.Remove(this.First());
            base.Add(source);
        }
        /// <summary>
        /// Generate the shader arguments
        /// </summary>
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
