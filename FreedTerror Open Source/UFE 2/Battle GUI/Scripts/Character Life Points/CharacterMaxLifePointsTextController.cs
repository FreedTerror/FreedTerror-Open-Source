using UnityEngine;
using UnityEngine.UI;

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
            if (UFE2Manager.GetControlsScript(player) != null
                && maxLifePointsText != null)
            {
                maxLifePointsText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber(UFE2Manager.GetControlsScript(player).myInfo.lifePoints);
            }
        }
    }
}