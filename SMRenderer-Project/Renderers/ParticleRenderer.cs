using SMRenderer.Drawing;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace SMRenderer.Renderers
{
    class ParticleRenderer : GenericRenderer
    {
        public static ParticleRenderer program;
        public ParticleRenderer(GLWindow window)
        {
            this.window = window;

            RequestedAttrib = new List<string>()
            {
                "aPosition",
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

                "uBloomUsage",
                "uBloomColor",

                "uParticleMovements",
                "uParticleTime",
                "uSize"
            };

            Create();

            program = this;
        }
        internal void Draw(Particles item, Matrix4 MVP)
        {
            Texture texture = item.Texture == -1 ? Texture.empty : ((TextureItem)DM.C["Textures"].Data(item.Texture)).texture;
            ObjectInfos obj = (ObjectInfos)DM.C["Meshes"].Data(item.Object);

            GL.UseProgram(mProgramId);

            GL.UniformMatrix4(Uniforms["uMVP"], false, ref MVP);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texture.TexId);
            GL.Uniform1(Uniforms["uTexture"], 0);

            GL.Uniform4(Uniforms["uColor"], item.Color);

            GL.Uniform1(Uniforms["uBloomUsage"], (int)item.VisualEffectArgs.BloomUsage);
            GL.Uniform4(Uniforms["uBloomColor"], item.VisualEffectArgs.BloomColor);

            GL.Uniform2(Uniforms["uParticleMovements"], item.Amount, item.Movements);
            GL.Uniform1(Uniforms["uParticleTime"], (float)item.CurrentTime);
            GL.Uniform2(Uniforms["uSize"], item.Size);

            GL.BindVertexArray(obj.GetVAO());
            GL.DrawArraysInstanced(obj.primitiveType, 0, obj.GetVerticesCount(), item.Amount);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }
    }
}
