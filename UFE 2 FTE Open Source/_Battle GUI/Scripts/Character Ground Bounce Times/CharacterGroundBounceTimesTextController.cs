using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class CharacterGroundBounceTimesTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Text groundBounceTimesText;

        private void Update()
        {
            SetGroundBounceTimesText(UFE2FTE.GetControlsScript(player));   
        }

        private void SetGroundBounceTimesText(ControlsScript player)
        {
            if (player == null
                || player.opControlsScript == null
                || player.opControlsScript.Physics == null)
            {
                return;
            }

            UFE2FTE.SetTextMessage(groundBounceTimesText, UFE2FTE.languageOptions.GetNormalNumber(player.opControlsScript.Physics.groundBounceTimes));
        }
    }
}