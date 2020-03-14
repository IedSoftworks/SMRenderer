using System;
using SMRenderer.ManagerIntergration.Base;

namespace SMRenderer.ManagerIntergration.Attributes
{
    /// <summary>
    ///     This attribute allow the field to be used by SMManager
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class EditorField : Attribute
    {
        public PresetField[] fields;

        public EditorField() { }

        public EditorField(params PresetField[] fields)
        {
            this.fields = fields;
        }
    }
}