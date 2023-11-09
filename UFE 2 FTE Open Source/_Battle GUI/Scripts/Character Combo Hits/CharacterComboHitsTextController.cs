using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class CharacterComboHitsTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Text comboHitsText;

        private void Update()
        {
            SetComboHitsText(UFE2FTE.GetControlsScript(player));
        }

        private void OnDisable()
        {
            UFE2FTE.SetTextMessage(comboHitsText, UFE2FTE.languageOptions.GetNormalNumber(0));
        }

        private void SetComboHitsText(ControlsScript player)
        {
            if (player == null
                || player.opControlsScript == null)
            {
                return;
            }

            UFE2FTE.SetTextMessage(comboHitsText, UFE2FTE.languageOptions.GetNormalNumber(player.opControlsScript.comboHits));
        }
    }
}