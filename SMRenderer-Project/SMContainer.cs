using OpenTK;
using SMRenderer.Animations;
using SMRenderer.Drawing;
using SMRenderer.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer
{
    /// <summary>
    /// Contains the options to choose, where the object should render
    /// </summary>
    public enum RenderPosition
    {
        /// <summary>
        /// Function; Tells the DrawItem to ignore the renderPosition
        /// </summary>
        Override,
        /// <summary>
        /// Background; Ignore camera movement
        /// </summary>
        StaticBackground,
        /// <summary>
        /// Background; Follows the camera
        /// </summary>
        DynamicBackground,
        /// <summary>
        /// Normal positioning
        /// </summary>
        Normal,
        /// <summary>
        /// Render above the world; Follows the camera
        /// </summary>
        HUD
    }
    public class SM
    {
        /// <summary>
        /// List for draws; !Please use SM.Add to add items.!
        /// </summary>
        static public List<SMItem> List = new List<SMItem>();
        /// <summary>
        /// Prepare the object and add it to the list.
        /// </summary>
        /// <param name="obj">Object</param>
        static public void Add(SMItem obj)
        {
            obj.Activate();
        }
        static public void Remove(SMItem obj)
        {
            obj.Deactivate();
        }
    }
    /// <summary>
    /// Creates a region to use relative values in DrawItem; All values in this class have to be absolute
    /// </summary>
    public class Region
    {
        public Vector2 Position = new Vector2(0, 0);
        public float Rotation = 0;
        public int ZIndex = 0;
        public RenderPosition renderPosition = RenderPosition.Override;
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
        public virtual void Draw() { }
        virtual public void Activate()
        {

        }
        virtual public void Deactivate()
        {

        }
        virtual public void Prepare() { }
    }
}
