using OpenTK;
using OpenTK.Graphics;
using SMRenderer.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Drawing
{
    class Particle : SMItem
    {
        static Random random = new Random();
        public double currentLifeTime = 0;
        public Object Object = OM.OB["Quad"];
        public Vector2 Position = Vector2.Zero;
        public float Rotation = 0;
        public Vector2 Size = Vector2.Zero;
        public Vector2 Motion = new Vector2(10,10);
        public Vector2 Range = new Vector2(25, 25);
        public Texture texture = Texture.empty;
        public Color4 color = Color4.White;
        public int Count = 5;
        public TimeSpan LifeTime = TimeSpan.FromSeconds(2);
        public float rand = 0;

        public EffectBloomUsage bloom = EffectBloomUsage.None;

        public override void Draw()
        {
            Matrix4 modelMatrix = Matrix4.CreateScale(Size.X, Size.Y, 1) * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation)) * Matrix4.CreateTranslation(Position.X, Position.Y, 0);
            ParticleRenderer.program.Draw(Object, this, modelMatrix * GLWindow.Window.ViewProjection);
        }
        public override void Prepare(double RenderSec)
        {
            if (rand == 0) rand = (float)random.NextDouble()*2;
            currentLifeTime += RenderSec;
            if (LifeTime.TotalSeconds < currentLifeTime) SM.Remove(this);
        }
    }
}
