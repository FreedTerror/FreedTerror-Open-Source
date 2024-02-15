using UnityEngine;

namespace FreedTerror.TTE
{
    [CreateAssetMenu(menuName = "FreedTerror/TTE/Sprite/Sprite References", fileName = "New Sprite References")]
    public class SpriteReferencesScriptableObject : ScriptableObject
    {
        [System.Serializable]
        public class Data
        {
#if UNITY_EDITOR
            [SerializeField]
            private string name;
#endif
            public Sprite[] colorSpriteArray;
            public Sprite[] highlightSpriteArray;
            public Sprite[] shadowSpriteArray;
        }
        public Data[] dataArray;
    }
}