using OpenTK.Graphics.OpenGL4;
using SMRenderer.Renderers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace SMRenderer
{
    public class ObjectManager
    {
        public static Dictionary<string, Object> OB{ get; private set; } = new Dictionary<string, Object>();
        /// <summary>
        /// DO NOT RUN THIS COMMAND!
        /// IT WILL BE AUTOMATICLY RUN WHEN LOAD THE WINDOW!
        /// </summary>
        public static void LoadObj()
        {
            OB = new Dictionary<string, Object>();

            foreach (Type type in Assembly.GetAssembly(typeof(Object)).GetTypes().Where(a => a.IsClass && !a.IsAbstract && a.IsSubclassOf(typeof(Object))))
            {
                Object obj = (Object)Activator.CreateInstance(type);

                obj.Compile();
                
                OB.Add(type.Name, obj);
            };
        }
    }
    public class OM : ObjectManager { }
    public class ObjectInfos
    {
        public static ObjectInfos empty = new ObjectInfos();

        virtual public List<float> Vertices { get; set; } = new List<float>();
        virtual public List<float> UVs { get; set; } = new List<float>();
        virtual public List<float> Normals { get; set; } = new List<float>();

        virtual public PrimitiveType primitiveType { get; set; } = PrimitiveType.Points;
        public int VAO { get; private set; } = -1;
        public int VerticesCount { get; private set; } = -1;

        public void Compile()
        {
            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            // VBO Vertex
            int VBOVertex = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOVertex);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Count * 4, Vertices.ToArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            // VBO Normal
            int VBONormal = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBONormal);
            GL.BufferData(BufferTarget.ArrayBuffer, Normals.Count * 4, Normals.ToArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(1);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            // VBO Texture
            int VBOTexture = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOTexture);
            GL.BufferData(BufferTarget.ArrayBuffer, UVs.Count * 4, UVs.ToArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(2);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.BindVertexArray(0);
            VerticesCount = Vertices.Count / 3;

            // Clean up
            Vertices = null;
            Normals = null;
            UVs = null;
        }
    }
    public class Object : ObjectInfos { }
}
