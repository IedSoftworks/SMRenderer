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
    public sealed class GeneralRenderer : GenericObjectRenderer
    {
        static public ShaderProgramFiles files = new ShaderProgramFiles(DefaultShaders.GeneralFragment, DefaultShaders.GeneralVertex);
        static public GeneralRenderer program;

        private GLWindow window;

        public GeneralRenderer(GLWindow window)
        {
            this.window = window;

            RequestedAttrib = new List<string>()
            {
                "aPosition",
                "aNormal",
                "aTexture",
            };
            RequestedFragData = new List<string>()
            {
                "color",
                "bloom"
            };
            RequestedUniforms = new List<string>()
            {
                "uMVP",
                "uColor",

                "uTexture",
                "uForm",

                "uObjectSize",

                "uBloomUsage",
                "uBloomColor",

                "uBorderUsage",
                "uBorderColor",
                "uBorderWidth",
                "uBorderLength",
            };

            Create(files);

            program = this;
        }

        
        /// <summary>
        /// TIME TO DRAW, MOTHERFUCKER!
        /// </summary>
        /// <param name="quad">The object</param>
        /// <param name="drawitem">The DrawItem</param>
        /// <param name="view">The view matrix</param>
        /// <param name="model">The model matrix</param>
        override internal void Draw(Object quad, SMItem item, Matrix4 view, Matrix4 model)
        {
            GL.UseProgram(mProgramId);
            DrawItem drawitem = (DrawItem)item;
            Matrix4 modelview = model * view;
            GL.UniformMatrix4(Uniforms["uMVP"], false, ref modelview);
            GL.Uniform4(Uniforms["uColor"], drawitem.Color);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, drawitem.Texture.TexId);
            GL.Uniform1(Uniforms["uTexture"], 0);

            GL.ActiveTexture(TextureUnit.Texture1);
            GL.BindTexture(TextureTarget.Texture2D, drawitem.Form.texture.TexId);
            GL.Uniform1(Uniforms["uForm"], 1);

            GL.Uniform2(Uniforms["uObjectSize"], drawitem.Size);

            GL.Uniform1(Uniforms["uBloomUsage"], (int)drawitem.effectArgs.BloomUsage);
            GL.Uniform4(Uniforms["uBloomColor"], drawitem.effectArgs.BloomColor);

            GL.Uniform1(Uniforms["uBorderUsage"], (int)drawitem.effectArgs.BorderUsage);
            GL.Uniform4(Uniforms["uBorderColor"], drawitem.effectArgs.BorderColor);
            GL.Uniform1(Uniforms["uBorderWidth"], drawitem.effectArgs.BorderWidth);
            GL.Uniform1(Uniforms["uBorderLength"], drawitem.effectArgs.BorderLength);

            GL.BindVertexArray(quad.VAO);
            GL.DrawArrays(quad.primitiveType, 0, quad.VerticesCount);

            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.UseProgram(0);
        }
    }
}
