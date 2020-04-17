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
    /// <summary>
    /// A particle system.
    /// </summary>
    [Serializable]
    public class Particles : SMItem
    {
        /// <summary>
        /// Contains the maximum amount of possible.
        /// <para>Required for the shader program.</para>
        /// </summary>
        public const int MaxAmount = 2048;
        /// <summary>
        /// Apply additional rotation to the objects.
        /// </summary>
        public int AdditionalRotation = 0;
        /// <summary>
        /// Specify the amount of particles.
        /// <para>Watch the MaxAmount for the maximum</para>
        /// </summary>
        public int Amount = 1;
        /// <summary>
        /// The direction towards the particle travel.
        /// </summary>
        public float Direction = 0;
        /// <summary>
        /// Specifies Origin of the particles
        /// </summary>
        public Vector2 Origin = Vector2.Zero;
        /// <summary>
        /// Specifies the range the particles spread 
        /// </summary>
        public Range Range = Range.Zero;
        /// <summary>
        /// Specifies the speed the particles travel
        /// </summary>
        public Range Speed = Range.CreateConst(1);

        /// <summary>
        /// Specifies how long the particles should travel
        /// </summary>
        public TimeSpan Duration = TimeSpan.FromSeconds(1);
        /// <summary>
        /// Contains the shader information for the motions.
        /// </summary>
        public float[] Movements { get; private set; }
        /// <summary>
        /// Contains the current time
        /// </summary>
        public double CurrentTime { get; private set; }
        /// <summary>
        /// If true, the particles travel directional, otherwise they travel in random directions.
        /// </summary>
        public bool Directional { get; private set; }

        /// <inheritdoc />
        public override void Draw(Matrix4 matrix)
        {
            modelMatrix = Matrix4.CreateScale(Object.Size.X, Object.Size.Y, 1) * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians((Direction + 180) % 360)) * Matrix4.CreateTranslation(Origin.X, Origin.Y, 0);

            Draw(matrix, modelMatrix);
        }

        /// <inheritdoc />
        public override void Prepare(double renderSec)
        {
            if (CurrentTime >= Duration.TotalSeconds) SM.Remove(this);
            CurrentTime += renderSec;
        }

        /// <inheritdoc />
        public override void Activate(SMLayer layer)
        {
            base.Activate(layer);
            Generate();
        }

        /// <summary>
        ///     Generate the movements.
        /// </summary>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Calculate how the particle should move.
        /// </summary>
        /// <returns></returns>
        public virtual Vector2 CalculateMotion()
        {
            Vector2 mot = new Vector2 { X = Directional ? Range.Value : 1, Y = Speed.FloatValue * 25 };

            return mot;
        }
    }
}