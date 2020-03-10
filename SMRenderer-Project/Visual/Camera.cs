using System;
using OpenTK;
using SMRenderer.Visual.Drawing;

namespace SMRenderer.Visual
{
    [Serializable]
    public class Camera
    {
        public static Matrix4 staticView;

        private static Matrix4 _projection;
        public DrawItem anchor;
        public Vector2 position = Vector2.Zero;

        public bool useAnchor = false;
        public float zoomLevel = 1;
        public Matrix4 ViewProjection => CreateView() * _projection;
        public Vector2 CurrentLocation => useAnchor ? anchor.Position : position;

        public static void UpdateProjection(Vector2 size)
        {
            _projection = Matrix4.CreateOrthographicOffCenter(0, size.X, size.Y, 0, .1f, 100f);
            staticView = Matrix4.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0) *
                         Matrix4.CreateOrthographicOffCenter(0, size.X, size.Y, 0, .1f, 100f);
        }

        public Matrix4 CreateView()
        {
            Vector2 pos = useAnchor ? anchor.Position : position;
            return Matrix4.LookAt(pos.X, pos.Y, 1, pos.X, pos.Y, 0, 0, 1, 0);
        }
    }
}