using System;

namespace SMRenderer.ManagerIntergration.Attributes
{
    /// <summary>
    /// Tells the SMManager to use the class as a VisualDataEntry for the specific type.
    /// </summary>
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

        /// <summary>
        /// The specific type
        /// </summary>
        public Type Type { get; }
    }
}