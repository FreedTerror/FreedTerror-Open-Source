using UnityEngine;
using UnityEngine.UI;
using FPLibrary;

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
            SetComboDamageText(UFE2Manager.GetControlsScript(player));
        }

        private void OnDisable()
        {
            UFE2Manager.SetTextMessage(comboDamageText, UFE2Manager.GetNormalStringNumber(0));
        }

        private void SetComboDamageText(ControlsScript player)
        {
            if (player == null
                || player.opControlsScript == null
                || player.opControlsScript.myInfo == null)
            {
                return;
            }

            UFE2Manager.SetTextMessage(comboDamageText, UFE2Manager.GetNormalStringNumber((int)FPMath.Floor(player.opControlsScript.comboDamage)));
        }
    }
}