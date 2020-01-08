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
            Configure.defaultTexture = new TextureItem(new Bitmap("draconier_logo.png"));
            Configure.UseGameController = true;
            //Configure.UseScale = false;

            GLWindow window = new GLWindow(500, 500);
            window.controller.Connect += (a) => Console.WriteLine(a + " is connected!");
            window.controller.Disconnect += (a) => Console.WriteLine(a + " is disconnected!");
            window.KeyDown += (a,b) =>
            {
            };
            window.Load += (ra, b) =>
            {
                item2 = new DrawItem
                {
                    Position = new Vector2(250, 250),
                    Color = Color.Red,
                    Size = new Vector2(5,10) * new Vector2(50),
                    effectArgs = new VisualEffectArgs()
                    {
                        BorderUsage = EffectBorderUsage.QuadEdgeBorder,
                        BorderWidth = 5,
                        
                    }
                };
                SM.Add(item2);

            };
            window.Run();
        }
    }
}
