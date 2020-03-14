using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Visual.Renderers
{
    /// <summary>
    /// Master class of all render programs.
    /// </summary>
    public class GenericRenderer
    {
        /// <summary>
        /// Contains the OpenGL ID for the render program
        /// </summary>
        public int mProgramId = -1;

        /// <summary>
        ///     Put here the requested attribute
        /// </summary>
        public List<string> RequestedAttrib = new List<string>();

        /// <summary>
        ///     Put here the requested fragment data
        /// </summary>
        public List<string> RequestedFragData = new List<string>();

        /// <summary>
        ///     Put here the requested uniforms
        /// </summary>
        public List<string> RequestedUniforms = new List<string>();

        /// <summary>
        /// The window.
        /// </summary>
        public GLWindow window;

        /// <summary>
        /// Contains all available attributes with IDs.
        /// <para>Require to update to use new attributes</para> 
        /// </summary>
        public static Dictionary<string, int> AttribIDs = new Dictionary<string, int>
        {
            {"aPosition", 0},
            {"aNormal", 1},
            {"aTexture", 2}
        };

        /// <summary>
        /// Contains all available fragment data with IDs.
        /// <para>Require to update to use new fragment data</para>
        /// </summary>
        public static Dictionary<string, int> FragDataIDs = new Dictionary<string, int>
        {
            {"color", 0},
            {"bloom", 1}
        };

        /// <summary>
        ///     Contains all requested uniforms and make them accessible
        /// </summary>
        public Dictionary<string, int> Uniforms { get; } = new Dictionary<string, int>();

        /// <summary>
        /// Creates the program.
        /// <para>Make sure the selected attributes and fragment data are in the ID list.</para>
        /// </summary>
        public void Create()
        {
            int i = -1;
            string currentClass = GetType().Name;

            mProgramId = GL.CreateProgram();

            var vertShader = Load(Shaders.Storage[GetType()].vertex);
            var fragShader = Load(Shaders.Storage[GetType()].fragment);


            Console.WriteLine(currentClass + ": ");
            vertShader.ForEach(a =>
            {
                string vertError = GL.GetShaderInfoLog(a);

                if (vertError != "" && vertError != "No errors. \n")
                {
                    window.ErrorAtLoading = true;
                    Console.WriteLine(currentClass + ": ");
                    Console.WriteLine($"Vertex {a}: \n{vertError}");
                }
            });
            fragShader.ForEach(a =>
            {
                string error = GL.GetShaderInfoLog(a);

                if (error != "" && error != "No errors. \n")
                {
                    window.ErrorAtLoading = true;
                    Console.WriteLine($"Fragment {a}: \n{error}");
                }
            });
            if (!window.ErrorAtLoading) Console.Clear();

            if (vertShader.All(a => a != -1) && fragShader.All(a => a != -1))
            {
                RequestedAttrib.ForEach(a =>
                {
                    if (!AttribIDs.ContainsKey(a))
                        throw new Exception("[ERROR] Attribute ID '" + a + "' doesn't exist. Please create one.");
                    GL.BindAttribLocation(mProgramId, AttribIDs[a], a);
                });
                RequestedFragData.ForEach(a =>
                {
                    if (!FragDataIDs.ContainsKey(a))
                        throw new Exception("[ERROR] FragData ID '" + a + "' doesn't exist. Please create one.");
                    GL.BindFragDataLocation(mProgramId, FragDataIDs[a], a);
                });
                GL.LinkProgram(mProgramId);
            }
            else
            {
                throw new Exception("!!!FATAL ERROR!!!\nCreating and linking shaders failed.");
            }

            for (i = 0; i < RequestedUniforms.Count; i++)
                Uniforms.Add(RequestedUniforms[i], GL.GetUniformLocation(mProgramId, RequestedUniforms[i]));
        }
        /// <summary>
        /// Load a shader program fragment. (Don't have to be a fragment shader)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public List<int> Load(ShaderProgramFragment s)
        {
            List<int> addrs = new List<int>();

            int addr = GL.CreateShader(s.type);
            GL.ShaderSource(addr, s.Main);
            GL.CompileShader(addr);
            GL.AttachShader(mProgramId, addr);
            addrs.Add(addr);

            foreach (string key in s)
            {
                string source = key.StartsWith("ext:") ? Shaders.ShaderSource[key] : key;

                int address = GL.CreateShader(s.type);
                GL.ShaderSource(address, source);
                GL.CompileShader(address);
                GL.AttachShader(mProgramId, address);
                addrs.Add(address);
            }

            return addrs;
        }
    }
}