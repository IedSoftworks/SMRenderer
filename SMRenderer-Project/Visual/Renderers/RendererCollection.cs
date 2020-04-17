using System.Collections.Generic;

namespace SMRenderer.Visual.Renderers
{
    /// <summary>
    /// Collects render programs
    /// </summary>
    public class RendererCollection : List<GenericRenderer>
    {
        /// <summary>
        /// Returns a render program id, based on the typename
        /// </summary>
        /// <param name="typename">The name</param>
        /// <returns></returns>
        public int this[string typename]
        {
            get { return FindIndex(a => a.GetType().Name == typename); }
        }
    }
}