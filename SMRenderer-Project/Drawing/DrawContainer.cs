
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Drawing
{
    public class DrawContainer : SMItem
    {
        public List<DrawItem> items = new List<DrawItem>();
        public override void Draw()
        {
            items.ForEach(a => { a.Draw(); });
        }
        public void Add(DrawItem i) { items.Add(i); }
        public void Remove(DrawItem i) { items.Remove(i); }
        public void RemoveAll() { items.ToList().ForEach(a => items.Remove(a)); }
        public override void Activate()
        {
            SM.List.Add(this);
        }
        public override void Deactivate()
        {
            SM.List.Remove(this);
        }
        public override void Prepare(double i)
        {
            items.ForEach(a => a.Prepare(i));
        }
    }
}
