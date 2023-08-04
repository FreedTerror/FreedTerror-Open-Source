using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTECharacterLifePointsTextController : MonoBehaviour
    {
        private enum Player
        {
            Player1,
            Player2
        }
        [SerializeField]
        private Player player;
        [SerializeField]
        private Text lifePointsPercentText;
        [SerializeField]
        private Text lifePointsText;
        [SerializeField]
        private UFE2FTEGCFreeStringNumbersScriptableObject gCFreeStringNumbersScriptableObject;

        private void Update()
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null
                || gCFreeStringNumbersScriptableObject == null)
            {
                return;
            }

            SetLifePointsText();  
        }

        private void SetLifePointsText()
        {
            if (player == Player.Player1)
            {
                SetTextMessage(lifePointsPercentText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positivePercentStringNumberArray, Mathf.FloorToInt((float)(UFE.GetPlayer1ControlsScript().currentLifePoints / UFE.GetPlayer1ControlsScript().myInfo.lifePoints * 100))));

                SetTextMessage(lifePointsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, Mathf.FloorToInt((float)UFE.GetPlayer1ControlsScript().currentLifePoints)));
            }
            else if (player == Player.Player2)
            {
                SetTextMessage(lifePointsPercentText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positivePercentStringNumberArray, Mathf.FloorToInt((float)(UFE.GetPlayer2ControlsScript().currentLifePoints / UFE.GetPlayer2ControlsScript().myInfo.lifePoints * 100))));

                SetTextMessage(lifePointsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, Mathf.FloorToInt((float)UFE.GetPlayer2ControlsScript().currentLifePoints)));
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