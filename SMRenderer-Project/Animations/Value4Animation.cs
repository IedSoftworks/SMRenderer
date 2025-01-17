﻿using System;
using OpenTK;

namespace SMRenderer.Animations
{
    /// <summary>
    /// Animation-system for Vector4
    /// </summary>
    [Serializable]
    public class Value4Animation : Animation
    {
        /// <summary>
        ///     Current value
        /// </summary>
        private Vector4 _current;
        /// <summary>
        ///     From value
        /// </summary>

        public Vector4 from;
        /// <summary>
        ///     How much it should move
        /// </summary>
        public Vector4 step;
        /// <summary>
        ///     To value
        /// </summary>
        public Vector4 to;
        /// <summary>
        ///     Function used to save the current value
        /// </summary>
        public Action<Value4Animation, Vector4> saveFunction;

        /// <summary>
        /// Construct the animation
        /// </summary>
        /// <param name="time">How long should the animation take</param>
        /// <param name="from">From value</param>
        /// <param name="to">To value</param>
        /// <param name="saveFunction">Function used to save the current value.</param>
        public Value4Animation(TimeSpan time, Vector4 from, Vector4 to,
            Action<Value4Animation, Vector4> saveFunction) : base(time)
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
            _current += step * new Vector4((float) per);
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