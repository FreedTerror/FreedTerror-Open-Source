using System;
using System.Collections.Generic;
using UFE3D;

namespace UFE2FTE
{
    public static class UFE2FTETrainingModeButtonRecordingRewiredOptionsManager
    {
        private static RewiredInputController player1RewiredInputController;
        private static RewiredInputController player2RewiredInputController;

        private static bool player2ButtonForwardPressed;
        private static bool player2ButtonBackPressed;
        private static bool player2ButtonUpPressed;
        private static bool player2ButtonDownPressed;
        private static bool player2Button1Pressed;
        private static bool player2Button2Pressed;
        private static bool player2Button3Pressed;
        private static bool player2Button4Pressed;
        private static bool player2Button5Pressed;
        private static bool player2Button6Pressed;
        private static bool player2Button7Pressed;
        private static bool player2Button8Pressed;
        private static bool player2Button9Pressed;
        private static bool player2Button10Pressed;
        private static bool player2Button11Pressed;
        private static bool player2Button12Pressed;

        public static ButtonPress[] excludedButtonPresses;

        private static bool IsExcludedButtonPress(ButtonPress buttonPress)
        {
            int length = excludedButtonPresses.Length;
            for (int i = 0; i < length; i++)
            {
                if (buttonPress != excludedButtonPresses[i])
                {
                    continue;                
                }

                return true;
            }

            return false;
        }

        public static int availableTracks = 5;
        public static int currentTrack;

        public static int maxRecordingFrames = 600;

        private static bool record;
        private static bool playback;

        private static int currentFrame;

        private enum ButtonPressType
        {
            Hold,
            Release
        }

        [Serializable]
        private struct ButtonPressData
        {
            /// <summary>
            /// Hold = 0. 
            /// Release = 1.
            /// </summary>
            public byte buttonPressType;
            /// <summary>
            /// Forward = 0.
            /// Back = 1.
            /// Up = 2.
            /// Down = 3.
            /// Button1 = 4.
            /// Button2 = 5.
            /// Button3 = 6.
            /// Button4 = 7.
            /// Button5 = 8.
            /// Button6 = 9.
            /// Button7 = 10.
            /// Button8 = 11.
            /// Button9 = 12.
            /// Button10 = 13.
            /// Button11 = 14.
            /// Button12 = 15.
            /// </summary>
            public byte buttonPress;
            public int buttonPressFrame;
        }

        [Serializable]
        private struct RecordedButtonPressData
        {
            public int trackNumber;
            private List<ButtonPressData> buttonPressDatas;
            public List<ButtonPressData> GetButtonPressDatas()
            {
                if (buttonPressDatas == null)
                {
                    buttonPressDatas = new List<ButtonPressData>();
                }

                return buttonPressDatas;
            }
        }
        private static RecordedButtonPressData[] recordedButtonPressDatas;

        #region Set Methods

        public static void SetAllPlayersRewiredInputController()
        {
            RewiredInputController[] rewiredInputControllers = UFE.UFEInstance.GetComponents<RewiredInputController>();

            if (rewiredInputControllers == null)
            {
                return;
            }

            int length = rewiredInputControllers.Length;
            for (int i = 0; i < length; i++)
            {
                if (rewiredInputControllers[i].rewiredPlayerId == 0)
                {
                    player1RewiredInputController = rewiredInputControllers[i];
                }
                else if (rewiredInputControllers[i].rewiredPlayerId == 1)
                {
                    player2RewiredInputController = rewiredInputControllers[i];
                }
            }
        }

        private static void SetRewiredInputControllersRecording()
        {
            if (player1RewiredInputController == null
                || player2RewiredInputController == null)
            {
                return;
            }

            player1RewiredInputController.rewiredPlayerId = 1;
            player2RewiredInputController.rewiredPlayerId = 0;
        }

        public static void SetRewiredInputControllersPlayback()
        {
            if (player1RewiredInputController == null
                || player2RewiredInputController == null)
            {
                return;
            }

            player1RewiredInputController.rewiredPlayerId = 0;
            player2RewiredInputController.rewiredPlayerId = 1;
        }

