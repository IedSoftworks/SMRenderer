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

            RequestedUniforms = new List<string>()
            {
                "uParticleMovements",
                "uParticleTime",
                "uSize",
            };

            PresetRendererCode.UniformEssencal(this);
            PresetRendererCode.UniformTexturing(this);
            PresetRendererCode.UniformLighting(this);
            PresetRendererCode.UniformBloom(this);

            Create();

            program = this;
        }
        internal void Draw(Particles item, Matrix4 MVP, Matrix4 NMatrix)
        {
            Texture texture = item.Texture == -1 ? Texture.empty : ((TextureItem)DM.C["Textures"].Data(item.Texture)).texture;
            ObjectInfos obj = (ObjectInfos)DM.C["Meshes"].Data(item.Object);

            GL.UseProgram(mProgramId);

            PresetRendererCode.DrawEssencal(this, MVP, item.Size);
            PresetRendererCode.DrawTexturing(this, item.Color, texture.TexId);
            PresetRendererCode.DrawLighting(this, item.ModelMatrix, NMatrix);
            PresetRendererCode.DrawBloom(this, item.Color, item.VisualEffectArgs);

            GL.Uniform3(Uniforms["uParticleMovements"], item.Amount, item.Movements);
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
