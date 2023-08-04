using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTEPingDisplayGUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject pingDisplayGameObject;
        [SerializeField]
        private Text pingDisplayText;
        [SerializeField]
        private UFE2FTEGCFreeStringNumbersScriptableObject gCFreeStringNumbersScriptableObject;

        private void Update()
        {
            if (gCFreeStringNumbersScriptableObject == null)
            {
                return;
            }

            if (UFE.gameMode == GameMode.NetworkGame)
            {
                SetGameObjectActive(pingDisplayGameObject, UFE2FTEPingDisplayOptionsManager.usePingDisplay);

                if (UFE.multiplayerAPI != null)
                {
                    SetTextMessage(pingDisplayText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, UFE.multiplayerAPI.GetLastPing()));
                }
            }
            else
            {
                SetGameObjectActive(pingDisplayGameObject, false);
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
