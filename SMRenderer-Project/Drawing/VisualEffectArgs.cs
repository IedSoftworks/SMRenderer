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
        Texture
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
        public static Dictionary<string, object> DefaultEffectArgs = new Dictionary<string, object>
        {
            // Bloom Args
            {"BloomColor", Color4.White },
            {"BloomUsage", EffectBloomUsage.None },

            // Border Args
            {"BorderUsage", EffectBorderUsage.None },
            {"BorderColor", Color4.Blue },
            {"BorderWidth", 5 },
            {"BorderLength", -1 },

            // Textures
            {"TextureRepeat", false }
        };
    }
}
