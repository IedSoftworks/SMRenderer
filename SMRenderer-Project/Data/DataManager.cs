using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SMRenderer.Data
{
    /// <summary>
    ///     Managed the data used by the SMRenderer.
    ///     <para>This class can be used to manage any data.</para>
    /// </summary>
    [Serializable]
    public class DataManager
    {


        /// <summary>
        ///     Current DataManager
        /// </summary>
        public static DataManager C;

        private static List<KeyValuePair<string, DataContainer>> _staticData = new List<KeyValuePair<string, DataContainer>>();

        /// <summary>
        ///     Contains all DataCategories
        /// </summary>
        private List<KeyValuePair<string, DataContainer>> _dataCategories =
            new List<KeyValuePair<string, DataContainer>>();

        /// <summary>
        ///     Represents the assembly that locked this 
        /// </summary>
        private Assembly _lockedAssembly;

        /// <summary>
        /// Returns true, if the current assembly is the assembly that locked this data manager
        /// </summary>
        public bool IsLockedAssembly => _lockedAssembly == Assembly.GetCallingAssembly();
        /// <summary>
        /// Returns true, if the data manager is locked.
        /// </summary>
        public bool IsLocked => _lockedAssembly != null;

        /// <summary>
        /// Locks the datamanager, if not.
        /// <para>It isn't a secure way to prevent the user to override it.</para>
        /// <para>If the user really wants to, he can unlock it easily.</para>
        /// <para>This is a problem, that I can't repair though the way the SMRenderer is published.</para>
        /// <para>Its only a way to prevent any other program to serialize it and do possible serious damage.</para>
        /// </summary>
        public void Lock()
        {
            if (IsLocked) 
                throw new Exception("ERROR AT LOCKING:\n  This dataManager has already been locked.");

            _lockedAssembly = Assembly.GetCallingAssembly();
        }
        /// <summary>
        /// Unlocks the data manager.
        /// </summary>
        public void Unlock()
        {
            if (_lockedAssembly == null)
            {
                Debug.WriteLine("Unlock terminated: No Lock");
                return;
            }

            if (_lockedAssembly != Assembly.GetCallingAssembly()) 
                throw new Exception($"ERROR AT UNLOCKING:\n  This dataManager doesn't have its lock from the selected assembly.\n\n  Selected Assembly: {Assembly.GetCallingAssembly().FullName}");

            _lockedAssembly = null;
        }

        /// <summary>
        ///     Returns the category based on the name
        /// </summary>
        /// <param name="name">The name of the category</param>
        /// <returns>A DataContainer</returns>
        public DataContainer this[string name]
        {
            get
            {
                bool dataCat = _dataCategories.Any(a => a.Key == name);
                bool staticCat = _staticData.Any(a => a.Key == name);

                if (dataCat && staticCat) throw new Exception($"Category '{name}' can be found in the static data and in the normal data. \nPlease use a second bool with 'true' to access the static data.\nOr use a unique category name.");
                if (!dataCat && !staticCat) throw new Exception($"Category '{name}' doesn't exist.");


                return (dataCat ?  _dataCategories : _staticData).First(a => a.Key == name).Value;
            }
        }
        /// <summary>
        /// Returns a category with specific selected if static or not.
        /// </summary>
        /// <param name="name">Category name</param>
        /// <param name="staticData">If it is true, its expected to be in a static category</param>
        /// <returns>The DataContainer for the selected name</returns>
        public DataContainer this[string name, bool staticData]
        {
            get
            {
                List<KeyValuePair<string, DataContainer>> list = staticData ? _staticData : _dataCategories;

                if (list.All(a => a.Key == name)) throw new Exception($"Category '{name}' doesn't exist.");
                return list.First(a => a.Key == name).Value;
            }
        }

        /// <summary>
        ///     Adds a category
        /// </summary>
        /// <param name="name">The name of the category</param>
        /// <param name="data">The DataContainer</param>
        /// <returns>The DataContainer, that you used as data-parameter</returns>
        public DataContainer AddCategory(string name, DataContainer data)
        {
            _dataCategories.Add(new KeyValuePair<string, DataContainer>(name, data));
            return data;
        }

        /// <summary>
        ///     Adds and creates a new category
        /// </summary>
        /// <param name="name">The name of the category</param>
        /// <returns>The newly created container</returns>
        public DataContainer AddCategory(string name)
        {
            return AddCategory(name, new DataContainer());
        }
        /// <summary>
        ///     Create a new static category.
        /// </summary>
        /// <param name="name">The name of the category</param>
        /// <returns>the newly created DataContainer</returns>
        public static DataContainer AddCategoryStatic(string name)
        {
            return AddCategoryStatic(name, new DataContainer());
        }
        /// <summary>
        ///     Add a category to the static data.
        /// </summary>
        /// <param name="name">Name of the category</param>
        /// <param name="data">Data to add</param>
        /// <returns>The inserted data.</returns>
        public static DataContainer AddCategoryStatic(string name, DataContainer data)
        {
            _staticData.Add(new KeyValuePair<string, DataContainer>(name, data));
            return data;
        }

        /// <summary>
        ///     Returns all names of the categories
        /// </summary>
        /// <returns></returns>
        public List<string> GetCategorys()
        {
            List<string> ret = new List<string>();
            foreach (KeyValuePair<string, DataContainer> pair in _dataCategories) ret.Add(pair.Key);
            return ret;
        }

        /// <summary>
        ///     Load all contained data.
        ///     <para>WARNING! This can cause some problem with the RAM!</para>
        /// </summary>
        public void LoadContents()
        {
            _dataCategories.ForEach(a => { a.Value.ForEach(b => b.Load()); });
        }
        /// <summary>
        /// Adds data to a category
        /// </summary>
        /// <param name="category">Category</param>
        /// <param name="data">Data</param>
        public static void Add(DataContainer category, Data data)
        {
            category.Add(data);
        }
        
        /// <summary>
        /// Adds data to the category
        /// </summary>
        /// <param name="category">category</param>
        /// <param name="isStatic">True, if it should be static</param>
        /// <param name="data">The data</param>
        public void Add(string category, bool isStatic, Data data) 
        {
            Add(this[category, isStatic], data);
        }
        /// <summary>
        /// Adds data to the category
        /// </summary>
        /// <param name="category">the category</param>
        /// <param name="data">the data</param>
        public void Add(string category, Data data)
        {
            Add(this[category], data);
        }
        /// <summary>
        /// Adds data to the datas category.
        /// </summary>
        /// <param name="data">The data.</param>
        public void Add(Data data)
        {
            Add(data.Category, data);
        }
        /// <summary>
        ///     Load a serialized DataManager
        /// </summary>
        /// <param name="stream">The stream that contains the data</param>
        /// <returns>The IsLoaded DataManager</returns>
        public static DataManager Load(Stream stream)
        {
            DataManager data = Deserialize(stream);

            return data;
        }

        /// <summary>
        ///     Saves the manager to the stream.
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
        ///     Deserialize a serialized DataManager
        /// </summary>
        /// <param name="stream">The stream that contains the data</param>
        /// <returns>The IsLoaded DataManager</returns>
        public static DataManager Deserialize(Stream stream)
        {
            DataManager data;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                data = (DataManager) formatter.Deserialize(stream);
            }
            catch (SerializationException e)
            {
                throw new Exception("DESERIALIZATION FAILED! Reason: " + e.Message);
            }

            return data;
        }

        /// <summary>
        ///     Creates a new DataManager for the SMRenderer.
        ///     <para>It set some categories needed by the SMRenderer.</para>
        /// </summary>
        /// <returns>The newly created DataManager</returns>
        public static DataManager Create()
        {
            bool cexist = C != null;
            DataManager data1 = null;
            if (cexist) data1 = C;

            DataManager data = new DataManager();
            data.AddCategory("Textures");
            data.AddCategory("Meshes");
            C = data;

            foreach (Type type in Assembly.GetAssembly(typeof(Object)).GetTypes()
                .Where(a => a.IsClass && !a.IsAbstract && a.IsSubclassOf(typeof(Object))))
            {
                Object obj = (Object) Activator.CreateInstance(type, type.Name);

                obj.Compile();
            }

            
            if (cexist) C = data1;

            return data;
        }
    }
}