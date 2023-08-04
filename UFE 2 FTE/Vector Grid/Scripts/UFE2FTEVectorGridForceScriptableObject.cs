using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Vector Grid Force", menuName = "U.F.E. 2 F.T.E./Vector Grid/Vector Grid Force")]
    public class UFE2FTEVectorGridForceScriptableObject : ScriptableObject
    {
        public Vector3 forceOffset;
        public float forceScale;
        public float radius;
        public bool hasColor;
        public Color color;
    }
}
