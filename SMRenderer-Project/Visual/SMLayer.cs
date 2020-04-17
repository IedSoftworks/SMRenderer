using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace SMRenderer.Visual
{
    /// <summary>
    /// Contains all SMItems, specifies matrices and rendereres,
    /// </summary>
    [Serializable]
    public class SMLayer : List<SMItem>
    {
        /// <summary>
        /// Clear function, when SM.ClearLayer is executed.
        /// </summary>
        public Action<SMLayer> clear = a => { a.ForEach(b => a.Remove(b)); };
        /// <summary>
        /// layer specific view matrix
        /// </summary>
        public Matrix4 matrix = Camera.staticView;
        /// <summary>
        /// If true, it uses the static view matrix.
        /// </summary>
        public bool staticMatrix = true;
        
        /// <summary>
        /// Orders the items based on they render orders.
        /// </summary>
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