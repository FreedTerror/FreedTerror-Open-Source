using UnityEngine;
using UnityEngine.UI;
using FPLibrary;

namespace FreedTerror.UFE2
{
    public class CharacterMaxLifePointsTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private Text maxLifePointsText;

        private void Update()
        {
            SetMaxLifePointsText(UFE2Manager.GetControlsScript(player));  
        }

        private void SetMaxLifePointsText(ControlsScript player)
        {
            if (player == null
                || player.myInfo == null)
            {
                return;
            }

            UFE2Manager.SetTextMessage(maxLifePointsText, UFE2Manager.GetNormalStringNumber(player.myInfo.lifePoints));
        }
    }
}