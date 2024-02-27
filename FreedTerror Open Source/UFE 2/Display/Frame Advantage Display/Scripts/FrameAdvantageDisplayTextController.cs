using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class FrameAdvantageDisplayTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private Text frameAdvantageText;
        private int frameAdvantage;

        private void FixedUpdate()
        {
            UpdateFrameAdvantage(UFE2Manager.GetControlsScript(player));
        }

        private void UpdateFrameAdvantage(ControlsScript player)
        {
            if (player == null
                || player != UFE2Manager.GetControlsScript(this.player)
                || player.opControlsScript == null
                || player.opControlsScript.isDead == true
                || UFE.IsPaused() == true)
            {
                return;
            }

            if (player.currentMove == null)
            {
                if (player.opControlsScript.stunTime > 0)
                {
                    frameAdvantage += 1;

                    UpdateFrameAdvantageText();
                }
                else if (player.opControlsScript.stunTime == 0)
                {
                    frameAdvantage = 0;
                }
            }
            else if (player.currentMove != null)
            {
                if (player.opControlsScript.stunTime > 0)
                {
                    frameAdvantage = 0;

                    UpdateFrameAdvantageText();
                }
                else if (player.opControlsScript.stunTime == 0)
                {
                    frameAdvantage -= 1;

                    UpdateFrameAdvantageText();
                }
            }
        }

        private void UpdateFrameAdvantageText()
        {
            if (frameAdvantage >= 0
                && frameAdvantageText != null)
            {
                frameAdvantageText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber(frameAdvantage);
            }
            else if (frameAdvantage < 0
                && frameAdvantageText != null)
            {
                frameAdvantageText.text = UFE2Manager.instance.cachedStringData.GetNegativeStringNumber(Mathf.Abs(frameAdvantage));
            } 
        }
    }
}
