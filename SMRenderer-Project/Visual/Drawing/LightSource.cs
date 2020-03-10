using System;
using OpenTK;
using OpenTK.Graphics;

namespace SMRenderer.Visual.Drawing
{
    /// <summary>
    ///     A light source for the scene
    /// </summary>
    [Serializable]
    public class LightSource
    {
        /// <summary>
        ///     Light color
        /// </summary>
        public Color4 Color = Color4.White;

        public Vector2 Direction = Vector2.Zero;

        /// <summary>
        ///     Light height
        /// </summary>
        public float Height = 1f;

        /// <summary>
        ///     Light intensity
        /// </summary>
        public float Intensity = 1;

        /// <summary>
        ///     Position
        /// </summary>
        public Vector2 Position = Vector2.Zero;
    }
}