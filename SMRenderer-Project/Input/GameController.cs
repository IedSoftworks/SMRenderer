using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.Input
{
    public class GameController
    {
        public Dictionary<int, bool> ActiveGamePads { get; private set; }
        private GLWindow window;
        public static int maxSlots = 2;
        public delegate void ConnectionEventArgs(int ControllerID);

        public event ConnectionEventArgs Connect;
        public event ConnectionEventArgs Disconnect;
        public GameController(GLWindow window) { this.window = window; }
        internal void Check()
        {
            if (ActiveGamePads == null)
            {
                ActiveGamePads = new Dictionary<int, bool>();
                for(int i = 0; i < maxSlots; i++)
                {
                    ActiveGamePads.Add(i, false);
                }
            }

            for(int i = 0; i < maxSlots; i++)
            {
                if (!ActiveGamePads[i] && GamePad.GetName(i) != "")
                {
                    ActiveGamePads[i] = true;
                    Connect?.Invoke(i);
                } else if (ActiveGamePads[i] && GamePad.GetName(i) == "")
                {
                    ActiveGamePads[i] = false;
                    Disconnect?.Invoke(i);
                }
            }
        }
    }
}
