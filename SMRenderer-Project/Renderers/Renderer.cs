using System;
using System.IO;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Objects;
using System.Linq;
using SMRenderer.Drawing;
using SMRenderer.TypeExtensions;
using System.Collections.Generic;

namespace SMRenderer.Renderers
{
    public sealed class Renderer : GenericRenderer
    {
        private int _fragShader = -1;
        private int _vertShader = -1;

        static public List<string> fragment = new List<string>();
        static public List<string> vertex = new List<string>();

        private int 
            uMVP,
            uColor,

            uTexture, 
            uTexSize, 

            uObjectSize,
            uWindowSize,

            // Bloom Parameeter
            uBloomUsage,
            uBloomColor,
            
            // Border Parameters
            uBorderUsage, 
            uBorderColor,
            uBorderWidth,
            uBorderLength = -1;

        private GLWindow window;

        public Renderer(GLWindow window)
        {
            this.window = window;

            mProgramId = GL.CreateProgram();

            if (fragment.Count == 0) fragment.Add(DefaultShaders.NormalFragment);
            if (vertex.Count == 0) vertex.Add(DefaultShaders.NormalVertex);

            _fragShader = Load(fragment[0], ShaderType.FragmentShader);
            _vertShader = Load(vertex[0], ShaderType.VertexShader);

            if (_vertShader != -1 && _fragShader != -1)
            {
                GL.BindAttribLocation(mProgramId, 0, "aPosition");
                GL.BindAttribLocation(mProgramId, 1, "aNormal");
                GL.BindAttribLocation(mProgramId, 2, "aTexture");

                GL.BindFragDataLocation(mProgramId, 0, "color");
                GL.BindFragDataLocation(mProgramId, 1, "bloom");
                GL.LinkProgram(mProgramId);
            } else
            {
                throw new Exception("!!!FATAL ERROR!!!\nCreating and linking shaders failed.");
            }

            uMVP = GL.GetUniformLocation(mProgramId, "uMVP");
            uColor = GL.GetUniformLocation(mProgramId, "uColor");

            uTexture = GL.GetUniformLocation(mProgramId, "uTexture");
            uTexSize = GL.GetUniformLocation(mProgramId, "uTexSize");

            uWindowSize = GL.GetUniformLocation(mProgramId, "uWindowSize");
            uObjectSize = GL.GetUniformLocation(mProgramId, "uObjSize");
            
            uBloomColor = GL.GetUniformLocation(mProgramId, "uBloom");
            uBloomUsage = GL.GetUniformLocation(mProgramId, "uBloomUsage");

            uBorderUsage = GL.GetUniformLocation(mProgramId, "uBorderUsage");
            uBorderColor = GL.GetUniformLocation(mProgramId, "uBorderColor");
            uBorderWidth = GL.GetUniformLocation(mProgramId, "uBorderWidth");
            uBorderLength = GL.GetUniformLocation(mProgramId, "uBorderLength");

            GL.UseProgram(mProgramId);
        }

        
        /// <summary>
        /// TIME TO DRAW, MOTHERFUCKER!
        /// </summary>
        /// <param name="quad">The object</param>
        /// <param name="drawitem">The DrawItem</param>
        /// <param name="view">The view matrix</param>
        /// <param name="model">The model matrix</param>
        internal void Draw(Object quad, DrawItem drawitem, Matrix4 view, Matrix4 model, Dictionary<string, object> effectArgs)
        {
            Matrix4 modelview = model * view;
            GL.UniformMatrix4(uMVP, false, ref modelview);
            GL.Uniform4(uColor, drawitem.Color);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, drawitem.Texture.TexId);
            GL.Uniform1(uTexture, 0);
            GL.Uniform2(uTexSize, new Vector2(drawitem.Texture.Width, drawitem.Texture.Height));

            GL.Uniform2(uWindowSize, window.Size.ToVector2());
            GL.Uniform2(uObjectSize, drawitem.Size);

            GL.Uniform1(uBloomUsage, (int)(EffectBloomUsage)effectArgs["BloomUsage"]);
            GL.Uniform4(uBloomColor, (Color4)effectArgs["BloomColor"]);

            GL.Uniform1(uBorderUsage, (int)(EffectBorderUsage)effectArgs["BorderUsage"]);
            GL.Uniform4(uBorderColor, (Color4)effectArgs["BorderColor"]);
            GL.Uniform1(uBorderWidth, (int)effectArgs["BorderWidth"]);
            GL.Uniform1(uBorderLength, (int)effectArgs["BorderLength"]);

            GL.BindVertexArray(quad.VAO);
            GL.DrawArrays(quad.primitiveType, 0, quad.VerticesCount);

            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}
