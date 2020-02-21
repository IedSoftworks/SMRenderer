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
using SMRenderer;

namespace TestProject
{
    class Program
    {
        static DrawItem item1, item2;
        static FileStream scene1, data;
        static void Main(string[] args)
        {
            //Configure.UseScale = false;
            GraficalConfig.ClearColor = Color.White;
            GraficalConfig.AllowBloom = true;

            string title = "Testing window";
            scene1 = new FileStream("scene1.scn", FileMode.OpenOrCreate);
            data = new FileStream("data.scn", FileMode.OpenOrCreate);

            //GeneralConfig.UseDataManager = data;

            GLWindow window = new GLWindow(500, 500);
            window.UpdateFrame += (a, b) =>
            {
                window.Title = $"{title} | {window.camera.currentLocation.X}, {window.camera.currentLocation.Y} | {b.Time * 1000}ms";
            };
            window.KeyDown += (a, b) =>
            {
                if (b.Key == OpenTK.Input.Key.P)
                    Console.WriteLine("p");
            };
            window.Load += (ra, b) =>
            {
                Test1();
                //Test2();
            };
            window.Run();
        }
        static void Test1()
        {
            Particles particles = new Particles
            {
                Direction = 0,
                Range = 10,
                Size = new Vector2(10),
                Speed = 1,
                Amount = 2,
                Color = Color.Blue,
                Origin = new Vector2(250)
            };
            SM.Add(particles);
        }
        static void Test2()
        {
            Scene.current = Scene.Deserialize(scene1);
        }
    }
}
