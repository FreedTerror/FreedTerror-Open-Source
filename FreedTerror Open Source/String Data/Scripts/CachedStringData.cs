using UnityEngine;

namespace FreedTerror
{
    [System.Serializable]
    public class CachedStringData
    {
        public int positiveStringNumberAmount = 1000;
        public string[] positiveStringNumberArray = System.Array.Empty<string>();

        public int negativeStringNumberAmount = 1000;
        public string[] negativeStringNumberArray = System.Array.Empty<string>();

        public int positivePercentStringNumberAmount = 100;
        public string[] positivePercentStringNumberArray = System.Array.Empty<string>();

        public void InitializePositiveStringNumberArray()
        {
            if (positiveStringNumberAmount > 0)
            {
                int stringNumberArrayLength = positiveStringNumberAmount + 1;
                positiveStringNumberArray = new string[stringNumberArrayLength];
                for (int i = 0; i < stringNumberArrayLength; i++)
                {
                    positiveStringNumberArray[i] = i.ToString();
                }
            }
            else
            {
                positiveStringNumberArray = System.Array.Empty<string>();
            }
        }

        public void InitializeNegativeStringNumberArray()
        {
            if (negativeStringNumberAmount > 0)
            {
                int stringNumberArrayLength = negativeStringNumberAmount + 1;
                negativeStringNumberArray = new string[stringNumberArrayLength];
                for (int i = 0; i < stringNumberArrayLength; i++)
                {
                    negativeStringNumberArray[i] = "-" + i;
                }
            }
            else
            {
                negativeStringNumberArray = System.Array.Empty<string>();
            }
        }

        public void InitializePositivePercentStringNumberArray()
        {
            if (positivePercentStringNumberAmount > 0)
            {
                int stringNumberArrayLength = positivePercentStringNumberAmount + 1;
                positivePercentStringNumberArray = new string[stringNumberArrayLength];
                for (int i = 0; i < stringNumberArrayLength; i++)
                {
                    positivePercentStringNumberArray[i] = i + "%";
                }
            }
            else
            {
                positivePercentStringNumberArray = System.Array.Empty<string>();
            }
        }

        public string GetPositiveStringNumber(int stringArrayIndex)
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

                return "+" + stringArrayIndex;
            }
        }

        public string GetNegativeStringNumber(int stringArrayIndex)
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

                return "-" + stringArrayIndex;
            }
        }

        public string GetPositivePercentStringNumber(int stringArrayIndex)
        {
            if (stringArrayIndex < positivePercentStringNumberArray.Length)
            {
                return positivePercentStringNumberArray[stringArrayIndex];
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log(nameof(positivePercentStringNumberArray) + " [" + stringArrayIndex + "] This causes memory allocation! Consider increasing the array size avoid this memory allocation.");
#endif

                return stringArrayIndex + "%";
            }
        }
    }
}