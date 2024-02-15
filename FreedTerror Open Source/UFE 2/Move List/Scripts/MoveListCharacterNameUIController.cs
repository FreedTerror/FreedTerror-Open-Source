using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class MoveListCharacterNameUIController : MonoBehaviour
    {
        [SerializeField]
        private Text characterNameText;
  
        private void Start()
        {
            SetCharacterNameText(UFE2Manager.GetControlsScript(UFE2Manager.instance.pausedPlayer));
        }

        private void SetCharacterNameText(ControlsScript player)
        {
            if (characterNameText == null
                || player == null
                || player.myInfo == null)
            {
                return;
            }

            characterNameText.text = player.myInfo.characterName + " Move List";
        }
    }
}