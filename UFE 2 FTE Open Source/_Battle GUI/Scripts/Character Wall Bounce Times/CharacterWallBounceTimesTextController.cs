using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class CharacterWallBounceTimesTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Text wallBounceTimesText;

        private void Update()
        {
            SetWallBounceTimesText(UFE2FTE.GetControlsScript(player));   
        }

        private void SetWallBounceTimesText(ControlsScript player)
        {
            if (player == null
                || player.opControlsScript == null
                || player.opControlsScript.Physics == null)
            {
                return;
            }

            UFE2FTE.SetTextMessage(wallBounceTimesText, UFE2FTE.languageOptions.GetNormalNumber(player.opControlsScript.Physics.wallBounceTimes));
        }
    }
}