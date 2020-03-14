using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Data
{
    /// <summary>
    /// Contains object information like Vertices, UV coords, normals, etc...
    /// </summary>
    [Serializable]
    public class ObjectInfos : Data
    {
        /// <summary>
        /// Contains a empty object.
        /// </summary>
        public static ObjectInfos empty = new ObjectInfos("emptyObj", false);

        /// <summary>
        /// The Vertex Array Object
        /// </summary>
        [NonSerialized] private int _vao = -1;
        /// <summary>
        /// Vertex count
        /// </summary>
        [NonSerialized] private int _verticesCount = -1;

        /// <summary>
        /// Parameterless constructor for automatic instance creation
        /// </summary>
        public ObjectInfos()
        {
        }

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="refname"></param>
        /// <param name="add"></param>
        public ObjectInfos(string refname, bool add = true)
        {
            refName = refname;
            if (add) DataManager.C.Add("Meshes", this);
        }

        /// <summary>
        /// Contains all Vertices for the object.
        /// </summary>
        public List<float> Vertices = new List<float>();
        /// <summary>
        /// Contains all UV coordinates for the object.
        /// </summary>
        public List<float> UVs = new List<float>();
        /// <summary>
        /// Contains all normals for the object.
        /// </summary>
        public List<float> Normals = new List<float>();
        /// <summary>
        /// Contains the primitiveType for the object
        /// </summary>
        public PrimitiveType PrimitiveType = PrimitiveType.Points;

        /// <summary>
        /// Returns true if the VAO is not -1
        /// </summary>
        protected override bool IsLoaded => _vao != -1;

        /// <summary>
        /// Returns the VAO
        /// </summary>
        /// <returns></returns>
        public int GetVAO()
        {
            return _vao;
        }

        /// <summary>
        /// Returns the VerticesCount;
        /// </summary>
        /// <returns></returns>
        public int GetVerticesCount()
        {
            return _verticesCount;
        }

        /// <summary>
        /// Compiles the object and load it properly.
        /// </summary>
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

        /// <inheritdoc />
        public override void Load()
        {
            Compile();
        }
    }
}