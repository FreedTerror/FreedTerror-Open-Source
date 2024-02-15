using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class PaletteSwapSpriteEditorApplyColorImageController : MonoBehaviour
    {
        [SerializeField]
        private PaletteSwapSpriteEditor paletteSwapSpriteEditor;
        [SerializeField]
        private Image colorImage;
        private Color32 currentImageColor;
        private Color32 previousImageColor;

        private void Start()
        {
            if (colorImage != null)
            {
                currentImageColor = colorImage.color;
                currentImageColor.a = 255;

                previousImageColor = currentImageColor;
            }
        }

        private void LateUpdate()
        {
            if (colorImage != null)
            {
                currentImageColor = colorImage.color;
                currentImageColor.a = 255;

                if (IsMatch(previousImageColor, currentImageColor) == false)
                {
                    previousImageColor = currentImageColor;

                    if (paletteSwapSpriteEditor != null)
                    {
                        paletteSwapSpriteEditor.ApplyColorCurrentPart(previousImageColor);
                    }
                }           
            }
        }

        private static bool IsMatch(Color32 comparing, Color32 matching)
        {
            return comparing.r == matching.r
                && comparing.g == matching.g
                && comparing.b == matching.b
                && comparing.a == matching.a;
        }
    }
}