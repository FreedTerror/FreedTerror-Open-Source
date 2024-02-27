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

        private void Update()
        {
            if (UFE2Manager.GetControlsScript(player) != null
                && characterNameText != null)
            {
                characterNameText.text = UFE2Manager.GetControlsScript(player).myInfo.characterName;
            }
        }
    }
}