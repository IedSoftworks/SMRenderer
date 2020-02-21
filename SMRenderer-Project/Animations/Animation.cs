using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SMRenderer.Animations
{
    /// <summary>
    /// Animation base class
    /// </summary>
    [Serializable]
    public class Animation
    {
        public static List<Animation> animations = new List<Animation>();
        public delegate void EndEventHandler(Animation sender);
        public delegate void ChangeEventHandler(double renderTime);
        /// <summary>
        /// Occur when the animation is done
        /// </summary>
        public event EndEventHandler End;
        
        /// <summary>
        /// Contains how many steps it need to reach the endpoint
        /// </summary>
        public int Steps = 0;
        /// <summary>
        /// Contains the current step
        /// </summary>
        public int curStep = 0;
        /// <summary>
        /// Contains the duration
        /// </summary>
        public TimeSpan time;
        /// <summary>
        /// Contains the current object
        /// <para>Used in save functions and for debugging</para>
        /// </summary>
        public object Object;
        /// <summary>
        /// Determant if this animation is active
        /// </summary>
        public bool Active = false;
        /// <summary>
        /// Tells how long the animation took
        /// </summary>
        private double curSeconds = 0;
        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="time">The Duration</param>
        public Animation(TimeSpan time)
        {
            this.time = time;
        }
        /// <summary>
        /// Starts the animation
        /// </summary>
        virtual public void Start()
        {
            if (Active) return;
            animations.Add(this);
            Active = true;
        }
        /// <summary>
        /// Updates the animations
        /// <para>Used by UpdateFrame in the GLWindow class</para>
        /// </summary>
        /// <param name="renderTime">Tells how much time past since last time</param>
        static public void Update(double renderTime)
        {
            animations.ToList().ForEach(a => a.Tick(renderTime));
        } 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timepast"></param>
        virtual public void Tick(double timepast) {
            curSeconds += timepast;
            if (curSeconds >= time.TotalSeconds) Stop();
        }
        /// <summary>
        /// Stops the animation and triggers the end event
        /// </summary>
        virtual public void Stop()
        {
            if (!Active) return;
            animations.Remove(this);
            End?.Invoke(this);
            Active = false;
        }
    }
    /// <summary>
    /// Contains Animations
    /// </summary>
    [Serializable]
    public class AnimationCollection : List<KeyValuePair<string, Animation>>
    {
        /// <summary>
        /// Returns the needed Animation based on the ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Animation this[string id] { get
            {
                return this.Find(a => a.Key == id).Value;
            } }
        /// <summary>
        /// Append a animation with a ID
        /// </summary>
        /// <param name="id">The ID</param>
        /// <param name="animation">The Animation</param>
        public void Add(string id, Animation animation)
        {
            Add(new KeyValuePair<string, Animation>(id, animation));
        }
        /// <summary>
        /// Remove a animation
        /// </summary>
        /// <param name="id"></param>
        public void Remove(string id)
        {
            Remove(Find(a => a.Key == id));
        }
        /// <summary>
        /// Restarts the animations if needed, after deserialization
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            foreach(KeyValuePair<string, Animation> pair in this)
            {
                if (pair.Value.Active)
                    Animation.animations.Add(pair.Value);
            }
        }
    }
}
