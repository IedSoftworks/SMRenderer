using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Animations
{
    /// <summary>
    /// Animation, that can animate two value
    /// </summary>
    [Serializable]
    public class Value2Animation : Animation
    {
        /// <summary>
        /// The saveFunction used to save the animated values
        /// </summary>
        public Action<Value2Animation, Vector2> saveFunction;

        /// <summary>
        /// Startvalue
        /// </summary>
        public Vector2 from;

        /// <summary>
        /// Endvalue
        /// </summary>
        public Vector2 to;
        /// <summary>
        /// Stepvalue
        /// </summary>
        public Vector2 step;
        /// <summary>
        /// Currentvalue
        /// </summary>
        private Vector2 current;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time">Duration</param>
        /// <param name="from">Startvalue</param>
        /// <param name="to">Endvalue</param>
        /// <param name="saveFunction">Function used to save values</param>
        public Value2Animation(TimeSpan time, Vector2 from, Vector2 to, Action<Value2Animation, Vector2> saveFunction) : base(time)
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
            current += step * new Vector2((float)per);
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