        public static void SetRecordedButtonPressDatas()
        {
            recordedButtonPressDatas = new RecordedButtonPressData[availableTracks];
            for (int i = 0; i < availableTracks; i++)
            {
                recordedButtonPressDatas[i] = new RecordedButtonPressData();
                recordedButtonPressDatas[i].trackNumber = i;
                recordedButtonPressDatas[i].GetButtonPressDatas();
            }
        }

        #endregion

        #region Track Methods

        public static void NextTrack()
        {
            currentTrack++;

            if (currentTrack > recordedButtonPressDatas.Length - 1)
            {
                currentTrack = 0;
            }

            currentFrame = 0;

            ResetReplayVariables();
        }

        public static void PreviousTrack()
        {
            currentTrack--;

            if (currentTrack < 0)
            {
                currentTrack = recordedButtonPressDatas.Length - 1;
            }

            currentFrame = 0;

            ResetReplayVariables();
        }

        private static bool IsCurrentTrackEnded()
        {
            int length = recordedButtonPressDatas.Length;
            for (int i = 0; i < length; i++)
            {
                if (i != currentTrack)
                {
                    continue;
                }

                int count = recordedButtonPressDatas[i].GetButtonPressDatas().Count;
                for (int a = 0; a < count; a++)
                {
                    if (a != recordedButtonPressDatas[i].GetButtonPressDatas().Count - 1)
                    {
                        continue;
                    }

                    if (currentFrame > recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressFrame) return true;
                }

                break;
            }

            return false;
        }

        #endregion

        #region Record Methods

        public static void StartRecording()
        {
            record = true;

            playback = false;

            currentFrame = 0;

            ResetCurrentTrackButtonPressData();
        }

        public static void StopRecording()
        {
            record = false;

            currentFrame = 0;
        }

        public static void Record()
        {
            if (record == false
                || UFE.isPaused() == true)
            {
                return;
            }

            playback = false;

            SetRewiredInputControllersRecording();

            RecordPlayerButtonPressData(2);

            currentFrame++;

            if (currentFrame >= maxRecordingFrames)
            {
                StopRecording();
            }
        }

        private static void RecordPlayerButtonPressData(int playerNumber)
        {
            #region Player 2

            if (playerNumber == 2)
            {
                if (UFE.p2ControlsScript.inputHeldDown[ButtonPress.Forward] > 0)
                {
                    if (player2ButtonForwardPressed == false)
                    {
                        player2ButtonForwardPressed = true;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Forward);
                    }
                }
                else
                {
                    if (player2ButtonForwardPressed == true)
                    {
                        player2ButtonForwardPressed = false;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Forward);
                    }
                }

