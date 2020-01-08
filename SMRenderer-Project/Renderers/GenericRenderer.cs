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
        public string fragment = "";
        public string vertex = "";

        public ShaderProgramFiles(string fragment1, string vertex1)
        {
            fragment = fragment1;
            vertex = vertex1;
        }
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
