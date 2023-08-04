using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTEFrameDelayDisplayGUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject frameDelayDisplayGameObject;
        [SerializeField]
        private Text frameDelayDisplayText;
        [SerializeField]
        private UFE2FTEGCFreeStringNumbersScriptableObject gCFreeStringNumbersScriptableObject;
        [SerializeField]
        private bool usePositiveFrameStringNumberArray;

        private void Update()
        {
            if (gCFreeStringNumbersScriptableObject == null)
            {
                return;
            }

            if (UFE.gameMode == GameMode.NetworkGame)
            {
                SetGameObjectActive(frameDelayDisplayGameObject, UFE2FTEFrameDelayDisplayOptionsManager.useFrameDelayDisplay);

                if (UFE.fluxCapacitor != null)
                {
                    if (usePositiveFrameStringNumberArray == true)
                    {
                        SetTextMessage(frameDelayDisplayText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveFrameStringNumberArray, UFE.fluxCapacitor.GetOptimalFrameDelay()));
                    }
                    else
                    {
                        SetTextMessage(frameDelayDisplayText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, UFE.fluxCapacitor.GetOptimalFrameDelay()));
                    }
                }
            }
            else
            {
                SetGameObjectActive(frameDelayDisplayGameObject, false);
            }
        }

        private static void SetGameObjectActive(GameObject gameObject, bool active)
        {
            if (gameObject == null)
            {
                return;
            }

            gameObject.SetActive(active);
        }

        private static void SetTextMessage(Text text, string message, Color32? color = null)
        {
            if (text == null)
            {
                return;
            }

            text.text = message;

            if (color != null)
            {
                text.color = (Color32)color;
            }
        }
    }
}