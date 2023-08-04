using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTECharacterStunTimeTextController : MonoBehaviour
    {
        private enum Player
        {
            Player1,
            Player2
        }
        [SerializeField]
        private Player player;
        [SerializeField]
        private Text stunTimeText;
        [SerializeField]
        private UFE2FTEGCFreeStringNumbersScriptableObject gCFreeStringNumbersScriptableObject;
        [SerializeField]
        private bool usePositiveFrameStringNumberArray;

        private void Update()
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null
                || gCFreeStringNumbersScriptableObject == null)
            {
                return;
            }

            SetStunTimeText();
        }

        private void OnDisable()
        {
            if (usePositiveFrameStringNumberArray == true)
            {
                SetTextMessage(stunTimeText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveFrameStringNumberArray, 0));
            }
            else
            {
                SetTextMessage(stunTimeText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, 0));
            }
        }

        private void SetStunTimeText()
        {
            if (player == Player.Player1)
            {
                if (usePositiveFrameStringNumberArray == true)
                {
                    SetTextMessage(stunTimeText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveFrameStringNumberArray, Mathf.RoundToInt((float)UFE.GetPlayer1ControlsScript().stunTime * UFE.config.fps)));
                }
                else
                {
                    SetTextMessage(stunTimeText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, Mathf.RoundToInt((float)UFE.GetPlayer1ControlsScript().stunTime * UFE.config.fps)));
                }
            }
            else if (player == Player.Player2)
            {
                if (usePositiveFrameStringNumberArray == true)
                {
                    SetTextMessage(stunTimeText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveFrameStringNumberArray, Mathf.RoundToInt((float)UFE.GetPlayer2ControlsScript().stunTime * UFE.config.fps)));
                }
                else
                {
                    SetTextMessage(stunTimeText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, Mathf.RoundToInt((float)UFE.GetPlayer2ControlsScript().stunTime * UFE.config.fps)));
                }
            }
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