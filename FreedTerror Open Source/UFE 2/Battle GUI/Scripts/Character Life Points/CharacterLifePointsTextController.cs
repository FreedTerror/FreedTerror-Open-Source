using UnityEngine;
using UnityEngine.UI;
using FPLibrary;

namespace FreedTerror.UFE2
{
    public class CharacterLifePointsTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private Text lifePointsText;

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

            UFE2Manager.SetTextMessage(lifePointsText, UFE2Manager.GetNormalStringNumber((int)FPMath.Floor(player.currentLifePoints)));
        }
    }
}