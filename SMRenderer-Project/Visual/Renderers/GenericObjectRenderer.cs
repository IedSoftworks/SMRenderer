using System;
using System.Collections.Generic;
using OpenTK;
using SMRenderer.Data;
using SMRenderer.Visual.Drawing;

namespace SMRenderer.Visual.Renderers
{
    /// <summary>
    /// Masterclass for object render programs
    /// </summary>
    public class GenericObjectRenderer : GenericRenderer
    {
        public static Dictionary<Type, GenericObjectRenderer> TRC = new Dictionary<Type, GenericObjectRenderer>();

        public GenericObjectRenderer(params Type[] TargetType)
        {
            foreach (Type type in TargetType)
            {
                TRC[type] = this;
            }

        }

        /// <summary>
        /// Tells the renderer to draw something
        /// </summary>
        /// <param name="obj">The object itself</param>
        /// <param name="item">Additional informations</param>
        /// <param name="viewMatrix">view Matrix</param>
        /// <param name="modelMatrix">model matrix</param>
        /// <param name="normalMatrix">the normal matrix for light calculation</param>
        internal virtual void Draw(DrawObject obj, SMItem item, Matrix4 viewMatrix, Matrix4 modelMatrix, Matrix4 normalMatrix)
        {
        }
    }
}