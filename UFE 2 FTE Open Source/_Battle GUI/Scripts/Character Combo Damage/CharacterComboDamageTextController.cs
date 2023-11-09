using UnityEngine;
using UnityEngine.UI;
using FPLibrary;

namespace UFE2FTE
{
    public class CharacterComboDamageTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Text comboDamageText;

        private void Update()
        {
            SetComboDamageText(UFE2FTE.GetControlsScript(player));
        }

        private void OnDisable()
        {
            UFE2FTE.SetTextMessage(comboDamageText, UFE2FTE.languageOptions.GetNormalNumber(0));
        }

        private void SetComboDamageText(ControlsScript player)
        {
            if (player == null
                || player.opControlsScript == null
                || player.opControlsScript.myInfo == null)
            {
                return;
            }

            UFE2FTE.SetTextMessage(comboDamageText, UFE2FTE.languageOptions.GetNormalNumber((int)FPMath.Floor(player.opControlsScript.comboDamage)));
        }
    }
}