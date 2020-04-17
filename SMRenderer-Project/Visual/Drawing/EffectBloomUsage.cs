namespace SMRenderer.Visual.Drawing
{
    /// <summary>
    ///     Tell how the bloom should be used
    /// </summary>
    public enum EffectBloomUsage
    {
        /// <summary>
        ///     No Bloom
        /// </summary>
        None,

        /// <summary>
        ///     Uses the bloom color
        /// </summary>
        BloomColor,

        /// <summary>
        ///     Uses the object color
        /// </summary>
        ObjectColor,

        /// <summary>
        ///     Uses the texture
        /// </summary>
        Texture,

        /// <summary>
        ///     Uses the texture + the object color
        /// </summary>
        RenderedTexture,

        /// <summary>
        ///     Uses anything that will be rendered
        /// </summary>
        Render
    }
}