using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Animations
{
    public class Value1Animation : Animation
    {
        public Action<Value1Animation,double> saveFunction;
        public double from;
        public double to;
        public double step;
        private double current;
        public Value1Animation(TimeSpan time, double from, double to, Action<Value1Animation, double> saveFunction) : base(time)
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
            current += step * per;
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
