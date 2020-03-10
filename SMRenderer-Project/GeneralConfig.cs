using System;
using System.Collections.Generic;
using System.IO;
using SMRenderer.Visual.Renderers;

namespace SMRenderer
{
    /// <summary>
    ///     Configuration for the SMRenderer
    /// </summary>
    public class GeneralConfig
    {
        /// <summary>
        ///     Allow / Disallow the scale of the window
        /// </summary>
        public static bool UseScale = false;

        /// <summary>
        ///     Scale of the window
        /// </summary>
        public static int Scale = 1600;

        /// <summary>
        ///     Determinant how often the 'OnUpdateFrame'-function runs in a second.
        /// </summary>
        public static int UpdatePerSecond = 100;

        /// <summary>
        ///     Determinant if you allow to render, when not focused.
        /// </summary>
        public static bool OnlyRenderIfFocused = true;

        /// <summary>
        ///     Determinant if the GameController is allowed
        /// </summary>
        public static bool UseGameController = false;

        public static List<Type> Renderer = new List<Type>
        {
            typeof(GeneralRenderer),
            typeof(BloomRenderer),
            typeof(ParticleRenderer)
        };

        /// <summary>
        ///     Fill that if you want use your pre saved data manager
        /// </summary>
        public static Stream UseDataManager = null;
    }
}