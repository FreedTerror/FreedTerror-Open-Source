using System;
using System.Collections.Generic;
using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public static class UFE2FTEReplayOptionsManager
    {
        private static bool player1ButtonForwardPressed;
        private static bool player1ButtonBackPressed;
        private static bool player1ButtonUpPressed;
        private static bool player1ButtonDownPressed;
        private static bool player1Button1Pressed;
        private static bool player1Button2Pressed;
        private static bool player1Button3Pressed;
        private static bool player1Button4Pressed;
        private static bool player1Button5Pressed;
        private static bool player1Button6Pressed;
        private static bool player1Button7Pressed;
        private static bool player1Button8Pressed;
        private static bool player1Button9Pressed;
        private static bool player1Button10Pressed;
        private static bool player1Button11Pressed;
        private static bool player1Button12Pressed;

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

        public static int currentFrame;

        public static bool replaySaved;

        public static bool IsReplaySaved()
        {
            return replaySaved;
        }

        [SerializeField]
        public static GameMode[] excludedGameModeArray = Array.Empty<GameMode>();

        public static bool IsExcludedGameMode(GameMode gameMode)
        {
            int length = excludedGameModeArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (gameMode == excludedGameModeArray[i]) return true;
            }

            return false;
        }

        public static ButtonPress[] excludedButtonPressArray = Array.Empty<ButtonPress>();

        private static bool IsExcludedButtonPress(ButtonPress buttonPress)
        {
            int length = excludedButtonPressArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (buttonPress == excludedButtonPressArray[i]) return true;
            }

            return false;
        }

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
        private struct MatchData
        {
            public string player1CharacterName;
            public string player2CharacterName;
            public string stageName;
        }

        [Serializable]
        private struct RecordedRoundData
        {
            public int roundNumber;
            private List<ButtonPressData> player1ButtonPressDataList;
            public List<ButtonPressData> GetPlayer1ButtonPressDataList()
            {
                if (player1ButtonPressDataList == null)
                {
                    player1ButtonPressDataList = new List<ButtonPressData>();
                }

                return player1ButtonPressDataList;
            }
            private List<ButtonPressData> player2ButtonPressDataList;
            public List<ButtonPressData> GetPlayer2ButtonPressDataList()
            {
                if (player2ButtonPressDataList == null)
                {
                    player2ButtonPressDataList = new List<ButtonPressData>();
                }

                return player2ButtonPressDataList;
            }
        }

        [Serializable]
        private struct RecordedReplayData
        {
            public MatchData matchData;
            private List<RecordedRoundData> recordedRoundDataList;
            public List<RecordedRoundData> GetRecordedRoundDataList()
            {
                if (recordedRoundDataList == null)
                {
                    recordedRoundDataList = new List<RecordedRoundData>();
                }

                return recordedRoundDataList;
            }
        }
        private static RecordedReplayData recordedReplayData;

        public static bool RecordedReplayDataExists()
        {
            if (recordedReplayData.GetRecordedRoundDataList() == null
                || recordedReplayData.GetRecordedRoundDataList().Count == 0)
            {
                return false;
            }

            return true;
        }

        [Serializable]
        private struct LoadedRoundData
        {
            public int roundNumber;
            public ButtonPressData[] player1ButtonPressDataArray;
            public ButtonPressData[] player2ButtonPressDataArray;
        }

        [Serializable]
        private struct LoadedReplayData
        {
            public MatchData matchData;
            public LoadedRoundData[] loadedRoundDataArray;
        }
        private static LoadedReplayData loadedReplayData;

        public static bool LoadedReplayDataExists()
        {
            if (loadedReplayData.loadedRoundDataArray == null
                || loadedReplayData.loadedRoundDataArray.Length == 0)
            {
                return false;
            }

            return true;
        }

        public static void CreateNewRecordedRoundData()
        {
            if (IsExcludedGameMode(UFE.gameMode) == true
                || LoadedReplayDataExists() == true) return;

            recordedReplayData.matchData.player1CharacterName = UFE.GetPlayer1ControlsScript().myInfo.characterName;
            recordedReplayData.matchData.player2CharacterName = UFE.GetPlayer2ControlsScript().myInfo.characterName;
            recordedReplayData.matchData.stageName = UFE.GetStage().stageName;
            RecordedRoundData newRecordedRoundData = new RecordedRoundData();
            newRecordedRoundData.roundNumber = UFE.config.currentRound;
            newRecordedRoundData.GetPlayer1ButtonPressDataList();
            newRecordedRoundData.GetPlayer2ButtonPressDataList();
            recordedReplayData.GetRecordedRoundDataList().Add(newRecordedRoundData);
        }

        #region Replay Methods

        public static void OpenReplayFilesFolder()
        {
            Application.OpenURL(Application.persistentDataPath + "\\" + "Replay Save Data");
        }

        public static string[] GetReplayFileNames()
        {
            if (ES3.DirectoryExists("Replay Save Data/") == false) return null;

            return ES3.GetFiles("Replay Save Data/");
        }

        public static void SaveReplay()
        {
            if (RecordedReplayDataExists() == false) return;

            LoadedReplayData newLoadedReplayData = new LoadedReplayData();
            newLoadedReplayData.matchData = new MatchData();
            newLoadedReplayData.matchData.player1CharacterName = recordedReplayData.matchData.player1CharacterName;
            newLoadedReplayData.matchData.player2CharacterName = recordedReplayData.matchData.player2CharacterName;
            newLoadedReplayData.matchData.stageName = recordedReplayData.matchData.stageName;

            int count = recordedReplayData.GetRecordedRoundDataList().Count;
            newLoadedReplayData.loadedRoundDataArray = new LoadedRoundData[count];
            for (int i = 0; i < count; i++)
            {
                newLoadedReplayData.loadedRoundDataArray[i] = new LoadedRoundData();
                newLoadedReplayData.loadedRoundDataArray[i].roundNumber = recordedReplayData.GetRecordedRoundDataList()[i].roundNumber;
                newLoadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray = recordedReplayData.GetRecordedRoundDataList()[i].GetPlayer1ButtonPressDataList().ToArray();
                newLoadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray = recordedReplayData.GetRecordedRoundDataList()[i].GetPlayer2ButtonPressDataList().ToArray();
            }

            try
            {
                replaySaved = true;

                ES3.Save("replaySaveData", newLoadedReplayData, "Replay Save Data/" + newLoadedReplayData.matchData.player1CharacterName + " VS. " + newLoadedReplayData.matchData.player2CharacterName + " " + System.DateTime.Now.ToString("MM-dd-yyyy HH-mm-ss") + ".es3");

#if UNITY_EDITOR               
                Debug.Log("File saved: " + "Replay Save Data/" + newLoadedReplayData.matchData.player1CharacterName + " VS. " + newLoadedReplayData.matchData.player2CharacterName + " " + System.DateTime.Now.ToString("MM-dd-yyyy HH-mm-ss") + ".es3");
#endif
            }
            catch (Exception exception)
            {
#if UNITY_EDITOR
                Debug.Log("File saved error: " + exception);
#endif

                replaySaved = false;
            }
        }

        private static void LoadReplay(string replayFileName)
        {
            try
            {
                loadedReplayData = ES3.Load<LoadedReplayData>("replaySaveData", "Replay Save Data/" + replayFileName);

#if UNITY_EDITOR
                Debug.Log("File loaded: " + "Replay Save Data/" + replayFileName);
#endif
            }
            catch (Exception exception)
            {
#if UNITY_EDITOR
                Debug.Log("File loaded error: " + exception);
#endif
            }
        }

        public static void DeleteReplay(string replayFileName)
        {
            try
            {
                ES3.DeleteFile("Replay Save Data/" + replayFileName);

#if UNITY_EDITOR
                Debug.Log("File deleted: " + "Replay Save Data/" + replayFileName);
#endif
            }
            catch (Exception exception)
            {
#if UNITY_EDITOR
                Debug.Log("File deleted error: " + exception);
#endif
            }
        }

        public static void StartReplay(string replayFileName, GameObject gameObjectToDestroy)
        {
            LoadReplay(replayFileName);

            UFE.SetPlayer1(UFE2FTEHelperMethodsManager.GetCharacterInfoFromGlobalInfo(loadedReplayData.matchData.player1CharacterName));

            UFE.SetPlayer2(UFE2FTEHelperMethodsManager.GetCharacterInfoFromGlobalInfo(loadedReplayData.matchData.player2CharacterName));

            UFE.SetStage(UFE2FTEHelperMethodsManager.GetStageOptionsFromGlobalInfo(loadedReplayData.matchData.stageName));

            if (UFE.config.player1Character == null
                || UFE.config.player2Character == null
                || UFE.config.selectedStage == null) return;

            //Add event

            if (gameObjectToDestroy != null)
            {
                //Destroy(gameObjectToDestroy);
            }

            UFE.gameMode = GameMode.VersusMode;

            UFE.StartLoadingBattleScreen((float)UFE.config.gameGUI.screenFadeDuration);
        }

        #endregion

        #region Record Methods

        public static void Record()
        {
            if (RecordedReplayDataExists() == false
                || LoadedReplayDataExists() == true
                || UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null
                || UFE.isPaused() == true) return;

            RecordPlayerButtonPressData(1);

            RecordPlayerButtonPressData(2);

            currentFrame++;
        }

        public static void RecordPlayerButtonPressData(int playerNumber)
        {
            #region Player 1

            if (playerNumber == 1)
            {
                if (UFE.GetPlayer1ControlsScript().inputHeldDown[ButtonPress.Forward] > 0)
                {
                    if (player1ButtonForwardPressed == false)
                    {
                        player1ButtonForwardPressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Hold, ButtonPress.Forward);
                    }
                }
                else
                {
                    if (player1ButtonForwardPressed == true)
                    {
                        player1ButtonForwardPressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Release, ButtonPress.Forward);
                    }
                }

                if (UFE.GetPlayer1ControlsScript().inputHeldDown[ButtonPress.Back] > 0)
                {
                    if (player1ButtonBackPressed == false)
                    {
                        player1ButtonBackPressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Hold, ButtonPress.Back);
                    }
                }
                else
                {
                    if (player1ButtonBackPressed == true)
                    {
                        player1ButtonBackPressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Release, ButtonPress.Back);
                    }
                }

                if (UFE.GetPlayer1ControlsScript().inputHeldDown[ButtonPress.Up] > 0)
                {
                    if (player1ButtonUpPressed == false)
                    {
                        player1ButtonUpPressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Hold, ButtonPress.Up);
                    }
                }
                else
                {
                    if (player1ButtonUpPressed == true)
                    {
                        player1ButtonUpPressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Release, ButtonPress.Up);
                    }
                }

                if (UFE.GetPlayer1ControlsScript().inputHeldDown[ButtonPress.Down] > 0)
                {
                    if (player1ButtonDownPressed == false)
                    {
                        player1ButtonDownPressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Hold, ButtonPress.Down);
                    }
                }
                else
                {
                    if (player1ButtonDownPressed == true)
                    {
                        player1ButtonDownPressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Release, ButtonPress.Down);
                    }
                }

                if (UFE.GetPlayer1ControlsScript().inputHeldDown[ButtonPress.Button1] > 0)
                {
                    if (player1Button1Pressed == false)
                    {
                        player1Button1Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Hold, ButtonPress.Button1);
                    }
                }
                else
                {
                    if (player1Button1Pressed == true)
                    {
                        player1Button1Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Release, ButtonPress.Button1);
                    }
                }

                if (UFE.GetPlayer1ControlsScript().inputHeldDown[ButtonPress.Button2] > 0)
                {
                    if (player1Button2Pressed == false)
                    {
                        player1Button2Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Hold, ButtonPress.Button2);
                    }
                }
                else
                {
                    if (player1Button2Pressed == true)
                    {
                        player1Button2Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Release, ButtonPress.Button2);
                    }
                }

                if (UFE.GetPlayer1ControlsScript().inputHeldDown[ButtonPress.Button3] > 0)
                {
                    if (player1Button3Pressed == false)
                    {
                        player1Button3Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Hold, ButtonPress.Button3);
                    }
                }
                else
                {
                    if (player1Button3Pressed == true)
                    {
                        player1Button3Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Release, ButtonPress.Button3);
                    }
                }

                if (UFE.GetPlayer1ControlsScript().inputHeldDown[ButtonPress.Button4] > 0)
                {
                    if (player1Button4Pressed == false)
                    {
                        player1Button4Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Hold, ButtonPress.Button4);
                    }
                }
                else
                {
                    if (player1Button4Pressed == true)
                    {
                        player1Button4Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Release, ButtonPress.Button4);
                    }
                }

                if (UFE.GetPlayer1ControlsScript().inputHeldDown[ButtonPress.Button5] > 0)
                {
                    if (player1Button5Pressed == false)
                    {
                        player1Button5Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Hold, ButtonPress.Button5);
                    }
                }
                else
                {
                    if (player1Button5Pressed == true)
                    {
                        player1Button5Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Release, ButtonPress.Button5);
                    }
                }

                if (UFE.GetPlayer1ControlsScript().inputHeldDown[ButtonPress.Button6] > 0)
                {
                    if (player1Button6Pressed == false)
                    {
                        player1Button6Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Hold, ButtonPress.Button6);
                    }
                }
                else
                {
                    if (player1Button6Pressed == true)
                    {
                        player1Button6Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Release, ButtonPress.Button6);
                    }
                }

                if (UFE.GetPlayer1ControlsScript().inputHeldDown[ButtonPress.Button7] > 0)
                {
                    if (player1Button7Pressed == false)
                    {
                        player1Button7Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Hold, ButtonPress.Button7);
                    }
                }
                else
                {
                    if (player1Button7Pressed == true)
                    {
                        player1Button7Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Release, ButtonPress.Button7);
                    }
                }

                if (UFE.GetPlayer1ControlsScript().inputHeldDown[ButtonPress.Button8] > 0)
                {
                    if (player1Button8Pressed == false)
                    {
                        player1Button8Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Hold, ButtonPress.Button8);
                    }
                }
                else
                {
                    if (player1Button8Pressed == true)
                    {
                        player1Button8Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Release, ButtonPress.Button8);
                    }
                }

                if (UFE.GetPlayer1ControlsScript().inputHeldDown[ButtonPress.Button9] > 0)
                {
                    if (player1Button9Pressed == false)
                    {
                        player1Button9Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Hold, ButtonPress.Button9);
                    }
                }
                else
                {
                    if (player1Button9Pressed == true)
                    {
                        player1Button9Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Release, ButtonPress.Button9);
                    }
                }

                if (UFE.GetPlayer1ControlsScript().inputHeldDown[ButtonPress.Button10] > 0)
                {
                    if (player1Button10Pressed == false)
                    {
                        player1Button10Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Hold, ButtonPress.Button10);
                    }
                }
                else
                {
                    if (player1Button10Pressed == true)
                    {
                        player1Button10Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Release, ButtonPress.Button10);
                    }
                }

                if (UFE.GetPlayer1ControlsScript().inputHeldDown[ButtonPress.Button11] > 0)
                {
                    if (player1Button11Pressed == false)
                    {
                        player1Button11Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Hold, ButtonPress.Button11);
                    }
                }
                else
                {
                    if (player1Button11Pressed == true)
                    {
                        player1Button11Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Release, ButtonPress.Button11);
                    }
                }

                if (UFE.GetPlayer1ControlsScript().inputHeldDown[ButtonPress.Button12] > 0)
                {
                    if (player1Button12Pressed == false)
                    {
                        player1Button12Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Hold, ButtonPress.Button12);
                    }
                }
                else
                {
                    if (player1Button12Pressed == true)
                    {
                        player1Button12Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(1, ButtonPressType.Release, ButtonPress.Button12);
                    }
                }
            }

            #endregion

            #region Player 2

            else if (playerNumber == 2)
            {
                if (UFE.GetPlayer2ControlsScript().inputHeldDown[ButtonPress.Forward] > 0)
                {
                    if (player2ButtonForwardPressed == false)
                    {
                        player2ButtonForwardPressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Forward);
                    }
                }
                else
                {
                    if (player2ButtonForwardPressed == true)
                    {
                        player2ButtonForwardPressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Forward);
                    }
                }

                if (UFE.GetPlayer2ControlsScript().inputHeldDown[ButtonPress.Back] > 0)
                {
                    if (player2ButtonBackPressed == false)
                    {
                        player2ButtonBackPressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Back);
                    }
                }
                else
                {
                    if (player2ButtonBackPressed == true)
                    {
                        player2ButtonBackPressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Back);
                    }
                }

                if (UFE.GetPlayer2ControlsScript().inputHeldDown[ButtonPress.Up] > 0)
                {
                    if (player2ButtonUpPressed == false)
                    {
                        player2ButtonUpPressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Up);
                    }
                }
                else
                {
                    if (player2ButtonUpPressed == true)
                    {
                        player2ButtonUpPressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Up);
                    }
                }

                if (UFE.GetPlayer2ControlsScript().inputHeldDown[ButtonPress.Down] > 0)
                {
                    if (player2ButtonDownPressed == false)
                    {
                        player2ButtonDownPressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Down);
                    }
                }
                else
                {
                    if (player2ButtonDownPressed == true)
                    {
                        player2ButtonDownPressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Down);
                    }
                }

                if (UFE.GetPlayer2ControlsScript().inputHeldDown[ButtonPress.Button1] > 0)
                {
                    if (player2Button1Pressed == false)
                    {
                        player2Button1Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button1);
                    }
                }
                else
                {
                    if (player2Button1Pressed == true)
                    {
                        player2Button1Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button1);
                    }
                }

                if (UFE.GetPlayer2ControlsScript().inputHeldDown[ButtonPress.Button2] > 0)
                {
                    if (player2Button2Pressed == false)
                    {
                        player2Button2Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button2);
                    }
                }
                else
                {
                    if (player2Button2Pressed == true)
                    {
                        player2Button2Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button2);
                    }
                }

                if (UFE.GetPlayer2ControlsScript().inputHeldDown[ButtonPress.Button3] > 0)
                {
                    if (player2Button3Pressed == false)
                    {
                        player2Button3Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button3);
                    }
                }
                else
                {
                    if (player2Button3Pressed == true)
                    {
                        player2Button3Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button3);
                    }
                }

                if (UFE.GetPlayer2ControlsScript().inputHeldDown[ButtonPress.Button4] > 0)
                {
                    if (player2Button4Pressed == false)
                    {
                        player2Button4Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button4);
                    }
                }
                else
                {
                    if (player2Button4Pressed == true)
                    {
                        player2Button4Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button4);
                    }
                }

                if (UFE.GetPlayer2ControlsScript().inputHeldDown[ButtonPress.Button5] > 0)
                {
                    if (player2Button5Pressed == false)
                    {
                        player2Button5Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button5);
                    }
                }
                else
                {
                    if (player2Button5Pressed == true)
                    {
                        player2Button5Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button5);
                    }
                }

                if (UFE.GetPlayer2ControlsScript().inputHeldDown[ButtonPress.Button6] > 0)
                {
                    if (player2Button6Pressed == false)
                    {
                        player2Button6Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button6);
                    }
                }
                else
                {
                    if (player2Button6Pressed == true)
                    {
                        player2Button6Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button6);
                    }
                }

                if (UFE.GetPlayer2ControlsScript().inputHeldDown[ButtonPress.Button7] > 0)
                {
                    if (player2Button7Pressed == false)
                    {
                        player2Button7Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button7);
                    }
                }
                else
                {
                    if (player2Button7Pressed == true)
                    {
                        player2Button7Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button7);
                    }
                }

                if (UFE.GetPlayer2ControlsScript().inputHeldDown[ButtonPress.Button8] > 0)
                {
                    if (player2Button8Pressed == false)
                    {
                        player2Button8Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button8);
                    }
                }
                else
                {
                    if (player2Button8Pressed == true)
                    {
                        player2Button8Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button8);
                    }
                }

                if (UFE.GetPlayer2ControlsScript().inputHeldDown[ButtonPress.Button9] > 0)
                {
                    if (player2Button9Pressed == false)
                    {
                        player2Button9Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button9);
                    }
                }
                else
                {
                    if (player2Button9Pressed == true)
                    {
                        player2Button9Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button9);
                    }
                }

                if (UFE.GetPlayer2ControlsScript().inputHeldDown[ButtonPress.Button10] > 0)
                {
                    if (player2Button10Pressed == false)
                    {
                        player2Button10Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button10);
                    }
                }
                else
                {
                    if (player2Button10Pressed == true)
                    {
                        player2Button10Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button10);
                    }
                }

                if (UFE.GetPlayer2ControlsScript().inputHeldDown[ButtonPress.Button11] > 0)
                {
                    if (player2Button11Pressed == false)
                    {
                        player2Button11Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button11);
                    }
                }
                else
                {
                    if (player2Button11Pressed == true)
                    {
                        player2Button11Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button11);
                    }
                }

                if (UFE.GetPlayer2ControlsScript().inputHeldDown[ButtonPress.Button12] > 0)
                {
                    if (player2Button12Pressed == false)
                    {
                        player2Button12Pressed = true;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Hold, ButtonPress.Button12);
                    }
                }
                else
                {
                    if (player2Button12Pressed == true)
                    {
                        player2Button12Pressed = false;

                        AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(2, ButtonPressType.Release, ButtonPress.Button12);
                    }
                }
            }

            #endregion
        }

        private static void AddButtonPressDataToRecordedReplayDataPlayerButtonPressDatasList(int playerNumber, ButtonPressType buttonPressType, ButtonPress buttonPress)
        {
            if (IsExcludedButtonPress(buttonPress) == true) return;

            if (playerNumber == 1)
            {
                int count = recordedReplayData.GetRecordedRoundDataList().Count;
                for (int i = 0; i < count; i++)
                {
                    if (UFE.config.currentRound != recordedReplayData.GetRecordedRoundDataList()[i].roundNumber) continue;

                    ButtonPressData newButtonPressData = new ButtonPressData();
                    newButtonPressData.buttonPressType = (byte)buttonPressType;
                    newButtonPressData.buttonPress = (byte)buttonPress;
                    newButtonPressData.buttonPressFrame = currentFrame;

                    recordedReplayData.GetRecordedRoundDataList()[i].GetPlayer1ButtonPressDataList().Add(newButtonPressData);

                    break;
                }
            }
            else if (playerNumber == 2)
            {
                int count = recordedReplayData.GetRecordedRoundDataList().Count;
                for (int i = 0; i < count; i++)
                {
                    if (UFE.config.currentRound != recordedReplayData.GetRecordedRoundDataList()[i].roundNumber) continue;

                    ButtonPressData newButtonPressData = new ButtonPressData();
                    newButtonPressData.buttonPressType = (byte)buttonPressType;
                    newButtonPressData.buttonPress = (byte)buttonPress;
                    newButtonPressData.buttonPressFrame = currentFrame;

                    recordedReplayData.GetRecordedRoundDataList()[i].GetPlayer2ButtonPressDataList().Add(newButtonPressData);

                    break;
                }
            }
        }

        #endregion

        #region Playback Methods

        public static void Playback()
        {
            if (LoadedReplayDataExists() == false
                || UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null
                || UFE.isPaused() == true) return;

            CheckLoadedReplayDataButtonPressData(1);

            CheckLoadedReplayDataButtonPressData(2);

            PressControllerInputs(1);

            PressControllerInputs(2);

            currentFrame++;
        }

        public static void CheckLoadedReplayDataButtonPressData(int playerNumber)
        {
            #region Player 1

            if (playerNumber == 1)
            {
                int length = loadedReplayData.loadedRoundDataArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (UFE.config.currentRound != loadedReplayData.loadedRoundDataArray[i].roundNumber) continue;

                    int lengthA = loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (currentFrame != loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressFrame) continue;

                        switch (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPress)
                        {
                            case 0:
                                if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player1ButtonForwardPressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player1ButtonForwardPressed = false;
                                }
                                break;

                            case 1:
                                if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player1ButtonBackPressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player1ButtonBackPressed = false;
                                }
                                break;

                            case 2:
                                if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player1ButtonUpPressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player1ButtonUpPressed = false;
                                }
                                break;

                            case 3:
                                if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player1ButtonDownPressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player1ButtonDownPressed = false;
                                }
                                break;

                            case 4:
                                if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player1Button1Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player1Button1Pressed = false;
                                }
                                break;

                            case 5:
                                if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player1Button2Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player1Button2Pressed = false;
                                }
                                break;

                            case 6:
                                if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player1Button3Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player1Button3Pressed = false;
                                }
                                break;

                            case 7:
                                if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player1Button4Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player1Button4Pressed = false;
                                }
                                break;

                            case 8:
                                if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player1Button5Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player1Button5Pressed = false;
                                }
                                break;

                            case 9:
                                if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player1Button6Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player1Button6Pressed = false;
                                }
                                break;

                            case 10:
                                if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player1Button7Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player1Button7Pressed = false;
                                }
                                break;

                            case 11:
                                if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player1Button8Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player1Button8Pressed = false;
                                }
                                break;

                            case 12:
                                if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player1Button9Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player1Button9Pressed = false;
                                }
                                break;

                            case 13:
                                if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player1Button10Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player1Button10Pressed = false;
                                }
                                break;

                            case 14:
                                if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player1Button11Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player1Button11Pressed = false;
                                }
                                break;

                            case 15:
                                if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player1Button12Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player1ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player1Button12Pressed = false;
                                }
                                break;
                        }
                    }

                    break;
                }
            }

            #endregion

            #region Player 2

            else if (playerNumber == 2)
            {
                int length = loadedReplayData.loadedRoundDataArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (UFE.config.currentRound != loadedReplayData.loadedRoundDataArray[i].roundNumber) continue;

                    int lengthA = loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (currentFrame != loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressFrame) continue;

                        switch (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPress)
                        {
                            case 0:
                                if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player2ButtonForwardPressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player2ButtonForwardPressed = false;
                                }
                                break;

                            case 1:
                                if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player2ButtonBackPressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player2ButtonBackPressed = false;
                                }
                                break;

                            case 2:
                                if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player2ButtonUpPressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player2ButtonUpPressed = false;
                                }
                                break;

                            case 3:
                                if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player2ButtonDownPressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player2ButtonDownPressed = false;
                                }
                                break;

                            case 4:
                                if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player2Button1Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player2Button1Pressed = false;
                                }
                                break;

                            case 5:
                                if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player2Button2Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player2Button2Pressed = false;
                                }
                                break;

                            case 6:
                                if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player2Button3Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player2Button3Pressed = false;
                                }
                                break;

                            case 7:
                                if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player2Button4Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player2Button4Pressed = false;
                                }
                                break;

                            case 8:
                                if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player2Button5Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player2Button5Pressed = false;
                                }
                                break;

                            case 9:
                                if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player2Button6Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player2Button6Pressed = false;
                                }
                                break;

                            case 10:
                                if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player2Button7Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player2Button7Pressed = false;
                                }
                                break;

                            case 11:
                                if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player2Button8Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player2Button8Pressed = false;
                                }
                                break;

                            case 12:
                                if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player2Button9Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player2Button9Pressed = false;
                                }
                                break;

                            case 13:
                                if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player2Button10Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player2Button10Pressed = false;
                                }
                                break;

                            case 14:
                                if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player2Button11Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 1)
                                {
                                    player2Button11Pressed = false;
                                }
                                break;

                            case 15:
                                if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 0)
                                {
                                    player2Button12Pressed = true;
                                }
                                else if (loadedReplayData.loadedRoundDataArray[i].player2ButtonPressDataArray[a].buttonPressType == 1)
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

        public static void PressControllerInputs(int playerNumber)
        {
            #region Player 1

            if (playerNumber == 1)
            {
                if (player1ButtonForwardPressed == true)
                {
                    if (UFE.GetPlayer1ControlsScript().mirror == -1)
                    {
                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer1Controller(), InputType.HorizontalAxis, 1);
                    }
                    else
                    {
                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer1Controller(), InputType.HorizontalAxis, -1);
                    }
                }

                if (player1ButtonBackPressed == true)
                {
                    if (UFE.GetPlayer1ControlsScript().mirror == -1)
                    {
                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer1Controller(), InputType.HorizontalAxis, -1);
                    }
                    else
                    {
                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer1Controller(), InputType.HorizontalAxis, 1);
                    }
                }

                if (player1ButtonUpPressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer1Controller(), InputType.VerticalAxis, 1);
                }

                if (player1ButtonDownPressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer1Controller(), InputType.VerticalAxis, -1);
                }

                if (player1Button1Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer1Controller(), ButtonPress.Button1);
                }

                if (player1Button2Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer1Controller(), ButtonPress.Button2);
                }

                if (player1Button3Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer1Controller(), ButtonPress.Button3);
                }

                if (player1Button4Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer1Controller(), ButtonPress.Button4);
                }

                if (player1Button5Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer1Controller(), ButtonPress.Button5);
                }

                if (player1Button6Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer1Controller(), ButtonPress.Button6);
                }

                if (player1Button7Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer1Controller(), ButtonPress.Button7);
                }

                if (player1Button8Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer1Controller(), ButtonPress.Button8);
                }

                if (player1Button9Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer1Controller(), ButtonPress.Button9);
                }

                if (player1Button10Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer1Controller(), ButtonPress.Button10);
                }

                if (player1Button11Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer1Controller(), ButtonPress.Button11);
                }

                if (player1Button12Pressed == true)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer1Controller(), ButtonPress.Button12);
                }
            }

            #endregion

            #region Player 2

            else if (playerNumber == 2)
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

        public static void ResetReplayVariables()
        {
            player1ButtonForwardPressed = false;
            player1ButtonBackPressed = false;
            player1ButtonUpPressed = false;
            player1ButtonDownPressed = false;
            player1Button1Pressed = false;
            player1Button2Pressed = false;
            player1Button3Pressed = false;
            player1Button4Pressed = false;
            player1Button5Pressed = false;
            player1Button6Pressed = false;
            player1Button7Pressed = false;
            player1Button8Pressed = false;
            player1Button9Pressed = false;
            player1Button10Pressed = false;
            player1Button11Pressed = false;
            player1Button12Pressed = false;

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

            currentFrame = 0;
        }

        public static void ResetRecordedReplayData()
        {
            recordedReplayData.matchData.player1CharacterName = null;
            recordedReplayData.matchData.player2CharacterName = null;
            recordedReplayData.matchData.stageName = null;
            recordedReplayData.GetRecordedRoundDataList().Clear();
        }

        public static void ResetLoadedReplayData()
        {
            loadedReplayData.matchData.player1CharacterName = null;
            loadedReplayData.matchData.player2CharacterName = null;
            loadedReplayData.matchData.stageName = null;
            loadedReplayData.loadedRoundDataArray = null;
        }

        #endregion
    }
}
