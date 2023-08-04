using System;
using UnityEngine;

namespace UFE2FTE
{
    public class FontFilterModeController : MonoBehaviour
    {
        [Serializable]
        private class FontFilterModeOptions
        {
            public FilterMode filterMode;
            public Font[] fontArray;

            public static void SetFontFilterModeOptions(FontFilterModeOptions fontFilterModeOptions)
            {
                if (fontFilterModeOptions == null)
                {
                    return;
                }

                SetFontFilterMode(fontFilterModeOptions.fontArray, fontFilterModeOptions.filterMode);
            }

            public static void SetFontFilterModeOptions(FontFilterModeOptions[] fontFilterModeOptionsArray)
            {
                if (fontFilterModeOptionsArray == null)
                {
                    return;
                }

                int length = fontFilterModeOptionsArray.Length;
                for (int i = 0; i < length; i++)
                {
                    SetFontFilterModeOptions(fontFilterModeOptionsArray[i]);
                }
            }

            public static void SetFontFilterMode(Font font, FilterMode filterMode)
            {
                if (font == null)
                {
                    return;
                }

                font.material.mainTexture.filterMode = filterMode;
            }

            public static void SetFontFilterMode(Font[] fontArray, FilterMode filterMode)
            {
                if (fontArray == null)
                {
                    return;
                }

                int length = fontArray.Length;
                for (int i = 0; i < length; i++)
                {
                    SetFontFilterMode(fontArray[i], filterMode);
                }
            }
        }
        [SerializeField]
        private FontFilterModeOptions[] fontFilterModeOptionsArray;

        private void Start()
        {
            FontFilterModeOptions.SetFontFilterModeOptions(fontFilterModeOptionsArray);  
        }

        [NaughtyAttributes.Button]
        private void SetAllFontFilterModes()
        {
            FontFilterModeOptions.SetFontFilterModeOptions(fontFilterModeOptionsArray);
        }
    }
}