using System;

namespace SMRenderer.ManagerIntergration.Attributes
{
    /// <summary>
    ///     This attribute allow the field to be used by SMManager
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class EditorField : Attribute
    {
    }
}