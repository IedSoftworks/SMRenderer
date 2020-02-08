using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Objects
{
    public class Model
    {
        public Dictionary<string, ObjectInfos> parts = new Dictionary<string, ObjectInfos>();
        public List<string> RenderOrder = new List<string>();
    }
}
