using OpenTK;

namespace SMRenderer.Visual
{
    public struct DepthSettings
    {
        public static DepthSettings None = new DepthSettings() {Factor = 0, MaxHeight=0, MinHeight = 0};
        public static DepthSettings Default = new DepthSettings() {Factor = 0.2f, MinHeight = -5, MaxHeight = 5};

        public float Factor;
        public float MinHeight;
        public float MaxHeight;

        public Vector3 ShaderArgument()
        {
            return new Vector3(MinHeight, MaxHeight, Factor);
        }
    }
}