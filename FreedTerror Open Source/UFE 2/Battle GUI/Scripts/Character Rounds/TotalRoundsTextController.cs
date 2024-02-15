using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class TotalRoundsTextController : MonoBehaviour
    {
        [SerializeField]
        private Text roundsWonText;

        private void Update()
        {
            SetRoundsWonText();
        }

        private void SetRoundsWonText()
        {
            UFE2Manager.SetTextMessage(roundsWonText, UFE2Manager.GetNormalStringNumber(GetNumberOfPossibleRoundWins()));
        }

        private static int GetNumberOfPossibleRoundWins()
        {
            if (UFE.config == null)
            {
                return 0;
            }

            return (UFE.config.roundOptions.totalRounds + 1) / 2;
        }
    }
}