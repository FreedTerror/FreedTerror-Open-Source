using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
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
            UFE2FTE.SetTextMessage(roundsWonText, UFE2FTE.GetNormalStringNumber(GetNumberOfPossibleRoundWins()));
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