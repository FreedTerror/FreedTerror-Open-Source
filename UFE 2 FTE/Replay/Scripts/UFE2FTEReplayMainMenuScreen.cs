using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTEReplayMainMenuScreen : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTEReplaySelectionScreen replaySelectionScreenPrefab;

        public void StartReplaySelectionScreen()
        {
            if (replaySelectionScreenPrefab == null) return;

            UFE.currentScreen.gameObject.SetActive(false);

            Instantiate(replaySelectionScreenPrefab);
        }
    }
}