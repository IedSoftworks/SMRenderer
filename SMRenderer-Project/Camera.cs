using OpenTK;
using SMRenderer.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer
{
    public class Camera
    {
        public static Matrix4 staticView;

        private Matrix4 projection;

        public bool useAnchor = false;
        public Vector2 position = Vector2.Zero;
        public DrawItem anchor;
        public float zoomLevel = 1;
 
        public void CreateProjection(Vector2 size)
        {
            projection = Matrix4.CreateOrthographicOffCenter(0, size.X, size.Y, 0, .1f, 100f);
            staticView = Matrix4.LookAt(0,0,1,0,0,0,0,1,0) * Matrix4.CreateOrthographicOffCenter(0, size.X, size.Y, 0, .1f, 100f);
        }
        public Matrix4 CreateView()
        {
            Vector2 pos = useAnchor ? anchor.Position : position;
            return Matrix4.LookAt(pos.X, pos.Y, 1, pos.X, pos.Y, 0, 0, 1, 0);
        }
        public Matrix4 viewProjection { get { return CreateView() * projection; } }
        public Vector2 currentLocation { get { return useAnchor ? anchor.Position : position; } }
    }
}
