using UFE3D;
using UnityEngine;

namespace UFE2FTE
{
    public class GameModeGameObjectController : MonoBehaviour
    {
        [System.Serializable]
        public class GameModeOptions
        {
            public GameMode[] gameModeArray;
            public GameObject[] gameObjectArray;
        }
        [SerializeField]
        private GameModeOptions[] gameModeOptionsArray;

        private void Update()
        {
            int length = gameModeOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                UFE2FTE.SetGameObjectActive(gameModeOptionsArray[i].gameObjectArray, UFE2FTE.IsGameModeMatch(UFE.gameMode, gameModeOptionsArray[i].gameModeArray));
            }
        }
    }
}