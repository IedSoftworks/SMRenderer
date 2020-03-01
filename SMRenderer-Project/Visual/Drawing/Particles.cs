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
    [Serializable]
    public class Particles : SMItem
    {
        const int MaxAmount = 2048;
        public float[] Movements { get; private set; }
        public Matrix4 ModelMatrix;
        public double CurrentTime { get; private set; } = 0;
        public bool directional { get; private set; }

        public int Object = DataManager.C["Meshes"].ID("Quad");
        public Vector2 Origin = Vector2.Zero;
        public Vector2 Size = Vector2.One;
        public Color4 Color = Color4.White;
        public int Texture = -1;
        public VisualEffectArgs VisualEffectArgs = new VisualEffectArgs();
        public int AdditionalRotation = 0;

        public TimeSpan Duration = TimeSpan.FromSeconds(1);
        public float Direction = 0;
        public Math.Range Range = Math.Range.Zero;
        public int Amount = 1;
        public Math.Range Speed = Math.Range.CreateConst(1);
        public override void Draw(Matrix4 matrix, GenericObjectRenderer renderer)
        {
            ModelMatrix = Matrix4.CreateScale(Size.X, Size.Y, 1) * Matrix4.CreateTranslation(Origin.X, Origin.Y, 0);

            Matrix4 no = Matrix4.Transpose(ModelMatrix);
            no.Invert();

            ParticleRenderer.program.Draw(this, ModelMatrix * matrix, no);
        }
        public override void Prepare(double RenderSec)
        {
            if (CurrentTime >= Duration.TotalSeconds) SM.Remove(this);
            CurrentTime += RenderSec;
        }
        public override void Activate(int layer)
        {
            base.Activate(layer);
            Generate();
        }
        public void Generate()
        {
            directional = Range != Math.Range.Zero;

            CurrentTime = 0;
            if (Amount > MaxAmount)
                throw new Exception("PARTICLES: Amount exede the maximum of " + MaxAmount);

            List<float> motions = new List<float>();
            List<int> rotations = new List<int>();
            Random r = new Random();
            for(int i = 0; i < Amount; i++)
            {
                Vector2 Mot = CalculateMotion();

                motions.AddRange(new float[] { Mot.X, Mot.Y, 0 });
            }
            Movements = motions.ToArray();
        }

        virtual public Vector2 CalculateMotion()
        {
            Vector2 Mot = new Vector2();
            Mot.X = directional ? Range.Value : 1;
            Mot.Y = Speed.floatValue * 25;

            return Helper.Rotation.PositionFromRotation(Vector2.Zero, Mot, directional ? (Direction + 180) % 380 : SMGl.random.Next(0, 360));
        }
    }
}
