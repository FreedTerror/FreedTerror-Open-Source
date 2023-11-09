using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class PaletteSwapSpriteEditorColorPickerSimple : MonoBehaviour
    {
        [SerializeField]
        private PaletteSwapSpriteEditor paletteSwapSpriteEditor;
        [SerializeField]
        private Image colorImage;
        [SerializeField]
        private Text colorTypeText;

        private enum ColorType
        {
            BlackColors,
            WhiteColors,
            GreyColors,
            RedColors,
            GreenColors,
            BlueColors,
            OrangeColors,
            YellowColors,
            PurpleColors,
            PinkColors,
            BrownColors,
            CyanColors,
            GoldColors
        }
        private ColorType currentColorType;
        private ColorType previousColorType;

        [SerializeField]
        private ColorDataScriptableObjectManager blackColorDataScriptableObjectManager;
        [SerializeField]
        private ColorDataScriptableObjectManager whiteColorDataScriptableObjectManager;
        [SerializeField]
        private ColorDataScriptableObjectManager greyColorDataScriptableObjectManager;
        [SerializeField]
        private ColorDataScriptableObjectManager redColorDataScriptableObjectManager;
        [SerializeField]
        private ColorDataScriptableObjectManager greenColorDataScriptableObjectManager;
        [SerializeField]
        private ColorDataScriptableObjectManager blueColorDataScriptableObjectManager;
        [SerializeField]
        private ColorDataScriptableObjectManager orangeColorDataScriptableObjectManager;
        [SerializeField]
        private ColorDataScriptableObjectManager yellowColorDataScriptableObjectManager;
        [SerializeField]
        private ColorDataScriptableObjectManager purpleColorDataScriptableObjectManager;
        [SerializeField]
        private ColorDataScriptableObjectManager pinkColorDataScriptableObjectManager;
        [SerializeField]
        private ColorDataScriptableObjectManager brownColorDataScriptableObjectManager;
        [SerializeField]
        private ColorDataScriptableObjectManager cyanColorDataScriptableObjectManager;
        [SerializeField]
        private ColorDataScriptableObjectManager goldColorDataScriptableObjectManager;
        private int colorDataScriptableObjectArrayIndex;

        private void Start()
        {
            previousColorType = currentColorType;
            if (colorTypeText != null)
            {
                colorTypeText.text = UFE2FTE.AddSpacesBeforeCapitalLetters(currentColorType.ToString());
            }
        }

        private void Update()
        {
            if (colorTypeText != null
                && previousColorType != currentColorType)
            {
                previousColorType = currentColorType;
                colorTypeText.text = UFE2FTE.AddSpacesBeforeCapitalLetters(currentColorType.ToString());
            }
        }

        #region Color Type Methods

        public void NextColorType()
        {
            currentColorType = GetNextEnum(currentColorType);
            colorDataScriptableObjectArrayIndex = -1;
        }

        public void PreviousColorType()
        {
            currentColorType = GetPreviousEnum(currentColorType);
            colorDataScriptableObjectArrayIndex = -1;
        }

        private ColorType GetNextEnum(ColorType value)
        {
            int index = (int)value;
            index += 1;
            if (System.Enum.IsDefined(typeof(ColorType), (ColorType)index) == false)
            {
                index = 0;
            }
            return (ColorType)index;
        }

        private ColorType GetPreviousEnum(ColorType value)
        {
            int index = (int)value;
            index -= 1;
            if (System.Enum.IsDefined(typeof(ColorType), (ColorType)index) == false)
            {
                index = System.Enum.GetValues(typeof(ColorType)).Length - 1;
            }
            return (ColorType)index;
        }

        #endregion

        #region Color Data Methods

        public void NextColorData()
        {
            if (GetColorDataScriptableObjectManager() == null
                || GetColorDataScriptableObjectManager().colorDataScriptableObjectArray == null
                || GetColorDataScriptableObjectManager().colorDataScriptableObjectArray.Length <= 0)
            {
                return;
            }

            colorDataScriptableObjectArrayIndex += 1;

            if (colorDataScriptableObjectArrayIndex >= GetColorDataScriptableObjectManager().colorDataScriptableObjectArray.Length)
            {
                colorDataScriptableObjectArrayIndex = 0;
            }

            if (colorImage != null)
            {
                colorImage.color = GetColorDataScriptableObjectManager().colorDataScriptableObjectArray[colorDataScriptableObjectArrayIndex].color;
            }

            if (paletteSwapSpriteEditor != null)
            {
                paletteSwapSpriteEditor.ApplyColorCurrentPart(GetColorDataScriptableObjectManager().colorDataScriptableObjectArray[colorDataScriptableObjectArrayIndex].color);
            }
        }

        public void PreviousColorData()
        {
            if (GetColorDataScriptableObjectManager() == null
                || GetColorDataScriptableObjectManager().colorDataScriptableObjectArray == null
                || GetColorDataScriptableObjectManager().colorDataScriptableObjectArray.Length <= 0)
            {
                return;
            }

            colorDataScriptableObjectArrayIndex -= 1;

            if (colorDataScriptableObjectArrayIndex < 0)
            {
                colorDataScriptableObjectArrayIndex = GetColorDataScriptableObjectManager().colorDataScriptableObjectArray.Length - 1;
            }

            if (colorImage != null)
            {
                colorImage.color = GetColorDataScriptableObjectManager().colorDataScriptableObjectArray[colorDataScriptableObjectArrayIndex].color;
            }

            if (paletteSwapSpriteEditor != null)
            {
                paletteSwapSpriteEditor.ApplyColorCurrentPart(GetColorDataScriptableObjectManager().colorDataScriptableObjectArray[colorDataScriptableObjectArrayIndex].color);
            }
        }

        private ColorDataScriptableObjectManager GetColorDataScriptableObjectManager()
        {
            switch (currentColorType)
            {
                case ColorType.BlackColors:
                    return blackColorDataScriptableObjectManager;

                case ColorType.WhiteColors:
                    return whiteColorDataScriptableObjectManager;

                case ColorType.GreyColors:
                    return greyColorDataScriptableObjectManager;

                case ColorType.RedColors:
                    return redColorDataScriptableObjectManager;

                case ColorType.GreenColors:
                    return greenColorDataScriptableObjectManager;

                case ColorType.BlueColors:
                    return blueColorDataScriptableObjectManager;

                case ColorType.OrangeColors:
                    return orangeColorDataScriptableObjectManager;

                case ColorType.YellowColors:
                    return yellowColorDataScriptableObjectManager;

                case ColorType.PurpleColors:
                    return purpleColorDataScriptableObjectManager;

                case ColorType.PinkColors:
                    return pinkColorDataScriptableObjectManager;

                case ColorType.BrownColors:
                    return brownColorDataScriptableObjectManager;

                case ColorType.CyanColors:
                    return cyanColorDataScriptableObjectManager;

                case ColorType.GoldColors:
                    return goldColorDataScriptableObjectManager;

                default:
                    return null;
            }
        }

        #endregion
    }
}