using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Animations
{
    class SizeAnimation : Animation
    {
        Vector2 from;
        Vector2 to;
        Vector2 step;

        public SizeAnimation(DrawItem item, TimeSpan time, Vector2 from, Vector2 to) : base(item, time)
        {
            this.to = to;
            this.from = from;

            step = new Vector2((to.X - from.X) / (float)Steps, (to.Y - from.Y) / (float)Steps);
        }
        public override void Start()
        {
            base.Start();
            item.Size.X = from.X;
            item.Size.Y = from.Y;
        }
        public override void Tick(Timer sender)
        {
            base.Tick(sender);
            item.Size.X += step.X;
            item.Size.Y += step.Y;
        }
        public override void Stop()
        {
            base.Stop();
            item.Size.X = to.X;
            item.Size.Y = to.Y;
        }
    }
}