                if (UFE.p2ControlsScript.inputHeldDown[ButtonPress.Back] > 0)
                {
                    if (player2ButtonBackPressed == false)
                    {
                        player2ButtonBackPressed = true;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Back);
                    }
                }
                else
                {
                    if (player2ButtonBackPressed == true)
                    {
                        player2ButtonBackPressed = false;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Back);
                    }
                }

                if (UFE.p2ControlsScript.inputHeldDown[ButtonPress.Up] > 0)
                {
                    if (player2ButtonUpPressed == false)
                    {
                        player2ButtonUpPressed = true;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Up);
                    }
                }
                else
                {
                    if (player2ButtonUpPressed == true)
                    {
                        player2ButtonUpPressed = false;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Up);
                    }
                }

                if (UFE.p2ControlsScript.inputHeldDown[ButtonPress.Down] > 0)
                {
                    if (player2ButtonDownPressed == false)
                    {
                        player2ButtonDownPressed = true;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Down);
                    }
                }
                else
                {
                    if (player2ButtonDownPressed == true)
                    {
                        player2ButtonDownPressed = false;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Down);
                    }
                }

                if (UFE.p2ControlsScript.inputHeldDown[ButtonPress.Button1] > 0)
                {
                    if (player2Button1Pressed == false)
                    {
                        player2Button1Pressed = true;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button1);
                    }
                }
                else
                {
                    if (player2Button1Pressed == true)
                    {
                        player2Button1Pressed = false;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button1);
                    }
                }

                if (UFE.p2ControlsScript.inputHeldDown[ButtonPress.Button2] > 0)
                {
                    if (player2Button2Pressed == false)
                    {
                        player2Button2Pressed = true;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button2);
                    }
                }
                else
                {
                    if (player2Button2Pressed == true)
                    {
                        player2Button2Pressed = false;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button2);
                    }
                }

                if (UFE.p2ControlsScript.inputHeldDown[ButtonPress.Button3] > 0)
                {
                    if (player2Button3Pressed == false)
                    {
                        player2Button3Pressed = true;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button3);
                    }
                }
                else
                {
                    if (player2Button3Pressed == true)
                    {
                        player2Button3Pressed = false;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button3);
                    }
                }

                if (UFE.p2ControlsScript.inputHeldDown[ButtonPress.Button4] > 0)
                {
                    if (player2Button4Pressed == false)
                    {
                        player2Button4Pressed = true;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button4);
                    }
                }
                else
                {
                    if (player2Button4Pressed == true)
                    {
                        player2Button4Pressed = false;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button4);
                    }
                }

                if (UFE.p2ControlsScript.inputHeldDown[ButtonPress.Button5] > 0)
                {
                    if (player2Button5Pressed == false)
                    {
                        player2Button5Pressed = true;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button5);
                    }
                }
                else
                {
                    if (player2Button5Pressed == true)
                    {
                        player2Button5Pressed = false;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button5);
                    }
                }

                if (UFE.p2ControlsScript.inputHeldDown[ButtonPress.Button6] > 0)
                {
                    if (player2Button6Pressed == false)
                    {
                        player2Button6Pressed = true;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button6);
                    }
                }
                else
                {
                    if (player2Button6Pressed == true)
                    {
                        player2Button6Pressed = false;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button6);
                    }
                }

                if (UFE.p2ControlsScript.inputHeldDown[ButtonPress.Button7] > 0)
                {
                    if (player2Button7Pressed == false)
                    {
                        player2Button7Pressed = true;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button7);
                    }
                }
                else
                {
                    if (player2Button7Pressed == true)
                    {
                        player2Button7Pressed = false;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button7);
                    }
                }

                if (UFE.p2ControlsScript.inputHeldDown[ButtonPress.Button8] > 0)
                {
                    if (player2Button8Pressed == false)
                    {
                        player2Button8Pressed = true;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button8);
                    }
                }
                else
                {
                    if (player2Button8Pressed == true)
                    {
                        player2Button8Pressed = false;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button8);
                    }
                }

                if (UFE.p2ControlsScript.inputHeldDown[ButtonPress.Button9] > 0)
                {
                    if (player2Button9Pressed == false)
                    {
                        player2Button9Pressed = true;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button9);
                    }
                }
                else
                {
                    if (player2Button9Pressed == true)
                    {
                        player2Button9Pressed = false;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button9);
                    }
                }

                if (UFE.p2ControlsScript.inputHeldDown[ButtonPress.Button10] > 0)
                {
                    if (player2Button10Pressed == false)
                    {
                        player2Button10Pressed = true;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button10);
                    }
                }
                else
                {
                    if (player2Button10Pressed == true)
                    {
                        player2Button10Pressed = false;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button10);
                    }
                }

                if (UFE.p2ControlsScript.inputHeldDown[ButtonPress.Button11] > 0)
                {
                    if (player2Button11Pressed == false)
                    {
                        player2Button11Pressed = true;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button11);
                    }
                }
                else
                {
                    if (player2Button11Pressed == true)
                    {
                        player2Button11Pressed = false;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button11);
                    }
                }

                if (UFE.p2ControlsScript.inputHeldDown[ButtonPress.Button12] > 0)
                {
                    if (player2Button12Pressed == false)
                    {
                        player2Button12Pressed = true;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button12);
                    }
                }
                else
                {
                    if (player2Button12Pressed == true)
                    {
                        player2Button12Pressed = false;

                        AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button12);
                    }
                }
            }

            #endregion
        }

        private static void AddButtonPressDataToRecordedButtonPressDataButtonPressDatasList(int playerNumber, ButtonPressType buttonPressType, ButtonPress buttonPress)
        {
            if (IsExcludedButtonPress(buttonPress) == true)
            {
                return;
            }

            if (playerNumber == 2)
            {
                int length = recordedButtonPressDatas.Length;
                for (int i = 0; i < length; i++)
                {
                    if (i != currentTrack)
                    {
                        continue;
                    }

                    ButtonPressData newButtonPressData = new ButtonPressData();
                    newButtonPressData.buttonPressType = (byte)buttonPressType;
                    newButtonPressData.buttonPress = (byte)buttonPress;
                    newButtonPressData.buttonPressFrame = currentFrame;

                    recordedButtonPressDatas[currentTrack].GetButtonPressDatas().Add(newButtonPressData);

                    break;
                }
            }
        }

        #endregion

        #region Playback Methods

        public static void StartPlayback()
        {
            record = false;

            playback = true;

            currentFrame = 0;
        }

        public static void StopPlayback()
        {
            playback = false;

            currentFrame = 0;
        }

        public static void Playback()
        {
            if (playback == false
                || UFE.isPaused() == true)
            {
                return;
            }

            record = false;

            SetRewiredInputControllersPlayback();

            CheckRecordedButtonPressData(2);

            PressControllerInputs(2);

            currentFrame++;

            if (IsCurrentTrackEnded() == true)
            {
                currentFrame = 0;
            }
        }

        private static void CheckRecordedButtonPressData(int playerNumber)
        {
            #region Player 2

            if (playerNumber == 2)
            {
                int length = recordedButtonPressDatas.Length;
                for (int i = 0; i < length; i++)
                {
                    if (i != currentTrack)
                    {
                        continue;
                    }

                    int countA = recordedButtonPressDatas[i].GetButtonPressDatas().Count;
                    for (int a = 0; a < countA; a++)
                    {
                        if (currentFrame != recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressFrame)
                        {
                            continue;
                        }

                        switch (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPress)
                        {
                            case 0:
                                if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 0)
                                {
                                    player2ButtonForwardPressed = true;
                                }
                                else if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 1)
                                {
                                    player2ButtonForwardPressed = false;
                                }
                                break;

                            case 1:
                                if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 0)
                                {
                                    player2ButtonBackPressed = true;
                                }
                                else if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 1)
                                {
                                    player2ButtonBackPressed = false;
                                }
                                break;

                            case 2:
                                if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 0)
                                {
                                    player2ButtonUpPressed = true;
                                }
                                else if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 1)
                                {
                                    player2ButtonUpPressed = false;
                                }
                                break;

                            case 3:
                                if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 0)
                                {
                                    player2ButtonDownPressed = true;
                                }
                                else if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 1)
                                {
                                    player2ButtonDownPressed = false;
                                }
                                break;

                            case 4:
                                if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 0)
                                {
                                    player2Button1Pressed = true;
                                }
                                else if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 1)
                                {
                                    player2Button1Pressed = false;
                                }
                                break;

                            case 5:
                                if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 0)
                                {
                                    player2Button2Pressed = true;
                                }
                                else if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 1)
                                {
                                    player2Button2Pressed = false;
                                }
                                break;

                            case 6:
                                if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 0)
                                {
                                    player2Button3Pressed = true;
                                }
                                else if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 1)
                                {
                                    player2Button3Pressed = false;
                                }
                                break;

                            case 7:
                                if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 0)
                                {
                                    player2Button4Pressed = true;
                                }
                                else if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 1)
                                {
                                    player2Button4Pressed = false;
                                }
                                break;

                            case 8:
                                if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 0)
                                {
                                    player2Button5Pressed = true;
                                }
                                else if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 1)
                                {
                                    player2Button5Pressed = false;
                                }
                                break;

                            case 9:
                                if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 0)
                                {
                                    player2Button6Pressed = true;
                                }
                                else if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 1)
                                {
                                    player2Button6Pressed = false;
                                }
                                break;

                            case 10:
                                if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 0)
                                {
                                    player2Button7Pressed = true;
                                }
                                else if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 1)
                                {
                                    player2Button7Pressed = false;
                                }
                                break;

                            case 11:
                                if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 0)
                                {
                                    player2Button8Pressed = true;
                                }
                                else if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 1)
                                {
                                    player2Button8Pressed = false;
                                }
                                break;

                            case 12:
                                if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 0)
                                {
                                    player2Button9Pressed = true;
                                }
                                else if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 1)
                                {
                                    player2Button9Pressed = false;
                                }
                                break;

                            case 13:
                                if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 0)
                                {
                                    player2Button10Pressed = true;
                                }
                                else if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 1)
                                {
                                    player2Button10Pressed = false;
                                }
                                break;

                            case 14:
                                if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 0)
                                {
                                    player2Button11Pressed = true;
                                }
                                else if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 1)
                                {
                                    player2Button11Pressed = false;
                                }
                                break;

                            case 15:
                                if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 0)
                                {
                                    player2Button12Pressed = true;
                                }
                                else if (recordedButtonPressDatas[i].GetButtonPressDatas()[a].buttonPressType == 1)
                                {
                                    player2Button12Pressed = false;
                                }
                                break;
                        }
                    }

                    break;
                }
            }

            #endregion
        }

        private static void PressControllerInputs(int playerNumber)
        {
            #region Player 2

            if (playerNumber == 2)
            {
                if (player2ButtonForwardPressed == true)
                {
                    if (UFE.GetPlayer2ControlsScript().mirror == -1)
                    {
                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.HorizontalAxis, 1);
                    }
                    else
                    {
                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.HorizontalAxis, -1);
                    }
                }

                if (player2ButtonBackPressed == true)
                {
                    if (UFE.GetPlayer2ControlsScript().mirror == -1)
                    {
                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.HorizontalAxis, -1);
                    }
                    else
                    {
                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.HorizontalAxis, 1);
                    }
                }

                if (player2ButtonUpPressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.VerticalAxis, 1);
                }

                if (player2ButtonDownPressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.VerticalAxis, -1);
                }

                if (player2Button1Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer2Controller(), ButtonPress.Button1);
                }

                if (player2Button2Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer2Controller(), ButtonPress.Button2);
                }

                if (player2Button3Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer2Controller(), ButtonPress.Button3);
                }

                if (player2Button4Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer2Controller(), ButtonPress.Button4);
                }

                if (player2Button5Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer2Controller(), ButtonPress.Button5);
                }

                if (player2Button6Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer2Controller(), ButtonPress.Button6);
                }

                if (player2Button7Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer2Controller(), ButtonPress.Button7);
                }

                if (player2Button8Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer2Controller(), ButtonPress.Button8);
                }

                if (player2Button9Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer2Controller(), ButtonPress.Button9);
                }

                if (player2Button10Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer2Controller(), ButtonPress.Button10);
                }

                if (player2Button11Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer2Controller(), ButtonPress.Button11);
                }

                if (player2Button12Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer2Controller(), ButtonPress.Button12);
                }
            }

            #endregion
        }

        #endregion

        #region Reset Methods

        private static void ResetReplayVariables()
        {
            player2ButtonForwardPressed = false;
            player2ButtonBackPressed = false;
            player2ButtonUpPressed = false;
            player2ButtonDownPressed = false;
            player2Button1Pressed = false;
            player2Button2Pressed = false;
            player2Button3Pressed = false;
            player2Button4Pressed = false;
            player2Button5Pressed = false;
            player2Button6Pressed = false;
            player2Button7Pressed = false;
            player2Button8Pressed = false;
            player2Button9Pressed = false;
            player2Button10Pressed = false;
            player2Button11Pressed = false;
            player2Button12Pressed = false;
        }

        public static void ResetButtonRecordingVariables()
        {
            currentTrack = 0;

            record = false;

            playback = false;

            currentFrame = 0;

            if (recordedButtonPressDatas != null)
            {
                int length = recordedButtonPressDatas.Length;
                for (int i = 0; i < length; i++)
                {
                    recordedButtonPressDatas[i].GetButtonPressDatas().Clear();
                }
            }
        }

        private static void ResetCurrentTrackButtonPressData()
        {
            int length = recordedButtonPressDatas.Length;
            for (int i = 0; i < length; i++)
            {
                if (i != currentTrack)
                {
                    continue;
                }

                recordedButtonPressDatas[i].GetButtonPressDatas().Clear();

                break;
            }
        }

        #endregion
    }
}
