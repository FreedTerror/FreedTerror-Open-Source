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
            if (roundsWonText != null)
            {
                roundsWonText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber(UFE2Manager.GetNumberOfPossibleRoundWins());
            }
        }
    }
}