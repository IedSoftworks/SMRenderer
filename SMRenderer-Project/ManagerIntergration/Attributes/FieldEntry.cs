using System;

namespace SMRenderer.ManagerIntergration.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FieldEntry : Attribute
    {
        /// <summary>
        ///     The Type is the target for what Type of field it for.
        /// </summary>
        /// <param name="type"></param>
        public FieldEntry(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}