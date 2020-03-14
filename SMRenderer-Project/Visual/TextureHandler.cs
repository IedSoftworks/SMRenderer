using OpenTK;
using SMRenderer.Data;

namespace SMRenderer.Visual
{
    /// <summary>
    ///     The handler to get a texture from the data.
    ///     <para>Additional it adds some features to it</para>
    /// </summary>
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

        /// <summary>
        /// Empty constructor for automatic creation
        /// </summary>
        public TextureHandler()
        { }
        /// <summary>
        /// Creates a Handler based on the id.
        /// </summary>
        /// <param name="id"></param>
        public TextureHandler(int id) : base(id, "Texture") 
        { }

        /// <summary>
        /// Creates a Handler based on the textureItem.
        /// </summary>
        /// <param name="item"></param>
        public TextureHandler(TextureItem item) : base(item.ID, item.Category)
        { }

        /// <summary>
        /// Checks if both handlers are equal
        /// </summary>
        /// <param name="first">Left</param>
        /// <param name="second">Right</param>
        /// <returns></returns>
        public static bool operator ==(TextureHandler first, TextureHandler second) => first.Equals(second);
        /// <summary>
        /// Checks if both handlers are unequal
        /// </summary>
        /// <param name="first">Left</param>
        /// <param name="second">Right</param>
        /// <returns></returns>
        public static bool operator !=(TextureHandler first, TextureHandler second) => !(first == second);
        /// <summary>
        /// Checks if the ids are equal
        /// </summary>
        /// <param name="first">Left</param>
        /// <param name="second">Right</param>
        /// <returns></returns>
        public static bool operator ==(TextureHandler first, int second) => first.Equals(second);
        /// <summary>
        /// Checks if the ids are unequal
        /// </summary>
        /// <param name="first">Left</param>
        /// <param name="second">Right</param>
        /// <returns></returns>
        public static bool operator !=(TextureHandler first, int second) => !(first == second);

        /// <inheritdoc />
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
        /// <summary>
        /// Checks if both handlers are equal
        /// </summary>
        /// <param name="other">The other one</param>
        /// <returns></returns>
        protected bool Equals(TextureHandler other)
        {
            return ID == other.ID && TexSize.Equals(other.TexSize) && TexPosition.Equals(other.TexPosition);
        }

        /// <inheritdoc />
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