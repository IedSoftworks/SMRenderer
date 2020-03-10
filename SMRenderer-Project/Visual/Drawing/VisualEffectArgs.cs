using System;
using OpenTK.Graphics;

namespace SMRenderer.Visual.Drawing
{
    /// <summary>
    ///     Contains all VisualsEffect arguments, that will be needed
    /// </summary>
    [Serializable]
    public class VisualEffectArgs
    {
        /// <summary>
        ///     Tells that color should be used if the BloomUsage is BloomColor
        /// </summary>
        public Color4 BloomColor = Color4.White;

        /// <summary>
        ///     Tells how the bloom will be used
        /// </summary>
        public EffectBloomUsage BloomUsage = EffectBloomUsage.None;
    }
}