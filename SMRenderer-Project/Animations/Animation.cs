using System;
using System.Collections.Generic;
using System.Linq;

namespace SMRenderer.Animations
{
    /// <summary>
    ///     Animation base class
    /// </summary>
    [Serializable]
    public class Animation
    {
        public delegate void ChangeEventHandler(double renderTime);

        public delegate void EndEventHandler(Animation sender);

        public static List<Animation> animations = new List<Animation>();

        /// <summary>
        ///     Tells how long the animation took
        /// </summary>
        private double _curSeconds;

        /// <summary>
        ///     Determent if this animation is active
        /// </summary>
        public bool Active;

        /// <summary>
        ///     Contains the Current step
        /// </summary>
        public int curStep = 0;

        /// <summary>
        ///     Contains the Current object
        ///     <para>Used in save functions and for debugging</para>
        /// </summary>
        public object Object;

        /// <summary>
        ///     Contains how many steps it need to reach the endpoint
        /// </summary>
        public int Steps = 0;

        /// <summary>
        ///     Contains the duration
        /// </summary>
        public TimeSpan time;

        /// <summary>
        ///     The Constructor
        /// </summary>
        /// <param name="time">The Duration</param>
        public Animation(TimeSpan time)
        {
            this.time = time;
        }

        /// <summary>
        ///     Occur when the animation is done
        /// </summary>
        public event EndEventHandler End;

        /// <summary>
        ///     Starts the animation
        /// </summary>
        public virtual void Start()
        {
            if (Active) return;
            animations.Add(this);
            Active = true;
        }

        /// <summary>
        ///     Updates the animations
        ///     <para>Used by UpdateFrame in the GLWindow class</para>
        /// </summary>
        /// <param name="renderTime">Tells how much time past since last time</param>
        public static void Update(double renderTime)
        {
            animations.ToList().ForEach(a => a.Tick(renderTime));
        }

        /// <summary>
        /// </summary>
        /// <param name="timepast"></param>
        public virtual void Tick(double timepast)
        {
            _curSeconds += timepast;
            if (_curSeconds >= time.TotalSeconds) Stop();
        }

        /// <summary>
        ///     Stops the animation and triggers the end event
        /// </summary>
        public virtual void Stop()
        {
            if (!Active) return;
            animations.Remove(this);
            End?.Invoke(this);
            Active = false;
        }
    }
}