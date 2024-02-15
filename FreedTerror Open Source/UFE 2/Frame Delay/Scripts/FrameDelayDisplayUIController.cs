using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class FrameDelayDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private Text frameDelayDisplayText;
        private bool previousFrameDelayDisplay;

        private void Start()
        {
            previousFrameDelayDisplay = UFE2Manager.instance.displayFrameDelay;

            if (frameDelayDisplayText != null)
            {
                frameDelayDisplayText.text = Utility.GetStringFromBool(UFE2Manager.instance.displayFrameDelay);
            }
        }

        private void Update()
        {
            if (previousFrameDelayDisplay != UFE2Manager.instance.displayFrameDelay)
            {
                previousFrameDelayDisplay = UFE2Manager.instance.displayFrameDelay;

                if (frameDelayDisplayText != null)
                {
                    frameDelayDisplayText.text = Utility.GetStringFromBool(UFE2Manager.instance.displayFrameDelay);
                }
            }
        }

        public void ToggleFrameDelayDisplay()
        {
            UFE2Manager.instance.displayFrameDelay = !UFE2Manager.instance.displayFrameDelay;
        }
    }
}
