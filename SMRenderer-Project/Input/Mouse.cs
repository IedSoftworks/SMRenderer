using System.Drawing;
using OpenTK;
using OpenTK.Input;
using SMRenderer.Visual;

namespace SMRenderer.Input
{
    public class Mouse
    {
        public Point mousePosition;
        public GLWindow window;

        public Vector2 InWorld()
        {
            Vector2 campos = window.camera.CurrentLocation;
            float zoom = window.camera.zoomLevel;
            return new Vector2(campos.X + mousePosition.X * zoom, campos.Y + mousePosition.Y * zoom);
        }

        public void SaveMousePosition(object sender, MouseMoveEventArgs e)
        {
            mousePosition = e.Position;
        }
    }
}