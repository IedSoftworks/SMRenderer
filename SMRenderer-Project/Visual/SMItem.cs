using System;
using OpenTK;
using SMRenderer.ManagerIntergration.Attributes;
using SMRenderer.Visual.Renderers;

namespace SMRenderer.Visual
{
    [Serializable]
    [NotInclude]
    public class SMItem
    {
        /// <summary>
        ///     Connect the DrawItem with a object. Usefully for debugging
        /// </summary>
        public object connected = new object();

        public SMLayer layer;
        public string purpose = "None set";
        public float RenderOrder = 0;

        public virtual void Draw(Matrix4 matrix, GenericObjectRenderer renderer)
        {
        }

        public virtual void Activate(int layer)
        {
            if (!Scene.Current.DrawLayer[layer].Contains(this)) Scene.Current.DrawLayer[layer].Add(this);
        }

        public virtual void Deactivate(int Layer)
        {
            if (Scene.Current.DrawLayer[Layer].Contains(this)) Scene.Current.DrawLayer[Layer].Remove(this);
        }

        public virtual void Prepare(double RenderSec)
        {
        }
    }
}