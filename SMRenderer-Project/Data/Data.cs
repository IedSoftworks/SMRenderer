using System;
using SMRenderer.ManagerIntergration.Attributes;

namespace SMRenderer.Data
{
    /// <summary>
    ///     The data-master class
    /// </summary>
    [Serializable]
    [NotInclude]
    public abstract class Data
    {
        /// <summary>
        ///     Contains the ID
        /// </summary>
        public int ID = -1;

        /// <summary>
        ///     Contains the reference name
        /// </summary>
        [EditorField] public string refName;

        public string Category;

        /// <summary>
        ///     Returns true if IsLoaded
        /// </summary>
        public bool Loaded => IsLoaded;

        /// <summary>
        ///     Returns true if IsLoaded
        /// </summary>
        protected abstract bool IsLoaded { get; }

        /// <summary>
        ///     Load function
        /// </summary>
        public virtual void Load()
        {
        }
    }
}