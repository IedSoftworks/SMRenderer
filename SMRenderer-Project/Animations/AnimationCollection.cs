using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SMRenderer.Animations
{
    /// <summary>
    ///     Contains Animations
    /// </summary>
    [Serializable]
    public class AnimationCollection : List<KeyValuePair<string, Animation>>
    {
        /// <summary>
        ///     Returns the needed Animation based on the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Animation this[string id]
        {
            get { return Find(a => a.Key == id).Value; }
        }

        /// <summary>
        ///     Append a animation with a ID
        /// </summary>
        /// <param name="id">The ID</param>
        /// <param name="animation">The Animation</param>
        public void Add(string id, Animation animation)
        {
            Add(new KeyValuePair<string, Animation>(id, animation));
        }

        /// <summary>
        ///     Remove a animation
        /// </summary>
        /// <param name="id"></param>
        public void Remove(string id)
        {
            Remove(Find(a => a.Key == id));
        }

        /// <summary>
        ///     Restarts the animations if needed, after deserialization
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            foreach (KeyValuePair<string, Animation> pair in this)
                if (pair.Value.Active)
                    Animation.animations.Add(pair.Value);
        }
    }
}