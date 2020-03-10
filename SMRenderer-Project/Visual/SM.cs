using System.Linq;

namespace SMRenderer.Visual
{
    public class SM
    {
        /// <summary>
        /// Add a object to the normal plane
        /// </summary>
        /// <param name="obj"></param>
        public static void Add(SMItem obj)
        {
            Add(obj, SMLayerID.Normal);
        }

        /// <summary>
        /// Prepare the object and add it to the layer.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="layer"></param>
        /// 
        public static void Add(SMItem obj, int layer)
        {
            obj.layer = Scene.Current.DrawLayer[layer];
            obj.Activate(layer);
        }


        public static void Add(SMItem obj, SMLayerID layer)
        {
            Add(obj, (int)layer);
        }

        public static void Remove(SMItem obj, int layer)
        {
            obj.layer = null;
            obj.Deactivate(layer);
        }

        public static void Remove(SMItem obj, SMLayerID layer)
        {
            Remove(obj, (int)layer);
        }

        public static void Remove(SMItem obj) { Remove(obj, (int)SMLayerID.Normal); }

        public static void AddLayer(int index)
        {
            if (!Scene.Current.DrawLayer.Keys.Contains(index)) Scene.Current.DrawLayer.Add(index, new SMLayer());
        }

        public static void ClearLayer(int index)
        {
            if (Scene.Current.DrawLayer.Keys.Contains(index)) Scene.Current.DrawLayer[index].clear(Scene.Current.DrawLayer[index]);
        }

        public static bool Exists(SMItem obj, int layer = (int)SMLayerID.Normal)
        {
            return Scene.Current.DrawLayer[layer].Contains(obj);
        }
    }
}
