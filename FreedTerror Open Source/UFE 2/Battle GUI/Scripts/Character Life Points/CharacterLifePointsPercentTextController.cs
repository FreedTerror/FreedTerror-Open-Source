using UnityEngine;
using UnityEngine.UI;
using FPLibrary;

namespace FreedTerror.UFE2
{
    public class CharacterLifePointsPercentTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private Text lifePointsPercentText;

        private void Update()
        {
            SetLifePointsText(UFE2Manager.GetControlsScript(player));  
        }

        private void SetLifePointsText(ControlsScript player)
        {
            if (player == null
                || player.myInfo == null)
            {
                return;
            }

            UFE2Manager.SetTextMessage(lifePointsPercentText, UFE2Manager.GetNormalPercentStringNumber((int)Fix64.Floor(player.currentLifePoints / player.myInfo.lifePoints * 100)));
        }
    }
}