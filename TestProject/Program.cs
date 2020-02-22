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
        static FileStream scene1, data; 
        static Particles particles;
        static void Main(string[] args)
        {
            
            //Configure.UseScale = false;
            GraficalConfig.ClearColor = Color.LightGray;
            GraficalConfig.AllowBloom = true;

            string title = "Testing window";

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
                else
                {
                    if (!SM.Exists(particles)) SM.Add(particles);
                    particles.Generate();
                }
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
            DrawItem item = new DrawItem
            {
                Color = Color.Red,
                Size = new Vector2(50),
                Position = new Vector2(250,500),
                effectArgs = new VisualEffectArgs
                {
                    BloomUsage = EffectBloomUsage.ObjectColor,
                }
            };
            SM.Add(item);
            particles = new Particles
            {
                Direction = 5,
                Range = new SMRenderer.Math.Range(20),
                Speed = new SMRenderer.Math.Range(1,2),
                Size = new Vector2(5,10),
                Amount = 55,
                Color = Color.Blue,
                Origin = new Vector2(250,500),
                
                VisualEffectArgs = new VisualEffectArgs
                {
                    BloomUsage = EffectBloomUsage.ObjectColor,
                },
                Duration = TimeSpan.FromSeconds(5),
            };
            SM.Add(particles);
        }
        static void Test2()
        {

        }
    }
}
