using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Data;
using SMRenderer.Visual.Drawing;

namespace SMRenderer.Visual.Renderers
{
    public sealed class GeneralRenderer : GenericObjectRenderer
    {
        public static GeneralRenderer program;

        public GeneralRenderer(GLWindow window)
        {
            this.window = window;

            PresetRendererCode.UniformEssencal(this);
            PresetRendererCode.UniformTexturing(this);
            PresetRendererCode.UniformLighting(this);
            PresetRendererCode.UniformBloom(this);

            Create();

            program = this;
        }


        /// <summary>
        ///     TIME TO DRAW, MOTHERFUCKER!
        /// </summary>
        /// <param name="quad">The object</param>
        /// <param name="item">The DrawItem</param>
        /// <param name="view">The view matrix</param>
        /// <param name="model">The model matrix</param>
        internal override void Draw(ObjectInfos quad, SMItem item, Matrix4 view, Matrix4 model)
        {
            GL.UseProgram(mProgramId);
            DrawItem drawitem = (DrawItem) item;
            Texture texture = drawitem.Texture.ID == -1
                ? Texture.empty
                : ((TextureItem) DataManager.C["Textures"].Data(drawitem.Texture.ID)).texture;
            Matrix4 modelview = model * view;

            PresetRendererCode.DrawEssencal(this, modelview, drawitem.Size);
            PresetRendererCode.DrawTexturing(this, drawitem.Color, texture.TexId, drawitem.Texture);
            PresetRendererCode.DrawLighting(this, drawitem.modelMatrix, drawitem.normalMatrix);
            PresetRendererCode.DrawBloom(this, drawitem.Color, drawitem.effectArgs);

            GL.BindVertexArray(quad.GetVAO());
            GL.DrawArrays(quad.PrimitiveType, 0, quad.GetVerticesCount());

            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.UseProgram(0);
        }
    }
}