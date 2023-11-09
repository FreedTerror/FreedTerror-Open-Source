using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class PaletteSwapSpriteEditorColorSliders : MonoBehaviour
    {
        [SerializeField]
        private PaletteSwapSpriteEditor paletteSwapSpriteEditor;
        [SerializeField]
        private Image colorImage;
        [SerializeField]
        private Slider redColorSlider;
        [SerializeField]
        private Slider blueColorSlider;
        [SerializeField]
        private Slider greenColorSlider;
        private Color32 colorSliderColor;
        private Color32 previousColorSliderColor;

        private void Start()
        {
            if (redColorSlider != null)
            {
                colorSliderColor.r = (byte)redColorSlider.value;
            }

            if (greenColorSlider != null)
            {
                colorSliderColor.b = (byte)greenColorSlider.value;
            }

            if (blueColorSlider != null)
            {
                colorSliderColor.g = (byte)blueColorSlider.value;
            }

            colorSliderColor.a = 255;

            previousColorSliderColor = colorSliderColor;
        }

        private void Update()
        {
            if (redColorSlider != null)
            {
                colorSliderColor.r = (byte)redColorSlider.value;
            }

            if (greenColorSlider != null)
            {
                colorSliderColor.b = (byte)greenColorSlider.value;
            }

            if (blueColorSlider != null)
            {
                colorSliderColor.g = (byte)blueColorSlider.value;
            }

            colorSliderColor.a = 255;

            if (colorImage != null)
            {
                colorImage.color = colorSliderColor;
            }

            if (paletteSwapSpriteEditor != null
                && IsMatch(previousColorSliderColor, colorSliderColor) == false)
            {
                previousColorSliderColor = new Color32(colorSliderColor.r, colorSliderColor.g, colorSliderColor.b, 255);
                paletteSwapSpriteEditor.ApplyColorCurrentPart(colorSliderColor);
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