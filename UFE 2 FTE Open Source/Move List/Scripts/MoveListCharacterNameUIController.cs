using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class MoveListCharacterNameUIController : MonoBehaviour
    {
        [SerializeField]
        private Text characterNameText;
  
        private void Start()
        {
            SetCharacterNameText(UFE2FTE.GetControlsScript(UFE2FTE.instance.pausedPlayer));
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