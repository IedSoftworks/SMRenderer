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

                int VAO = GL.GenVertexArray();
                GL.BindVertexArray(VAO);

                // VBO Vertex
                int VBOVertex = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBOVertex);
                GL.BufferData(BufferTarget.ArrayBuffer, obj.Vertices.Length * 4, obj.Vertices, BufferUsageHint.StaticDraw);

                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.EnableVertexAttribArray(0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

                // VBO Normal
                int VBONormal = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBONormal);
                GL.BufferData(BufferTarget.ArrayBuffer, obj.Normals.Length * 4, obj.Normals, BufferUsageHint.StaticDraw);

                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.EnableVertexAttribArray(1);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

                // VBO Texture
                int VBOTexture = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBOTexture);
                GL.BufferData(BufferTarget.ArrayBuffer, obj.UVs.Length * 4, obj.UVs, BufferUsageHint.StaticDraw);

                GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 0, 0);
                GL.EnableVertexAttribArray(2);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

                GL.BindVertexArray(0);
                obj.VAO = VAO;
                OB.Add(type.Name, obj);
            };
        }
        public static Dictionary<string, Bitmap> insertForms = new Dictionary<string, Bitmap>();
        public static Dictionary<string, Form> Forms { get; private set; } = new Dictionary<string, Form>();
        public static void LoadForms()
        {
            Assembly ass = typeof(ObjectManager).Assembly;
            string name = ass.GetName().Name;

            Bitmap quad = new Bitmap(1, 1);
            quad.SetPixel(0, 0, System.Drawing.Color.White);

            insertForms.Add("Circle", new Bitmap(ass.GetManifestResourceStream(name + ".Form.Circle.png")));
            insertForms.Add("Quad", quad);

            foreach (KeyValuePair<string, Bitmap> map in insertForms)
            {
                Forms.Add(map.Key, new Form(map.Value) { AutoDispose = true });
            }
        }
    }
    public class OM : ObjectManager { }
    public class Object
    {
        virtual public float[] Vertices { get; set; }
        virtual public float[] UVs { get; set; }
        virtual public float[] Normals { get; set; }
        virtual public PrimitiveType primitiveType { get; set; } = PrimitiveType.Points;
        public int VAO { get; set; } = -1;
        public int VerticesCount { get { return Vertices.Length / 3; } }
    }
    public class Form : TextureItem { public Form(Bitmap bitmap) : base(bitmap) { } }
}
