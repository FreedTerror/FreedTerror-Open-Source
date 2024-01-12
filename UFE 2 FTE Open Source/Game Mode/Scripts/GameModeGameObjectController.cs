using UFE3D;
using UnityEngine;

namespace UFE2FTE
{
    public class GameModeGameObjectController : MonoBehaviour
    {
        [System.Serializable]
        public class GameModeOptions
        {
            public GameMode gameMode;
            public GameObject[] gameObjectArray;
        }
        [SerializeField]
        private GameModeOptions[] gameModeOptionsArray;

        private void Update()
        {
            int length = gameModeOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (UFE.gameMode == gameModeOptionsArray[i].gameMode)
                {
                    UFE2FTE.SetGameObjectActive(gameModeOptionsArray[i].gameObjectArray, true);

                    continue;
                }
                else
                {
                    UFE2FTE.SetGameObjectActive(gameModeOptionsArray[i].gameObjectArray, false);
                }
            }
        }
    }
}