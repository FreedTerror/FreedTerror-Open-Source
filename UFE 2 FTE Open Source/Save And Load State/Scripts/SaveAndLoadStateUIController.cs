using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class SaveAndLoadStateUIController : MonoBehaviour
    {
        public void SaveState()
        {
            UFE.PauseGame(false);

            UFE2FTE.SaveState();
        }

        public void LoadState()
        {
            UFE.PauseGame(false);

            UFE2FTE.LoadState();
        }
    }
}
