using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class CharacterRoundsWonTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Text roundsWonText;

        private void Update()
        {
            SetRoundsWonText(UFE2FTE.GetControlsScript(player));
        }

        private void SetRoundsWonText(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            UFE2FTE.SetTextMessage(roundsWonText, UFE2FTE.languageOptions.GetNormalNumber(player.roundsWon));
        }
    }
}