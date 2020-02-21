using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Drawing;

namespace SMRenderer.Renderers
{
    class PresetRendererCode
    {
        public static void AddRangeIfNotExist(List<string> list, string[] addArray)
        {
            foreach (string a in addArray)
            {
                if (!list.Contains(a)) list.Add(a);
            }
        }


        public static void UniformEssencal(GenericRenderer renderer)
        {
            AddRangeIfNotExist(renderer.RequestedAttrib, new string[] { "aPosition", "aTexture" });
            AddRangeIfNotExist(renderer.RequestedFragData, new string[] { "color" });
            AddRangeIfNotExist(renderer.RequestedUniforms, new string[] { "uMVP", "uObjectSize" });
        }
        public static void DrawEssencal(GenericRenderer r, Matrix4 mvp, Vector2 size)
        {
            GL.UniformMatrix4(r.Uniforms["uMVP"], false, ref mvp);
            GL.Uniform2(r.Uniforms["uObjectSize"], size);
        }


        public static void UniformLighting(GenericRenderer renderer)
        {
            AddRangeIfNotExist(renderer.RequestedAttrib, new string[] { "aNormal", "aPosition" });
            AddRangeIfNotExist(renderer.RequestedFragData, new string[] { "color" });
            AddRangeIfNotExist(renderer.RequestedUniforms, new string[] {
                "uN",
                "uM",


                "uLightPositions",
                "uLightColors",
                "uLightCount",

                "uAmbientLight" 
            });
        }
        public static void DrawLighting(GenericRenderer r, Matrix4 modelm, Matrix4 normalm)
        {
            GL.UniformMatrix4(r.Uniforms["uM"], false, ref modelm);
            GL.UniformMatrix4(r.Uniforms["uN"], false, ref normalm);

            GL.Uniform4(r.Uniforms["uLightPositions"], Scene.current.lights.Count, Scene.current.lights.shaderArgs_positions);
            GL.Uniform4(r.Uniforms["uLightColors"], Scene.current.lights.Count, Scene.current.lights.shaderArgs_colors);
            GL.Uniform1(r.Uniforms["uLightCount"], Scene.current.lights.Count);

            GL.Uniform4(r.Uniforms["uAmbientLight"], Scene.current.ambientLight);
        }


        public static void UniformTexturing(GenericRenderer renderer)
        {
            AddRangeIfNotExist(renderer.RequestedAttrib, new string[] { "aPosition", "aTexture" });
            AddRangeIfNotExist(renderer.RequestedFragData, new string[] { "color" });
            AddRangeIfNotExist(renderer.RequestedUniforms, new string[]
            {
                "uColor",

                "uTexture",
                "uForm",
            });
        }
        public static void DrawTexturing(GenericRenderer r, Color4 color, int mainTex)
        {
            GL.Uniform4(r.Uniforms["uColor"], color);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, mainTex);
            GL.Uniform1(r.Uniforms["uTexture"], 0);
        }
        public static void DrawTexturing(GenericRenderer r, Color4 color, int mainTex, int formTex)
        {
            DrawTexturing(r, color, mainTex);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, mainTex);
            GL.Uniform1(r.Uniforms["uForm"], 1);
        }


        public static void UniformBloom(GenericRenderer r)
        {
            AddRangeIfNotExist(r.RequestedFragData, new string[] { "color", "bloom" });
            AddRangeIfNotExist(r.RequestedUniforms, new string[] { "uBloomUsage", "uBloomColor", "uColor" });
        }
        public static void DrawBloom(GenericRenderer r, Color4 objectColor, VisualEffectArgs effectArgs)
        {
            GL.Uniform1(r.Uniforms["uBloomUsage"], (int)effectArgs.BloomUsage);
            GL.Uniform4(r.Uniforms["uBloomColor"], effectArgs.BloomColor);
        }
    }
}
