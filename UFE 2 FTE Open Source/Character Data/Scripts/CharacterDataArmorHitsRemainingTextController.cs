using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class CharacterDataArmorHitsRemainingTextController : MonoBehaviour
    {
        [SerializeField]
        private CharacterAlertController characterAlertController;
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Text characterDataArmorHitsRemainingText;

        private void Update()
        {
            if (characterAlertController == null)
            {
                return;
            }

            UFE2FTE.SetTextMessage(characterDataArmorHitsRemainingText, UFE2FTE.languageOptions.GetNormalNumber(characterAlertController.GetCharacterData(player).armorHitsRemaining));
        }
    }
}