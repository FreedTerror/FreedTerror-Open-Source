using System;
using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTEPlayerPausedController : MonoBehaviour
    {
        [Serializable]
        private class PlayerPausedOptions
        {
            public UFE2FTEPlayerPausedOptionsManager.Player player;
            public GameObject[] disabledGameObjectArray;
            public GameObject[] enabledGameObjectArray;

            public static void SetPlayerPausedOptions(PlayerPausedOptions playerPausedOptions)
            {
                if (playerPausedOptions == null)
                {
                    return;
                }

                if (playerPausedOptions.player == UFE2FTEPlayerPausedOptionsManager.Player.Player1
                    && UFE2FTEPlayerPausedOptionsManager.playerPaused == UFE2FTEPlayerPausedOptionsManager.Player.Player1)
                {
                    int length = playerPausedOptions.disabledGameObjectArray.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (playerPausedOptions.disabledGameObjectArray[i] == null)
                        {
                            continue;
                        }

                        playerPausedOptions.disabledGameObjectArray[i].SetActive(false);
                    }

                    length = playerPausedOptions.enabledGameObjectArray.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (playerPausedOptions.enabledGameObjectArray[i] == null)
                        {
                            continue;
                        }
                        
                        playerPausedOptions.enabledGameObjectArray[i].SetActive(true);
                    }
                }
                else if (playerPausedOptions.player == UFE2FTEPlayerPausedOptionsManager.Player.Player2
                    && UFE2FTEPlayerPausedOptionsManager.playerPaused == UFE2FTEPlayerPausedOptionsManager.Player.Player2)
                {
                    int length = playerPausedOptions.disabledGameObjectArray.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (playerPausedOptions.disabledGameObjectArray[i] == null)
                        {
                            continue;
                        }

                        playerPausedOptions.disabledGameObjectArray[i].SetActive(false);
                    }

                    length = playerPausedOptions.enabledGameObjectArray.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (playerPausedOptions.enabledGameObjectArray[i] == null)
                        {
                            continue;
                        }

                        playerPausedOptions.enabledGameObjectArray[i].SetActive(true);
                    }
                }
            }

            public static void SetPlayerPausedOptions(PlayerPausedOptions[] playerPausedOptionsArray)
            {
                if (playerPausedOptionsArray == null)
                {
                    return;
                }

                int length = playerPausedOptionsArray.Length;
                for (int i = 0; i < length; i++)
                {
                    SetPlayerPausedOptions(playerPausedOptionsArray[i]);
                }
            }
        }
        [SerializeField]
        private PlayerPausedOptions[] playerPausedOptionsArray;

        private void Update()
        {
            PlayerPausedOptions.SetPlayerPausedOptions(playerPausedOptionsArray);   
        }
    }
}