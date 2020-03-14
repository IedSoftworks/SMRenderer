using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using SMRenderer.Data;
using SMRenderer.Helper;
using SMRenderer.Math;
using SMRenderer.Visual.Renderers;

namespace SMRenderer.Visual.Drawing
{
    [Serializable]
    public class Particles : SMItem
    {
        private const int MaxAmount = 2048;
        public int AdditionalRotation = 0;
        public int Amount = 1;
        public Color4 Color = Color4.White;
        public float Direction = 0;

        public TimeSpan Duration = TimeSpan.FromSeconds(1);
        public Matrix4 ModelMatrix;

        public int Object = DataManager.C["Meshes"].ID("Quad");
        public Vector2 Origin = Vector2.Zero;
        public Range Range = Range.Zero;
        public Vector2 Size = Vector2.One;
        public Range Speed = Range.CreateConst(1);
        public TextureHandler Texture = new TextureHandler(-1);
        public VisualEffectArgs VisualEffectArgs = new VisualEffectArgs();
        public float[] Movements { get; private set; }
        public double CurrentTime { get; private set; }
        public bool Directional { get; private set; }

        public override void Draw(Matrix4 matrix, GenericObjectRenderer renderer)
        {
            ModelMatrix = Matrix4.CreateScale(Size.X, Size.Y, 1) * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians((Direction + 180) % 360)) * Matrix4.CreateTranslation(Origin.X, Origin.Y, 0);

            Matrix4 no = Matrix4.Transpose(ModelMatrix);
            no.Invert();

            ParticleRenderer.program.Draw(this, ModelMatrix * matrix, no);
        }

        public override void Prepare(double renderSec)
        {
            if (CurrentTime >= Duration.TotalSeconds) SM.Remove(this);
            CurrentTime += renderSec;
        }

        public override void Activate(int layer)
        {
            base.Activate(layer);
            Generate();
        }

        public void Generate()
        {
            Directional = Range != Range.Zero;

            CurrentTime = 0;
            if (Amount > MaxAmount)
                throw new Exception("PARTICLES: Amount exede the maximum of " + MaxAmount);

            List<float> motions = new List<float>();
            Random r = new Random();
            for (int i = 0; i < Amount; i++)
            {
                Vector2 mot = CalculateMotion();

                motions.AddRange(new[] {mot.X, mot.Y, 0});
            }

            Movements = motions.ToArray();
        }

        public virtual Vector2 CalculateMotion()
        {
            Vector2 mot = new Vector2 { X = Directional ? Range.Value : 1, Y = Speed.FloatValue * 25 };

            return mot;
        }
    }
}