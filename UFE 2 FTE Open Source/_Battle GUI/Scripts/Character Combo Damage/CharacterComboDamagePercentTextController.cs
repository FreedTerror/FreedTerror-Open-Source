using UnityEngine;
using UnityEngine.UI;
using FPLibrary;

namespace UFE2FTE
{
    public class CharacterComboDamagePercentTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Text comboDamagePercentText;

        private void Update()
        {
            SetComboDamagePercentText(UFE2FTE.GetControlsScript(player));
        }

        private void OnDisable()
        {
            UFE2FTE.SetTextMessage(comboDamagePercentText, UFE2FTE.GetNormalPercentStringNumber(0));
        }

        private void SetComboDamagePercentText(ControlsScript player)
        {
            if (player == null
                || player.opControlsScript == null
                || player.opControlsScript.myInfo == null)
            {
                return;
            }

            UFE2FTE.SetTextMessage(comboDamagePercentText, UFE2FTE.GetNormalPercentStringNumber((int)FPMath.Round(player.opControlsScript.comboDamage / player.opControlsScript.myInfo.lifePoints * 100)));
        }
    }
}