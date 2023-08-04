using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEReplaySelectionOptionsScreen : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTEReplaySelectionScreen replaySelectionScreenPrefab;
        [SerializeField]
        private Button firstSelectedButton;
        public Text replayFileNameText;

        private void Start()
        {
            SelectButton(firstSelectedButton);
        }

        public void StartReplaySelectionScreen()
        {
            if (replaySelectionScreenPrefab == null) return;

            Destroy(gameObject);

            Instantiate(replaySelectionScreenPrefab);
        }

        public void StartReplay()
        {
            if (replayFileNameText == null) return;

            UFE2FTEReplayOptionsManager.StartReplay(replayFileNameText.text, gameObject);
        }

        public void DeleteReplay()
        {
            if (replayFileNameText == null) return;

            UFE2FTEReplayOptionsManager.DeleteReplay(replayFileNameText.text);

            StartReplaySelectionScreen();
        }

        private static void SelectButton(Button button)
        {
            if (button == null)
            {
                return;
            }

            button.Select();
        }
    }
}