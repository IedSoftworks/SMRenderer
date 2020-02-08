using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Drawing
{
    public class Image : DrawItem
    {
        /// <summary>
        /// Contains the acutal calculated size
        /// </summary>
        public Vector2 _actualSize { get; private set; } = new Vector2(0);
        /// <summary>
        /// Works as sometype of argument; 0 uses the image-height or if Width is set the calculated height
        /// <para>It will be used´to scale the image</para>
        /// </summary>
        public int Height = 0;
        /// <summary>
        /// Works as sometype of argument; 0 uses the image-width or if Height is set the calculated width
        /// <para>It will be used´to scale the image</para>
        /// </summary>
        public int Width = 0;
        /// <summary>
        /// Scales the image. Will be applied on the actual size
        /// </summary>
        public int Scale = 1;
        public Image(TextureItem item)
        {
            Texture = item;
        }
        public override void Prepare(double i)
        {
            float aspect = (float)Texture.size.X / Texture.size.Y;
            if (Width != 0 && Height == 0)
            {
                _actualSize = new Vector2(Width, Width / aspect);
            } else if (Width == 0 && Height != 0)
            {
                _actualSize = new Vector2(Height * aspect, Height);
            } else if (Width != 0 && Height != 0)
            {
                _actualSize = new Vector2(Width, Height);
            } else
            {
                _actualSize = new Vector2(Texture.size.X, Texture.size.Y);
            }
            Size = _actualSize;

            base.Prepare(i);
        }
    }
}
