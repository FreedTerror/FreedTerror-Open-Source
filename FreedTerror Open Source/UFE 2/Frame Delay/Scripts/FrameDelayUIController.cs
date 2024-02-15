using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class FrameDelayUIController : MonoBehaviour
    {
        [SerializeField]
        private Text applyFrameDelayOfflineText;
        private bool previousApplyFrameDelayOffline;
        [SerializeField]
        private Text frameDelayText;
        private int previousFrameDelay;

        private void Start()
        {
            if (UFE.config == null)
            {
                return;
            }

            previousApplyFrameDelayOffline = UFE.config.networkOptions.applyFrameDelayOffline;

            if (applyFrameDelayOfflineText != null)
            {
                applyFrameDelayOfflineText.text = Utility.GetStringFromBool(UFE.config.networkOptions.applyFrameDelayOffline);
            }

            previousFrameDelay = UFE2Manager.GetFrameDelay();

            if (frameDelayText != null)
            {
                frameDelayText.text = UFE2Manager.GetFrameDelay().ToString();
            }
        }

        private void Update()
        {
            if (UFE.config == null)
            {
                return;
            }

            if (previousApplyFrameDelayOffline != UFE.config.networkOptions.applyFrameDelayOffline)
            {
                previousApplyFrameDelayOffline = UFE.config.networkOptions.applyFrameDelayOffline;

                if (applyFrameDelayOfflineText != null)
                {
                    applyFrameDelayOfflineText.text = Utility.GetStringFromBool(UFE.config.networkOptions.applyFrameDelayOffline);
                }
            }

            if (previousFrameDelay != UFE2Manager.GetFrameDelay())
            {
                previousFrameDelay = UFE2Manager.GetFrameDelay();

                if (frameDelayText != null)
                {
                    frameDelayText.text = previousFrameDelay.ToString();
                }
            }
        }

        public void ToggleApplyFrameDelayOffline()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.networkOptions.applyFrameDelayOffline = !UFE.config.networkOptions.applyFrameDelayOffline;
        }

        public void DefaultFrameDelay()
        {
            if (UFE2Manager.instance.defaultFrameDelayScriptableObject == null)
            {
                return;
            }

            UFE2Manager.instance.defaultFrameDelayScriptableObject.UpdateFrameDelaySettings();
        }

        public void NextFrameDelay()
        {
            UFE2Manager.AddOrSubtractFrameDelay(1);
        }

        public void PreviousFrameDelay()
        {
            UFE2Manager.AddOrSubtractFrameDelay(-1);
        }
    }
}
