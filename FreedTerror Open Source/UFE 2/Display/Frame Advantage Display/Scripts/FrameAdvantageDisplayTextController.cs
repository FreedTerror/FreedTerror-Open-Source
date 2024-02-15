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
            SetFrameAdvantage(UFE2Manager.GetControlsScript(player));
        }

        private void SetFrameAdvantage(ControlsScript player)
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
                    frameAdvantage++;

                    SetFrameAdvantageText();
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

                    SetFrameAdvantageText();
                }
                else if (player.opControlsScript.stunTime == 0)
                {
                    frameAdvantage--;

                    SetFrameAdvantageText();
                }
            }
        }

        private void SetFrameAdvantageText()
        {
            if (frameAdvantage == 0)
            {
                UFE2Manager.SetTextMessage(frameAdvantageText, UFE2Manager.GetNormalStringNumber(frameAdvantage));
            }
            else if (frameAdvantage > 0)
            {
                UFE2Manager.SetTextMessage(frameAdvantageText, UFE2Manager.GetPositiveStringNumber(frameAdvantage));
            }
            else
            {
                UFE2Manager.SetTextMessage(frameAdvantageText, UFE2Manager.GetNegativeStringNumber(Mathf.Abs(frameAdvantage)));
            } 
        }
    }
}
