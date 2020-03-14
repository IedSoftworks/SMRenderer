using OpenTK;
using SMRenderer.Data;

namespace SMRenderer.Visual
{
    public class TextureHandler : DataSelection
    {
        /// <summary>
        ///     Sets the visible area in the texture. (in pixels)
        ///     <para>The size of the object ís not effected</para>
        /// </summary>
        /// <example>
        ///     You have a 64x64 pixel texture, but you only want to use 16x16.
        ///     Then you set TexSize to X:16 and Y:16.
        /// </example>
        public Vector2 TexSize = Vector2.Zero;

        /// <summary>
        ///     Sets where the visible area is located.
        /// </summary>
        public Vector2 TexPosition = Vector2.Zero;

        public TextureHandler() { }
        public TextureHandler(int id)
        {
            ID = id;
            Category = "Texture";
        }

        public TextureHandler(TextureItem item)
        {
            ID = item.ID;
            Category = item.Category;
        }

        public static bool operator ==(TextureHandler first, TextureHandler second) => first.Equals(second);

        public static bool operator !=(TextureHandler first, TextureHandler second) => !(first == second);

        public static bool operator ==(TextureHandler first, int second) => first.Equals(second);

        public static bool operator !=(TextureHandler first, int second) => !(first == second);

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(TextureHandler))
            {
                TextureHandler ob = (TextureHandler)obj;
                return ob.ID == ID && ob.TexPosition == TexPosition && ob.TexSize == TexSize;
            } else if (obj is int i)
            {
                return i == ID;
            }
            else return false;
        }

        protected bool Equals(TextureHandler other)
        {
            return ID == other.ID && TexSize.Equals(other.TexSize) && TexPosition.Equals(other.TexPosition);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = ID;
                hashCode = (hashCode * 397) ^ TexSize.GetHashCode();
                hashCode = (hashCode * 397) ^ TexPosition.GetHashCode();
                return hashCode;
            }
        }
    }
}