using System;
using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New GC Free String Numbers", menuName = "U.F.E. 2 F.T.E./String/GC Free String Numbers")]
    public class UFE2FTEGCFreeStringNumbersScriptableObject : ScriptableObject
    {
        [SerializeField]
        private string negativeName = "-";
        [SerializeField]
        private string frameName = "F";
        [SerializeField]
        private string secondsName = "S";
        [SerializeField]
        private string percentName = "%";
        [SerializeField]
        private string roundName = "ROUND";
        [SerializeField]
        private string armorName = "ARMOR";
        [SerializeField]
        private string juggleName = "JUGGLE";
        [SerializeField]
        private string groundBounceName = "GROUND BOUNCE";
        [SerializeField]
        private string wallBounceName = "WALL BOUNCE";

        [SerializeField]
        private int positiveStringNumberAmount;
        [HideInInspector]
        public string[] positiveStringNumberArray { get; private set; } = Array.Empty<string>();

        [SerializeField]
        private int negativeStringNumberAmount;
        [HideInInspector]
        public string[] negativeStringNumberArray { get; private set; } = Array.Empty<string>();

        [SerializeField]
        private int positiveFrameStringNumberAmount;
        [HideInInspector]
        public string[] positiveFrameStringNumberArray { get; private set; } = Array.Empty<string>();

        [SerializeField]
        private int negativeStringFrameNumberAmount;
        [HideInInspector]
        public string[] negativeFrameStringNumberArray { get; private set; } = Array.Empty<string>();

        [SerializeField]
        private int positiveSecondsStringNumberAmount;
        [HideInInspector]
        public string[] positiveSecondsStringNumberArray { get; private set; } = Array.Empty<string>();

        [SerializeField]
        private int positivePercentStringNumberAmount;
        [HideInInspector]
        public string[] positivePercentStringNumberArray { get; private set; } = Array.Empty<string>();

        [HideInInspector]
        public string[] roundStringNumberArray { get; private set; } = Array.Empty<string>();

        [HideInInspector]
        public string[] armorHitsRemainingStringNumberArray { get; private set; } = Array.Empty<string>();

        [HideInInspector]
        public string[] juggleStringNumberArray { get; private set; } = Array.Empty<string>();

        [HideInInspector]
        public string[] groundBounceStringNumberArray { get; private set; } = Array.Empty<string>();

        [HideInInspector]
        public string[] wallBounceStringNumberArray { get; private set; } = Array.Empty<string>();

        public static void InitializeGCFreeStringNumbers(UFE2FTEGCFreeStringNumbersScriptableObject gCFreeStringNumbersScriptableObject)
        {
            if (gCFreeStringNumbersScriptableObject == null)
            {
                return;
            }

            int stringNumberArrayLength = gCFreeStringNumbersScriptableObject.positiveStringNumberAmount + 1;
            gCFreeStringNumbersScriptableObject.positiveStringNumberArray = new string[stringNumberArrayLength];
            for (int i = 0; i < stringNumberArrayLength; i++)
            {
                gCFreeStringNumbersScriptableObject.positiveStringNumberArray[i] = i.ToString();
            }

            stringNumberArrayLength = gCFreeStringNumbersScriptableObject.negativeStringNumberAmount + 1;
            gCFreeStringNumbersScriptableObject.negativeStringNumberArray = new string[stringNumberArrayLength];
            for (int i = 0; i < stringNumberArrayLength; i++)
            {
                gCFreeStringNumbersScriptableObject.negativeStringNumberArray[i] = gCFreeStringNumbersScriptableObject.negativeName + i;
            }

            stringNumberArrayLength = gCFreeStringNumbersScriptableObject.positiveFrameStringNumberAmount + 1;
            gCFreeStringNumbersScriptableObject.positiveFrameStringNumberArray = new string[stringNumberArrayLength];
            for (int i = 0; i < stringNumberArrayLength; i++)
            {
                gCFreeStringNumbersScriptableObject.positiveFrameStringNumberArray[i] = i + gCFreeStringNumbersScriptableObject.frameName;
            }

            stringNumberArrayLength = gCFreeStringNumbersScriptableObject.negativeStringFrameNumberAmount + 1;
            gCFreeStringNumbersScriptableObject.negativeFrameStringNumberArray = new string[stringNumberArrayLength];
            for (int i = 0; i < stringNumberArrayLength; i++)
            {
                gCFreeStringNumbersScriptableObject.negativeFrameStringNumberArray[i] = gCFreeStringNumbersScriptableObject.negativeName + i + gCFreeStringNumbersScriptableObject.frameName;
            }

            stringNumberArrayLength = gCFreeStringNumbersScriptableObject.positiveSecondsStringNumberAmount + 1;
            gCFreeStringNumbersScriptableObject.positiveSecondsStringNumberArray = new string[stringNumberArrayLength];
            for (int i = 0; i < stringNumberArrayLength; i++)
            {
                gCFreeStringNumbersScriptableObject.positiveSecondsStringNumberArray[i] = i + gCFreeStringNumbersScriptableObject.secondsName;
            }

            stringNumberArrayLength = gCFreeStringNumbersScriptableObject.positivePercentStringNumberAmount + 1;
            gCFreeStringNumbersScriptableObject.positivePercentStringNumberArray = new string[stringNumberArrayLength];
            for (int i = 0; i < stringNumberArrayLength; i++)
            {
                gCFreeStringNumbersScriptableObject.positivePercentStringNumberArray[i] = i + gCFreeStringNumbersScriptableObject.percentName;
            }

            if (UFE.config != null)
            {
                stringNumberArrayLength = UFE.config.roundOptions.totalRounds + 1;
                gCFreeStringNumbersScriptableObject.roundStringNumberArray = new string[stringNumberArrayLength];
                for (int i = 0; i < stringNumberArrayLength; i++)
                {
                    gCFreeStringNumbersScriptableObject.roundStringNumberArray[i] = gCFreeStringNumbersScriptableObject.roundName + i;
                }

                stringNumberArrayLength = UFE.config.comboOptions.maxCombo + 1;
                gCFreeStringNumbersScriptableObject.armorHitsRemainingStringNumberArray = new string[stringNumberArrayLength];
                for (int i = 0; i < stringNumberArrayLength; i++)
                {
                    gCFreeStringNumbersScriptableObject.armorHitsRemainingStringNumberArray[i] = gCFreeStringNumbersScriptableObject.armorName + i;
                }

                stringNumberArrayLength = UFE.config.comboOptions.maxCombo + 1;
                gCFreeStringNumbersScriptableObject.juggleStringNumberArray = new string[stringNumberArrayLength];
                for (int i = 0; i < stringNumberArrayLength; i++)
                {
                    gCFreeStringNumbersScriptableObject.juggleStringNumberArray[i] = gCFreeStringNumbersScriptableObject.juggleName + i;
                }

                stringNumberArrayLength = UnityEngine.Mathf.CeilToInt((float)UFE.config.groundBounceOptions._maximumBounces) + 1;
                gCFreeStringNumbersScriptableObject.groundBounceStringNumberArray = new string[stringNumberArrayLength];
                int bounceAmount = stringNumberArrayLength - 1;
                for (int i = 0; i < stringNumberArrayLength; i++)
                {
                    gCFreeStringNumbersScriptableObject.groundBounceStringNumberArray[i] = gCFreeStringNumbersScriptableObject.groundBounceName + bounceAmount;

                    bounceAmount--;
                }

                stringNumberArrayLength = UnityEngine.Mathf.CeilToInt((float)UFE.config.wallBounceOptions._maximumBounces) + 1;
                gCFreeStringNumbersScriptableObject.wallBounceStringNumberArray = new string[stringNumberArrayLength];
                bounceAmount = stringNumberArrayLength - 1;
                for (int i = 0; i < stringNumberArrayLength; i++)
                {
                    gCFreeStringNumbersScriptableObject.wallBounceStringNumberArray[i] = gCFreeStringNumbersScriptableObject.wallBounceName + bounceAmount;

                    bounceAmount--;
                }
            }
        }

        [NaughtyAttributes.Button("Initialize GC Free String Numbers")]
        private void InitializeGCFreeStringNumbersButton()
        {
            InitializeGCFreeStringNumbers(this);
        }

        public static string GetStringFromStringArray(UFE2FTEGCFreeStringNumbersScriptableObject gCFreeStringNumbersScriptableObject, string[] stringArray, int stringArrayIndex)
        {
            if (gCFreeStringNumbersScriptableObject == null
                || stringArray == null)
            {
                return "";
            }

            if (stringArrayIndex > -1
                && stringArrayIndex < stringArray.Length)
            {
                return stringArray[stringArrayIndex];
            }
            else if (stringArrayIndex >= stringArray.Length)
            {
                if (stringArray == gCFreeStringNumbersScriptableObject.positiveStringNumberArray)
                {
#if UNITY_EDITOR
                    Debug.Log("positiveStringNumberArray[" + stringArrayIndex + "] This causes memory allocation! Consider increasing the array size avoid this memory allocation.");
#endif

                    return stringArrayIndex.ToString();
                }
                else if (stringArray == gCFreeStringNumbersScriptableObject.negativeStringNumberArray)
                {
#if UNITY_EDITOR
                    Debug.Log("negativeStringNumberArray[" + stringArrayIndex + "] This causes memory allocation! Consider increasing the array size avoid this memory allocation.");
#endif

                    return gCFreeStringNumbersScriptableObject.negativeName + stringArrayIndex.ToString();
                }
                else if (stringArray == gCFreeStringNumbersScriptableObject.positiveFrameStringNumberArray)
                {
#if UNITY_EDITOR
                    Debug.Log("positiveFrameStringNumberArray[" + stringArrayIndex + "] This causes memory allocation! Consider increasing the array size avoid this memory allocation.");
#endif

                    return stringArrayIndex.ToString() + gCFreeStringNumbersScriptableObject.frameName;
                }
                else if (stringArray == gCFreeStringNumbersScriptableObject.negativeFrameStringNumberArray)
                {
#if UNITY_EDITOR
                    Debug.Log("negativeFrameStringNumberArray[" + stringArrayIndex + "] This causes memory allocation! Consider increasing the array size avoid this memory allocation.");
#endif

                    return gCFreeStringNumbersScriptableObject.negativeName + stringArrayIndex.ToString() + gCFreeStringNumbersScriptableObject.frameName;
                }
                else if (stringArray == gCFreeStringNumbersScriptableObject.positiveSecondsStringNumberArray)
                {
#if UNITY_EDITOR
                    Debug.Log("positiveSecondsStringNumberArray[" + stringArrayIndex + "] This causes memory allocation! Consider increasing the array size avoid this memory allocation.");
#endif

                    return stringArrayIndex.ToString() + gCFreeStringNumbersScriptableObject.secondsName;
                }
                else if (stringArray == gCFreeStringNumbersScriptableObject.positivePercentStringNumberArray)
                {
#if UNITY_EDITOR
                    Debug.Log("positivePercentStringNumberArray[" + stringArrayIndex + "] This causes memory allocation! Consider increasing the array size avoid this memory allocation.");
#endif

                    return stringArrayIndex.ToString() + gCFreeStringNumbersScriptableObject.percentName;
                }
                else if (stringArray == gCFreeStringNumbersScriptableObject.roundStringNumberArray)
                {
#if UNITY_EDITOR
                    Debug.Log("roundStringNumberArray[" + stringArrayIndex + "] This causes memory allocation! Consider increasing the array size avoid this memory allocation.");
#endif

                    return gCFreeStringNumbersScriptableObject.roundName + stringArrayIndex.ToString();
                }
                else if (stringArray == gCFreeStringNumbersScriptableObject.armorHitsRemainingStringNumberArray)
                {
#if UNITY_EDITOR
                    Debug.Log("armorHitsRemainingStringNumberArray[" + stringArrayIndex + "] This causes memory allocation! Consider increasing the array size avoid this memory allocation.");
#endif

                    return gCFreeStringNumbersScriptableObject.armorHitsRemainingStringNumberArray + stringArrayIndex.ToString();
                }
                else if (stringArray == gCFreeStringNumbersScriptableObject.juggleStringNumberArray)
                {
#if UNITY_EDITOR
                    Debug.Log("juggleStringNumberArray[" + stringArrayIndex + "] This causes memory allocation! Consider increasing the array size avoid this memory allocation.");
#endif

                    return gCFreeStringNumbersScriptableObject.juggleName + stringArrayIndex.ToString();
                }
                else if (stringArray == gCFreeStringNumbersScriptableObject.groundBounceStringNumberArray)
                {
#if UNITY_EDITOR
                    Debug.Log("groundBounceStringNumberArray[" + stringArrayIndex + "] This causes memory allocation! Consider increasing the array size avoid this memory allocation.");
#endif

                    return gCFreeStringNumbersScriptableObject.groundBounceName + stringArrayIndex.ToString();
                }
                else if (stringArray == gCFreeStringNumbersScriptableObject.wallBounceStringNumberArray)
                {
#if UNITY_EDITOR
                    Debug.Log("wallBounceStringNumberArray[" + stringArrayIndex + "] This causes memory allocation! Consider increasing the array size avoid this memory allocation.");
#endif

                    return gCFreeStringNumbersScriptableObject.wallBounceName + stringArrayIndex.ToString();
                }

#if UNITY_EDITOR
                Debug.Log("No matching string array found!");
#endif

                return "";
            }

            return "";
        }
    }
}
