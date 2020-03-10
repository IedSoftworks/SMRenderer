using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Data
{
    [Serializable]
    public class ObjectInfos : Data
    {
        public static ObjectInfos empty = new ObjectInfos("emptyObj", false);

        [NonSerialized] private int _vao = -1;
        [NonSerialized] private int _verticesCount = -1;

        public ObjectInfos()
        {
        }

        public ObjectInfos(string refname, bool add = true)
        {
            refName = refname;
            if (add) DataManager.C.Add("Meshes", this);
        }

        public virtual List<float> Vertices { get; set; } = new List<float>();
        public virtual List<float> UVs { get; set; } = new List<float>();
        public virtual List<float> Normals { get; set; } = new List<float>();
        public virtual PrimitiveType PrimitiveType { get; set; } = PrimitiveType.Points;

        protected override bool IsLoaded => _vao != -1;

        public int GetVAO()
        {
            return _vao;
        }

        public int GetVerticesCount()
        {
            return _verticesCount;
        }

        public void Compile()
        {
            _vao = GL.GenVertexArray();
            GL.BindVertexArray(_vao);

            // VBO Vertex
            int vboVertex = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVertex);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Count * 4, Vertices.ToArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            // VBO Normal
            int vboNormal = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboNormal);
            GL.BufferData(BufferTarget.ArrayBuffer, Normals.Count * 4, Normals.ToArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(1);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            // VBO ID
            int vboTexture = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboTexture);
            GL.BufferData(BufferTarget.ArrayBuffer, UVs.Count * 4, UVs.ToArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(2);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.BindVertexArray(0);
            _verticesCount = Vertices.Count / 3;
        }

        public override void Load()
        {
            Compile();
        }
    }
}