using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class FrameAdvantageDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private Text frameAdvantageDisplayText;
        private bool previousFrameAdvantageDisplay;

        private void Start()
        {
            previousFrameAdvantageDisplay = UFE2Manager.instance.displayFrameAdvantage;

            if (frameAdvantageDisplayText != null)
            {
                frameAdvantageDisplayText.text = Utility.GetStringFromBool(UFE2Manager.instance.displayFrameAdvantage);
            }
        }

        private void Update()
        {
            if (previousFrameAdvantageDisplay != UFE2Manager.instance.displayFrameAdvantage)
            {
                previousFrameAdvantageDisplay = UFE2Manager.instance.displayFrameAdvantage;

                if (frameAdvantageDisplayText != null)
                {
                    frameAdvantageDisplayText.text = Utility.GetStringFromBool(UFE2Manager.instance.displayFrameAdvantage);
                }
            }        
        }

        public void ToggleFrameAdvantageDisplay()
        {
            UFE2Manager.instance.displayFrameAdvantage = !UFE2Manager.instance.displayFrameAdvantage;
        }
    }
}
