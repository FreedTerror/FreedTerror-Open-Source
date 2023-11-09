using UnityEngine;
using UnityEngine.UI;
using FPLibrary;

namespace UFE2FTE
{
    public class CharacterComboHitDamageTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Text comboHitDamageText;
        private Fix64 previousComboDamage;

        private void Update()
        {
            SetComboHitDamageText(UFE2FTE.GetControlsScript(player));
        }

        private void OnDisable()
        {
            UFE2FTE.SetTextMessage(comboHitDamageText, UFE2FTE.languageOptions.GetNormalNumber(0));

            previousComboDamage = 0;
        }

        private void SetComboHitDamageText(ControlsScript player)
        {
            if (player == null
                || player.opControlsScript == null
                || player.opControlsScript.myInfo == null)
            {
                return;
            }

            if (player.opControlsScript.comboDamage > previousComboDamage)
            {
                int differenceValue = (int)FPMath.Floor(player.opControlsScript.comboDamage - previousComboDamage);

                if (differenceValue > 0)
                {
                    UFE2FTE.SetTextMessage(comboHitDamageText, UFE2FTE.languageOptions.GetNormalNumber(differenceValue));
                }
            }

            previousComboDamage = (int)FPMath.Floor(player.opControlsScript.comboDamage);

            if (player.opControlsScript.comboDamage <= 0)
            {
                UFE2FTE.SetTextMessage(comboHitDamageText, UFE2FTE.languageOptions.GetNormalNumber(0));

                previousComboDamage = 0;
            }
        }
    }
}