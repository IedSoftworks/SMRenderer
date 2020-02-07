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
        public GLWindow window;

        static Dictionary<string, int> AttribIDs { get; } = new Dictionary<string, int>()
        {
            { "aPosition", 0},
            { "aNormal", 1 },
            { "aTexture", 2 }
        };
        static Dictionary<string, int> FragDataIDs { get; } = new Dictionary<string, int>()
        {
            { "color", 0 },
            { "bloom", 1 }
        };

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

            string vertError = GL.GetShaderInfoLog(_vertShader);
            string fragError = GL.GetShaderInfoLog(_fragShader);

            if ((vertError != "" && vertError != "No errors.\n") || (fragError != "" && fragError != "No errors.\n"))
            {
                window.ErrorAtLoading = true;
                Console.WriteLine(currentClass+": ");
                Console.WriteLine($"Vertex: \n{vertError}");
                Console.WriteLine($"Fragment: \n{fragError}");
                Console.WriteLine("---------");
            }

            if (_vertShader != -1 && _fragShader != -1)
            {
                RequestedAttrib.ForEach(a =>
                {
                    if (!AttribIDs.ContainsKey(a)) throw new Exception("[ERROR] Attribute ID '"+a+"' doesn't exist. Please create one.");
                    GL.BindAttribLocation(mProgramId, AttribIDs[a], a);
                });
                RequestedFragData.ForEach(a =>
                {
                    if (!FragDataIDs.ContainsKey(a)) throw new Exception("[ERROR] FragData ID '"+a+"' doesn't exist. Please create one.");
                    GL.BindFragDataLocation(mProgramId, FragDataIDs[a], a);
                });
                GL.LinkProgram(mProgramId);
            }
            else
            {
                throw new Exception("!!!FATAL ERROR!!!\nCreating and linking shaders failed.");
            }

            for (i = 0; i < RequestedUniforms.Count; i++) Uniforms.Add(RequestedUniforms[i], GL.GetUniformLocation(mProgramId, RequestedUniforms[i]));

        }

        public int Load(string s, ShaderType type)
        {
            int address = GL.CreateShader(type);
            GL.ShaderSource(address, s);
            GL.CompileShader(address);
            GL.AttachShader(mProgramId, address);

            return address;
        }
    }
    public class GenericObjectRenderer : GenericRenderer
    {
        virtual internal void Draw(ObjectInfos obj, SMItem item, Matrix4 view, Matrix4 model) { }
    }
}
