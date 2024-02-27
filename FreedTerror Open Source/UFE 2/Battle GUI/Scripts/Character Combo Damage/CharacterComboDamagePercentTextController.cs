using FPLibrary;
using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class CharacterComboDamagePercentTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private Text comboDamagePercentText;

        private void Update()
        {
            if (UFE2Manager.GetControlsScript(player) != null
                && comboDamagePercentText != null)
            {
                comboDamagePercentText.text = UFE2Manager.instance.cachedStringData.GetPositivePercentStringNumber((int)Fix64.Round(UFE2Manager.GetControlsScript(player).opControlsScript.comboDamage / UFE2Manager.GetControlsScript(player).opControlsScript.myInfo.lifePoints * 100));
            }
        }

        private void OnDisable()
        {
            if (comboDamagePercentText != null)
            {
                comboDamagePercentText.text = UFE2Manager.instance.cachedStringData.GetPositivePercentStringNumber(0);
            }
        }
    }
}