using System;
using System.Collections.Generic;
using SMRenderer.Data;

namespace SMRenderer.Visual.Objects
{
    [Serializable]
    public class Model : Data.Data
    {
        public List<ObjectInfos> objects = new List<ObjectInfos>();
        public List<string> RenderOrder = new List<string>();

        public Model()
        {
        }

        public Model(string id)
        {
            refName = id;
            DataManager.C.Add("Meshes", this);
        }

        protected override bool IsLoaded => true;
    }
}