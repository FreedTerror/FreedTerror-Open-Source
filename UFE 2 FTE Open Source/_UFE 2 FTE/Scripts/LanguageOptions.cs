using UnityEngine;

namespace UFE2FTE
{
    [System.Serializable]
    public class LanguageOptions
    {
        //TODO save language
        public LanguageDataScriptableObject selectedLanguage;
        public LanguageDataScriptableObject[] languageDataScriptableObjectArray;

        public void Initialize()
        {
            InitializeSelectedLanguage();

            InitializeNormalStringNumberArray();
            InitializeNormalPercentStringNumberArray();
            InitializeNormalFrameStringNumberArray();
            InitializePositiveStringNumberArray();
            InitializeNegativeStringNumberArray();
        }

        private void InitializeSelectedLanguage()
        {
            if (languageDataScriptableObjectArray == null)
            {
                //selectedLanguage = CreateInstance<LanguageDataScriptableObject>();

                return;
            }

            int length = languageDataScriptableObjectArray.Length;
            if (length == 0)
            {
                //selectedLanguage = CreateInstance<LanguageDataScriptableObject>();

                return;
            }
            for (int i = 0; i < length; i++)
            {
                if (languageDataScriptableObjectArray[i] == null
                    || languageDataScriptableObjectArray[i].DefaultLanguage == false)
                {
                    continue;
                }

                selectedLanguage = languageDataScriptableObjectArray[i];

                return;
            }

            //selectedLanguage = CreateInstance<LanguageDataScriptableObject>();
        }

        public int normalStringNumberAmount = 1000;
        private string[] normalStringNumberArray = System.Array.Empty<string>();
        private void InitializeNormalStringNumberArray()
        {
            if (normalStringNumberAmount > 0)
            {
                int stringNumberArrayLength = normalStringNumberAmount + 1;
                normalStringNumberArray = new string[stringNumberArrayLength];
                for (int i = 0; i < stringNumberArrayLength; i++)
                {
                    normalStringNumberArray[i] = i.ToString();
                }
            }
            else
            {
                normalStringNumberArray = System.Array.Empty<string>();
            }
        }
        public string GetNormalNumber(int stringArrayIndex)
        {
            if (stringArrayIndex < 0)
            {
#if UNITY_EDITOR
                Debug.Log("[" + stringArrayIndex + "] This causes memory allocation!");
#endif

                return stringArrayIndex.ToString();
            }

            if (stringArrayIndex < normalStringNumberArray.Length)
            {
                return normalStringNumberArray[stringArrayIndex];
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log(nameof(normalStringNumberArray) + " [" + stringArrayIndex + "] This causes memory allocation! Consider increasing the array size avoid this memory allocation.");
#endif

                return stringArrayIndex.ToString();
            }
        }

        public int normalPercentStringNumberAmount = 100;
        private string[] normalPercentStringNumberArray = System.Array.Empty<string>();
        private void InitializeNormalPercentStringNumberArray()
        {
            if (normalPercentStringNumberAmount > 0)
            {
                int stringNumberArrayLength = normalPercentStringNumberAmount + 1;
                normalPercentStringNumberArray = new string[stringNumberArrayLength];
                for (int i = 0; i < stringNumberArrayLength; i++)
                {
                    normalPercentStringNumberArray[i] = i + selectedLanguage.PercentAbbrevation;
                }
            }
            else
            {
                normalPercentStringNumberArray = System.Array.Empty<string>();
            }
        }
        public string GetNormalPercentNumber(int stringArrayIndex)
        {
            if (stringArrayIndex < normalPercentStringNumberArray.Length)
            {
                return normalPercentStringNumberArray[stringArrayIndex];
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log(nameof(normalPercentStringNumberArray) + " [" + stringArrayIndex + "] This causes memory allocation! Consider increasing the array size avoid this memory allocation.");
#endif

                return stringArrayIndex + selectedLanguage.PercentAbbrevation;
            }
        }

        public int normalFrameStringNumberAmount = 1000;
        private string[] normalFrameStringNumberArray = System.Array.Empty<string>();
        private void InitializeNormalFrameStringNumberArray()
        {
            if (normalFrameStringNumberAmount > 0)
            {
                int stringNumberArrayLength = normalFrameStringNumberAmount + 1;
                normalFrameStringNumberArray = new string[stringNumberArrayLength];
                for (int i = 0; i < stringNumberArrayLength; i++)
                {
                    normalFrameStringNumberArray[i] = i + selectedLanguage.FrameAbbrevation;
                }
            }
            else
            {
                normalFrameStringNumberArray = System.Array.Empty<string>();
            }
        }
        public string GetNormalFrameNumber(int stringArrayIndex)
        {
            if (stringArrayIndex < 0)
            {
                return "";
            }

            if (stringArrayIndex < normalFrameStringNumberArray.Length)
            {
                return normalFrameStringNumberArray[stringArrayIndex];
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log(nameof(normalFrameStringNumberArray) + " [" + stringArrayIndex + "] This causes memory allocation! Consider increasing the array size avoid this memory allocation.");
#endif

                return stringArrayIndex + selectedLanguage.FrameAbbrevation;
            }
        }

        public int positiveStringNumberAmount = 1000;
        private string[] positiveStringNumberArray = System.Array.Empty<string>();
        private void InitializePositiveStringNumberArray()
        {
            if (positiveStringNumberAmount > 0)
            {
                int stringNumberArrayLength = positiveStringNumberAmount + 1;
                positiveStringNumberArray = new string[stringNumberArrayLength];
                for (int i = 0; i < stringNumberArrayLength; i++)
                {
                    positiveStringNumberArray[i] = selectedLanguage.PositiveAbbrevation + i;
                }
            }
            else
            {
                positiveStringNumberArray = System.Array.Empty<string>();
            }
        }
        public string GetPositiveNumber(int stringArrayIndex)
        {
            if (stringArrayIndex < 0)
            {
                return "";
            }

            if (stringArrayIndex < positiveStringNumberArray.Length)
            {
                return positiveStringNumberArray[stringArrayIndex];
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log(nameof(positiveStringNumberArray) + " [" + stringArrayIndex + "] This causes memory allocation! Consider increasing the array size avoid this memory allocation.");
#endif

                return selectedLanguage.PositiveAbbrevation + stringArrayIndex;
            }
        }

        public int negativeStringNumberAmount = 1000;
        private string[] negativeStringNumberArray = System.Array.Empty<string>();
        private void InitializeNegativeStringNumberArray()
        {
            if (negativeStringNumberAmount > 0)
            {
                int stringNumberArrayLength = negativeStringNumberAmount + 1;
                negativeStringNumberArray = new string[stringNumberArrayLength];
                for (int i = 0; i < stringNumberArrayLength; i++)
                {
                    negativeStringNumberArray[i] = selectedLanguage.NegativeAbbrevation + i;
                }
            }
            else
            {
                negativeStringNumberArray = System.Array.Empty<string>();
            }
        }
        public string GetNegativeNumber(int stringArrayIndex)
        {
            if (stringArrayIndex < 0)
            {
                return "";
            }

            if (stringArrayIndex < negativeStringNumberArray.Length)
            {
                return negativeStringNumberArray[stringArrayIndex];
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log(nameof(negativeStringNumberArray) + " [" + stringArrayIndex + "] This causes memory allocation! Consider increasing the array size avoid this memory allocation.");
#endif

                return selectedLanguage.NegativeAbbrevation + stringArrayIndex;
            }
        }
    }
}