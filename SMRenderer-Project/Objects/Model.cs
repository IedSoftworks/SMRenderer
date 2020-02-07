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
        
        public static Model LoadFile(string file)
        {
            return LoadData(File.ReadAllText(file));
        }
        public static Model LoadData(string data)
        {
            Model model = new Model();
            ObjectInfos last = ObjectInfos.empty;
            string[] lines = data.Split('\n');

            foreach (string line in lines)
            {
                if (line == "") continue;

                string[] strParts = line.Split(' ');
                
                if (strParts[0] == "o")
                {
                    if (last != ObjectInfos.empty) last.Compile();

                    model.parts.Add(strParts[1], last = new ObjectInfos());
                    model.RenderOrder.Add(strParts[1]);
                }


                switch(strParts[0])
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

            Console.WriteLine("Model loaded");
            return model;
        }
    }
}
