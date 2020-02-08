using OpenTK.Graphics;
using SMRenderer.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer
{
    [Serializable]
    public class Scene
    {
        public static Scene _default;

        static Scene _current;
        public static Scene current { get => _current; set { SetCurrent(value); } }

        static void SetCurrent(Scene nextScene)
        {
            GLWindow.Window.camera = nextScene.camera;
            _current = nextScene;
        }

        public void GenerateDrawLayer(bool @override = false)
        {
            if (DrawLayer != null && !@override) return;
            DrawLayer = new Dictionary<int, SMLayer>()
            {
                { (int)SMLayerID.Skyplane, new SMLayer() },
                { (int)SMLayerID.Normal, new SMLayer() { staticMatrix = false } },
                { (int)SMLayerID.HUD, new SMLayer() },
            };
            matrixSetFunc = a =>
            {
                a.DrawLayer[(int)SMLayerID.Normal].matrix = camera.viewProjection;
                foreach (SMLayer layer in a.DrawLayer.Values.Where(b => b.staticMatrix && b.matrix != Camera.staticView)) layer.matrix = Camera.staticView;
            };
        }

        /// <summary>
        /// Sets the ambientLight
        /// </summary>
        public Color4 ambientLight = Color4.DarkCyan;

        /// <summary>
        /// Contains all the drawable objects
        /// </summary>
        public Dictionary<int, SMLayer> DrawLayer = null;

        /// <summary>
        /// used to set the matrices for the SMLayers
        /// </summary>
        public Action<Scene> matrixSetFunc = a => { foreach (SMLayer layer in a.DrawLayer.Values) layer.matrix = Camera.staticView; };

        /// <summary>
        /// Contains the camera of this scene
        /// </summary>
        public Camera camera = new Camera();

        /// <summary>
        /// Contains all the lights inside this scene.
        /// <para>maximal 4 lights at the time</para>
        /// </summary>
        public LightCollection lights = new LightCollection();
    }
}
