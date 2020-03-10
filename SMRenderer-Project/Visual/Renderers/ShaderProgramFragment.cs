using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Visual.Renderers
{
    public class ShaderProgramFragment : List<string>
    {
        public string Main;
        public ShaderType type;

        public ShaderProgramFragment(string file, ShaderType shaderType)
        {
            type = shaderType;
            Main = file;
        }

        public void AddRange(params string[] vs)
        {
            foreach (string v in vs) Add(v);
        }
    }
}