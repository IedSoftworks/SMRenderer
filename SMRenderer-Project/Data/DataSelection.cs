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
        public Data Data => DataManager.C[Category].Data(ID);

        /// <summary>
        /// Contains the dataID
        /// </summary>
        public int ID;
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
    }
}