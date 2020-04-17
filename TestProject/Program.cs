
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using OpenTK;
using OpenTK.Input;
using SMRenderer;
using SMRenderer.Data;
using SMRenderer.Input;
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
            GraficalConfig.ClearColor = Color.Black;
            GraficalConfig.AllowBloom = true;

            string title = "Testing window";


            //GeneralConfig.UseDataManager = data;

            DataManager.AddCategoryStatic("sTextures");

            GLWindow window = new GLWindow(500, 500);
            window.RenderFrame += (a, b) =>
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
            Scene.Current.depthSettings = DepthSettings.Default;

            TextureItem tex = new TextureItem("tex1", "sTextures", new Bitmap("draconier_logo.png"));

            DrawItem i1 = new DrawItem {
                Position = new Vector3(100,250,-1),
                Object = new DrawObject
                {
                    Size = new Vector2(50),
                    Texture = new TextureHandler(tex)
                }
            };
            SM.Add(i1);
            DrawItem i2 = new DrawItem
            {
                Position = new Vector3(400, 250, 0),
                Object = new DrawObject
                {
                    Size = new Vector2(50)
                }
            };
            SM.Add(i2);

            DrawItem i3 = new DrawItem {
                Position = new Vector3(100, 400, 0),
                Object = new DrawObject
                {
                    Size = new Vector2(300, 50),
                },
                positionAnchor = "lu"
            };
            i3.Object.effectArgs.BloomUsage = EffectBloomUsage.Render;
            SM.Add(i3);
        }
        static void Test2()
        {

        }
    }
}
