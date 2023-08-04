using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEFrameDelayUI : MonoBehaviour
    {
        [SerializeField]
        private Toggle frameDelayDisplayToggle;

        private void Update()
        {
            SetToggleIsOn(frameDelayDisplayToggle, UFE2FTEFrameDelayDisplayOptionsManager.useFrameDelayDisplay);
        }

        public void SetUseFrameDelayDisplay(bool useFrameDelayDisplay)
        {
            UFE2FTEFrameDelayDisplayOptionsManager.SetUseFrameDelayDisplayWithPlayerPrefs(useFrameDelayDisplay);
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
