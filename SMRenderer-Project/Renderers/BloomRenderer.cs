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
        static public List<string> fragment = new List<string>();
        static public List<string> vertex = new List<string>();

        private int _fragShader = -1;
        private int _vertShader = -1;

        private int mAttr_vpos = -1;
        private int mAttr_vtex = -1;

        private int uMVP = -1;
        private int uTextureBloom = -1;
        private int uTextureScene = -1;
        private int uMerge = -1;
        private int uHorizontal = -1;
        private int uResolution = -1;

        public BloomRenderer()
        {
            mProgramId = GL.CreateProgram();

            if (fragment.Count == 0) fragment.Add(DefaultShaders.BloomFragment);
            if (vertex.Count == 0) vertex.Add(DefaultShaders.BloomVertex);

            _fragShader = Load(fragment[0], ShaderType.FragmentShader);
            _vertShader = Load(vertex[0], ShaderType.VertexShader);

            Console.WriteLine(GL.GetShaderInfoLog(_fragShader));
            Console.WriteLine(GL.GetShaderInfoLog(_vertShader));

            if (_vertShader >= 0 && _fragShader >= 0)
            {
                GL.BindAttribLocation(mProgramId, 0, "aPosition");
                GL.BindAttribLocation(mProgramId, 2, "aTexture");

                GL.BindFragDataLocation(mProgramId, 0, "color");
                GL.LinkProgram(mProgramId);
            }
            else
            {
                throw new Exception("!!!FATAL ERROR!!!\nCreating and linking shaders failed.");
            }

            mAttr_vpos = GL.GetAttribLocation(mProgramId, "aPos");
            mAttr_vtex = GL.GetAttribLocation(mProgramId, "aTex");

            uMVP = GL.GetUniformLocation(mProgramId, "uMVP");
            uTextureScene = GL.GetUniformLocation(mProgramId, "uTextureScene");
            uTextureBloom = GL.GetUniformLocation(mProgramId, "uTextureBloom");
            uMerge = GL.GetUniformLocation(mProgramId, "uMerge");
            uHorizontal = GL.GetUniformLocation(mProgramId, "uHorizontal");
            uResolution = GL.GetUniformLocation(mProgramId, "uResolution");
        }

        internal void DrawBloom(Object obj, ref Matrix4 mvp, bool bloomDirectionHorizontal, bool merge, int width, int height, int sceneTexture, int bloomTexture)
        {
            GL.UniformMatrix4(uMVP, false, ref mvp);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, bloomTexture);
            GL.Uniform1(uTextureBloom, 0);

            if (merge)
            {
                GL.ActiveTexture(TextureUnit.Texture1);
                GL.BindTexture(TextureTarget.Texture2D, sceneTexture);
                GL.Uniform1(uTextureScene, 1);

                GL.Uniform1(uMerge, 1);
            }
            else GL.Uniform1(uMerge, 0);

            GL.Uniform1(uHorizontal, bloomDirectionHorizontal ? 1 : 0);
            GL.Uniform2(uResolution, width, height);

            GL.BindVertexArray(obj.VAO);
            GL.DrawArrays(obj.primitiveType, 0, obj.VerticesCount);

            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}
