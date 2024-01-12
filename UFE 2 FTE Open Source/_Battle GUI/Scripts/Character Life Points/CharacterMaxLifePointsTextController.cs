using UnityEngine;
using UnityEngine.UI;
using FPLibrary;

namespace UFE2FTE
{
    public class CharacterMaxLifePointsTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Text maxLifePointsText;

        private void Update()
        {
            SetMaxLifePointsText(UFE2FTE.GetControlsScript(player));  
        }

        private void SetMaxLifePointsText(ControlsScript player)
        {
            if (player == null
                || player.myInfo == null)
            {
                return;
            }

            UFE2FTE.SetTextMessage(maxLifePointsText, UFE2FTE.GetNormalStringNumber(player.myInfo.lifePoints));
        }
    }
}