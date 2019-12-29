using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace SMRenderer.Renderers
{
    class DefaultShaders
    {
        public static Assembly ass = typeof(DefaultShaders).Assembly;
        public static string NormalVertex = new StreamReader(ass.GetManifestResourceStream(ass.GetName().Name + ".Shaders.shader_vertex.glsl")).ReadToEnd();
        public static string NormalFragment = new StreamReader(ass.GetManifestResourceStream(ass.GetName().Name + ".Shaders.shader_fragment.glsl")).ReadToEnd();
        public static string BloomVertex = new StreamReader(ass.GetManifestResourceStream(ass.GetName().Name + ".Shaders.shader_bloom_vertex.glsl")).ReadToEnd();
        public static string BloomFragment = new StreamReader(ass.GetManifestResourceStream(ass.GetName().Name + ".Shaders.shader_bloom_fragment.glsl")).ReadToEnd();
    }
}
