using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class CharacterNameTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Text characterNameText;

        private void Start()
        {
            SetCharacterNameText(UFE2FTE.GetControlsScript(player));
        }

        private void SetCharacterNameText(ControlsScript player)
        {
            if (player == null
                || player.myInfo == null)
            {
                return;
            }

            UFE2FTE.SetTextMessage(characterNameText, player.myInfo.characterName);
        }
    }
}