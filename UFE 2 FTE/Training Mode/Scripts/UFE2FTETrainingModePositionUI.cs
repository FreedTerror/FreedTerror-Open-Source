using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTETrainingModePositionUI : MonoBehaviour
    {
        public void SetAllPlayersLeftCornerPosition()
        {
            if (UFE2FTEPlayerPausedOptionsManager.playerPaused == UFE2FTEPlayerPausedOptionsManager.Player.Player1)
            {
                UFE2FTEHelperMethodsManager.SetAllPlayersLeftCornerPosition(UFE.GetPlayer1ControlsScript(), UFE2FTETrainingModePositionOptionsManager.cornerPositionXOffset);
            }
            else if (UFE2FTEPlayerPausedOptionsManager.playerPaused == UFE2FTEPlayerPausedOptionsManager.Player.Player2)
            {
                UFE2FTEHelperMethodsManager.SetAllPlayersLeftCornerPosition(UFE.GetPlayer2ControlsScript(), UFE2FTETrainingModePositionOptionsManager.cornerPositionXOffset);
            }

            UFE.PauseGame(false);
        }

        public void SetAllPlayersRightCornerPosition()
        {
            if (UFE2FTEPlayerPausedOptionsManager.playerPaused == UFE2FTEPlayerPausedOptionsManager.Player.Player1)
            {
                UFE2FTEHelperMethodsManager.SetAllPlayersRightCornerPosition(UFE.GetPlayer1ControlsScript(), UFE2FTETrainingModePositionOptionsManager.cornerPositionXOffset);
            }
            else if (UFE2FTEPlayerPausedOptionsManager.playerPaused == UFE2FTEPlayerPausedOptionsManager.Player.Player2)
            {
                UFE2FTEHelperMethodsManager.SetAllPlayersRightCornerPosition(UFE.GetPlayer2ControlsScript(), UFE2FTETrainingModePositionOptionsManager.cornerPositionXOffset);
            }

            UFE.PauseGame(false);
        }

        public void ResetAllPlayersPosition()
        {
            UFE2FTEHelperMethodsManager.ResetAllPlayersPosition();

            UFE.PauseGame(false);
        }
    }
}
