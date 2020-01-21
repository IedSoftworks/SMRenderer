using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Drawing
{

    public enum EffectBloomUsage
    {
        None,
        BloomColor,
        ObjectColor,
        Texture,
        RenderedTexture,
        Render,
    }
    public enum EffectBorderUsage
    {
        None,
        TextureBorder,
        QuadBorder,
        QuadEdgeBorder
    }
    /// <summary>
    /// Contains all VisualsEffect arguments, that will be needed
    /// </summary>
    public class VisualEffectArgs
    {
        public static VisualEffectArgs Default = new VisualEffectArgs();

        public Color4 BloomColor = Color4.White;
        public EffectBloomUsage BloomUsage = EffectBloomUsage.None;

        public EffectBorderUsage BorderUsage = EffectBorderUsage.None;
        public Color4 BorderColor = Color4.White;
        public int BorderWidth = 1;
        public int BorderLength = -1;

    }
}
