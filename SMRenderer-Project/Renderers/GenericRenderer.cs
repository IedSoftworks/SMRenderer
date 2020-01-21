using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Drawing;

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

        /// <summary>
        /// Put here the requested attrib
        /// </summary>
        public List<string> RequestedAttrib { private get; set; } = new List<string>();
        /// <summary>
        /// Put here the requested fragdata
        /// </summary>
        public List<string> RequestedFragData { private get; set; } = new List<string>();
        /// <summary>
        /// Put here the requested uniforms
        /// </summary>
        public List<string> RequestedUniforms { private get; set; } = new List<string>();
        /// <summary>
        /// Contains all requested uniforms and make them accessable
        /// </summary>
        public Dictionary<string, int> Uniforms { get; private set; } = new Dictionary<string, int>();

        public void Create(ShaderProgramFiles files)
        {
            int i, _fragShader, _vertShader = -1;
            string currentClass = GetType().Name;

            mProgramId = GL.CreateProgram();

            _fragShader = Load(files.fragment, ShaderType.FragmentShader);
            _vertShader = Load(files.vertex, ShaderType.VertexShader);

            Console.WriteLine(currentClass + " - Vertex: " + GL.GetShaderInfoLog(_vertShader));
            Console.WriteLine(currentClass + " - Fragment: " + GL.GetShaderInfoLog(_fragShader));

            if (_vertShader != -1 && _fragShader != -1)
            {
                for (i = 0; i < RequestedAttrib.Count; i++) GL.BindAttribLocation(mProgramId, i, RequestedAttrib[i]);
                for (i = 0; i < RequestedFragData.Count; i++) GL.BindFragDataLocation(mProgramId, i, RequestedFragData[i]);
                GL.LinkProgram(mProgramId);
            }
            else
            {
                throw new Exception("!!!FATAL ERROR!!!\nCreating and linking shaders failed.");
            }

            for (i = 0; i < RequestedUniforms.Count; i++) Uniforms.Add(RequestedUniforms[i], GL.GetUniformLocation(mProgramId, RequestedUniforms[i]));

            Console.Clear();
        }

        public int Load(string s, ShaderType type)
        {
            int address = GL.CreateShader(type);
            GL.ShaderSource(address, s);
            GL.AttachShader(mProgramId, address);

            return address;
        }
    }
}
