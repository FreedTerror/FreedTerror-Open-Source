using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEStartReplaySelectionOptionsScreenButton : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTEReplaySelectionOptionsScreen replaySelectionOptionsScreenPrefab;
        public Text replayFileNameText;

        public void StartReplaySelectionOptionsScreen()
        {
            if (replaySelectionOptionsScreenPrefab == null) return;

            Destroy(transform.root.gameObject);

            UFE2FTEReplaySelectionOptionsScreen newReplaySelectionOptionsScreen = Instantiate(replaySelectionOptionsScreenPrefab);

            SetTextMessage(newReplaySelectionOptionsScreen.replayFileNameText, GetTextMessage(replayFileNameText));       
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

        private static string GetTextMessage(Text text)
        {
            if (text == null)
            {
                return "";
            }

            return text.text;
        }
    }
}