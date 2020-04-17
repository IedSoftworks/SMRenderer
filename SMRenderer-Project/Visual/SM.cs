using System;
using System.Linq;

namespace SMRenderer.Visual
{
    /// <summary>
    /// The main ShowManager
    /// </summary>
    public class SM
    {
        /// <summary>
        /// Prepare the object and add it to the layer.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="layer"></param>
        public static void Add(SMItem obj, SMLayer layer)
        {
            obj.layer = layer;
            obj.Activate(layer);
        }

        /// <summary>
        /// Prepare the object and add it to the current scene.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="layer"></param>
        public static void Add(SMItem obj, int layer)
        {
            Add(obj, Scene.Current.DrawLayer[layer]);
        }

        /// <summary>
        /// Prepare the object and add it to the current scene.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="layer"></param>
        public static void Add(SMItem obj, SMLayerID layer)
        {
            Add(obj, (int)layer);
        }

        /// <summary>
        /// Add a object to the normal plane
        /// </summary>
        /// <param name="obj"></param>
        public static void Add(SMItem obj)
        {
            Add(obj, SMLayerID.Normal);
        }

        /// <summary>
        /// Remove the object from the layer
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="layer"></param>
        public static void Remove(SMItem obj, SMLayer layer)
        {
            obj.layer = null;
            obj.Deactivate(layer);
        }
        /// <summary>
        /// Remove the object from the layer of the current scene
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="layer"></param>
        public static void Remove(SMItem obj, int layer)
        {
            Remove(obj, Scene.Current.DrawLayer[layer]);
        }

        /// <summary>
        /// Remove the object from the layer of the current scene
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="layer"></param>
        public static void Remove(SMItem obj, SMLayerID layer)
        {
            Remove(obj, (int)layer);
        }

        /// <summary>
        /// Remove the object from main layer of the current scene
        /// </summary>
        /// <param name="obj"></param>
        public static void Remove(SMItem obj) { Remove(obj, (int)SMLayerID.Normal); }

        /// <summary>
        /// Add a layer to the scene
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="index"></param>
        /// <returns>the new created scene or a already existed scene.</returns>
        public static SMLayer AddLayer(Scene scene, int index)
        {
            if (scene.DrawLayer.Keys.Contains(index))
                return scene.DrawLayer[index];

            SMLayer layer = new SMLayer();
            scene.DrawLayer.Add(index, layer);
            return layer;
        }
        /// <summary>
        /// Add a layer to the current scene
        /// </summary>
        /// <param name="index"></param>
        /// <returns>the new created scene or a already existed scene.</returns>
        public static SMLayer AddLayer(int index)
        {
            return AddLayer(Scene.Current, index);
        }
        /// <summary>
        /// Clears the specific layer, based on the clear function
        /// </summary>
        /// <param name="layer"></param>
        public static void ClearLayer(SMLayer layer)
        {
            layer.clear(layer);
        }

        /// <summary>
        /// Clears the layer of the current scene selected by the index.
        /// </summary>
        /// <param name="index"></param>
        public static void ClearLayer(int index)
        {
            if (!Scene.Current.DrawLayer.Keys.Contains(index))
                throw new Exception("[ERROR] No DrawLayer found.\nSearched layer: "+index);

            ClearLayer(Scene.Current.DrawLayer[index]);
        }
    }
}
