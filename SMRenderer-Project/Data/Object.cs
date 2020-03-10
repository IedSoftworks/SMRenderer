using System;
using SMRenderer.ManagerIntergration.Attributes;

namespace SMRenderer.Data
{
    [Serializable]
    [NotInclude]
    public class Object : ObjectInfos
    {
        public Object(string refName) : base(refName)
        {
        }
    }
}