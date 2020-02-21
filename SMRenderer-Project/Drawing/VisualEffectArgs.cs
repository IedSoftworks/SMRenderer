using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Drawing
{
    /// <summary>
    /// Tell how the bloom sould be used
    /// </summary>
    public enum EffectBloomUsage
    {
        /// <summary>
        /// No Bloom
        /// </summary>
        None,
        /// <summary>
        /// Uses the bloom color
        /// </summary>
        BloomColor,
        /// <summary>
        /// Uses the object color
        /// </summary>
        ObjectColor,
        /// <summary>
        /// Uses the texture
        /// </summary>
        Texture,
        /// <summary>
        /// Uses the texture + the object color
        /// </summary>
        RenderedTexture,
        /// <summary>
        /// Uses anything that will be rendered
        /// </summary>
        Render,
    }
    /// <summary>
    /// Tells how the border effect will be used
    /// </summary>
    public enum EffectBorderUsage
    {
        /// <summary>
        /// No border
        /// </summary>
        None,
        /// <summary>
        /// Creates a border around the texture
        /// </summary>
        TextureBorder,
        /// <summary>
        /// Create a border as quad
        /// </summary>
        QuadBorder,
        /// <summary>
        /// Create a border ín the edges
        /// </summary>
        QuadEdgeBorder
    }
    /// <summary>
    /// Contains all VisualsEffect arguments, that will be needed
    /// </summary>
    [Serializable]
    public class VisualEffectArgs
    { 
        /// <summary>
        /// Tells that color should be used if the BloomUsage is BloomColor
        /// </summary>
        public Color4 BloomColor = Color4.White;
        /// <summary>
        /// Tells how the bloom will be used
        /// </summary>
        public EffectBloomUsage BloomUsage = EffectBloomUsage.None;

        /// <summary>
        /// Tells how the border will be used
        /// </summary>
        public EffectBorderUsage BorderUsage = EffectBorderUsage.None;
        /// <summary>
        /// Determant in what color the border should have
        /// </summary>
        public Color4 BorderColor = Color4.White;
        /// <summary>
        /// Tells how thick the border should be
        /// </summary>
        public int BorderWidth = 1;
        /// <summary>
        /// Tells how long the border should be.
        /// <para>Comes only to effect if BorderUsage is QuadEdgeBorder</para>
        /// </summary>
        public int BorderLength = -1;

    }
}
