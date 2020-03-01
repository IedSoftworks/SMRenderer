using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.ManagerIntergration.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FieldEntry : Attribute
    {
        public Type type { get; private set; }
        /// <summary>
        /// The type is the target for what type of field it for.
        /// </summary>
        /// <param name="type"></param>
        public FieldEntry(Type type)
        {
            this.type = type;
        }
    }
}
