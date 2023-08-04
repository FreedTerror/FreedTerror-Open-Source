using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEFrameDelayOfflineUI : MonoBehaviour
    {
        [SerializeField]
        private Toggle frameDelayOfflineToggle;
        [SerializeField]
        private Text frameDelayOfflineText;
        private int currentFrameDelayOffline;
        [SerializeField]
        private int minFrameDelayOffline = 1;
        [SerializeField]
        private int maxFrameDelayOffline = 10;

        private void Start()
        {
            if (UFE.config.networkOptions.frameDelayType == NetworkFrameDelay.Fixed)
            {
                currentFrameDelayOffline = UFE.config.networkOptions.defaultFrameDelay;
            }
            else if (UFE.config.networkOptions.frameDelayType == NetworkFrameDelay.Auto)
            {
                currentFrameDelayOffline = UFE.config.networkOptions.minFrameDelay;
            }

            SetTextMessage(frameDelayOfflineText, currentFrameDelayOffline.ToString());
        }

        private void Update()
        {
            SetToggleIsOn(frameDelayOfflineToggle, UFE.config.networkOptions.applyFrameDelayOffline);
        }

        public void SetApplyFrameDelayOffline(bool applyFrameDelayOffline)
        {
            if (applyFrameDelayOffline == true)
            {
                UFE.config.networkOptions.applyFrameDelayOffline = true;
            }
            else
            {
                UFE.config.networkOptions.applyFrameDelayOffline = false;
            }
        }

        public void NextFrameDelayOffline()
        {
            currentFrameDelayOffline++;

            if (currentFrameDelayOffline > maxFrameDelayOffline)
            {
                currentFrameDelayOffline = minFrameDelayOffline;
            }

            if (UFE.config.networkOptions.frameDelayType == NetworkFrameDelay.Fixed)
            {
                UFE.config.networkOptions.defaultFrameDelay = currentFrameDelayOffline;
            }
            else if (UFE.config.networkOptions.frameDelayType == NetworkFrameDelay.Auto)
            {
                UFE.config.networkOptions.minFrameDelay = currentFrameDelayOffline;
            }

            SetTextMessage(frameDelayOfflineText, currentFrameDelayOffline.ToString());
        }

        public void PreviousFrameDelayOffline()
        {
            currentFrameDelayOffline--;

            if (currentFrameDelayOffline < minFrameDelayOffline)
            {
                currentFrameDelayOffline = maxFrameDelayOffline;
            }

            if (UFE.config.networkOptions.frameDelayType == NetworkFrameDelay.Fixed)
            {
                UFE.config.networkOptions.defaultFrameDelay = currentFrameDelayOffline;
            }
            else if (UFE.config.networkOptions.frameDelayType == NetworkFrameDelay.Auto)
            {
                UFE.config.networkOptions.minFrameDelay = currentFrameDelayOffline;
            }

            SetTextMessage(frameDelayOfflineText, currentFrameDelayOffline.ToString());
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

        private static void SetToggleIsOn(Toggle toggle, bool isOn)
        {
            if (toggle == null)
            {
                return;
            }

            toggle.isOn = isOn;
        }
    }
}
