using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;

namespace SMRenderer.Data
{
    /// <summary>
    ///     Contains all the data
    /// </summary>
    [Serializable]
    public class DataContainer : List<Data>
    {
        public DataManager ParentManager;

        /// <summary>
        ///     Saved the next ID
        /// </summary>
        public int index;

        /// <summary>
        ///     I am not sure, why I added this...
        /// </summary>
        public Action<DataContainer> Load;

        /// <summary>
        ///     Adds new data to the container
        /// </summary>
        /// <param name="data">The data</param>
        public new void Add(Data data)
        {
            if (this.Any(a => a.refName == data.refName))
                throw new Exception($"INSERT DATA FAILED: The referenceName '{data.refName}' exist already.");
            base.Add(data);
            data.ID = index++;
        }

        /// <summary>
        ///     Returns the ID of the object by the reference name.
        /// </summary>
        /// <param name="refName">The reference name</param>
        /// <returns>The ID</returns>
        public int ID(string refName)
        {
            return Find(a => a.refName == refName).ID;
        }

        /// <summary>
        ///     Returns the reference name based on the ID.
        /// </summary>
        /// <param name="ID">The ID</param>
        /// <returns>The reference name</returns>
        public string Reference(int ID)
        {
            return Find(a => a.ID == ID).refName;
        }

        /// <summary>
        ///     Returns the actual data based on the reference name.
        ///     <para>This function load the data, if needed</para>
        /// </summary>
        /// <param name="refName">The reference name</param>
        /// <returns>The data</returns>
        public Data Data(string refName)
        {
            return CheckLoad(Find(a => a.refName == refName));
        }

        /// <summary>
        ///     Returns the actual data based on the ID.
        ///     <para>This function load the data, if needed</para>
        /// </summary>
        /// <param name="ID">The ID</param>
        /// <returns>The data</returns>
        public Data Data(int ID)
        {
            return CheckLoad(Find(a => a.ID == ID));
        }

        /// <summary>
        ///     Checks if the data need to load and load it, if needed
        /// </summary>
        /// <param name="data">The data</param>
        /// <returns>The same data</returns>
        private Data CheckLoad(Data data)
        {
            if (!data.Loaded) data.Load();
            return data;
        }
    }
}