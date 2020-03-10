using OpenTK.Graphics;
using SMRenderer.Data;

namespace SMRenderer
{
    public class GraficalConfig
    {
        /// <summary>
        ///     Determinate what color will be rendered, when no object is before it.
        /// </summary>
        public static Color4 ClearColor = Color4.Black;

        /// <summary>
        ///     Saves the default texture. Must be set, before the window was created.
        /// </summary>
        public static TextureItem defaultTexture;

        public static bool AllowBloom = false;
    }
}