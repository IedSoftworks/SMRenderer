using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer
{
    public class SMGlobals
    {

        public static double currentDeltaTime = 0;
        public static double currentDeltaTimeUnscaled = 0;

        public static Random random = new Random();
    }
    public class SMGl : SMGlobals { }
}
