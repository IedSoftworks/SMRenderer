using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Data;
using SMRenderer.Visual.Drawing;

namespace SMRenderer.Visual.Renderers
{
    internal class ParticleRenderer : GenericRenderer
    {
        public static ParticleRenderer program;

        public ParticleRenderer(GLWindow window)
        {
            this.window = window;

            RequestedUniforms = new List<string>
            {
                "uParticleMovements",
                "uParticleTime",
                "uSize"
            };

            PresetRendererCode.UniformEssencal(this);
            PresetRendererCode.UniformTexturing(this);
            PresetRendererCode.UniformLighting(this);
            PresetRendererCode.UniformBloom(this);

            Create();

            program = this;
        }

        internal void Draw(Particles item, Matrix4 mvp, Matrix4 nMatrix)
        {
            Texture texture = item.Texture == -1
                ? Texture.empty
                : ((TextureItem) DataManager.C["Textures"].Data(item.Texture.ID)).texture;
            ObjectInfos obj = (ObjectInfos) DataManager.C["Meshes"].Data(item.Object);

            GL.UseProgram(mProgramId);

            PresetRendererCode.DrawEssencal(this, mvp, item.Size);
            PresetRendererCode.DrawTexturing(this, item.Color, texture.TexId, item.Texture);
            PresetRendererCode.DrawLighting(this, item.ModelMatrix, nMatrix);
            PresetRendererCode.DrawBloom(this, item.Color, item.VisualEffectArgs);

            GL.Uniform3(Uniforms["uParticleMovements"], item.Amount, item.Movements);
            GL.Uniform1(Uniforms["uParticleTime"], (float) item.CurrentTime);
            GL.Uniform2(Uniforms["uSize"], item.Size);

            GL.BindVertexArray(obj.GetVAO());
            GL.DrawArraysInstanced(obj.PrimitiveType, 0, obj.GetVerticesCount(), item.Amount);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }
    }
}