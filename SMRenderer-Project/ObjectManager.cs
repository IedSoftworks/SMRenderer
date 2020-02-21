using OpenTK.Graphics.OpenGL4;
using SMRenderer.Objects;
using SMRenderer.Renderers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace SMRenderer
{
    public class ObjectManager
    {
        public static void LoadModelFile(string id, string file)
        {
            LoadModelData(id, File.ReadAllText(file));
        }
        public static void LoadModelData(string data)
        {
            LoadModelData(CreateID(data), data);
        }
        public static void LoadModelData(string id, string data)
        {
            Model model = new Model(id);
            ObjectInfos last = ObjectInfos.empty;
            string[] lines = data.Split('\n');

            foreach (string line in lines)
            {
                if (line == "") continue;

                string[] strParts = line.Split(' ');

                if (strParts[0] == "o")
                {
                    if (last != ObjectInfos.empty) last.Compile();

                    model.objects.Add(last = new ObjectInfos(strParts[1], false));
                    model.RenderOrder.Add(strParts[1]);
                }


                switch (strParts[0])
                {
                    case "o":
                        break;

                    case "v":
                        last.Vertices.AddRange(new float[] { float.Parse(strParts[1]), float.Parse(strParts[2]), float.Parse(strParts[3]) });
                        break;

                    case "vt":
                        last.UVs.AddRange(new float[] { float.Parse(strParts[1]), float.Parse(strParts[2]) });
                        break;

                    case "vn":
                        last.Normals.AddRange(new float[] { float.Parse(strParts[1]), float.Parse(strParts[2]), float.Parse(strParts[3]) });
                        break;
                }
            }
            last.Compile();
        }

        static MD5 md5 = MD5.Create();
        public static string CreateID(string model)
        {
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(model));
            StringBuilder sb = new StringBuilder();

            foreach (byte b in data)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }
    }
    public class OM : ObjectManager { }

    [Serializable]
    public class ObjectInfos : Data
    {
        public static ObjectInfos empty = new ObjectInfos("emptyObj", false);
        public ObjectInfos(string refname, bool add = true) : base(refname, "Meshes", add) { }

        virtual public List<float> Vertices { get; set; } = new List<float>();
        virtual public List<float> UVs { get; set; } = new List<float>();
        virtual public List<float> Normals { get; set; } = new List<float>();
        virtual public PrimitiveType primitiveType { get; set; } = PrimitiveType.Points;

        protected override bool loaded => VAO != -1;

        [NonSerialized] private int VAO = -1;
        [NonSerialized] private int VerticesCount = -1;

        public int GetVAO() { return VAO; }
        public int GetVerticesCount() { return VerticesCount; }

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
        }
        public override void Load()
        {
            Compile();
        }
    }
    [Serializable]
    public class Object : ObjectInfos { public Object(string refName) : base(refName) { } }
}
