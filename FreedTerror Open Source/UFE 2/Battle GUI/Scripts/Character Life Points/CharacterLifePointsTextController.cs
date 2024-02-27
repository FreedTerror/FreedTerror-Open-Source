using FPLibrary;
using UnityEngine;
using UnityEngine.UI;

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
            if (UFE2Manager.GetControlsScript(player) != null
                && lifePointsText != null)
            {
                lifePointsText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber((int)Fix64.Floor(UFE2Manager.GetControlsScript(player).currentLifePoints));
            }
        }
    }
}