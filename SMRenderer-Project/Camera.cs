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
        private Matrix4 ortho;
        private Vector2 _centeredObject { get
            {
                if (anchor != null)
                    return new Vector2(anchor.Position.X, anchor.Position.Y);
                else
                    return new Vector2(window.pxSize.X / 2, window.pxSize.Y / 2);
            } }
        private float _zoom = 1f;
        public GLWindow window = null;
        public DrawItem anchor = null;

        public Vector2 RealCenter { get {
                Vector2 center = _centeredObject;
                return new Vector2(center.X * _zoom, center.Y * _zoom);
            } }
        public float zoomfactor { get { return _zoom; }  set { _zoom = value; CalcOrtho(); } }

        public void CalcOrtho()
        {
            if (_zoom < .001) _zoom = .001f;
            ortho = Matrix4.CreateOrthographicOffCenter(0, window.pxSize.X, window.pxSize.Y, 0, 0.1f, 100f) * Matrix4.CreateScale(_zoom, _zoom, 1);
        }
        /// <summary>
        /// Calculate the viewMatrix
        /// </summary>
        public Matrix4 viewMatrix { get
            {
                Vector2 center = _centeredObject;
                return Matrix4.LookAt(center.X, center.Y, 1, center.X, center.Y, 0, 0, 1, 0) * Matrix4.CreateScale(_zoom, _zoom, 1);
            }
        }
        /// <summary>
        /// Gets the currentCamera Location
        /// <para>z = ZoomFactor</para>
        /// </summary>
        /// <returns></returns>
        public Vector3 CameraLocation()
        {
            if (anchor != null) return new Vector3(_centeredObject.X, _centeredObject.Y, zoomfactor);
            else return new Vector3(window.pxSize.X / 2, (window.pxSize.Y / 2), zoomfactor);
        }
        public Matrix4 Calculate()
        {
            return viewMatrix * ortho;
        }
    }
}
