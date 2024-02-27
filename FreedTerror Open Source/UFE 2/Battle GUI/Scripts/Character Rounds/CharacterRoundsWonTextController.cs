using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class CharacterRoundsWonTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private Text roundsWonText;

        private void Update()
        {
            if (UFE2Manager.GetControlsScript(player) != null
                && roundsWonText != null)
            {
                roundsWonText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber(UFE2Manager.GetControlsScript(player).roundsWon);
            }
        }
    }
}