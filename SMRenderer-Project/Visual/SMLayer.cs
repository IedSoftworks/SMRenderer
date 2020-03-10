using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace SMRenderer.Visual
{
    [Serializable]
    public class SMLayer : List<SMItem>
    {
        public Action<SMLayer> clear = a => { a.ForEach(b => a.Remove(b)); };
        public Matrix4 matrix = Camera.staticView;
        public int renderer = GLWindow.Window.rendererList["GeneralRenderer"];
        public bool staticMatrix = true;

        public void Order()
        {
            Console.WriteLine();
            SMItem[] orde = this.OrderBy(a => a.RenderOrder).ToArray();
            RemoveAll(a => true);
            AddRange(orde);
            Console.WriteLine();
        }
    }
}