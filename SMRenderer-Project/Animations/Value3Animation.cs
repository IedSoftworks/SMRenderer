using System;
using OpenTK;

namespace SMRenderer.Animations
{
    /// <summary>
    /// Animation-system for Vector3
    /// </summary>
    [Serializable]
    public class Value3Animation : Animation
    {
        /// <summary>
        ///     Current value
        /// </summary>
        private Vector3 _current;
        /// <summary>
        ///     From value
        /// </summary>
        public Vector3 from;
        /// <summary>
        ///     How much it should move
        /// </summary>
        public Vector3 step;
        /// <summary>
        ///     To value
        /// </summary>
        public Vector3 to;
        /// <summary>
        ///     Function used to save the current value
        /// </summary>
        public Action<Value3Animation, Vector3> saveFunction;


        /// <summary>
        /// Construct the animation
        /// </summary>
        /// <param name="time">How long should the animation take</param>
        /// <param name="from">From value</param>
        /// <param name="to">To value</param>
        /// <param name="saveFunction">Function used to save the current value.</param>
        public Value3Animation(TimeSpan time, Vector3 from, Vector3 to,
            Action<Value3Animation, Vector3> saveFunction) : base(time)
        {
            this.from = from;
            this.to = to;
            this.saveFunction = saveFunction;

            step = to - from;
        }

        /// <inheritdoc />
        public override void Start()
        {
            base.Start();
            _current = from;
            saveFunction(this, _current);
        }

        /// <inheritdoc />
        public override void Tick(double renderTime)
        {
            TimeSpan t = TimeSpan.FromSeconds(renderTime);
            double per = t.TotalMilliseconds / time.TotalMilliseconds;
            _current += step * new Vector3((float) per);
            saveFunction(this, _current);
            base.Tick(renderTime);
        }

        /// <inheritdoc />
        public override void Stop()
        {
            _current = to;
            saveFunction(this, _current);
            base.Stop();
        }
    }
}