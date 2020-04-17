using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using OpenTK.Graphics;
using SMRenderer.Visual.Drawing;

namespace SMRenderer.Visual
{
    /// <summary>
    /// Scenes contains the data to show something.
    /// </summary>
    [Serializable]
    public class Scene
    {
        /// <summary>
        /// The default scene, mostly nothing special.
        /// </summary>
        public static Scene Default;

        /// <summary>
        ///     Anything in this layer will be render above the DrawLayers
        ///     <para>Useful for HUDs</para>
        ///     <para>Doesn't change if the scene is changing.</para>
        /// </summary>
        public static SMLayer Overlayer = new SMLayer();

        /// <summary>
        /// If true, the Overlayer is shown.
        /// </summary>
        public static bool ShowOverlayer = true;

        /// <summary>
        /// Contains the current scene.
        /// </summary>
        private static Scene _current;

        /// <summary>
        ///     Sets the ambientLight
        /// </summary>
        public Color4 ambientLight = Color4.White;

        /// <summary>
        ///     Contains the camera of this scene
        /// </summary>
        public Camera camera = new Camera();

        /// <summary>
        ///     Contains all the drawable objects
        /// </summary>
        public Dictionary<int, SMLayer> DrawLayer;

        /// <summary>
        ///     Contains all the lights inside this scene.
        ///     <para>maximal 4 lights at the time</para>
        /// </summary>
        public LightCollection lights = new LightCollection();

        public DepthSettings depthSettings = DepthSettings.None;

        /// <summary>
        ///     Action to set the matrices for the SMLayers
        /// </summary>
        public Action<Scene> matrixSetFunc = a =>
        {
            foreach (SMLayer layer in a.DrawLayer.Values) layer.matrix = Camera.staticView;
        };

        /// <summary>
        /// Returns / Sets the current scene.
        /// </summary>
        public static Scene Current
        {
            get => _current;
            set => SetCurrent(value);
        }

        /// <summary>
        /// Sets the current scene.
        /// </summary>
        /// <param name="nextScene"></param>
        private static void SetCurrent(Scene nextScene)
        {
            GLWindow.Window.camera = nextScene.camera;
            _current = nextScene;
        }

        /// <summary>
        /// Generate the normal drawlayer system.
        /// </summary>
        /// <param name="override"></param>
        public void GenerateDrawLayer(bool @override = false)
        {
            if (DrawLayer != null && !@override) return;
            DrawLayer = new Dictionary<int, SMLayer>
            {
                {(int) SMLayerID.Skyplane, new SMLayer()},
                {(int) SMLayerID.Normal, new SMLayer {staticMatrix = false}},
                {(int) SMLayerID.HUD, new SMLayer()}
            };
            matrixSetFunc = a =>
            {
                a.DrawLayer[(int) SMLayerID.Normal].matrix = camera.ViewProjection;
                foreach (SMLayer layer in a.DrawLayer.Values.Where(b => b.staticMatrix && b.matrix != Camera.staticView)) 
                    layer.matrix = Camera.staticView;
            };
        }

        /// <summary>
        /// Serialize the scene in the stream.
        /// </summary>
        /// <param name="stream"></param>
        public void Serialize(Stream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                formatter.Serialize(stream, this);
            }
            catch (Exception e)
            {
                throw new Exception("SERIALIZATION FAILED! Reason: " + e.Message);
            }
        }

        /// <summary>
        /// Deserialize the current stream to a scene
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>The scene</returns>
        public static Scene Deserialize(Stream stream)
        {
            Scene scene;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                scene = (Scene) formatter.Deserialize(stream);
            }
            catch (SerializationException e)
            {
                throw new Exception("DESERIALIZATION FAILED! Reason: " + e.Message);
            }

            return scene;
        }
    }
}