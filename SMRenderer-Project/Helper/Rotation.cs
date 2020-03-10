/*
 * Author: Lutz Karau
 * Purpose: Rotate!
 */

using OpenTK;

namespace SMRenderer.Helper
{
    public static class Rotation
    {
        private static Vector3 _zeroVector = Vector3.Zero;

        /// <summary>
        ///     Calculate a new position from a rotation
        ///     <para>Made by: Lutz Karau (KWEngine)</para>
        /// </summary>
        /// <param name="center">The position for the center</param>
        /// <param name="pos">The origin position</param>
        /// <param name="degrees">The wished degrees</param>
        /// <returns>The newly calculated position</returns>
        public static Vector2 PositionFromRotation(Vector2 center, Vector2 pos, float degrees)
        {
            float radians = MathHelper.DegreesToRadians(degrees); // ggf.: (degrees % 360)

            var finalMatrix = Matrix4.CreateTranslation(pos.X, pos.Y, 0) *
                              Matrix4.CreateRotationZ(radians) *
                              Matrix4.CreateTranslation(center.X, center.Y, 0);

            // Use the finalMatrix to move the object:
            Vector3.TransformPosition(ref _zeroVector, ref finalMatrix, out var finalPosition);
            return new Vector2(finalPosition.X, finalPosition.Y);
        }
    }
}