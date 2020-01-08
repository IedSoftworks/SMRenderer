using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Objects
{
    public class QuadRepeat : Object
    {
        public QuadRepeat()
        {
            Vertices = new float[] {
                -.5f,-.5f,0f,
                -.5f,+.5f,0f,
                +.5f,+.5f,0f,
                +.5f,-.5f,0f
            };
            UVs = new float[] {
                0,0,
                0,10f,
                10f,10f,
                10f,0
            };
            Normals = new float[]{
                0,0,1,
                0,0,1,
                0,0,1,
                0,0,1,
            };
            primitiveType = PrimitiveType.Quads;
        }
    }
}
