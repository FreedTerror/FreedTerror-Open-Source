using UnityEngine;
using UFE3D;
using FPLibrary;

namespace UFE2FTE
{
    public class UFE2FTETrainingModePositionController : MonoBehaviour
    {
        private enum Player
        {
            AllPlayers,
            Player1,
            Player2
        }

        [SerializeField]
        private Player trainingModePositionPlayerControl;
        [SerializeField]
        private bool useLeftCornerPositionButtonPress;
        [SerializeField]
        private ButtonPress leftCornerPositionButtonPress;
        [SerializeField]
        private bool useRightCornerPositionButtonPress;
        [SerializeField]
        private ButtonPress rightCornerPositionButtonPress;
        [SerializeField]
        private bool useResetPositionButtonPress;
        [SerializeField]
        private ButtonPress resetPositionButtonPress;
        [Fix64Range(0, 100)]
        [SerializeField]
        private Fix64 cornerPositionXOffset = 3;

        private void OnEnable()
        {
            UFE.OnButton += OnButton;
        }

        private void Update()
        {
            UFE2FTETrainingModePositionOptionsManager.cornerPositionXOffset = cornerPositionXOffset;
        }

        private void OnDisable()
        {
            UFE.OnButton -= OnButton;
        }

        private void OnButton(ButtonPress button, ControlsScript player)
        {
            if (player == null
                || UFE.gameMode != GameMode.TrainingRoom)
            {
                return;
            }

            SetAllPlayersLeftCornerPositionButtonPress(button, player);

            SetAllPlayersRightCornerPositionButtonPress(button, player);

            ResetAllPlayersPositionButtonPress(button, player);
        }

        private void SetAllPlayersLeftCornerPositionButtonPress(ButtonPress button, ControlsScript player)
        {
            if (useLeftCornerPositionButtonPress == false
                || button != leftCornerPositionButtonPress
                || player == null)
            {
                return;
            }

            switch (trainingModePositionPlayerControl)
            {
                case Player.AllPlayers:
                    if (player.playerNum == 1
                        || player.playerNum == 2)
                    {
                        UFE2FTEHelperMethodsManager.SetAllPlayersLeftCornerPosition(player, cornerPositionXOffset);
                    }
                    break;

                case Player.Player1:
                    if (player.playerNum == 1)
                    {
                        UFE2FTEHelperMethodsManager.SetAllPlayersLeftCornerPosition(player, cornerPositionXOffset);
                    }
                    break;

                case Player.Player2:
                    if (player.playerNum == 2)
                    {
                        UFE2FTEHelperMethodsManager.SetAllPlayersLeftCornerPosition(player, cornerPositionXOffset);
                    }
                    break;
            }
        }
    
        private void SetAllPlayersRightCornerPositionButtonPress(ButtonPress button, ControlsScript player)
        {
            if (useRightCornerPositionButtonPress == false
                || button != rightCornerPositionButtonPress
                || player == null)
            {
                return;
            }

            switch (trainingModePositionPlayerControl)
            {
                case Player.AllPlayers:
                    if (player.playerNum == 1
                        || player.playerNum == 2)
                    {
                        UFE2FTEHelperMethodsManager.SetAllPlayersRightCornerPosition(player, cornerPositionXOffset);
                    }
                    break;

                case Player.Player1:
                    if (player.playerNum == 1)
                    {
                        UFE2FTEHelperMethodsManager.SetAllPlayersRightCornerPosition(player, cornerPositionXOffset);
                    }
                    break;

                case Player.Player2:
                    if (player.playerNum == 2)
                    {
                        UFE2FTEHelperMethodsManager.SetAllPlayersRightCornerPosition(player, cornerPositionXOffset);
                    }
                    break;
            }
        }

        private void ResetAllPlayersPositionButtonPress(ButtonPress button, ControlsScript player)
        {
            if (useResetPositionButtonPress == false
                || button != resetPositionButtonPress
                || player == null)
            {
                return;
            }

            switch (trainingModePositionPlayerControl)
            {
                case Player.AllPlayers:
                    if (player.playerNum == 1
                        || player.playerNum == 2)
                    {
                        UFE2FTEHelperMethodsManager.ResetAllPlayersPosition();
                    }
                    break;

                case Player.Player1:
                    if (player.playerNum == 1)
                    {
                        UFE2FTEHelperMethodsManager.ResetAllPlayersPosition();
                    }
                    break;

                case Player.Player2:
                    if (player.playerNum == 2)
                    {
                        UFE2FTEHelperMethodsManager.ResetAllPlayersPosition();
                    }
                    break;
            }
        }
        
        [NaughtyAttributes.Button("Set Left Corner Position")]
        private void SetAllPlayersLeftCornerPosition()
        {
            UFE2FTEHelperMethodsManager.SetAllPlayersLeftCornerPosition(
                UFE.GetControlsScript(RandomWithExclusion(1, 2)),
                UFE2FTETrainingModePositionOptionsManager.cornerPositionXOffset);
        }

        [NaughtyAttributes.Button("Set Right Corner Position")]
        private void SetAllPlayersRightCornerPosition()
        {
            UFE2FTEHelperMethodsManager.SetAllPlayersRightCornerPosition(
                UFE.GetControlsScript(RandomWithExclusion(1, 2)),
                UFE2FTETrainingModePositionOptionsManager.cornerPositionXOffset);
        }

        [NaughtyAttributes.Button("Reset Position")]
        private void ResetAllPlayersPosition()
        {
            UFE2FTEHelperMethodsManager.ResetAllPlayersPosition();
        }

        int excludeLastRandNum;
        bool firstRun = true;
        int RandomWithExclusion(int min, int max)
        {
            int result;
            //Don't exclude if this is first run.
            if (firstRun)
            {
                //Generate normal random number
                result = UnityEngine.Random.Range(min, max);
                excludeLastRandNum = result;
                firstRun = false;
                return result;
            }

            //Not first run, exclude last random number with -1 on the max
            result = UnityEngine.Random.Range(min, max - 1);
            //Apply +1 to the result to cancel out that -1 depending on the if statement
            result = (result < excludeLastRandNum) ? result : result + 1;
            excludeLastRandNum = result;
            return result;
        }
    }
}
