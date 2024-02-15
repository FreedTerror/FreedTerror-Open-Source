using UFE3D;
using UnityEngine;

namespace FreedTerror.UFE2
{
    public class GameModeGameObjectController : MonoBehaviour
    {
        [System.Serializable]
        private class GameModeOptions
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
                var item = gameModeOptionsArray[i];

                if (UFE.gameMode == item.gameMode)
                {
                    UFE2Manager.SetGameObjectActive(item.gameObjectArray, true);
                }
                else
                {
                    UFE2Manager.SetGameObjectActive(item.gameObjectArray, false);
                }
            }
        }
    }
}