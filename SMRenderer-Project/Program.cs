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
        static DrawItem item1, item2;
        static FileStream scene1;
        static void Main(string[] args)
        {
            GraficalConfig.defaultTexture = new TextureItem(new Bitmap("draconier_logo.png"));
            //Configure.UseScale = false;
            GraficalConfig.ClearColor = Color.White;
            GraficalConfig.AllowBloom = true;

            string title = "Testing window";
            scene1 = new FileStream("scene1.scn", FileMode.OpenOrCreate);

            GLWindow window = new GLWindow(500, 500);
            window.UpdateFrame += (a, b) =>
            {
                window.Title = $"{title} | {window.camera.currentLocation.X}, {window.camera.currentLocation.Y} | {b.Time*1000}ms";
            };
            window.KeyDown += (a,b) =>
            {
                if (b.Key == OpenTK.Input.Key.S)
                {
                    Scene.current.Serialize(scene1);
                    scene1.Close();
                } else
                {
                    item1.Animations["move"].Start();
                }
            };
            window.Load += (ra, b) =>
            {
                Test1();
                Test2();
            };
            window.Run();
        }
        static void Test1()
        {
            OM.LoadModelFile("UziLong.obj");
            Scene.current.ambientLight = Color.Blue;
            item1 = new DrawItem
            {
                Size = new Vector2(50),
                Position = new Vector2(250),
                Color = Color.Red
            };
            SM.Add(item1);
            item2 = new DrawItem
            {
                Size = new Vector2(100),
                Position = new Vector2(400),
                Color = Color.Blue,

            };
            SM.Add(item2);
            item1.Animations = new AnimationCollection()
                {
                    { "move", new Value2Animation(TimeSpan.FromSeconds(2), new Vector2(250), new Vector2(25), SFPresets.DrawItem_Position) { Object = item1} }
                };

            Scene.current.lights.Add(new LightSource { Position = new Vector2(200) });

        }
        static void Test2()
        {
            Scene.current = Scene.Deserialize(scene1);
        }
    }
}
