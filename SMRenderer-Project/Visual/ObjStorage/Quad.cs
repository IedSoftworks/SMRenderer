﻿using OpenTK.Graphics.OpenGL4;
using SMRenderer.SMEditor.Attributes;
using System;
using System.Collections.Generic;

namespace SMRenderer.Objects
{
    [Serializable, NotInclude]
    public class Quad : Object
    {
        public Quad(string id) : base(id)
        {
            Vertices = new List<float>() {
                -.5f,-.5f,0f,
                -.5f,+.5f,0f,
                +.5f,+.5f,0f,
                +.5f,-.5f,0f
            };
            UVs = new List<float>() {
                0,0,
                0,1,
                1,1,
                1,0
            };
            Normals = new List<float>(){
                0,0,1,
                0,0,1,
                0,0,1,
                0,0,1,
            };
            primitiveType = PrimitiveType.Quads;
        }
    }
}