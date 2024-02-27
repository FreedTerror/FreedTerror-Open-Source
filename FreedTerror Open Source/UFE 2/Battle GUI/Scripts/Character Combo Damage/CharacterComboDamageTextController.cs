using FPLibrary;
using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class CharacterComboDamageTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private Text comboDamageText;

        private void Update()
        {
            if (UFE2Manager.GetControlsScript(player) != null
                && comboDamageText != null)
            {
                comboDamageText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber((int)Fix64.Floor(UFE2Manager.GetControlsScript(player).opControlsScript.comboDamage));
            }
        }

        private void OnDisable()
        {
            if (comboDamageText != null)
            {
                comboDamageText.text = UFE2Manager.instance.cachedStringData.GetPositivePercentStringNumber(0);
            }
        }
    }
}