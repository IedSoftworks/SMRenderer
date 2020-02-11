using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Animations
{
    [Serializable]
    public class Value2Animation : Animation
    {
        public Action<Value2Animation, Vector2> saveFunction;
        public Vector2 from;
        public Vector2 to;
        public Vector2 step;
        private Vector2 current;
        public Value2Animation(TimeSpan time, Vector2 from, Vector2 to, Action<Value2Animation, Vector2> saveFunction) : base(time)
        {
            this.from = from;
            this.to = to;
            this.saveFunction = saveFunction;

            step = to - from;
        }
        public override void Start()
        {
            current = from;
            saveFunction(this, current);
            base.Start();
        }
        public override void Tick(double renderTime)
        {
            TimeSpan t = TimeSpan.FromSeconds(renderTime);
            double per = t.TotalMilliseconds / time.TotalMilliseconds;
            current += step * new Vector2((float)per);
            saveFunction(this, current);
            base.Tick(renderTime);
        }
        public override void Stop()
        {
            current = to;
            saveFunction(this, current);
            base.Stop();
        }
    }
}
