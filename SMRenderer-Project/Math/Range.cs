using SMRenderer.Data;

namespace SMRenderer.Math
{
    public struct Range
    {
        public static Range Zero = new Range(0);
        public static Range One = new Range(1);

        private float _min;
        private float _max;
        private bool _const;

        public int Value => ReadValue();
        public float FloatValue => ReadFloatValue();

        private int ReadValue()
        {
            if (_const) return (int) _min;

            return SMGlobals.random.Next((int) _min, (int) _max);
        }

        private float ReadFloatValue()
        {
            if (_const) return _min;

            return (float) (SMGlobals.random.NextDouble() * _max) + _min;
        }

        public Range(float value)
        {
            _min = -value;
            _max = value;
            _const = false;
        }

        public Range(float min, float max)
        {
            _min = min;
            _max = max;
            _const = false;
        }

        public static Range CreateConst(float value)
        {
            return new Range
            {
                _const = true,
                _min = value,
                _max = 0
            };
        }

        public static bool operator ==(Range first, Range second)
        {
            return first._min == second._min && first._max == second._max && first._const == second._const;
        }

        public static bool operator !=(Range first, Range second)
        {
            return !(first == second);
        }
    }
}