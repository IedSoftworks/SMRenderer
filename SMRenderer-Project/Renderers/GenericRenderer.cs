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
        public ShaderProgramFragment fragment;
        public ShaderProgramFragment vertex;
    }
    public class ShaderProgramFragment : List<string>
    {
        public ShaderType type = ShaderType.VertexShader;
        public string Main;
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

        public void Create()
        {
            int i = -1;
            List<int> _fragShader, _vertShader;
            string currentClass = GetType().Name;

            mProgramId = GL.CreateProgram();

            _vertShader = Load(Shaders.Storage[GetType()].vertex);
            _fragShader = Load(Shaders.Storage[GetType()].fragment);


            Console.WriteLine(currentClass + ": ");
            _vertShader.ForEach(a =>
            {
                string vertError = GL.GetShaderInfoLog(a);

                if (vertError != "" && vertError != "No errors. \n")
                {
                    window.ErrorAtLoading = true;
                    Console.WriteLine(currentClass + ": ");
                    Console.WriteLine($"Vertex {a}: \n{vertError}");
                }
            });
            _fragShader.ForEach(a =>
            {
                string error = GL.GetShaderInfoLog(a);

                if (error != "" && error != "No errors. \n")
                {
                    window.ErrorAtLoading = true;
                    Console.WriteLine($"Fragment {a}: \n{error}");
                }
            });
            if (!window.ErrorAtLoading) Console.Clear();

            if (!_vertShader.Any(a => a == -1) && !_fragShader.Any(a => a == -1))
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

        public List<int> Load(ShaderProgramFragment s)
        {
            List<int> addrs = new List<int>();
            s.Insert(0, s.Main);
            foreach (string source in s)
            {
                int address = -1;
                address = GL.CreateShader(s.type);
                GL.ShaderSource(address, source);
                GL.CompileShader(address);
                GL.AttachShader(mProgramId, address);
                addrs.Add(address);
            }
            return addrs;
        }
    }
    public class GenericObjectRenderer : GenericRenderer
    {
        virtual internal void Draw(ObjectInfos obj, SMItem item, Matrix4 view, Matrix4 model) { }
    }
    public class RendererCollection : List<GenericRenderer>
    {
        public int this[string typename]
        {
            get
            {
                return this.FindIndex(a => a.GetType().Name == typename);
            }
        }
    }
}
