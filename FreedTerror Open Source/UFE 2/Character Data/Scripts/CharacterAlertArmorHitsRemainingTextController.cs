using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class CharacterAlertArmorHitsRemainingTextController : MonoBehaviour
    {
        [SerializeField]
        private CharacterAlertController characterAlertController;
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private Text armorHitsRemainingText;

        private void Update()
        {
            if (characterAlertController != null
                && armorHitsRemainingText != null)
            {
                armorHitsRemainingText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber(characterAlertController.GetCharacterData(player).armorHitsRemaining);
            }
        }
    }
}