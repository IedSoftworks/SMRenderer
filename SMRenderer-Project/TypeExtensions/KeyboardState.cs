using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMRenderer.TypeExtensions
{
    public static class KeyboardStateExt
    {
        public static bool AreFollowingKeysDown(this KeyboardState state, IEnumerable<Key> keys)
        {
            return keys.All(a => state.IsKeyDown(a));
        }
        public static bool AreFollowingKeysDown(this KeyboardState state, params Key[] keys)
        {
            return keys.All(a => state.IsKeyDown(a));
        }
    }
}
