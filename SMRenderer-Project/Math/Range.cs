using SMRenderer.Data;

namespace SMRenderer.Math
{
    /// <summary>
    /// Represents a range of two values, that can be randomly selected.  
    /// </summary>
    public struct Range
    {
        /// <summary>
        /// Static range of 0.
        /// </summary>
        public static Range Zero = new Range(0);
        /// <summary>
        /// Static range of 1.
        /// </summary>
        public static Range One = new Range(1);

        /// <summary>
        /// The minimum
        /// </summary>
        private float _min;
        /// <summary>
        /// The maximum
        /// </summary>
        private float _max;
        /// <summary>
        /// Determinants if its constant
        /// </summary>
        private bool _const;

        /// <summary>
        /// Generates a value (int).
        /// </summary>
        public int Value => ReadValue();
        /// <summary>
        /// Generates a value (float).
        /// </summary>
        public float FloatValue => ReadFloatValue();

        /// <summary>
        /// Generate a value between minimum and maximum.
        /// </summary>
        /// <returns>Integer</returns>
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
        /// <summary>
        /// Constructor, that set the minimum to a negative and maximum to the positive of the value
        /// </summary>
        /// <param name="value">The value</param>
        public Range(float value)
        {
            _min = -value;
            _max = value;
            _const = false;
        }

        /// <summary>
        /// Constuctor, that set minimum and maximum.
        /// </summary>
        /// <param name="min">The minimum</param>
        /// <param name="max">The maximum</param>
        public Range(float min, float max)
        {
            _min = min;
            _max = max;
            _const = false;
        }
        /// <summary>
        /// Generate a constant.
        /// </summary>
        /// <param name="value">The constant value</param>
        /// <returns>The range</returns>
        public static Range CreateConst(float value)
        {
            return new Range
            {
                _const = true,
                _min = value,
                _max = 0
            };
        }

        /// <summary>
        /// Equal function
        /// </summary>
        /// <param name="first">Left</param>
        /// <param name="second">Right</param>
        /// <returns></returns>
        public static bool operator ==(Range first, Range second)
        {
            return first._min == second._min && first._max == second._max && first._const == second._const;
        }
        /// <summary>
        /// Unequal function
        /// </summary>
        /// <param name="first">Left</param>
        /// <param name="second">Right</param>
        /// <returns></returns>
        public static bool operator !=(Range first, Range second)
        {
            return !(first == second);
        }
    }
}