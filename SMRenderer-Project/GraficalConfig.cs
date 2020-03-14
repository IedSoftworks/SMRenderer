using OpenTK.Graphics;
using SMRenderer.Data;

namespace SMRenderer
{
    /// <summary>
    /// Configure grafical settings
    /// </summary>
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

        /// <summary>
        /// If true, the Bloom feature is active and ready to go.
        /// <para>Bloom is a post-processing and require a lot computing time.</para>
        /// </summary>
        public static bool AllowBloom = false;
    }
}