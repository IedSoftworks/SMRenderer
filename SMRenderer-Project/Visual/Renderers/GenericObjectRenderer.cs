using OpenTK;
using SMRenderer.Data;

namespace SMRenderer.Visual.Renderers
{
    /// <summary>
    /// Masterclass for object render programs
    /// </summary>
    public class GenericObjectRenderer : GenericRenderer
    {
        internal virtual void Draw(ObjectInfos obj, SMItem item, Matrix4 view, Matrix4 model)
        {
        }
    }
}