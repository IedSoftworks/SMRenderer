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
        static DrawItem item;
        static void Main(string[] args)
        {
            Configure.defaultTexture = new TextureItem(new Bitmap("draconier_logo.png"));

            GLWindow window = new GLWindow(500, 500);
            window.WindowState = WindowState.Normal;
            window.KeyDown += (a,b) =>
            {
                item.Animations["Rot1"].Start();
                item.Animations["Motion"].Start();
            };
            window.Load += (a, b) =>
            {
                item = new DrawItem
                {
                    obj = ObjectManager.OB["Quad"],
                    Position = new Vector2(50, 50),
                    Rotation = 0,
                    Size = new Vector2(200, 200),
                    ZIndex = 5,
                    Texture = Configure.defaultTexture.texture,
                    positionAnchor = "lu",
                    EffectArgs = new Dictionary<string, object>()
                    {
                        { "BloomUsage", EffectBloomUsage.None },
                        {"BorderUsage", EffectBorderUsage.QuadEdgeBorder }
                    }
                };
                item.Animations = new Dictionary<string, Animation>
                {
                    {"Rot1", new Value1Animation(TimeSpan.FromSeconds(2), 0,360, SFPresets.Rotation(item)) },
                    {"Motion", new Value2Animation(TimeSpan.FromSeconds(2), new Vector2(500), new Vector2(200), SFPresets.Position(item)) }
                };
                //item.Animations["Rot1"].End += (v) => item.Animations["Rot1"].Start(); 
                SM.Add(item);
                //window.camera.anchor = item;

                Region region = new Region
                {
                    anchor = item
                };
                DrawItem item1 = new DrawItem
                {
                    obj = ObjectManager.OB["Quad"],
                    Color = Color.Blue,
                    Position = new Vector2(0, 50),
                    Region = region,
                    EffectArgs = new Dictionary<string, object>()
                    {
                        {"UseColorAsBloom", true }
                    }
                };
            };
            window.Run();
        }
    }
}
