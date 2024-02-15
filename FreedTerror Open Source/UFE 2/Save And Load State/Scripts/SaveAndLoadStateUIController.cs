using UnityEngine;

namespace FreedTerror.UFE2
{
    public class SaveAndLoadStateUIController : MonoBehaviour
    {
        public void SaveState()
        {
            UFE.PauseGame(false);

            UFE2Manager.SaveState();
        }

        public void LoadState()
        {
            UFE.PauseGame(false);

            UFE2Manager.LoadState();
        }
    }
}
