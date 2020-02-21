using OpenTK;
using OpenTK.Graphics;
using SMRenderer.Renderers;
using SMRenderer.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Drawing
{
    [Serializable]
    public class Particles : SMItem
    {
        const int MaxAmount = 255;
        public float[] Movements { get; private set; }
        public Matrix4 ModelMatrix;
        public double CurrentTime { get; private set; } = 0;

        public int Object = DataManager.C["Meshes"].ID("Quad");
        public Vector2 Origin = Vector2.Zero;
        public Vector2 Size = Vector2.One;
        public Color4 Color = Color4.White;
        public int Texture = -1;
        public VisualEffectArgs VisualEffectArgs = new VisualEffectArgs();

        public TimeSpan Duration = TimeSpan.FromSeconds(1);
        public float Direction = 0;
        public Range Range = new Range(1);
        public int Amount = 1;
        public Range Speed = Range.CreateConst(1);

        public override void Draw(Matrix4 matrix, GenericObjectRenderer renderer)
        {
            ModelMatrix = Matrix4.CreateScale(Size.X, Size.Y, 1) * Matrix4.CreateRotationZ(0) * Matrix4.CreateTranslation(Origin.X, Origin.Y, 0);

            Matrix4 no = Matrix4.Transpose(ModelMatrix);
            no.Invert();

            ParticleRenderer.program.Draw(this, ModelMatrix * matrix, no);
        }
        public override void Prepare(double RenderSec)
        {
            //if (CurrentTime >= Duration.TotalSeconds) SM.Remove(this);
            CurrentTime += RenderSec;
        }
        public override void Activate(int layer)
        {
            base.Activate(layer);
            Generate();
            CurrentTime = 0;
        }
        public void Generate()
        {
            if (Amount > MaxAmount)
                throw new Exception("PARTICLES: Amount exede the maximum of " + MaxAmount);

            List<float> motions = new List<float>();
            Random r = new Random();
            for(int i = 0; i < Amount; i++)
            {
                Vector2 Mot = new Vector2();
                Mot.X = Range.Value;
                Mot.Y = Speed.Value * 75;

                Mot = Helper.Rotation.CalculatePositionForRotationAroundPoint(Vector2.Zero, Mot, (Direction+180) % 380);

                motions.AddRange(new float[] { Mot.X, Mot.Y });
            }
            Movements = motions.ToArray();
        }
    }
}
