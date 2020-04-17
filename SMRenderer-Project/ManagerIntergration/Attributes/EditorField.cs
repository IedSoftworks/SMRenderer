using System;
using SMRenderer.ManagerIntergration.Base;

namespace SMRenderer.ManagerIntergration.Attributes
{
    /// <summary>
    ///     This attribute allow the field to be used by SMManager
    /// <para>If set with a PresetField, it uses the PresetField</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class EditorField : Attribute
    {
        /// <summary>
        /// Says if what field should be used.
        /// </summary>
        public PresetField field;
        /// <summary>
        /// Parameter-less constructor for tagging
        /// </summary>
        public EditorField() { }

        /// <summary>
        /// Constructor for setting a PresetField
        /// </summary>
        /// <param name="field">The wished PresetField</param>
        public EditorField(PresetField field)
        {
            this.field = field;
        }
    }
}