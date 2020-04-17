using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Data;
using SMRenderer.Visual.Drawing;

namespace SMRenderer.Visual.Renderers
{
    /// <summary>
    /// The render program for any cases.
    /// </summary>
    public sealed class GeneralRenderer : GenericObjectRenderer
    {
        /// <summary>
        /// Reference to the render program
        /// </summary>
        public static GeneralRenderer program;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="window"></param>
        public GeneralRenderer(GLWindow window) : base(typeof(DrawItem))
        {
            this.window = window;

            PresetRendererCode.UniformEssencal(this);
            PresetRendererCode.UniformTexturing(this);
            PresetRendererCode.UniformLighting(this);
            PresetRendererCode.UniformDepth(this);
            PresetRendererCode.UniformBloom(this);

            Create();

            program = this;
        }


        /// <inheritdoc />
        internal override void Draw(DrawObject quad, SMItem item, Matrix4 view, Matrix4 model, Matrix4 normal)
        {
            GL.UseProgram(mProgramId);
            DrawItem drawitem = (DrawItem) item;

            ObjectInfos quadData = (ObjectInfos) quad.obj.Data;
            Texture texture = ((TextureItem) quad.Texture.Data).texture;
            Matrix4 modelview = model * view;

            PresetRendererCode.DrawEssencal(this, modelview, drawitem.Object.Size);
            PresetRendererCode.DrawTexturing(this, drawitem.Object.Color, texture.TexId, drawitem.Object.Texture);
            PresetRendererCode.DrawLighting(this, drawitem.modelMatrix, normal, drawitem.Object.effectArgs);
            PresetRendererCode.DrawBloom(this, drawitem.Object.Color, drawitem.Object.effectArgs);

            GL.BindVertexArray(quadData.GetVAO());
            GL.DrawArrays(quadData.PrimitiveType, 0, quadData.GetVerticesCount());

            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.UseProgram(0);
        }
    }
}