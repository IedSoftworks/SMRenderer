using System;
using OpenTK;
using SMRenderer.Visual.Drawing;

namespace SMRenderer.Visual
{
    /// <summary>
    /// Controls the camera and the matrices
    /// </summary>
    [Serializable]
    public class Camera
    {
        /// <summary>
        /// Matrix for a static view.
        /// </summary>
        public static Matrix4 staticView;

        /// <summary>
        /// Current projection
        /// </summary>
        private static Matrix4 _projection;
        /// <summary>
        /// A anchor, that the camera needs to center
        /// </summary>
        public DrawItem anchor;
        /// <summary>
        /// Position, where the camera currently is.
        /// </summary>
        public Vector2 position = Vector2.Zero;

        /// <summary>
        /// If true, the camera use the anchor
        /// </summary>
        public bool useAnchor = false;
        /// <summary>
        /// Sets the zoomLevel
        /// <para>1 = normal; below 1 zoom in; above 1 zoom out; </para>
        /// </summary>
        public float zoomLevel = 1;
        /// <summary>
        /// Calculate and returns the ViewProjection
        /// </summary>
        public Matrix4 ViewProjection => CreateView() * _projection;
        /// <summary>
        /// Returns where the camera currently is.
        /// </summary>
        public Vector2 CurrentLocation => useAnchor ? anchor.Position : position;

        /// <summary>
        /// Updates the projection
        /// </summary>
        /// <param name="size">The size of the visible world</param>
        public static void UpdateProjection(Vector2 size)
        {
            _projection = Matrix4.CreateOrthographicOffCenter(0, size.X, size.Y, 0, .1f, 100f);
            staticView = Matrix4.LookAt(0, 0, 1, 0, 0, 0, 0, 1, 0) *
                         Matrix4.CreateOrthographicOffCenter(0, size.X, size.Y, 0, .1f, 100f);
        }

        /// <summary>
        /// Generate the view-matrix
        /// </summary>
        /// <returns></returns>
        public Matrix4 CreateView()
        {
            Vector2 pos = useAnchor ? anchor.Position : position;
            return Matrix4.LookAt(pos.X, pos.Y, 1, pos.X, pos.Y, 0, 0, 1, 0);
        }
    }
}