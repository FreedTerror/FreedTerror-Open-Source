using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class FrameDelayUIController : MonoBehaviour
    {
        [SerializeField]
        private Text applyFrameDelayOfflineToggleText;

        private void Update()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE2FTE.SetTextMessage(applyFrameDelayOfflineToggleText, UFE2FTE.GetStringFromBool(UFE.config.networkOptions.applyFrameDelayOffline));
        }

        public void ToggleApplyFrameDelayOffline()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.networkOptions.applyFrameDelayOffline = UFE2FTE.ToggleBool(UFE.config.networkOptions.applyFrameDelayOffline);
        }

        public void DefaultFrameDelay()
        {
            if (UFE2FTE.instance.defaultFrameDelayScriptableObject == null)
            {
                return;
            }

            UFE2FTE.instance.defaultFrameDelayScriptableObject.UpdateFrameDelaySettings();
        }

        public void NextFrameDelay()
        {
            UFE2FTE.AddOrSubtractFrameDelay(1);
        }

        public void PreviousFrameDelay()
        {
            UFE2FTE.AddOrSubtractFrameDelay(-1);
        }
    }
}
