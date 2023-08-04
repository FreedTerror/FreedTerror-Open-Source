using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTETrainingModeSaveAndLoadStateUI : MonoBehaviour
    {
        public void SaveState()
        {
            if (UFE.replayMode == null)
            {
                return;
            }

            UFE.PauseGame(false);

            UFE2FTEHelperMethodsManager.SaveState();
        }

        public void LoadState()
        {
            if (UFE.replayMode == null
                || UFE.fluxCapacitor.savedState == null)
            {
                return;
            }

            UFE.PauseGame(false);

            UFE2FTEHelperMethodsManager.LoadState();
        }
    }
}
