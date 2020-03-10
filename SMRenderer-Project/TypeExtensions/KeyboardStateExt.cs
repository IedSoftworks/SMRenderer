using System.Collections.Generic;
using System.Linq;
using OpenTK.Input;

namespace SMRenderer.TypeExtensions
{
    public static class KeyboardStateExt
    {
        public static bool AreFollowingKeysDown(this KeyboardState state, IEnumerable<Key> keys)
        {
            return keys.All(state.IsKeyDown);
        }

        public static bool AreFollowingKeysDown(this KeyboardState state, params Key[] keys)
        {
            return keys.All(state.IsKeyDown);
        }
    }
}