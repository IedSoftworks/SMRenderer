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
    public sealed class SkyboxRenderer : GenericRenderer
    {
        static public ShaderProgramFiles files = new ShaderProgramFiles(DefaultShaders.SkyboxFragment, DefaultShaders.SkyboxVertex);
        static public SkyboxRenderer program;

        private GLWindow window;

        public SkyboxRenderer(GLWindow window)
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
                "uTexSize",
                "uForm",
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
        internal void Draw(Object quad, DrawItem drawitem, Matrix4 modelview)
        {
            GL.UseProgram(mProgramId);
            GL.UniformMatrix4(Uniforms["uMVP"], false, ref modelview);
            GL.Uniform4(Uniforms["uColor"], drawitem.Color);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, drawitem.Texture.TexId);
            GL.Uniform1(Uniforms["uTexture"], 0);

            GL.ActiveTexture(TextureUnit.Texture1);
            GL.BindTexture(TextureTarget.Texture2D, drawitem.Form.texture.TexId);
            GL.Uniform1(Uniforms["uForm"], 1);

            GL.Uniform2(Uniforms["uTexSize"], new Vector2(drawitem.Texture.Width, drawitem.Texture.Height));

            GL.BindVertexArray(quad.VAO);
            GL.DrawArrays(quad.primitiveType, 0, quad.VerticesCount);

            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.UseProgram(0);
        }
    }
}
