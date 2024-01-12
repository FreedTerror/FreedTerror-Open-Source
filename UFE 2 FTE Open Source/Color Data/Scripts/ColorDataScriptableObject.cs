using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Color Data", menuName = "U.F.E. 2 F.T.E./Color/Color Data")]
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
