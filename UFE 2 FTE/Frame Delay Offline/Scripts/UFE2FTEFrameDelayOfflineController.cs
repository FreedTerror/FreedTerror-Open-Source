using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTEFrameDelayOfflineController : MonoBehaviour
    {
        [SerializeField]
        private int minFrameDelay = 1;
        [SerializeField]
        private int maxFrameDelay = 10;
        [SerializeField]
        private int defaultFrameDelay = 4;
        [SerializeField]
        private bool applyFrameDelayOffline;

        private void Start()
        {
            if (UFE.gameMode == GameMode.NetworkGame)
            {
                return;
            }

            SetFrameDelayOfflineOptions();
        }

        private void Update()
        {
            if (UFE.gameMode == GameMode.NetworkGame
                || UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                return;
            }

            SetFrameDelayOfflineOptions();
        }

        private void SetFrameDelayOfflineOptions()
        {
            UFE.config.networkOptions.minFrameDelay = minFrameDelay;

            UFE.config.networkOptions.maxFrameDelay = maxFrameDelay;

            UFE.config.networkOptions.defaultFrameDelay = defaultFrameDelay;

            UFE.config.networkOptions.applyFrameDelayOffline = applyFrameDelayOffline;
        }
    }
}
