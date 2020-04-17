using System;

namespace SMRenderer.Animations
{
    /// <summary>
    ///     Animation, that can animate one value
    /// </summary>
    [Serializable]
    public class Value1Animation : Animation
    {
        /// <summary>
        ///     _current value
        /// </summary>
        private double _current;

        /// <summary>
        ///     Start value
        /// </summary>
        public double from;

        /// <summary>
        ///     The saveFunction used to save the animated values
        /// </summary>
        public Action<Value1Animation, double> saveFunction;

        /// <summary>
        ///     Step value
        /// </summary>
        public double step;

        /// <summary>
        ///     End value
        /// </summary>
        public double to;

        /// <summary>
        /// </summary>
        /// <param name="time">Duration</param>
        /// <param name="from">Start value</param>
        /// <param name="to">End value</param>
        /// <param name="saveFunction">Function used to save values</param>
        public Value1Animation(TimeSpan time, double from, double to, Action<Value1Animation, double> saveFunction) :
            base(time)
        {
            this.from = from;
            this.to = to;
            this.saveFunction = saveFunction;

            step = to - from;
        }

        /// <summary>
        ///     See base: <see cref="Animation.Start()" />
        /// </summary>
        public override void Start()
        {
            base.Start();
            _current = from;
            saveFunction(this, _current);
        }

        /// <summary>
        ///     Calculate the values together and save them.
        /// </summary>
        /// <param name="renderTime"></param>
        public override void Tick(double renderTime)
        {
            TimeSpan t = TimeSpan.FromSeconds(renderTime);
            double per = t.TotalMilliseconds / time.TotalMilliseconds;
            _current += step * per;
            saveFunction(this, _current);
            base.Tick(renderTime);
        }

        /// <summary>
        ///     See base: <see cref="Animation.Stop()" />
        /// </summary>
        public override void Stop()
        {
            _current = to;
            saveFunction(this, _current);
            base.Stop();
        }
    }
}