using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Animations
{
    [Serializable]
    public class Value3Animation : Animation
    {
        public Action<Value3Animation, Vector3> saveFunction;
        public Vector3 from;
        public Vector3 to;
        public Vector3 step;
        private Vector3 current;
        public Value3Animation(TimeSpan time, Vector3 from, Vector3 to, Action<Value3Animation, Vector3> saveFunction) : base(time)
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
            current += step * new Vector3((float)per);
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
