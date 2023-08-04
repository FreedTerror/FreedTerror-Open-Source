using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTECharacterNameTextController : MonoBehaviour
    {
        private enum Player
        {
            Player1,
            Player2
        }
        [SerializeField]
        private Player player;
        [SerializeField]
        private Text characterNameText;

        private void Start()
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                return;
            }

            SetCharacterNameText();
        }

        private void SetCharacterNameText()
        {
            if (player == Player.Player1)
            {
                SetTextMessage(characterNameText, UFE.GetPlayer1ControlsScript().myInfo.characterName);
            }
            else if (player == Player.Player2)
            {
                SetTextMessage(characterNameText, UFE.GetPlayer2ControlsScript().myInfo.characterName);
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