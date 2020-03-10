using OpenTK;
using OpenTK.Graphics;

namespace SMRenderer.Visual.Drawing
{
    /// <summary>
    ///     Creates a AxisHelper
    /// </summary>
    public class AxisHelper : DrawContainer
    {
        /// <summary>
        ///     Creates it at a specific position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public AxisHelper(int x, int y)
        {
            Create(new Vector2(x, y));
        }

        /// <summary>
        ///     Creates it at a specific position
        /// </summary>
        /// <param name="position"></param>
        public AxisHelper(Vector2 position)
        {
            Create(position);
        }

        /// <summary>
        ///     Creates it with a region
        /// </summary>
        /// <param name="region"></param>
        public AxisHelper(Region region)
        {
            Create(region);
        }

        private void Create(Vector2 position)
        {
            RenderOrder = 255;
            purpose = "AxisHelper at " + position.X + " " + position.Y;
            items.AddRange(new[]
            {
                new DrawItem
                {
                    Position = position + new Vector2(0, 5),
                    Size = new Vector2(5, 15),
                    positionAnchor = "lu",
                    connected = this,
                    Rotation = 0,
                    Color = Color4.Blue
                },
                new DrawItem
                {
                    Position = position + new Vector2(5, 0),
                    Size = new Vector2(15, 5),
                    positionAnchor = "lu",
                    connected = this,
                    Rotation = 0,
                    Color = Color4.Red
                }
            });
        }

        private void Create(Region region)
        {
            RenderOrder = 255;
            purpose = "AxisHelper for region";
            items.AddRange(new[]
            {
                new DrawItem
                {
                    Region = region,
                    Position = new Vector2(0, 5),
                    Size = new Vector2(5, 15),
                    positionAnchor = "lu",
                    connected = this,
                    Rotation = 0,
                    Color = Color4.Blue
                },
                new DrawItem
                {
                    Region = region,
                    Position = new Vector2(5, 0),
                    Size = new Vector2(15, 5),
                    positionAnchor = "lu",
                    connected = this,
                    Rotation = 0,
                    Color = Color4.Red
                }
            });
        }
    }
}