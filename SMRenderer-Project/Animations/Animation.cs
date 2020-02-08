using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SMRenderer.Animations
{
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
        static public event ChangeEventHandler Change;
        public int Steps = 0;
        public int curStep = 0;
        public TimeSpan time;
        public object Object;
        public bool Active = false;
        private double curSeconds = 0;
        public Animation(TimeSpan time)
        {
            this.time = time;
        }
        virtual public void Start()
        {
            animations.Add(this);
            Active = true;
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
            Active = false;
        }
    }
    [Serializable]
    public class AnimationCollection : List<KeyValuePair<string, Animation>>
    {
        public Animation this[string id] { get
            {
                return this.Find(a => a.Key == id).Value;
            } }
        public void Add(string id, Animation animation)
        {
            Add(new KeyValuePair<string, Animation>(id, animation));
        }
        public void Remove(string id)
        {
            Remove(Find(a => a.Key == id));
        }
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
