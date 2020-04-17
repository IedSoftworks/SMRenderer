namespace SMRenderer.Data
{
    /// <summary>
    /// Contains a selection to data.
    /// </summary>
    public class DataSelection
    {

        /// <summary>
        /// Returns the data selection
        /// </summary>
        public Data Data => GetData();

        /// <summary>
        /// Contains the dataID
        /// </summary>
        public int ID = -1;
        /// <summary>
        /// Contains the category for the data.
        /// </summary>
        public string Category;
        
        /// <summary>
        /// Parameterless constructor for automatic instance creation
        /// </summary>
        public DataSelection() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="category">Category</param>
        public DataSelection(int id, string category)
        {
            ID = id;
            Category = category;
        }
        public DataSelection(string reference, string category)
        {
            ID = DM.C[category].ID(reference);
            Category = category;
        }

        /// <summary>
        /// Returns the data, that is selected
        /// </summary>
        /// <returns></returns>
        public virtual Data GetData()
        {
            return DataManager.C[Category].Data(ID);
        }
    }
}