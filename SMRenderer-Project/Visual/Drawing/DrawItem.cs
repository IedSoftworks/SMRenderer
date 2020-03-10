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
        public Vector2 ActualPosition = new Vector2(0, 0);
        public float ActualRotation;

        /// <summary>
        ///     Dictionary for all animations that are possible on this object; Key = identify-string; Value = Animation;
        /// </summary>
        public AnimationCollection Animations = new AnimationCollection();

        // Private
        public Vector2 CenterPoint = new Vector2(0, 0);

        /// <summary>
        ///     Determinant if the object will check if it is visible
        /// </summary>
        public bool CheckVisible = false;

        /// <summary>
        ///     Colorize the texture in that color; Default: White;
        /// </summary>
        public Color4 Color = Color4.White;

        /// <summary>
        ///     Contains all arguments for visual effects
        /// </summary>
        public VisualEffectArgs effectArgs = new VisualEffectArgs();

        public Matrix4 modelMatrix;
        public Matrix4 normalMatrix;

        /// <summary>
        ///     The object that need to be render.
        /// </summary>
        public int obj = DataManager.C["Meshes"].ID("Quad");

        /// <summary>
        ///     Specifies the position of the object
        /// </summary>
        public Vector2 Position = Vector2.Zero;
        
        /// <summary>
        ///     Specifies the anchor for the position
        /// </summary>
        public string positionAnchor = "cc";

        /// <summary>
        ///     Specifies the region; Makes Position, Rotation and Z-Index values relative and ignore the RenderPosition
        /// </summary>
        public Region Region = Region.zero;

        /// <summary>
        ///     Specifies the rotation of the object
        /// </summary>
        public float Rotation = 0;

        /// <summary>
        ///     Specifies the scale of the object
        /// </summary>
        public Vector2 Size = new Vector2(50, 50);

        /// <summary>
        ///     Specifies the used texture
        /// </summary>
        public TextureHandler Texture = new TextureHandler(-1);

        /// <summary>
        ///     Determinant if the object is visible in the window
        /// </summary>
        public bool Visible { get; private set; } = true;

        /// <summary>
        ///     Tell the program to actual draw the object
        /// </summary>
        public override void Draw(Matrix4 viewMatrix, GenericObjectRenderer renderer)
        {
            if (!Visible) return; // Stops the drawing process, if the object is not visible

            Matrix4.Transpose(ref modelMatrix, out normalMatrix);
            normalMatrix.Invert();

            renderer.Draw((ObjectInfos) DataManager.C["Meshes"].Data(obj), this, viewMatrix, modelMatrix);
        }

        /// <summary>
        ///     Prepare the object to drawing
        /// </summary>
        /// <param name="i"></param>
        public override void Prepare(double i)
        {
            ActualRotation = Rotation + Region.GetRotation();
            ActualRotation %= 360;

            // Calculate the position
            ActualPosition = Helper.Rotation.PositionFromRotation(Region.GetPosition(), Position, Region.GetRotation());
            CenterPoint = CalculatePositionAnchor(Position, Size, positionAnchor);

            modelMatrix = Matrix4.CreateScale(Size.X, Size.Y, 1) *
                          Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(ActualRotation)) *
                          Matrix4.CreateTranslation(CenterPoint.X, CenterPoint.Y, 0);

            if (CheckVisible)
                Visible = Rectangle.Intersect(GLWindow.Window.WindowRect,
                    new Rectangle((int) (CenterPoint.X - Size.Y / 2), (int) (CenterPoint.Y - Size.Y / 2),
                        (int) Size.X, (int) Size.Y)) != Rectangle.Empty;
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