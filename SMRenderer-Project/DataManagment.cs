using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer
{
    /// <summary>
    /// Faster way to DataManager
    /// </summary>
    [Serializable]
    public class DM : DataManager{ }
    [Serializable]
    public class DataManager
    {
        /// <summary>
        /// Current DataManager
        /// </summary>
        static public DataManager C;
        private List<KeyValuePair<string, DataContainer>> DataCategorys = new List<KeyValuePair<string, DataContainer>>();
        public DataContainer this[string name]
        {
            get
            {
                if (!DataCategorys.Any(a => a.Key == name)) throw new Exception($"Category '{name}' doesn't exist.");
                return DataCategorys.First(a => a.Key == name).Value;
            }
        }
        public DataContainer AddCategory(string name, DataContainer data)
        {
            DataCategorys.Add(new KeyValuePair<string, DataContainer>(name, data));
            return data;
        }
        public DataContainer AddCategory(string name)
        {
            return AddCategory(name, new DataContainer());
        }
        public List<string> GetCategorys()
        {
            List<string> ret = new List<string>();
            foreach (KeyValuePair<string, DataContainer> pair in DataCategorys) ret.Add(pair.Key);
            return ret;
        }
        public void Load()
        {
            DataCategorys.ForEach(a =>
            {
                a.Value.ForEach(b => b.Load());
            });
        }
        public static DataManager Load(Stream stream)
        {
            DataManager data = Deserialize(stream);
            data.Load();
            return data;
        }
        public void Serialize(Stream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                formatter.Serialize(stream, this);
            }
            catch (Exception e)
            {
                throw new Exception("SERIALIZATION FAILED! Reason: " + e.Message);
            }
        }
        public static DataManager Deserialize(Stream stream)
        {
            DataManager data = new DataManager();
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                data = (DataManager)formatter.Deserialize(stream);
            }
            catch (SerializationException e)
            {
                throw new Exception("DESERIALIZATION FAILED! Reason: " + e.Message);
            }
            return data;
        }
        public static DataManager Create()
        {
            bool Cexist = C != null;
            DataManager data1 = null;
            if (Cexist) data1 = C;

            DataManager data = new DataManager();
            data.AddCategory("Textures");
            data.AddCategory("Meshes");
            C = data;

            foreach (Type type in Assembly.GetAssembly(typeof(Object)).GetTypes().Where(a => a.IsClass && !a.IsAbstract && a.IsSubclassOf(typeof(Object))))
            {
                Object obj = (Object)Activator.CreateInstance(type, type.Name);

                obj.Compile();
            };
            if (Cexist) C = data1;

            return data;
        }
    }
    [Serializable]
    public class DataContainer : List<Data>
    {
        public Action<DataContainer> Load;
        static int index = -1;
        new public void Add(Data data)
        {
            if (this.Any(a => a.refName == data.refName))
                throw new Exception($"INSERT DATA FAILED: The referenceName '{data.refName}' exist already.");
            base.Add(data);
            data.ID = index++;
        }
        public int ID(string refName) => Find(a => a.refName == refName).ID;
        public string Reference(int ID) => Find(a => a.ID == ID).refName;
        public Data Data(string refName) => Find(a => a.refName == refName); 
        public Data Data(int ID) => Find(a => a.ID == ID); 

        new public Data this[int ID] => Find(a => a.ID == ID);
        

    }
    [Serializable]
    public class Data
    {
        public string refName;
        public int ID = -1;
        public Data(string name, string category, bool Add = true)
        {
            refName = name;
            if (Add && DM.C != null) DataManager.C[category].Add(this);
        }
        virtual public void Load() { }
    }
}
