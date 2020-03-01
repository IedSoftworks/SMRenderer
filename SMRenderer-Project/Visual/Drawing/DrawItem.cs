using OpenTK;
using OpenTK.Graphics;
using SMRenderer.Animations;
using SMRenderer.Renderers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Drawing
{
    /// <summary>
    /// DrawItem saves instructions of the draw
    /// </summary>
    [Serializable]
    public class DrawItem : SMItem
    {
        // Private
        public Vector2 _centerPoint = new Vector2(0, 0);
        public Vector2 _actualPosition = new Vector2(0, 0);
        public float _actualRotation = 0;
        public Matrix4 modelMatrix;
        public Matrix4 normalMatrix;

        /// <summary>
        /// Determant if the object is visible in the window
        /// </summary>
        public bool Visible { get; private set; } = true;

        /// <summary>
        /// Determant if the object will check if it is visible
        /// </summary>
        public bool CheckVisible = false;

        /// <summary>
        /// The object that need to be render.
        /// </summary>
        public int obj = DM.C["Meshes"].ID("Quad");

        /// <summary>
        /// Specifies the position of the object
        /// </summary>
        public Vector2 Position = Vector2.Zero;

        /// <summary>
        /// Specifies the anchor for the position
        /// </summary>
        public string positionAnchor = "cc";

        /// <summary>
        /// Contains the center position of the object; Important when you not use the center-anchor
        /// </summary>
        public Vector2 CenterPoint { get { return _centerPoint; } }

        /// <summary>
        /// Specifies the rotation of the object
        /// </summary>
        public float Rotation = 0;

        /// <summary>
        /// Specifies the scale of the object
        /// </summary>
        public Vector2 Size = new Vector2(50, 50);

        /// <summary>
        /// Dictionary for all animations that are possible on this object; Key = identify-string; Value = Animation;
        /// </summary>
        public AnimationCollection Animations = new AnimationCollection();

        /// <summary>
        /// Specifies the region; Makes Position, Rotation and Z-Index values relative and ignore the RenderPosition
        /// </summary>
        public Region Region = Region.zero;

        /// <summary>
        /// Specifies the used texture
        /// </summary>
        public int Texture = -1;

        /// <summary>
        /// Colorize the texture in that color; Default: White;
        /// </summary>
        public Color4 Color = Color4.White;

        /// <summary>
        /// Contains all arguments for visual effects
        /// </summary>
        public VisualEffectArgs effectArgs = new VisualEffectArgs();

        /// <summary>
        /// Tell the program to actual draw the object
        /// </summary>
        override public void Draw(Matrix4 viewMatrix, GenericObjectRenderer renderer)
        {
            if (!Visible) return; // Stops the drawing process, if the object is not visible

            Matrix4.Transpose(ref modelMatrix, out normalMatrix);
            normalMatrix.Invert();

            renderer.Draw((ObjectInfos)DM.C["Meshes"].Data(obj), this, viewMatrix, modelMatrix);
        }
        /// <summary>
        /// Prepare the object to drawing
        /// </summary>
        /// <param name="i"></param>
        override public void Prepare(double i)
        {
            _actualRotation = Rotation + Region.GetRotation();
            _actualRotation %= 360;

            // Calcuate the position
            _actualPosition = Helper.Rotation.PositionFromRotation(Region.GetPosition(), Position, Region.GetRotation());
            _centerPoint = CalculatePositionAnchor(Position, Size, positionAnchor);

            modelMatrix = Matrix4.CreateScale(Size.X, Size.Y, 1) * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(_actualRotation)) * Matrix4.CreateTranslation(_centerPoint.X, _centerPoint.Y, 0);

            if (CheckVisible)
                 Visible = Rectangle.Intersect(GLWindow.Window.windowRect, new Rectangle((int)(_centerPoint.X - Size.Y / 2), (int)(_centerPoint.Y - Size.Y / 2), (int)Size.X, (int)Size.Y)) != Rectangle.Empty;
        }
        /// <summary>
        /// Calculate the object centerpoint
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
