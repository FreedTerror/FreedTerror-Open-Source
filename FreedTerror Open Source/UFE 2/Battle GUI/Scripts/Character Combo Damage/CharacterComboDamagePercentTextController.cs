using UnityEngine;
using UnityEngine.UI;
using FPLibrary;

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
            SetComboDamagePercentText(UFE2Manager.GetControlsScript(player));
        }

        private void OnDisable()
        {
            UFE2Manager.SetTextMessage(comboDamagePercentText, UFE2Manager.GetNormalPercentStringNumber(0));
        }

        private void SetComboDamagePercentText(ControlsScript player)
        {
            if (player == null
                || player.opControlsScript == null
                || player.opControlsScript.myInfo == null)
            {
                return;
            }

            UFE2Manager.SetTextMessage(comboDamagePercentText, UFE2Manager.GetNormalPercentStringNumber((int)FPMath.Round(player.opControlsScript.comboDamage / player.opControlsScript.myInfo.lifePoints * 100)));
        }
    }
}