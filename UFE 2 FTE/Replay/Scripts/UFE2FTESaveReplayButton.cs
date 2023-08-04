using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTESaveReplayButton : MonoBehaviour
    {
        [SerializeField]
        private Button saveReplayButton;

        private void Update()
        {
            if (UFE2FTEReplayOptionsManager.IsReplaySaved() == true
                || UFE2FTEReplayOptionsManager.IsExcludedGameMode(UFE.gameMode) == true
                || UFE2FTEReplayOptionsManager.LoadedReplayDataExists() == true)
            {
                SetButtonInteractable(saveReplayButton, false);
            }
            else if (UFE2FTEReplayOptionsManager.IsReplaySaved() == false
                || UFE2FTEReplayOptionsManager.IsExcludedGameMode(UFE.gameMode) == false
                || UFE2FTEReplayOptionsManager.LoadedReplayDataExists() == false)
            {
                SetButtonInteractable(saveReplayButton, true);
            }
        }

        public void SaveReplay()
        {
            UFE2FTEReplayOptionsManager.SaveReplay();
        }

        private static void SetButtonInteractable(Button button, bool interactable)
        {
            if (button == null)
            {
                return;
            }

            button.interactable = interactable;
        }
    }
}