using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace SMRenderer
{
    public class Texture
    {
        private int width = 0;
        private int height = 0;
        private int handle = -1;

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public int TexId { get { return handle; } }


        public static Texture empty = null;

        /// <summary>
        /// Creates a texture
        /// </summary>
        /// <param name="bm"></param>
        public Texture(Bitmap bm, bool autodispose = false)
        {
                BitmapData data;

                handle = GL.GenTexture();

                GL.BindTexture(TextureTarget.Texture2D, handle);

                if (bm.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                {
                    data = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                }
                else
                {
                    data = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
                }

                width = bm.Width;
                height = bm.Height;

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                GL.BindTexture(TextureTarget.Texture2D, 0);
                bm.UnlockBits(data);
            if (autodispose) bm.Dispose();
        }
        /// <summary>
        /// Deletes the texture
        /// </summary>
        public void Dispose()
        {
            GL.DeleteTexture(handle);
        }
        public static void CreateEmpty()
        {
            Bitmap bit = new Bitmap(1, 1);
            bit.SetPixel(0, 0, System.Drawing.Color.White);
            empty = new Texture(bit);
            bit.Dispose();
        }
    }
    /// <summary>
    /// A storage item, to prevent repeatly loading the same texture
    /// </summary>
    public class TextureItem
    {
        private Texture _tex;
        public Bitmap bitmap;
        /// <summary>
        /// Dispose automaticly the bitmap after the texture was created
        /// </summary>
        public bool AutoDispose = false;
        public Texture texture { get { if (_tex == null) _tex = new Texture(bitmap, AutoDispose); return _tex; } }
        public Vector2 size { get {
                if (_tex == null) _tex = new Texture(bitmap);
                return new Vector2(_tex.Width, _tex.Height);
            } }
        /// <summary>
        /// Creates a textureitem
        /// </summary>
        /// <param name="bitmap"></param>
        public TextureItem(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }
    }
}
