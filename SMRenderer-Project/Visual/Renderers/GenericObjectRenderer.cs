using OpenTK;
using SMRenderer.Data;

namespace SMRenderer.Visual.Renderers
{
    public class GenericObjectRenderer : GenericRenderer
    {
        internal virtual void Draw(ObjectInfos obj, SMItem item, Matrix4 view, Matrix4 model)
        {
        }
    }
}