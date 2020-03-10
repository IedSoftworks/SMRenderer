using System.Collections.Generic;
using OpenTK.Input;
using SMRenderer.Visual;

namespace SMRenderer.Input
{
    public class GameController
    {
        public delegate void ConnectionEventArgs(int controllerId);

        public static int maxSlots = 2;
        private GLWindow _window;

        public GameController(GLWindow window)
        {
            _window = window;
        }

        public Dictionary<int, bool> ActiveGamePads { get; private set; }

        public event ConnectionEventArgs Connect;
        public event ConnectionEventArgs Disconnect;

        internal void Check()
        {
            if (ActiveGamePads == null)
            {
                ActiveGamePads = new Dictionary<int, bool>();
                for (int i = 0; i < maxSlots; i++) ActiveGamePads.Add(i, false);
            }

            for (int i = 0; i < maxSlots; i++)
                if (!ActiveGamePads[i] && GamePad.GetName(i) != "")
                {
                    ActiveGamePads[i] = true;
                    Connect?.Invoke(i);
                }
                else if (ActiveGamePads[i] && GamePad.GetName(i) == "")
                {
                    ActiveGamePads[i] = false;
                    Disconnect?.Invoke(i);
                }
        }
    }
}