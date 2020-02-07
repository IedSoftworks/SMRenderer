using SMRenderer.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMRenderer.Animations;
using SMRenderer.Drawing;
using OpenTK;
using OpenTK.Graphics;
using SMRenderer.Renderers;
using System.IO;

namespace SMRenderer
{
    class Program
    {
        static DrawItem item1; static DrawItem item2;
        static void Main(string[] args)
        {
            GraficalConfig.defaultTexture = new TextureItem(new Bitmap("draconier_logo.png"));
            //Configure.UseScale = false;
            GraficalConfig.ClearColor = Color.Gray;
            GraficalConfig.AllowBloom = true;

            string title = "Testing window";

            GLWindow window = new GLWindow(500, 500);
            window.UpdateFrame += (a, b) =>
            {
                window.Title = $"{title} | {window.camera.currentLocation.X}, {window.camera.currentLocation.Y} | {b.Time*1000}ms";
            };
            window.KeyDown += (a,b) =>
            {
            };
            window.Load += (ra, b) =>
            {
                item1 = new DrawItem
                {
                    Size = new Vector2(50),
                    Position = new Vector2(250),
                    Color = Color.Red,
                    Form = OM.Forms["Circle"]
                };
                SM.Add(item1);
                item2 = new DrawItem
                {
                    Size = new Vector2(100),
                    Position = new Vector2(400),
                    Color = Color.Blue,

                };
                SM.Add(item2);
            };
            window.Run();
        }
    }
}
