using FPLibrary;
using UnityEngine;
using UnityEngine.UI;

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
            if (UFE2Manager.GetControlsScript(player) != null
                && lifePointsPercentText != null)
            {
                lifePointsPercentText.text = UFE2Manager.instance.cachedStringData.GetPositivePercentStringNumber((int)Fix64.Floor(UFE2Manager.GetControlsScript(player).currentLifePoints / UFE2Manager.GetControlsScript(player).myInfo.lifePoints * 100));
            }
        }
    }
}