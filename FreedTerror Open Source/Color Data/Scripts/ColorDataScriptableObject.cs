using UnityEngine;

namespace FreedTerror
{
    [CreateAssetMenu(menuName = "FreedTerror/Color/Color Data", fileName = "New Color Data")]
    public class ColorDataScriptableObject : ScriptableObject
    {
        public Color32 color;

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
    }
}
