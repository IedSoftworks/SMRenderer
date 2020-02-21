using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Animations
{
    /// <summary>
    /// Animation, that can animate one value
    /// </summary>
    [Serializable]
    public class Value1Animation : Animation
    {
        /// <summary>
        /// The saveFunction used to save the animated values
        /// </summary>
        public Action<Value1Animation,double> saveFunction;
        /// <summary>
        /// Startvalue
        /// </summary>
        public double from;
        /// <summary>
        /// Endvalue
        /// </summary>
        public double to;
        /// <summary>
        /// Stepvalue
        /// </summary>
        public double step;
        /// <summary>
        /// Currentvalue
        /// </summary>
        private double current;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time">Duration</param>
        /// <param name="from">Startvalue</param>
        /// <param name="to">Endvalue</param>
        /// <param name="saveFunction">Function used to save values</param>
        public Value1Animation(TimeSpan time, double from, double to, Action<Value1Animation, double> saveFunction) : base(time)
        {
            this.from = from;
            this.to = to;
            this.saveFunction = saveFunction;

            step = to - from;
        }
        /// <summary>
        /// See base: <see cref="Animation.Start()"/>
        /// </summary>
        public override void Start()
        {
            base.Start();
            current = from;
            saveFunction(this, current);
        }
        /// <summary>
        /// Calculate the values together and save them.
        /// </summary>
        /// <param name="renderTime"></param>
        public override void Tick(double renderTime)
        {
            TimeSpan t = TimeSpan.FromSeconds(renderTime);
            double per = t.TotalMilliseconds / time.TotalMilliseconds;
            current += step * per;
            saveFunction(this, current);
            base.Tick(renderTime);
        }
        /// <summary>
        /// See base: <see cref="Animation.Stop()"/>
        /// </summary>
        public override void Stop()
        {
            current = to;
            saveFunction(this, current);
            base.Stop();
        }
    }
}
