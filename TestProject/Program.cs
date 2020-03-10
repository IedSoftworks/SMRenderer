
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMRenderer.Animations;
using OpenTK;
using OpenTK.Graphics;
using System.IO;
using SMRenderer;
using SMRenderer.Data;
using SMRenderer.Visual;
using SMRenderer.Visual.Drawing;

namespace TestProject
{
    class Program
    {
        static FileStream scene1, data; 
        static Particles particles;
        static void Main(string[] args)
        {
            
            //Configure.UseScale = false;
            GraficalConfig.ClearColor = Color.White;
            GraficalConfig.AllowBloom = true;

            string title = "Testing window";

            data = new FileStream("data.scn", FileMode.OpenOrCreate);

            //GeneralConfig.UseDataManager = data;

            GLWindow window = new GLWindow(500, 500);
            window.UpdateFrame += (a, b) =>
            {
                window.Title = $"{title} | {window.camera.CurrentLocation.X}, {window.camera.CurrentLocation.Y} | {b.Time * 1000}ms";
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
            Scene.Current.ambientLight = Color.Blue;
            new TextureItem("Borderlands", new Bitmap("tex.jpg"));

            DrawItem item = new DrawItem
            {
                Color = Color.Red,
                Size = new Vector2(20),
                Position = new Vector2(250),
                Texture = new TextureHandler(new TextureItem("Daconier's Wings", new Bitmap("draconier_logo.png"))),
                effectArgs = new VisualEffectArgs
                {
                    BloomUsage = EffectBloomUsage.Render
                }
            };
            item.Texture.TexSize = new Vector2(32);
            SM.Add(item);
            particles = new Particles
            {
                Direction = 90,
                Range = new SMRenderer.Math.Range(1),
                Speed = new SMRenderer.Math.Range(1,2),
                Size = new Vector2(10,50),
                Amount = 5,
                Color = Color.Blue,
                Origin = new Vector2(250),
                VisualEffectArgs = new VisualEffectArgs
                {
                    //BloomUsage = EffectBloomUsage.ObjectColor
                },
                Duration = TimeSpan.FromSeconds(5),
            };
            SM.Add(particles);
            //DataManager.C.Serialize(data);
            data.Close();
        }
        static void Test2()
        {

        }
    }
}
