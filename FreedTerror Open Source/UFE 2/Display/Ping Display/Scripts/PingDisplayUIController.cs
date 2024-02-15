using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class PingDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private Text pingDisplayText;
        private bool previousPingDisplay;

        private void Start()
        {
            previousPingDisplay = UFE2Manager.instance.displayPing;

            if (pingDisplayText != null)
            {
                pingDisplayText.text = Utility.GetStringFromBool(UFE2Manager.instance.displayPing);
            }
        }

        private void Update()
        {
            if (previousPingDisplay != UFE2Manager.instance.displayPing)
            {
                previousPingDisplay = UFE2Manager.instance.displayPing;

                if (pingDisplayText != null)
                {
                    pingDisplayText.text = Utility.GetStringFromBool(UFE2Manager.instance.displayPing);
                }
            }        
        }

        public void TogglePingDisplay()
        {
            UFE2Manager.instance.displayPing = !UFE2Manager.instance.displayPing;
        }
    }
}
