/*
 * Author: Lutz Karau
 * Purpose: Rotate!
 */
using OpenTK;

namespace SMRenderer.Helper
{
    public static class Rotation
    {
        private static Vector3 zeroVector = Vector3.Zero;
        public static Vector2 CalculatePositionForRotationAroundPoint(Vector2 center, Vector2 pos, float degrees)
        {
            Matrix4 finalMatrix = Matrix4.Zero;
            Vector3 finalPosition = Vector3.Zero;
            float radians = MathHelper.DegreesToRadians(degrees); // ggf.: (degrees % 360)

            finalMatrix = Matrix4.CreateTranslation(pos.X, pos.Y, 0) *
                Matrix4.CreateRotationZ(radians) *
                Matrix4.CreateTranslation(center.X, center.Y, 0);

            // Nutze die finale Matrix, um das Objekt an die gewünschte Uhrzeit zu verschieben:
            Vector3.TransformPosition(ref zeroVector, ref finalMatrix, out finalPosition);
            finalMatrix = Matrix4.Zero;
            return new Vector2(finalPosition.X, finalPosition.Y);
        }
    }
}