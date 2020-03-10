using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Data;

namespace SMRenderer.Visual.Renderers
{
    public class BloomRenderer : GenericRenderer
    {
        public static BloomRenderer program;

        public int MAttrVpos, MAttrVtex = -1;

        public BloomRenderer(GLWindow window)
        {
            this.window = window;
            RequestedAttrib = new List<string>
            {
                "aPosition",
                "aTexture"
            };
            RequestedFragData = new List<string>
            {
                "color"
            };
            RequestedUniforms = new List<string>
            {
                "uMVP",
                "uTextureScene",
                "uTextureBloom",
                "uMerge",
                "uHorizontal",
                "uResolution"
            };
            Create();

            MAttrVpos = GL.GetAttribLocation(mProgramId, "aPos");
            MAttrVtex = GL.GetAttribLocation(mProgramId, "aTex");

            program = this;
        }

        internal void DrawBloom(ref Matrix4 mvp, bool bloomDirectionHorizontal, bool merge, int width, int height,
            int sceneTexture, int bloomTexture)
        {
            GL.UniformMatrix4(Uniforms["uMVP"], false, ref mvp);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, bloomTexture);
            GL.Uniform1(Uniforms["uTextureBloom"], 0);

            if (merge)
            {
                GL.ActiveTexture(TextureUnit.Texture1);
                GL.BindTexture(TextureTarget.Texture2D, sceneTexture);
                GL.Uniform1(Uniforms["uTextureScene"], 1);

                GL.Uniform1(Uniforms["uMerge"], 1);
            }
            else
            {
                GL.Uniform1(Uniforms["uMerge"], 0);
            }

            GL.Uniform1(Uniforms["uHorizontal"], bloomDirectionHorizontal ? 1 : 0);
            GL.Uniform2(Uniforms["uResolution"], width, height);

            ObjectInfos obj = (ObjectInfos) DataManager.C["Meshes"].Data("Quad");

            GL.BindVertexArray(obj.GetVAO());
            GL.DrawArrays(obj.PrimitiveType, 0, obj.GetVerticesCount());

            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}