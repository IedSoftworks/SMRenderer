using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;

namespace SMRenderer
{
    public class Mouse
    {
        public GLWindow window;
        public Point mousePosition;
        public Vector2 InWorld()
        {
            Vector2 campos = window.camera.RealCenter;
            float zoom = window.camera.zoomfactor;
            return new Vector2(campos.X + mousePosition.X * zoom, campos.Y + mousePosition.Y * zoom);
        }
        public void SaveMousePosition(object sender, MouseMoveEventArgs e)
        {
            mousePosition = e.Position;
        }
    }
}
