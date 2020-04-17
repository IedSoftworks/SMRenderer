using OpenTK;
using OpenTK.Graphics;
using SMRenderer.Animations;
using SMRenderer.Data;

namespace SMRenderer.Visual.Drawing
{
    /// <summary>
    /// Creates a object that, will be used to draw stuff.
    /// </summary>
    public class DrawObject
    {
        /// <summary>
        ///     Specifies the used texture
        /// </summary>
        public TextureHandler Texture = new TextureHandler(-1);
        /// <summary>
        ///     The object that need to be render.
        /// </summary>
        public DataSelection obj = new DataSelection("Quad", "Meshes");

        /// <summary>
        ///     Specifies the scale of the object
        /// </summary>
        public Vector2 Size = new Vector2(50, 50);

        /// <summary>
        ///     Colorize the texture in that color; Default: White;
        /// </summary>
        public Color4 Color = Color4.White;
        /// <summary>
        ///     Contains all arguments for visual effects
        /// </summary>
        public VisualEffectArgs effectArgs = new VisualEffectArgs();

        public virtual void Update(double deltatime)
        {
            if (Texture.GetType() == typeof(TextureAnimation)) ((TextureAnimation) Texture).Update(deltatime);
        }
    }
}