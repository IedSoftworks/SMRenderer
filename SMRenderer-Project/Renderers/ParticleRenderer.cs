using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Drawing;

namespace SMRenderer.Renderers
{
    class ParticleRenderer : GenericRenderer
    {
        static public ShaderProgramFiles files = new ShaderProgramFiles(DefaultShaders.ParticleFragment, DefaultShaders.ParticleVertex);
        static public ParticleRenderer program;


        private GLWindow window;
        public ParticleRenderer(GLWindow window)
        {
            this.window = window;

            RequestedAttrib = new List<string>()
            {
                "aPosition",
                "aTexture"
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
                "uMotion",
                "uRange",
                "uLifeTime",
                "uRandom"
            };

            program = this;
        }
        internal void Draw(Object obj, Particle item, Matrix4 MVP)
        {
            GL.UseProgram(mProgramId);
            GL.UniformMatrix4(Uniforms["uMVP"], false, ref MVP);
            GL.Uniform4(Uniforms["uColor"], item.color);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, item.texture.TexId);

            GL.Uniform2(Uniforms["uMotion"], item.Motion);
            GL.Uniform2(Uniforms["uRange"], item.Range);
            GL.Uniform1(Uniforms["uLifeTime"], (float)item.currentLifeTime);
            GL.Uniform1(Uniforms["uRandom"], item.rand);

            GL.BindVertexArray(obj.VAO);
            GL.DrawArraysInstanced(obj.primitiveType, 0, obj.VerticesCount, item.Count);

            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            GL.UseProgram(0);
        }
    }
}
