using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Data;
using SMRenderer.Visual.Drawing;

namespace SMRenderer.Visual.Renderers
{
    internal class ParticleRenderer : GenericObjectRenderer
    {
        public static ParticleRenderer program;

        public ParticleRenderer(GLWindow window) : base(typeof(Particles))
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

        internal override void Draw(DrawObject drawObject, SMItem inputItem, Matrix4 viewMatrix, Matrix4 modelMatrix, Matrix4 normalMatrix)
        {
            Matrix4 mvp = modelMatrix * viewMatrix;

            Particles item = (Particles) inputItem;
            Texture texture = ((TextureItem) item.Object.Texture.Data).texture;
            ObjectInfos obj = (ObjectInfos) item.Object.obj.Data;

            GL.UseProgram(mProgramId);

            PresetRendererCode.DrawEssencal(this, mvp, item.Object.Size);
            PresetRendererCode.DrawTexturing(this, item.Object.Color, texture.TexId, item.Object.Texture);
            PresetRendererCode.DrawLighting(this, modelMatrix, normalMatrix, item.Object.effectArgs);
            PresetRendererCode.DrawBloom(this, item.Object.Color, item.Object.effectArgs);

            GL.Uniform3(Uniforms["uParticleMovements"], item.Amount, item.Movements);
            GL.Uniform1(Uniforms["uParticleTime"], (float) item.CurrentTime);
            GL.Uniform2(Uniforms["uSize"], item.Object.Size);

            GL.BindVertexArray(obj.GetVAO());
            GL.DrawArraysInstanced(obj.PrimitiveType, 0, obj.GetVerticesCount(), item.Amount);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }
    }
}