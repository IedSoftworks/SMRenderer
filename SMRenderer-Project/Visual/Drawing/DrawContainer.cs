using System.Collections.Generic;
using System.Linq;
using OpenTK;
using SMRenderer.Visual.Renderers;

namespace SMRenderer.Visual.Drawing
{
    /// <summary>
    ///     Can save multiple SMItems at once
    /// </summary>
    public class DrawContainer : SMItem
    {
        /// <summary>
        ///     Contains the items
        /// </summary>
        public List<SMItem> items = new List<SMItem>();

        /// <summary>
        ///     Called, when it need to draw
        /// </summary>
        /// <param name="viewMatrix">Current viewMatrix</param>
        /// <param name="renderer">The Current renderer</param>
        public override void Draw(Matrix4 viewMatrix)
        {
            items.ForEach(a => { a.Draw(viewMatrix); });
        }

        /// <summary>
        ///     Adds the item to the list
        /// </summary>
        /// <param name="i"></param>
        public void Add(SMItem i)
        {
            items.Add(i);
        }

        /// <summary>
        ///     Removes item from the list
        /// </summary>
        /// <param name="i"></param>
        public void Remove(SMItem i)
        {
            items.Remove(i);
        }

        /// <summary>
        ///     Removes any item from the list
        /// </summary>
        public void RemoveAll()
        {
            items.ToList().ForEach(a => items.Remove(a));
        }

        /// <summary>
        ///     Prepare any item for the drawing
        /// </summary>
        /// <param name="i"></param>
        public override void Prepare(double i)
        {
            items.ForEach(a => a.Prepare(i));
        }
    }
}