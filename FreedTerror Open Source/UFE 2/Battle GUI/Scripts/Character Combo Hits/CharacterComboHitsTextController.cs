using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class CharacterComboHitsTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private Text comboHitsText;

        private void Update()
        {
            SetComboHitsText(UFE2Manager.GetControlsScript(player));
        }

        private void OnDisable()
        {
            UFE2Manager.SetTextMessage(comboHitsText, UFE2Manager.GetNormalStringNumber(0));
        }

        private void SetComboHitsText(ControlsScript player)
        {
            if (player == null
                || player.opControlsScript == null)
            {
                return;
            }

            UFE2Manager.SetTextMessage(comboHitsText, UFE2Manager.GetNormalStringNumber(player.opControlsScript.comboHits));
        }
    }
}