using UnityEngine;

namespace FreedTerror.UFE2
{
    public class InputReplayDataUIController : MonoBehaviour
    {
        [SerializeField]
        private InputReplayControllerScriptableObject inputReplayDataScriptableObjectManager;

        public void StartPlayback()
        {
            if (inputReplayDataScriptableObjectManager != null
                && inputReplayDataScriptableObjectManager.GetCurrentInputReplayController() != null)
            {
                inputReplayDataScriptableObjectManager.GetCurrentInputReplayController().StartPlayback();
            }

            UFE.PauseGame(false);
        }

        public void StopPlayback()
        {
            if (inputReplayDataScriptableObjectManager != null
                && inputReplayDataScriptableObjectManager.GetCurrentInputReplayController() != null)
            {
                inputReplayDataScriptableObjectManager.GetCurrentInputReplayController().StopPlayBack();
            }

            UFE.PauseGame(false);
        }

        public void StartRecording()
        {
            if (inputReplayDataScriptableObjectManager != null
                && inputReplayDataScriptableObjectManager.GetCurrentInputReplayController() != null)
            {
                inputReplayDataScriptableObjectManager.GetCurrentInputReplayController().StartRecording();
            }

            UFE.PauseGame(false);
        }

        public void StopRecording()
        {
            if (inputReplayDataScriptableObjectManager != null
                && inputReplayDataScriptableObjectManager.GetCurrentInputReplayController() != null)
            {
                inputReplayDataScriptableObjectManager.GetCurrentInputReplayController().StopRecording();
            }

            UFE.PauseGame(false);
        }
    }
}