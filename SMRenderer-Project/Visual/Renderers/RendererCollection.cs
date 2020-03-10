using System.Collections.Generic;

namespace SMRenderer.Visual.Renderers
{
    public class RendererCollection : List<GenericRenderer>
    {
        public int this[string typename]
        {
            get { return FindIndex(a => a.GetType().Name == typename); }
        }
    }
}