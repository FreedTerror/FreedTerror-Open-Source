using UnityEngine;
using UnityEngine.UI;
using FPLibrary;

namespace UFE2FTE
{
    public class TrainingModePositionUIController : MonoBehaviour
    {  
        [SerializeField]
        private Text cornerTargetText;
        [SerializeField]
        private Text cornerPositionXOffsetText;

        private void Update()
        {
            UFE2FTE.SetTextMessage(cornerPositionXOffsetText, UFE2FTE.instance.trainingModeCornerPositionXOffset.ToString());

            UFE2FTE.SetTextMessage(cornerTargetText, UFE2FTE.GetStringFromEnum(UFE2FTE.instance.trainingModeCornerTarget));
        }

        public void NextCornerTarget()
        {
            UFE2FTE.instance.trainingModeCornerTarget = UFE2FTE.GetNextEnum(UFE2FTE.instance.trainingModeCornerTarget);
        }

        public void PreviousCornerTarget()
        {
            UFE2FTE.instance.trainingModeCornerTarget = UFE2FTE.GetPreviousEnum(UFE2FTE.instance.trainingModeCornerTarget);
        }

        public void NextCornerOffset()
        {
            UFE2FTE.instance.trainingModeCornerPositionXOffset += 0.5;

            if (UFE2FTE.instance.trainingModeCornerPositionXOffset > UFE.config.cameraOptions._maxDistance)
            {
                UFE2FTE.instance.trainingModeCornerPositionXOffset = 0;
            }
        }

        public void PreviousCornerOffset()
        {
            UFE2FTE.instance.trainingModeCornerPositionXOffset -= 0.5;

            if (UFE2FTE.instance.trainingModeCornerPositionXOffset < 0)
            {
                UFE2FTE.instance.trainingModeCornerPositionXOffset = UFE.config.cameraOptions._maxDistance;
            }
        }

        public void SetAllPlayersLeftCornerPosition()
        {
            UFE2FTE.SetAllPlayersLeftCornerPosition(UFE2FTE.GetControlsScript(UFE2FTE.instance.trainingModeCornerTarget), UFE2FTE.instance.trainingModeCornerPositionXOffset);

            UFE.PauseGame(false);
        }

        public void SetAllPlayersRightCornerPosition()
        {
            UFE2FTE.SetAllPlayersRightCornerPosition(UFE2FTE.GetControlsScript(UFE2FTE.instance.trainingModeCornerTarget), UFE2FTE.instance.trainingModeCornerPositionXOffset);

            UFE.PauseGame(false);
        }

        public void ResetAllPlayersPosition()
        {
            UFE2FTE.ResetAllPlayersPosition();

            UFE.PauseGame(false);
        }
    }
}
