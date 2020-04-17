using System;
using System.Drawing;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using SMRenderer.Animations;
using SMRenderer.Data;
using SMRenderer.Visual.Renderers;

namespace SMRenderer.Visual.Drawing
{
    /// <summary>
    ///     DrawItem saves instructions of the draw
    /// </summary>
    [Serializable]
    public class DrawItem : SMItem
    {

        /// <summary>
        /// The actual position with Region already calculated.
        /// </summary>
        public Vector2 ActualPosition = new Vector2(0, 0);
        /// <summary>
        /// The actual rotation with Region calculated.
        /// </summary>
        public float ActualRotation;

        /// <summary>
        ///     Dictionary for all animations that are possible on this object; Key = identify-string; Value = Animation;
        /// </summary>
        public AnimationCollection Animations = new AnimationCollection();
        
        /// <summary>
        /// Contains the current CenterPoint. Used to create the modelMatrix
        /// </summary>
        public Vector2 CenterPoint = new Vector2(0, 0);

        /// <summary>
        ///     Determinant if the object will check if it is visible
        /// </summary>
        public bool CheckVisible = false;

        /// <summary>
        ///     Specifies the position of the object
        /// </summary>
        public Vector3 Position = Vector3.Zero;
        
        /// <summary>
        ///     Specifies the anchor for the position
        /// </summary>
        public string positionAnchor = "cc";

        /// <summary>
        ///     Specifies the region; Makes Position, Rotation and Z-Index values relative and ignore the RenderPosition
        /// </summary>
        public Region Region = Region.zero;

        /// <summary>
        ///     Determinant if the object is visible in the window
        /// </summary>
        public bool Visible { get; private set; } = true;

        /// <summary>
        ///     Specifies the rotation of the object
        /// </summary>
        public float Rotation = 0;


        /// <inheritdoc />
        public override void Draw(Matrix4 viewMatrix)
        {
            if (!Visible) return; // Stops the drawing process, if the object is not visible

            Draw(Object, viewMatrix, modelMatrix);
        }


        /// <inheritdoc />
        public override void Prepare(double i)
        {
            base.Prepare(i);

            ActualRotation = Rotation + Region.GetRotation();
            ActualRotation %= 360;

            // Calculate the position
            ActualPosition = Helper.Rotation.PositionFromRotation(Region.GetPosition().Xy, Position.Xy, Region.GetRotation());
            CenterPoint = CalculatePositionAnchor(Position.Xy, Object.Size, positionAnchor);

            modelMatrix = Matrix4.CreateScale(Object.Size.X, Object.Size.Y, 1) *
                          Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(ActualRotation)) *
                          Matrix4.CreateTranslation(CenterPoint.X, CenterPoint.Y, Position.Z);

            if (CheckVisible)
                Visible = Rectangle.Intersect(GLWindow.Window.WindowRect,
                    new Rectangle((int) (CenterPoint.X - Object.Size.X / 2), (int) (CenterPoint.Y - Object.Size.Y / 2),
                        (int) Object.Size.X, (int) Object.Size.Y)) != Rectangle.Empty;
        }

        /// <summary>
        ///     Calculate the object center point
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="anchor"></param>
        /// <returns></returns>
        public static Vector2 CalculatePositionAnchor(Vector2 position, Vector2 size, string anchor)
        {
            float stepX = size.X / 2;
            float stepY = size.Y / 2;
            switch (anchor.First())
            {
                case 'l':
                    position.X += stepX;
                    break;
                case 'r':
                    position.X -= stepX;
                    break;
            }

            switch (anchor.Last())
            {
                case 'u':
                    position.Y += stepY;
                    break;
                case 'l':
                    position.Y -= stepY;
                    break;
            }

            return position;
        }
    }
}