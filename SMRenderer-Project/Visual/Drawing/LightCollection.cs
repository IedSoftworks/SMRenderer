using System;
using System.Collections.Generic;
using System.Linq;

namespace SMRenderer.Visual.Drawing
{
    [Serializable]
    public class LightCollection : List<LightSource>
    {
        /// <summary>
        ///     Shader argument for the colors
        /// </summary>
        [NonSerialized] internal float[] ShaderArgsColors;

        /// <summary>
        ///     Shader argument for the colors
        /// </summary>
        [NonSerialized] internal float[] ShaderArgsDirections;

        /// <summary>
        ///     Shader argument for the positions
        /// </summary>
        [NonSerialized] internal float[] ShaderArgsPositions;

        /// <summary>
        ///     Adds a LightSource.
        ///     <para>Removes the first, if the capacity is exceeded</para>
        /// </summary>
        /// <param name="source"></param>
        public new void Add(LightSource source)
        {
            if (Count >= 4) Remove(this.First());
            base.Add(source);
        }

        /// <summary>
        ///     Generate the shader arguments
        /// </summary>
        public void CreateShaderArgs()
        {
            List<float> shaderArgsPos = new List<float>();
            List<float> shaderArgsColor = new List<float>();
            List<float> shaderArgsDirection = new List<float>();
            foreach (LightSource light in this)
            {
                shaderArgsPos.AddRange(new[] {light.Position.X, light.Position.Y, 2, light.Intensity});
                shaderArgsColor.AddRange(new[] {light.Color.R, light.Color.G, light.Color.B, light.Color.A});

                float dirLight = light.Direction.X + light.Direction.Y == 0 ? 0 : 1;
                shaderArgsDirection.AddRange(new[] {light.Direction.X, light.Direction.Y, dirLight});
            }

            ShaderArgsColors = shaderArgsColor.ToArray();
            ShaderArgsPositions = shaderArgsPos.ToArray();
            ShaderArgsDirections = shaderArgsDirection.ToArray();
        }
    }
}