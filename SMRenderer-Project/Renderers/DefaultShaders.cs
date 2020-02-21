using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace SMRenderer.Renderers
{
    class Shaders
    {
        public static Assembly ass = typeof(Shaders).Assembly;
        public static Dictionary<string, string> ShaderSource = new Dictionary<string, string>()
        {
            { "general_vertex", Read("Shaders.general.v.vert", ass) },
            { "general_frag", Read("Shaders.general.f.frag", ass) },
            { "general_border", Read("Shaders.general.border.frag", ass) },

            { "bloom_vertex", Read("Shaders.bloom.v.vert", ass) },
            { "bloom_frag", Read("Shaders.bloom.f.frag", ass) },
            { "bloom_config", Read("Shaders.bloom.config.frag", ass) },

            { "particle_vertex", Read("Shaders.particle.v.vert", ass) },
            { "particle_frag", Read("Shaders.particle.f.frag", ass) },
        };


        public static Dictionary<Type, ShaderProgramFiles> Storage = new Dictionary<Type, ShaderProgramFiles>
        {
            { typeof(GeneralRenderer), new ShaderProgramFiles() { 
                vertex = new ShaderProgramFragment(ShaderSource["general_vertex"], OpenTK.Graphics.OpenGL4.ShaderType.VertexShader),
                fragment = new ShaderProgramFragment(ShaderSource["general_frag"], OpenTK.Graphics.OpenGL4.ShaderType.FragmentShader)
                {
                    ShaderSource["general_border"],
                    ShaderSource["bloom_config"],
                }
            } },
            
            { typeof(BloomRenderer), new ShaderProgramFiles() { 
                vertex = new ShaderProgramFragment(ShaderSource["bloom_vertex"], OpenTK.Graphics.OpenGL4.ShaderType.VertexShader),
                fragment = new ShaderProgramFragment(ShaderSource["bloom_frag"], OpenTK.Graphics.OpenGL4.ShaderType.FragmentShader)
            } },

            { typeof(ParticleRenderer), new ShaderProgramFiles() { 
                vertex = new ShaderProgramFragment(ShaderSource["particle_vertex"], OpenTK.Graphics.OpenGL4.ShaderType.VertexShader),
                fragment = new ShaderProgramFragment(ShaderSource["particle_frag"], OpenTK.Graphics.OpenGL4.ShaderType.FragmentShader) { 
                    ShaderSource["bloom_config"]
                },
            } }
        };
            
        /// <summary>
        /// Reads the file contents
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns></returns>
        public static string Read(string path)
        {
            Stream stream = File.OpenRead(path);
            return new StreamReader(stream).ReadToEnd();
        }
        /// <summary>
        /// Reads a file from a assembly
        /// </summary>
        /// <param name="path">The Path to file</param>
        /// <param name="assem">The Assembly</param>
        /// <returns></returns>
        public static string Read(string path, Assembly assem) {
            return new StreamReader(assem.GetManifestResourceStream(assem.GetName().Name + "." + path)).ReadToEnd();
        }
    }
}
