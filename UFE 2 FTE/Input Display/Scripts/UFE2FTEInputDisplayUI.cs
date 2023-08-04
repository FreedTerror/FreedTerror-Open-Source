using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEInputDisplayUI : MonoBehaviour
    {
        [SerializeField]
        private Toggle inputDisplayToggle;

        private void Update()
        {
            SetToggleIsOn(inputDisplayToggle, UFE2FTEInputDisplayOptionsManager.useInputDisplay);
        }

        public void SetUseInputDisplay(bool useInputDisplay)
        {
            UFE2FTEInputDisplayOptionsManager.useInputDisplay = useInputDisplay;
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