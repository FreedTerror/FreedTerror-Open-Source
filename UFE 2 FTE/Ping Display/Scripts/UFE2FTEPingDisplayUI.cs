using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEPingDisplayUI : MonoBehaviour
    {
        [SerializeField]
        private Toggle pingDisplayToggle;

        private void Update()
        {
            SetToggleIsOn(pingDisplayToggle, UFE2FTEPingDisplayOptionsManager.usePingDisplay);
        }

        public void SetUsePingDisplay(bool usePingDisplay)
        {
            UFE2FTEPingDisplayOptionsManager.SetUsePingDisplayWithPlayerPrefs(usePingDisplay);
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
