using System;
using System.Collections.Generic;
using System.Linq;

namespace SMRenderer.Animations
{
    public class Animation
    {
        static List<Animation> animations = new List<Animation>();
        public delegate void EndEventHandler(Animation sender);
        public delegate void ChangeEventHandler(double renderTime);
        /// <summary>
        /// Occur when the animation is done
        /// </summary>
        public event EndEventHandler End;
        static public event ChangeEventHandler Change;
        public int Steps = 0;
        public int curStep = 0;
        public TimeSpan time;
        private double curSeconds = 0;
        public Animation(TimeSpan time)
        {
            this.time = time;
        }
        virtual public void Start()
        {
            animations.Add(this);
        }
        static public void Update(double renderTime)
        {
            animations.ToList().ForEach(a => a.Tick(renderTime));
            Change?.Invoke(renderTime);
        } 
        virtual public void Tick(double timepast) {
            curSeconds += timepast;
            if (curSeconds >= time.TotalSeconds) Stop();
        }
        virtual public void Stop()
        {
            animations.Remove(this);
            End?.Invoke(this);
        }
    }
}
