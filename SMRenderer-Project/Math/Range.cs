using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Math
{
    public struct Range
    {
        static public Range Zero = new Range(0);
        static public Range One = new Range(1);

        private float Min;
        private float Max;
        private bool Const;

        public int Value => ReadValue();
        public float floatValue => ReadFloatValue();

        private int ReadValue()
        {
            if (Const) return (int)Min;

            return SMGlobals.random.Next((int)Min, (int)Max);
        }
        private float ReadFloatValue()
        {
            if (Const) return Min;

            return (float)(SMGlobals.random.NextDouble() * Max) + Min;
        }

        public Range(float value)
        {
            Min = -value;
            Max = value;
            Const = false;
        }
        public Range(float min, float max)
        {
            Min = min;
            Max = max;
            Const = false;
        }
        static public Range CreateConst(float value)
        {
            return new Range
            {
                Const = true,
                Min = value,
                Max = 0
            };
        }
        public static bool operator == (Range first, Range second)
        {
            return first.Min == second.Min && first.Max == second.Max && first.Const == second.Const;
        }
        public static bool operator != (Range first, Range second)
        {
            return !(first == second);
        }
    }
}
