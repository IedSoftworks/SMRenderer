using System;

namespace SMRenderer.Data
{
    public class SMGl : SMGlobals
    {
    }

    public class SMGlobals
    {
        public static double currentDeltaTime = 0;
        public static double currentDeltaTimeUnscaled = 0;

        public static Random random = new Random();
    }
}