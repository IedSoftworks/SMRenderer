using System;
using SMRenderer.ManagerIntergration.Attributes;

namespace SMRenderer.Data
{
    /// <summary>
    /// Dummy class, for assembly objects.
    /// </summary>
    [Serializable]
    [NotInclude]
    public class Object : ObjectInfos
    {
        /// <inheritdoc />
        public Object(string refName) : base(refName)
        {
        }
    }
}