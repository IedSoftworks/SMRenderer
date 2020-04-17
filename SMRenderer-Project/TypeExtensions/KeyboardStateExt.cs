using System.Collections.Generic;
using System.Linq;
using OpenTK.Input;

namespace SMRenderer.TypeExtensions
{
    /// <summary>
    /// Extensions to KeyboardState
    /// </summary>
    public static class KeyboardStateExt
    {
        /// <summary>
        /// Returns true, if all keys are down. (Any IEnumerable)
        /// </summary>
        /// <param name="state">The state, that should be checked</param>
        /// <param name="keys">The keys, that needs to check</param>
        /// <returns></returns>
        public static bool AreFollowingKeysDown(this KeyboardState state, IEnumerable<Key> keys)
        {
            return keys.All(state.IsKeyDown);
        }
        /// <summary>
        /// Returns true, if all keys are down. (params)
        /// </summary>
        /// <param name="state">The state, that should be checked</param>
        /// <param name="keys">The keys, that needs to check</param>
        /// <returns></returns>
        public static bool AreFollowingKeysDown(this KeyboardState state, params Key[] keys)
        {
            return keys.All(state.IsKeyDown);
        }
    }
}