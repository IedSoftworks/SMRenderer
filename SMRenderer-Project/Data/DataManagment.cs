using SMRenderer.ManagerIntergration.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SMRenderer
{
    /// <summary>
    /// Faster way to DataManager
    /// </summary>
    [Serializable]
    public class DM : DataManager{ }
    /// <summary>
    /// Managed the data used by the SMRenderer.
    /// <para>This class can be used to manage any data.</para>
    /// </summary>
    [Serializable]
    public class DataManager
    {
        /// <summary>
        /// Current DataManager
        /// </summary>
        static public DataManager C;
        /// <summary>
        /// Contains all DataCategorys
        /// </summary>
        private List<KeyValuePair<string, DataContainer>> DataCategorys = new List<KeyValuePair<string, DataContainer>>();
        /// <summary>
        /// Returns the category based on the name
        /// </summary>
        /// <param name="name">The name of the category</param>
        /// <returns>A DataContainer</returns>
        public DataContainer this[string name]
        {
            get
            {
                if (!DataCategorys.Any(a => a.Key == name)) throw new Exception($"Category '{name}' doesn't exist.");
                return DataCategorys.First(a => a.Key == name).Value;
            }
        }
        /// <summary>
        /// Adds a category
        /// </summary>
        /// <param name="name">The name of the category</param>
        /// <param name="data">The DataContainer</param>
        /// <returns>The DataContainer, that you used as data-parameter</returns>
        public DataContainer AddCategory(string name, DataContainer data)
        {
            DataCategorys.Add(new KeyValuePair<string, DataContainer>(name, data));
            return data;
        }
        /// <summary>
        /// Adds and creates a new category
        /// </summary>
        /// <param name="name">The name of the category</param>
        /// <returns>The newly created container</returns>
        public DataContainer AddCategory(string name)
        {
            return AddCategory(name, new DataContainer());
        }
        /// <summary>
        /// Returns all names of the categorys
        /// </summary>
        /// <returns></returns>
        public List<string> GetCategorys()
        {
            List<string> ret = new List<string>();
            foreach (KeyValuePair<string, DataContainer> pair in DataCategorys) ret.Add(pair.Key);
            return ret;
        }
        /// <summary>
        /// Load all contained data.
        /// <para>WARNING! This can cause some problem with the RAM!</para>
        /// </summary>
        public void LoadContents()
        {
            DataCategorys.ForEach(a =>
            {
                a.Value.ForEach(b => b.Load());
            });
        }
        /// <summary>
        /// Load a serialized DataManager
        /// </summary>
        /// <param name="stream">The stream that contains the data</param>
        /// <returns>The loaded DataManager</returns>
        public static DataManager Load(Stream stream)
        {
            DataManager data = Deserialize(stream);
            
            return data;
        }
        /// <summary>
        /// Saves the manager to the stream.
        /// </summary>
        /// <param name="stream"></param>
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
        /// <summary>
        /// Deserialize a serialized DataManager
        /// </summary>
        /// <param name="stream">The stream that contains the data</param>
        /// <returns>The loaded DataManager</returns>
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
        /// <summary>
        /// Creates a new DataManager for the SMRenderer.
        /// <para>It set some categorys needed by the SMRenderer.</para>
        /// </summary>
        /// <returns>The newly created DataManager</returns>
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
    /// <summary>
    /// Contains all the data
    /// </summary>
    [Serializable]
    public class DataContainer : List<Data>
    {
        /// <summary>
        /// I am not sure, why I added this...
        /// </summary>
        public Action<DataContainer> Load;
        /// <summary>
        /// Saved the next ID
        /// </summary>
        public int index = 0;
        /// <summary>
        /// Adds new data to the container
        /// </summary>
        /// <param name="data">The data</param>
        new public void Add(Data data)
        {
            if (this.Any(a => a.refName == data.refName))
                throw new Exception($"INSERT DATA FAILED: The referenceName '{data.refName}' exist already.");
            base.Add(data);
            data.ID = index++;
        }
        /// <summary>
        /// Returns the ID of the object by the reference name.
        /// </summary>
        /// <param name="refName">The reference name</param>
        /// <returns>The ID</returns>
        public int ID(string refName) => Find(a => a.refName == refName).ID;
        /// <summary>
        /// Returns the reference name based on the ID.
        /// </summary>
        /// <param name="ID">The ID</param>
        /// <returns>The reference name</returns>
        public string Reference(int ID) => Find(a => a.ID == ID).refName;
        /// <summary>
        /// Returns the actual data based on the reference name.
        /// <para>This function load the data, if needed</para>
        /// </summary>
        /// <param name="refName">The reference name</param>
        /// <returns>The data</returns>
        public Data Data(string refName) {
            return CheckLoad(Find(a => a.refName == refName));
        }
        /// <summary>
        /// Returns the actual data based on the ID.
        /// <para>This function load the data, if needed</para>
        /// </summary>
        /// <param name="ID">The ID</param>
        /// <returns>The data</returns>
        public Data Data(int ID)
        {
            return CheckLoad(Find(a => a.ID == ID));
        }
        /// <summary>
        /// Checks if the data need to load and load it, if needed
        /// </summary>
        /// <param name="data">The data</param>
        /// <returns>The same data</returns>
        private Data CheckLoad(Data data)
        {
            if (!data.Loaded) data.Load();
            return data;
        }
    }
    /// <summary>
    /// The data-master class
    /// </summary>
    [Serializable, NotInclude]
    public abstract class Data
    {
        /// <summary>
        /// Contains the reference name
        /// </summary>
        public string refName;
        /// <summary>
        /// Contains the ID
        /// </summary>
        public int ID = -1;
        /// <summary>
        /// Returns true if loaded
        /// </summary>
        public bool Loaded { get => loaded; }
        /// <summary>
        /// Returns true if loaded
        /// </summary>
        abstract protected bool loaded { get; }
        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="name">The reference name</param>
        /// <param name="category">The category</param>
        /// <param name="Add">If true, the data will be added automaticly</param>
        public Data(string name, string category, bool Add = true)
        {
            refName = name;
            if (Add && DM.C != null) DataManager.C[category].Add(this);
        }
        /// <summary>
        /// Loadfunction
        /// </summary>
        virtual public void Load() { }
    }
}
