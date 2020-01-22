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
        static Particle item; static DrawItem item2;
        static void Main(string[] args)
        {
            GraficalConfig.defaultTexture = new TextureItem(new Bitmap("draconier_logo.png"));
            //Configure.UseScale = false;
            GraficalConfig.ClearColor = Color.Gray;
            GraficalConfig.AllowBloom = true;

            GLWindow window = new GLWindow(500, 500);
            window.KeyDown += (a,b) =>
            {
            };
            window.Load += (ra, b) =>
            {
                item2 = new DrawItem
                {
                    Position = new Vector2(250, 250),
                    Color = Color.Red,
                    Size = new Vector2(50,100)
                };
                SM.Add(item2, SMLayer.Skyplane);

            };
            window.Run();
        }
    }
}
