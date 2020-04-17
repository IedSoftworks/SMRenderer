using System.Drawing;
using OpenTK;
using OpenTK.Input;
using SMRenderer.Visual;

namespace SMRenderer.Input
{
    /// <summary>
    /// Contains mouse functions for the window.
    /// </summary>
    public class Mouse
    {
        /// <summary>
        /// Current mousePosition without any calculation
        /// </summary>
        public Point mousePosition;
        /// <summary>
        /// The window.
        /// </summary>
        public GLWindow window;

        /// <summary>
        /// Returns a Vector2 that is relative to the camera.
        /// </summary>
        /// <returns></returns>
        public Vector2 InWorld()
        {
            Vector2 campos = window.camera.CurrentLocation;
            float zoom = window.camera.zoomLevel;
            return new Vector2(campos.X + mousePosition.X * zoom, campos.Y + mousePosition.Y * zoom);
        }

        /// <summary>
        /// Event for saving the mouse position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SaveMousePosition(object sender, MouseMoveEventArgs e)
        {
            mousePosition = e.Position;
        }
    }
}