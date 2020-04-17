using System;
using OpenTK;
using SMRenderer.Data;
using SMRenderer.ManagerIntergration.Attributes;
using SMRenderer.Visual.Drawing;
using SMRenderer.Visual.Renderers;

namespace SMRenderer.Visual
{
    /// <summary>
    /// Master class to add something to the DrawLayers
    /// </summary>
    [Serializable]
    [NotInclude]
    public class SMItem
    {
        /// <summary>
        /// 
        /// </summary>
        public GenericObjectRenderer renderer;

        /// <summary>
        ///     Connect the DrawItem with a object. Usefully for debugging
        /// </summary>
        public object connected = new object();

        /// <summary>
        /// Contains the modelMatrix
        /// </summary>
        public Matrix4 modelMatrix;

        /// <summary>
        /// Saves in what layer the item is in.
        /// </summary>
        public SMLayer layer;
        /// <summary>
        /// Debugging purpose
        /// </summary>
        public string purpose = "None set";
        /// <summary>
        /// When it should render. Is always Layer relative.
        /// <para>Ex.: DrawLayer has 255 and SMItem has 2. Its actual RenderOrder is 257.</para>
        /// </summary>
        public float RenderOrder = 0;
        /// <summary>
        /// Contains information that only need the object.
        /// <para>such like: mesh, texture, etc...</para>
        /// </summary>
        public DrawObject Object = new DrawObject();

        /// <summary>
        /// Tells the program to render stuff.
        /// </summary>
        /// <param name="viewMatrix">The viewMatrix</param>
        public virtual void Draw(Matrix4 viewMatrix)
        {
        }

        public void Draw(Matrix4 view, Matrix4 model, bool normal = true)
        {
            Draw(Object, view, model, normal);
        }

        public void Draw(DrawObject obj, Matrix4 view, Matrix4 model, bool normal = true)
        {
            if (renderer == null)
            {
                if (!GenericObjectRenderer.TRC.ContainsKey(GetType()))
                    throw new Exception("[ERROR]\nThis type has no renderer.");

                renderer = GenericObjectRenderer.TRC[GetType()];
            }

            Matrix4 normalmatrix = Matrix4.Zero;
            if (normal)
            {
                normalmatrix = Matrix4.Transpose(model);
                normalmatrix.Invert();
            }

            renderer.Draw(obj, this, view, model, normalmatrix);
        }

        /// <summary>
        /// Activate is called when the instance is called to add to a SMLayer.
        /// <para>Often used to add it to layer. Don't happen automatic.</para>
        /// </summary>
        /// <param name="layer">The layer the object is added to.</param>
        public virtual void Activate(SMLayer layer)
        {
            if (!layer.Contains(this)) layer.Add(this);
        }
        /// <summary>
        /// Deactivate is called, when the instance is called to remove it self from a SMLayer
        /// <para>Often used to remove it from layer. Don't happen automatic.</para>
        /// </summary>
        /// <param name="Layer"></param>
        public virtual void Deactivate(SMLayer Layer)
        {
            if (Layer.Contains(this)) Layer.Remove(this);
        }

        /// <summary>
        /// Prepare is called right before the Draw.
        /// <para>Its possible to remove the instance from a layer without a error.</para>
        /// </summary>
        /// <param name="RenderSec"></param>
        public virtual void Prepare(double RenderSec)
        {
            Object.Update(RenderSec);
        }
    }
}