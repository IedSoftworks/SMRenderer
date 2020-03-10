using System;
using OpenTK;

namespace SMRenderer.Animations
{
    [Serializable]
    public class Value4Animation : Animation
    {
        private Vector4 _current;
        public Vector4 from;
        public Action<Value4Animation, Vector4> saveFunction;
        public Vector4 step;
        public Vector4 to;

        public Value4Animation(TimeSpan time, Vector4 from, Vector4 to,
            Action<Value4Animation, Vector4> saveFunction) : base(time)
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
            _current += step * new Vector4((float) per);
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