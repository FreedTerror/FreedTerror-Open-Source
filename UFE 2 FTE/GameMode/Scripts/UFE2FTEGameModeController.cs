using System;
using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTEGameModeController : MonoBehaviour
    {
        [Serializable]
        private class GameModeOptions
        {
            public GameMode[] gameModeArray;
            public GameObject[] disabledGameObjectArray;
            public GameObject[] enabledGameObjectArray;

            public static void SetGameModeOptions(GameModeOptions gameModeOptions)
            {
                if (gameModeOptions == null)
                {
                    return;
                }

                int length = gameModeOptions.gameModeArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (UFE.gameMode != gameModeOptions.gameModeArray[i])
                    {
                        continue;
                    }

                    int lengthA = gameModeOptions.disabledGameObjectArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (gameModeOptions.disabledGameObjectArray[a] == null)
                        {
                            continue;
                        }

                        gameModeOptions.disabledGameObjectArray[a].SetActive(false);
                    }

                    lengthA = gameModeOptions.enabledGameObjectArray.Length;
                    for (int a = 0; a < lengthA; a++)
                    {
                        if (gameModeOptions.enabledGameObjectArray[a] == null)
                        {
                            continue;
                        }

                        gameModeOptions.enabledGameObjectArray[a].SetActive(true);
                    }

                    break;
                }
            }

            public static void SetGameModeOptions(GameModeOptions[] gameModeOptionsArray)
            {
                if (gameModeOptionsArray == null)
                {
                    return;
                }

                int length = gameModeOptionsArray.Length;
                for (int i = 0; i < length; i++)
                {
                    SetGameModeOptions(gameModeOptionsArray[i]);
                }
            }
        }
        [SerializeField]
        private GameModeOptions[] gameModeOptionsArray;

        private void Update()
        {
            GameModeOptions.SetGameModeOptions(gameModeOptionsArray);   
        }
    }
}