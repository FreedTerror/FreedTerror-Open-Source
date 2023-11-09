using UnityEngine;
using UnityEngine.UI;
using FPLibrary;

namespace UFE2FTE
{
    public class CharacterLifePointsPercentTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Text lifePointsPercentText;

        private void Update()
        {
            SetLifePointsText(UFE2FTE.GetControlsScript(player));  
        }

        private void SetLifePointsText(ControlsScript player)
        {
            if (player == null
                || player.myInfo == null)
            {
                return;
            }

            UFE2FTE.SetTextMessage(lifePointsPercentText, UFE2FTE.languageOptions.GetNormalPercentNumber((int)Fix64.Floor(player.currentLifePoints / player.myInfo.lifePoints * 100)));
        }
    }
}