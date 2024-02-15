using UnityEngine;

namespace FreedTerror
{
    [CreateAssetMenu(menuName = "FreedTerror/Scroll/Scroll Data", fileName = "New Scroll Data")]
    public class ScrollDataScriptableObject : ScriptableObject
    {
        [Header("Horizontal Scroll")]
        public float horizontalScrollAcceleration;  
        public float minHorizontalScrollSpeed;
        public float maxHorizontalScrollSpeed;

        [Header("Vertical Scroll")]
        public float verticalScrollAcceleration;
        public float minVerticalScrollSpeed;    
        public float maxVerticalScrollSpeed;
    }
}