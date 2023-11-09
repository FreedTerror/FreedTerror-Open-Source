using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(menuName = "U.F.E. 2 F.T.E./Scroll Rect/Scroll Data", fileName = "New Scroll Rect Scroll Data")]
    public class ScrollRectScrollDataScriptableObject : ScriptableObject
    {
        public float horizontalScrollAcceleration;
        public float verticalScrollAcceleration;
        public float horizontalScrollSpeed;
        public float verticalScrollSpeed;
        public float horizontalMaxScrollSpeed;
        public float verticalMaxScrollSpeed;
    }
}