using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu]
    public class ColorDataScriptableObject : ScriptableObject
    {
        public Color32 color;

#if UNITY_EDITOR
        [SerializeField]
        private string hexColorCode;
        [NaughtyAttributes.Button]
        private void GenerateHexCodeColor()
        {
            Color newColor;
            if (ColorUtility.TryParseHtmlString(hexColorCode, out newColor) == false)
            {
                return;
            }
            color = newColor;
        }
#endif
    }
}
