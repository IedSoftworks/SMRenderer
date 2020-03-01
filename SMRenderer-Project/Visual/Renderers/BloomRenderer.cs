using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Renderers
{
    public class BloomRenderer : GenericRenderer
    {
        static public BloomRenderer program;

        public int mAttr_vpos, mAttr_vtex = -1;

        public BloomRenderer(GLWindow window)
        {
            this.window = window;
            RequestedAttrib = new List<string>()
            {
                "aPosition",
                "aTexture"
            };
            RequestedFragData = new List<string>()
            {
                "color"
            };
            RequestedUniforms = new List<string>()
            {
                "uMVP",
                "uTextureScene",
                "uTextureBloom",
                "uMerge",
                "uHorizontal",
                "uResolution"
            };
            Create();

            mAttr_vpos = GL.GetAttribLocation(mProgramId, "aPos");
            mAttr_vtex = GL.GetAttribLocation(mProgramId, "aTex");

            program = this;
        }

        internal void DrawBloom(ref Matrix4 mvp, bool bloomDirectionHorizontal, bool merge, int width, int height, int sceneTexture, int bloomTexture)
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
            else GL.Uniform1(Uniforms["uMerge"], 0);

            GL.Uniform1(Uniforms["uHorizontal"], bloomDirectionHorizontal ? 1 : 0);
            GL.Uniform2(Uniforms["uResolution"], width, height);

            ObjectInfos obj = (ObjectInfos)DM.C["Meshes"].Data("Quad");

            GL.BindVertexArray(obj.GetVAO());
            GL.DrawArrays(obj.primitiveType, 0, obj.GetVerticesCount());

            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}
