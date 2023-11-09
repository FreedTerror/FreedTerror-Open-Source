using UnityEngine;
using UnityEngine.UI;
using FPLibrary;

namespace UFE2FTE
{
    public class CharacterLifePointsTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Text lifePointsText;

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

            UFE2FTE.SetTextMessage(lifePointsText, UFE2FTE.languageOptions.GetNormalNumber((int)FPMath.Floor(player.currentLifePoints)));
        }
    }
}