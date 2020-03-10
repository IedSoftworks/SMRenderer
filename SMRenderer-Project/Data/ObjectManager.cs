using System.IO;
using System.Security.Cryptography;
using System.Text;
using SMRenderer.Visual.Objects;

namespace SMRenderer.Data
{
    public class ObjectManager
    {
        private static readonly MD5 md5 = MD5.Create();

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
                        last.Vertices.AddRange(new[]
                            {float.Parse(strParts[1]), float.Parse(strParts[2]), float.Parse(strParts[3])});
                        break;

                    case "vt":
                        last.UVs.AddRange(new[] {float.Parse(strParts[1]), float.Parse(strParts[2])});
                        break;

                    case "vn":
                        last.Normals.AddRange(new[]
                            {float.Parse(strParts[1]), float.Parse(strParts[2]), float.Parse(strParts[3])});
                        break;
                }
            }

            last.Compile();
        }

        public static string CreateID(string model)
        {
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(model));
            StringBuilder sb = new StringBuilder();

            foreach (byte b in data)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }
    }

    public class OM : ObjectManager
    {
    }
}