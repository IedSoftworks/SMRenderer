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
        private bool Const;

        public int Value => ReadValue();
        public float floatValue => ReadFloatValue();

        private int ReadValue()
        {
            if (Const) return Min;

            return SMGlobals.random.Next(Min, Max);
        }
        private float ReadFloatValue()
        {
            if (Const) return (float)Min;

            return (float)(SMGlobals.random.NextDouble() * Max) + Min;
        }

        public Range(int value)
        {
            Min = -value;
            Max = value;
            Const = false;
        }
        public Range(int min, int max)
        {
            Min = min;
            Max = max;
            Const = false;
        }
        static public Range CreateConst(int value)
        {
            return new Range
            {
                Const = true,
                Min = value,
                Max = 0
            };
        }
    }
}
