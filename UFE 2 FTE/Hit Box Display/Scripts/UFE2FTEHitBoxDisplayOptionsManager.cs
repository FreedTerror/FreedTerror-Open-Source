namespace UFE2FTE
{
    public static class UFE2FTEHitBoxDisplayOptionsManager
    {
        public enum DisplayMode
        {
            Off,
            SpriteRenderer2DInfront,
            SpriteRenderer2DBehind,
            MeshRenderer3D,
            PopcronGizmos2D,
            PopcronGizmos3D
        }
        public static DisplayMode displayMode;
        public static byte alphaValue;
        public static bool useProjectileTotalHitsText;
    }
}