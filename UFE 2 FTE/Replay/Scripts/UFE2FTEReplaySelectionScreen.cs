using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEReplaySelectionScreen : MonoBehaviour
    {
        [SerializeField]
        private Button firstSelectedButton;

        private void Start()
        {
            SelectButton(firstSelectedButton);     
        }

        public void StartMainMenuScreen()
        {
            Destroy(gameObject);

            UFE.StartMainMenuScreen();
        }

        public void OpenReplayFilesFolder()
        {
            UFE2FTEReplayOptionsManager.OpenReplayFilesFolder();
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