using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEReplaySelectionScreenPopulate : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTEStartReplaySelectionOptionsScreenButton startReplaySelectionOptionsScreenButtonPrefab;

        private void Start()
        {
            Populate();     
        }

        private void Populate()
        {
            if (startReplaySelectionOptionsScreenButtonPrefab == null) return;

            string[] replayFileNames = UFE2FTEReplayOptionsManager.GetReplayFileNames();

            if (replayFileNames == null) return;

            int length = replayFileNames.Length;
            for (int i = 0; i < length; i++)
            {
                UFE2FTEStartReplaySelectionOptionsScreenButton newStartReplaySelectionOptionsScreenButton = Instantiate(startReplaySelectionOptionsScreenButtonPrefab, transform);

                SetTextMessage(newStartReplaySelectionOptionsScreenButton.replayFileNameText, replayFileNames[i]);
            }
        }

        private static void SetTextMessage(Text text, string message, Color32? color = null)
        {
            if (text == null)
            {
                return;
            }

            text.text = message;

            if (color != null)
            {
                text.color = (Color32)color;
            }
        }
    }
}
