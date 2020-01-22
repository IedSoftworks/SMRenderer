using OpenTK;
using SMRenderer.Animations;
using SMRenderer.Drawing;
using SMRenderer.Objects;
using SMRenderer.Renderers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer
{
    public enum SMLayer
    {
        Skyplane = -255,
        Normal = 0,
        HUD = 255
    }
    public class SM
    {
        internal static void LoadLayer()
        {
            DrawLayer = new Dictionary<int, DrawLayer>()
            {
                { (int)SMLayer.Skyplane, new DrawLayer() { clear = a => { a.ForEach(b => { if (a.FindIndex(c => c == b) != 0) a.Remove(b); }); } } },
                { (int)SMLayer.Normal, new DrawLayer() },
                { (int)SMLayer.HUD, new DrawLayer() },

            };
        }
        /// <summary>
        /// Contains all objects, that will be rendered.
        /// </summary>
        static public Dictionary<int, DrawLayer> DrawLayer;
        /// <summary>
        /// Prepare the object and add it to the list.
        /// </summary>
        /// <param name="obj">Object</param>
        static public void Add(SMItem obj, int layer = (int)SMLayer.Normal)
        {
            obj.layer = DrawLayer[layer];
            obj.Activate(layer);
        }
        static public void Add(SMItem obj, SMLayer layer = SMLayer.Normal)
        {
            Add(obj, (int)layer);
        }
        /// <summary>
        /// Add a object to the normal plane
        /// </summary>
        /// <param name="obj"></param>
        static public void Add(SMItem obj) { Add(obj, (int)SMLayer.Normal); }
        static public void Remove(SMItem obj, int layer = (int)SMLayer.Normal)
        {
            obj.layer = null;
            obj.Deactivate(layer);
        }
        static public void Remove(SMItem obj, SMLayer layer = SMLayer.Normal)
        {
            Remove(obj, (int)layer);
        }
        static public void Remove(SMItem obj) { Remove(obj, (int)SMLayer.Normal); }
        static public void AddLayer(int index)
        {
            if (!DrawLayer.Keys.Contains(index))
            {
                DrawLayer.Add(index, new DrawLayer());
            }
        }
        static public void ClearLayer(int index)
        {
            if (DrawLayer.Keys.Contains(index))
            {
                DrawLayer[index].clear(DrawLayer[index]);
            }
        }
    }
    public class DrawLayer : List<SMItem>
    {
        public Matrix4 matrix = Matrix4.Zero;
        public GenericObjectRenderer renderer = GeneralRenderer.program;
        public int? layer { get {
                if (!SM.DrawLayer.ContainsValue(this)) return null;
                return SM.DrawLayer.First(a => a.Value == this).Key;
            } }
        public Action<DrawLayer> clear = a => { a.ForEach(b => a.Remove(b)); };
        
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
        public DrawLayer layer;
        public virtual void Draw(Matrix4 matrix) { }
        virtual public void Activate(int layer)
        {
            if (!SM.DrawLayer[layer].Contains(this)) SM.DrawLayer[layer].Add(this);
        }
        virtual public void Deactivate(int layer)
        {
            if (SM.DrawLayer[layer].Contains(this)) SM.DrawLayer[layer].Remove(this);
        }
        virtual public void Prepare(double RenderSec) { }
    }
}
