using System;
using OpenTK;

namespace SMRenderer.Animations
{
    [Serializable]
    public class Value3Animation : Animation
    {
        private Vector3 _current;
        public Vector3 from;
        public Action<Value3Animation, Vector3> saveFunction;
        public Vector3 step;
        public Vector3 to;

        public Value3Animation(TimeSpan time, Vector3 from, Vector3 to,
            Action<Value3Animation, Vector3> saveFunction) : base(time)
        {
            this.from = from;
            this.to = to;
            this.saveFunction = saveFunction;

            step = to - from;
        }

        public override void Start()
        {
            base.Start();
            _current = from;
            saveFunction(this, _current);
        }

        public override void Tick(double renderTime)
        {
            TimeSpan t = TimeSpan.FromSeconds(renderTime);
            double per = t.TotalMilliseconds / time.TotalMilliseconds;
            _current += step * new Vector3((float) per);
            saveFunction(this, _current);
            base.Tick(renderTime);
        }

        public override void Stop()
        {
            _current = to;
            saveFunction(this, _current);
            base.Stop();
        }
    }
}