using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.ManagerIntergration.Attributes;
using Object = SMRenderer.Data.Object;

namespace SMRenderer.Visual.ObjStorage
{
    [Serializable]
    [NotInclude]
    public class Quad : Object
    {
        public Quad(string id) : base(id)
        {
            Vertices = new List<float>
            {
                -.5f, -.5f, 0f,
                -.5f, +.5f, 0f,
                +.5f, +.5f, 0f,
                +.5f, -.5f, 0f
            };
            UVs = new List<float>
            {
                0, 0,
                0, 1,
                1, 1,
                1, 0
            };
            Normals = new List<float>
            {
                0, 0, 1,
                0, 0, 1,
                0, 0, 1,
                0, 0, 1
            };
            PrimitiveType = PrimitiveType.Quads;
        }
    }
}