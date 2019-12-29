using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Renderers
{
    public class ShaderProgramFiles
    {
        public List<string> fragment = new List<string>();
        public List<string> vertex = new List<string>();
    }
    public class GenericRenderer
    {
        public int mProgramId = -1;
        public int Load(string s, ShaderType type)
        {
            int address = GL.CreateShader(type);
            GL.ShaderSource(address, s);
            GL.AttachShader(mProgramId, address);

            return address;
        }
    }
}
