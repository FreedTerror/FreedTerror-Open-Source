using UnityEngine;

namespace UFE2FTE
{
    public class FontController : MonoBehaviour
    {
        [System.Serializable]
        public class FontOptions
        {
            public FilterMode filterMode;
            public Font[] fontArray;

            public static void SetFontOptions(FontOptions fontOptions)
            {
                if (fontOptions == null)
                {
                    return;
                }

                SetFontFilterMode(fontOptions.fontArray, fontOptions.filterMode);
            }

            public static void SetFontOptions(FontOptions[] fontOptions)
            {
                if (fontOptions == null)
                {
                    return;
                }

                int length = fontOptions.Length;
                for (int i = 0; i < length; i++)
                {
                    SetFontOptions(fontOptions[i]);
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

            public static void SetFontFilterMode(Font[] font, FilterMode filterMode)
            {
                if (font == null)
                {
                    return;
                }

                int length = font.Length;
                for (int i = 0; i < length; i++)
                {
                    SetFontFilterMode(font[i], filterMode);
                }
            }
        }
        [SerializeField]
        private FontOptions[] fontOptionsArray;

        private void Start()
        {
            FontOptions.SetFontOptions(fontOptionsArray);
        }
    }
}