using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Math
{
    public struct Range
    {
        private int Min;
        private int Max;
        private bool AlwaysMin;

        public int Value => ReadValue();

        private int ReadValue()
        {
            if (AlwaysMin) return Min;

            return SMGlobals.random.Next(Min, Max);
        }

        public Range(int value)
        {
            Min = -value;
            Max = value;
            AlwaysMin = false;
        }
        public Range(int min, int max)
        {
            Min = min;
            Max = max;
            AlwaysMin = false;
        }
        static public Range CreateConst(int value)
        {
            return new Range
            {
                AlwaysMin = true,
                Min = value,
                Max = 0
            };
        }
    }
}
