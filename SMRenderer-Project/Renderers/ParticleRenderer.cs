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


        private int
            uMVP,
            uColor,
            uMotion,
            uRange,
            uLifeTime,
            uRandom = -1;

        private GLWindow window;
        public ParticleRenderer(GLWindow window)
        {
            this.window = window;

            int _fragShader, _vertShader = -1;

            mProgramId = GL.CreateProgram();

            _fragShader = Load(files.fragment, ShaderType.FragmentShader);
            _vertShader = Load(files.vertex, ShaderType.VertexShader);

            if (_vertShader != -1 && _fragShader != -1)
            {
                GL.BindAttribLocation(mProgramId, 0, "aPosition");
                GL.BindAttribLocation(mProgramId, 2, "aTexture");

                GL.BindFragDataLocation(mProgramId, 0, "color");
                GL.BindFragDataLocation(mProgramId, 1, "bloom");
                GL.LinkProgram(mProgramId);
            }
            else
            {
                throw new Exception("!!!FATAL ERROR!!!\nCreating and linking shaders failed.");
            }

            uMVP = GL.GetUniformLocation(mProgramId, "uMVP");
            uColor = GL.GetUniformLocation(mProgramId, "uColor");
            uMotion = GL.GetUniformLocation(mProgramId, "uMotion");
            uRange = GL.GetUniformLocation(mProgramId, "uRange");
            uLifeTime = GL.GetUniformLocation(mProgramId, "uTime");
            uRandom = GL.GetUniformLocation(mProgramId, "uRand");
            program = this;
        }
        internal void Draw(Object obj, Particle item, Matrix4 MVP)
        {
            GL.UseProgram(mProgramId);
            GL.UniformMatrix4(uMVP, false, ref MVP);
            GL.Uniform4(uColor, item.color);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, item.texture.TexId);

            GL.Uniform2(uMotion, item.Motion);
            GL.Uniform2(uRange, item.Range);
            GL.Uniform1(uLifeTime, (float)item.currentLifeTime);
            GL.Uniform1(uRandom, item.rand);

            GL.BindVertexArray(obj.VAO);
            GL.DrawArraysInstanced(obj.primitiveType, 0, obj.VerticesCount, item.Count);

            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            GL.UseProgram(0);
        }
    }
}
