using OpenTK;
using SMRenderer.Animations;
using SMRenderer.Drawing;
using SMRenderer.Objects;
using SMRenderer.Renderers;
using SMRenderer.TypeExtensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer
{
    public enum SMLayerID
    {
        Skyplane = -255,
        Normal = 0,
        HUD = 255
    }
    public class SM
    {
        /// <summary>
        /// Prepare the object and add it to the layer.
        /// </summary>
        /// <param name="obj">Object</param>
        static public void Add(SMItem obj, int layer = (int)SMLayerID.Normal)
        {
            obj.layer = Scene.current.DrawLayer[layer];
            obj.Activate(layer);
        }
        static public void Add(SMItem obj, SMLayerID layer = SMLayerID.Normal)
        {
            Add(obj, (int)layer);
        }
        /// <summary>
        /// Add a object to the normal plane
        /// </summary>
        /// <param name="obj"></param>
        static public void Add(SMItem obj) { Add(obj, (int)SMLayerID.Normal); }
        static public void Remove(SMItem obj, int layer = (int)SMLayerID.Normal)
        {
            obj.layer = null;
            obj.Deactivate(layer);
        }
        static public void Remove(SMItem obj, SMLayerID layer = SMLayerID.Normal)
        {
            Remove(obj, (int)layer);
        }
        static public void Remove(SMItem obj) { Remove(obj, (int)SMLayerID.Normal); }
        static public void AddLayer(int index)
        {
            if (!Scene.current.DrawLayer.Keys.Contains(index))
            {
                Scene.current.DrawLayer.Add(index, new SMLayer());
            }
        }
        static public void ClearLayer(int index)
        {
            if (Scene.current.DrawLayer.Keys.Contains(index))
            {
                Scene.current.DrawLayer[index].clear(Scene.current.DrawLayer[index]);
            }
        }
    }
    public class SMLayer : List<SMItem>
    {
        public Matrix4 matrix = Camera.staticView;
        public bool staticMatrix = true;
        public GenericObjectRenderer renderer = GeneralRenderer.program;
        public Action<SMLayer> clear = a => { a.ForEach(b => a.Remove(b)); };
    }

    /// <summary>
    /// Creates a region to use relative values in DrawItem; All values in this class have to be absolute
    /// </summary>
    public class Region
    {
        public Vector2 Position = new Vector2(0, 0);
        public float Rotation = 0;
        public int ZIndex = 0;
        public bool? HUD = null;
        /// <summary>
        /// The region will always follow the anchor; This value will make all other values irrelevant;
        /// </summary>
        public DrawItem anchor = null;

        public Vector2 GetPosition()
        {
            return anchor != null ? anchor.Position : Position;
        }
        public float GetRotation()
        {
            return anchor != null ? anchor.Rotation : Rotation;
        }
        public int GetZIndex()
        {
            return (int)(anchor != null ? anchor.ZIndex : ZIndex);
        }

        public static Region zero = new Region { Position = new Vector2(0), Rotation = 0, ZIndex = 0 };
    }

    public class SMItem
    {
        /// <summary>
        /// Conntect the DrawItem with a object. Usefully for debugging
        /// </summary>
        public object connected = new object();
        public string purpose = "None set";
        public float _RenderOrder = 0;
        public SMLayer layer;
        public virtual void Draw(Matrix4 matrix, GenericObjectRenderer renderer) { }
        virtual public void Activate(int layer)
        {
            if (!Scene.current.DrawLayer[layer].Contains(this)) Scene.current.DrawLayer[layer].Add(this);
        }
        virtual public void Deactivate(int layer)
        {
            if (Scene.current.DrawLayer[layer].Contains(this)) Scene.current.DrawLayer[layer].Remove(this);
        }
        virtual public void Prepare(double RenderSec) { }
    }
}
