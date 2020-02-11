using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Objects
{
    [Serializable]
    public class Model : Data
    {
        public List<string> RenderOrder = new List<string>();
        public List<ObjectInfos> objects = new List<ObjectInfos>();
        public Model(string id) : base(id, "Meshes") { }
    }
}
