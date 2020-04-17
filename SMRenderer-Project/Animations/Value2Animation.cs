using System;
using OpenTK;

namespace SMRenderer.Animations
{
    /// <summary>
    ///     Animation, that can animate two value
    /// </summary>
    [Serializable]
    public class Value2Animation : Animation
    {
        /// <summary>
        ///     _current value
        /// </summary>
        private Vector2 _current;

        /// <summary>
        ///     Start value
        /// </summary>
        public Vector2 from;

        /// <summary>
        ///     The saveFunction used to save the animated values
        /// </summary>
        public Action<Value2Animation, Vector2> saveFunction;

        /// <summary>
        ///     Step value
        /// </summary>
        public Vector2 step;

        /// <summary>
        ///     End value
        /// </summary>
        public Vector2 to;

        /// <summary>
        /// </summary>
        /// <param name="time">Duration</param>
        /// <param name="from">Start value</param>
        /// <param name="to">End value</param>
        /// <param name="saveFunction">Function used to save values</param>
        public Value2Animation(TimeSpan time, Vector2 from, Vector2 to,
            Action<Value2Animation, Vector2> saveFunction) : base(time)
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
            _current += step * new Vector2((float) per);
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