using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class FrameAdvantageDisplayTextController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Text frameAdvantageText;
        private int frameAdvantage;

        private void FixedUpdate()
        {
            SetFrameAdvantage(UFE2FTE.GetControlsScript(player));
        }

        private void SetFrameAdvantage(ControlsScript player)
        {
            if (player == null
                || player != UFE2FTE.GetControlsScript(this.player)
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
                UFE2FTE.SetTextMessage(frameAdvantageText, UFE2FTE.GetNormalStringNumber(frameAdvantage));
            }
            else if (frameAdvantage > 0)
            {
                UFE2FTE.SetTextMessage(frameAdvantageText, UFE2FTE.GetPositiveStringNumber(frameAdvantage));
            }
            else
            {
                UFE2FTE.SetTextMessage(frameAdvantageText, UFE2FTE.GetNegativeStringNumber(Mathf.Abs(frameAdvantage)));
            } 
        }
    }
}
