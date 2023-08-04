using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTETrainingModeSaveAndLoadStateController : MonoBehaviour
    {
        private enum PlayerControl
        {
            AllPlayers,
            Player1,
            Player2 
        }

        [SerializeField]
        private PlayerControl trainingModeSaveAndLoadStatePlayerControl = PlayerControl.AllPlayers;
        [SerializeField]
        private bool useSaveStateButtonPress;
        [SerializeField]
        private ButtonPress saveStateButtonPress;
        [SerializeField]
        private bool useLoadStateButtonPress;
        [SerializeField]
        private ButtonPress loadStateButtonPress;

        private void OnEnable()
        {
            UFE.OnButton += OnButton;
        }

        private void OnDisable ()
        {
            UFE.OnButton -= OnButton;
        }

        private void OnButton(ButtonPress button, ControlsScript player)
        {
            if (UFE.gameMode != GameMode.TrainingRoom)
            {
                return;
            }

            SaveStateButtonPress(button, player);

            LoadStateButtonPress(button, player);
        }

        private void SaveStateButtonPress(ButtonPress button, ControlsScript player)
        {
            if (useSaveStateButtonPress == false
                || button != saveStateButtonPress)
            {
                return;
            }

            switch (trainingModeSaveAndLoadStatePlayerControl)
            {
                case PlayerControl.Player1:
                    if (player.playerNum == 1)
                    {
                        UFE2FTEHelperMethodsManager.SaveState();
                    }
                    break;

                case PlayerControl.Player2:
                    if (player.playerNum == 2)
                    {
                        UFE2FTEHelperMethodsManager.SaveState();
                    }
                    break;

                case PlayerControl.AllPlayers:
                    if (player.playerNum == 1
                        || player.playerNum == 2)
                    {
                        UFE2FTEHelperMethodsManager.SaveState();
                    }
                    break;
            }
        }

        private void LoadStateButtonPress(ButtonPress button, ControlsScript player)
        {
            if (useLoadStateButtonPress == false
                || button != loadStateButtonPress)
            {
                return;
            }

            switch (trainingModeSaveAndLoadStatePlayerControl)
            {
                case PlayerControl.AllPlayers:
                    if (player.playerNum == 1
                        || player.playerNum == 2)
                    {
                        UFE2FTEHelperMethodsManager.LoadState();

                        UFE.GetPlayer1ControlsScript().inputHeldDown[saveStateButtonPress] = 0;

                        UFE.GetPlayer1ControlsScript().inputHeldDown[loadStateButtonPress] = 1;

                        UFE.GetPlayer2ControlsScript().inputHeldDown[saveStateButtonPress] = 0;

                        UFE.GetPlayer2ControlsScript().inputHeldDown[loadStateButtonPress] = 1;
                    }
                    break;

                case PlayerControl.Player1:
                    if (player.playerNum == 1)
                    {
                        UFE2FTEHelperMethodsManager.LoadState();

                        UFE.GetPlayer1ControlsScript().inputHeldDown[saveStateButtonPress] = 0;

                        UFE.GetPlayer1ControlsScript().inputHeldDown[loadStateButtonPress] = 1;
                    }
                    break;

                case PlayerControl.Player2:
                    if (player.playerNum == 2)
                    {
                        UFE2FTEHelperMethodsManager.LoadState();

                        UFE.GetPlayer2ControlsScript().inputHeldDown[saveStateButtonPress] = 0;

                        UFE.GetPlayer2ControlsScript().inputHeldDown[loadStateButtonPress] = 1;
                    }
                    break;
            }
        }

        [NaughtyAttributes.Button]
        private void SaveState()
        {
            UFE2FTEHelperMethodsManager.SaveState();
        }

        [NaughtyAttributes.Button]
        private void LoadState()
        {
            UFE2FTEHelperMethodsManager.LoadState();
        }
    }
}
