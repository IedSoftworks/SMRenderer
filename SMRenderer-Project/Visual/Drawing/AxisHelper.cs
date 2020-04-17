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

        private void Create(Vector2 pos)
        {
            Vector3 position = new Vector3(pos);
            RenderOrder = 255;
            purpose = "AxisHelper at " + position.X + " " + position.Y;
            items.AddRange(new[]
            {
                new DrawItem
                {
                    Object =
                    {

                        Color = Color4.Blue,
                        Size = new Vector2(5, 15),
                    },
                    Position = position + new Vector3(0, 5, 0),
                    positionAnchor = "lu",
                    connected = this,
                    Rotation = 0,
                },
                new DrawItem
                {
                    Object =
                    {
                        Color = Color4.Red,
                        Size = new Vector2(15, 5),
                    },
                    Position = position + new Vector3(5, 0,0),
                    positionAnchor = "lu",
                    connected = this,
                    Rotation = 0,
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
                    Object =
                    {

                        Color = Color4.Blue,
                        Size = new Vector2(5, 15),
                    },
                    Region = region,
                    Position = new Vector3(0, 5, 0),
                    positionAnchor = "lu",
                    connected = this,
                    Rotation = 0,
                },
                new DrawItem
                {
                    Object =
                    {
                        Color = Color4.Red,
                        Size = new Vector2(15, 5),
                    },
                    Region = region,
                    Position = new Vector3(5, 0, 0),
                    positionAnchor = "lu",
                    connected = this,
                    Rotation = 0,
                }
            });
        }
    }
}