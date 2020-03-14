using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Visual.Renderers
{
    /// <summary>
    /// Contains the shader data and its extensions
    /// </summary>
    public class ShaderProgramFragment : List<string>
    {
        /// <summary>
        /// The main shader data
        /// </summary>
        public string Main;
        /// <summary>
        /// The type of shader
        /// </summary>
        public ShaderType type;

        /// <summary>
        /// Constructor of the fragment
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="shaderType"></param>
        public ShaderProgramFragment(string data, ShaderType shaderType)
        {
            type = shaderType;
            Main = data;
        }

        /// <summary>
        /// Adds the extensions in a range. (params)
        /// </summary>
        /// <param name="vs"></param>
        public void AddRange(params string[] vs)
        {
            foreach (string v in vs) Add(v);
        }
    }
}