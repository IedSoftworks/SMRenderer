
using System;
using System.Collections.Generic;
using System.Diagnostics;
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


            //GeneralConfig.UseDataManager = data;

            DataManager.AddCategoryStatic("sTextures");

            GLWindow window = new GLWindow(500, 500);
            window.UpdateFrame += (a, b) =>
            {
                window.Title = $"{title} | {window.camera.CurrentLocation.X}, {window.camera.CurrentLocation.Y} | {b.Time * 1000}ms";
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
            Scene.Current.ambientLight = Color.Black;
            //new TextureItem("Borderlands", new Bitmap("tex.jpg"));

            Scene.Current.lights.Add(new LightSource()
            {
                Color = Color.White,
                Position = new Vector2(0,250),
                Intensity = 1,
                Direction = new Vector2(1,0)
            });

            DrawItem item = new DrawItem
            {
                Object = new DrawObject() { 
                    Size = new Vector2(20),
                    effectArgs = new VisualEffectArgs
                    {
                        BloomUsage = EffectBloomUsage.ObjectColor,
                    }
                },
                Position = new Vector2(100),
                Rotation = 45
            };
            item.Animations = new AnimationCollection
            {
                {
                    "move1",
                    new Value2Animation(TimeSpan.FromSeconds(2), new Vector2(100), new Vector2(100,400), SFPresets.DrawItemPosition) {Object = item}
                },
                {
                    "move2",
                    new Value2Animation(TimeSpan.FromSeconds(2), new Vector2(100,400), new Vector2(400), SFPresets.DrawItemPosition) {Object = item}
                },
                {
                    "move3",
                    new Value2Animation(TimeSpan.FromSeconds(2), new Vector2(400), new Vector2(400,100), SFPresets.DrawItemPosition) {Object = item}
                },
                {
                    "move4",
                    new Value2Animation(TimeSpan.FromSeconds(2), new Vector2(400,100), new Vector2(100), SFPresets.DrawItemPosition) {Object = item}
                }
            };
            SM.Add(item);
            item.Animations["move1"].End += sender => { item.Animations["move2"].Start(); }; 
            item.Animations["move2"].End += sender => { item.Animations["move3"].Start(); }; 
            item.Animations["move3"].End += sender => { item.Animations["move4"].Start(); };
            item.Animations["move1"].Start();

            particles = new Particles
            {
                Object =
                {
                    effectArgs = new VisualEffectArgs
                    {
                        BloomUsage = EffectBloomUsage.Render
                    },
                    Color = Color.Red,
                    Size = new Vector2(20),
                },

                RenderOrder = -1,
                Direction = -45,
                Range = new SMRenderer.Math.Range(5),
                Speed = new SMRenderer.Math.Range(1,3),
                Amount = 50,
                Origin = new Vector2(500),
                
                Duration = TimeSpan.FromSeconds(20),
            };
            SM.Add(particles);

            Particles particles2 = new Particles
            {
                Object =
                {
                    effectArgs = new VisualEffectArgs
                    {
                        BloomUsage = EffectBloomUsage.Render
                    },
                    Color = Color4.Blue,
                    Size = new Vector2(20)
                },
                RenderOrder = -2,
                Direction = 45,
                Amount = 50,
                Origin = new Vector2(0,500),
                Range = new SMRenderer.Math.Range(5),
                Speed = new SMRenderer.Math.Range(1, 3),
                
                Duration = TimeSpan.FromSeconds(20),
            };
            SM.Add(particles2);
        }
        static void Test2()
        {

        }
    }
}
