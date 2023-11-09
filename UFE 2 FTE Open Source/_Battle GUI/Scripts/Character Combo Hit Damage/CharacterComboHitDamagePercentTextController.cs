using UnityEngine;
using UnityEngine.UI;
using FPLibrary;

namespace UFE2FTE
{
    public class CharacterComboHitDamagePercentTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;   
        [SerializeField]
        private Text comboHitDamagePercentText;
        private Fix64 previousComboDamage;

        private void Update()
        {
            SetComboHitDamagePercentText(UFE2FTE.GetControlsScript(player));
        }

        private void OnDisable()
        {
            UFE2FTE.SetTextMessage(comboHitDamagePercentText, UFE2FTE.languageOptions.GetNormalPercentNumber(0));

            previousComboDamage = 0;
        }

        private void SetComboHitDamagePercentText(ControlsScript player)
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
                    UFE2FTE.SetTextMessage(comboHitDamagePercentText, UFE2FTE.languageOptions.GetNormalPercentNumber(Mathf.FloorToInt((float)differenceValue / player.opControlsScript.myInfo.lifePoints * 100)));
                }
            }

            previousComboDamage = (int)FPMath.Floor(player.opControlsScript.comboDamage);

            if (player.opControlsScript.comboDamage <= 0)
            {
                UFE2FTE.SetTextMessage(comboHitDamagePercentText, UFE2FTE.languageOptions.GetNormalPercentNumber(0));

                previousComboDamage = 0;
            }
        }
    }
}