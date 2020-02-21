using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Animations
{
    [Serializable]
    public class Value4Animation : Animation
    {
        public Action<Value4Animation, Vector4> saveFunction;
        public Vector4 from;
        public Vector4 to;
        public Vector4 step;
        private Vector4 current;
        public Value4Animation(TimeSpan time, Vector4 from, Vector4 to, Action<Value4Animation, Vector4> saveFunction) : base(time)
        {
            this.from = from;
            this.to = to;
            this.saveFunction = saveFunction;

            step = to - from;
        }
        public override void Start()
        {
            base.Start();
            current = from;
            saveFunction(this, current);
        }
        public override void Tick(double renderTime)
        {
            TimeSpan t = TimeSpan.FromSeconds(renderTime);
            double per = t.TotalMilliseconds / time.TotalMilliseconds;
            current += step * new Vector4((float)per);
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
