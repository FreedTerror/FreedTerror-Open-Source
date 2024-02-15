using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class CharacterRoundsWonTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private Text roundsWonText;

        private void Update()
        {
            SetRoundsWonText(UFE2Manager.GetControlsScript(player));
        }

        private void SetRoundsWonText(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            UFE2Manager.SetTextMessage(roundsWonText, UFE2Manager.GetNormalStringNumber(player.roundsWon));
        }
    }
}