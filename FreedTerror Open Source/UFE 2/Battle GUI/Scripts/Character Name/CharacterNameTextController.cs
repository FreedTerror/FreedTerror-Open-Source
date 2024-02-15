using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class CharacterNameTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private Text characterNameText;

        private void Start()
        {
            SetCharacterNameText(UFE2Manager.GetControlsScript(player));
        }

        private void SetCharacterNameText(ControlsScript player)
        {
            if (player == null
                || player.myInfo == null)
            {
                return;
            }

            UFE2Manager.SetTextMessage(characterNameText, player.myInfo.characterName);
        }
    }
}