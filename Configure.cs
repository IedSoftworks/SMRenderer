using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer
{
    /// <summary>
    /// Configuration for the SMRenderer
    /// </summary>
    public class Configure
    {
        /// <summary>
        /// Allow / Disallow the scale of the window
        /// </summary>
        public static bool UseScale = false;
        /// <summary>
        /// Scale of the window
        /// </summary>
        public static int Scale = 1600;
        /// <summary>
        /// Saves the default texture. Must be set, before the window was created.
        /// </summary>
        public static TextureItem defaultTexture;
        /// <summary>
        /// Determante how often the 'OnUpdateFrame'-function runs in a second.
        /// </summary>
        public static int UpdatePerSecond = 100;
        /// <summary>
        /// Determante if you allow to render, when not focused.
        /// </summary>
        public static bool OnlyRenderIfFocused = true;
        /// <summary>
        /// Determante what color will be rendered, when not object is before it.
        /// </summary>
        public static Color4 ClearColor = Color4.Black;
    }
}
