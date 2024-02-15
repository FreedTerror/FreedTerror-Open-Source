using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class CharacterDataArmorHitsRemainingTextController : MonoBehaviour
    {
        [SerializeField]
        private CharacterAlertController characterAlertController;
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private Text characterDataArmorHitsRemainingText;

        private void Update()
        {
            if (characterAlertController == null)
            {
                return;
            }

            UFE2Manager.SetTextMessage(characterDataArmorHitsRemainingText, UFE2Manager.GetNormalStringNumber(characterAlertController.GetCharacterData(player).armorHitsRemaining));
        }
    }
}