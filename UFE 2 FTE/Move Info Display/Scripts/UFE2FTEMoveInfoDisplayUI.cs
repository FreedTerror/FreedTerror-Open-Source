using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEMoveInfoDisplayUI : MonoBehaviour
    {
        [SerializeField]
        private Toggle moveInfoDisplayToggle;

        private void Update()
        {
            SetToggleIsOn(moveInfoDisplayToggle, UFE2FTEMoveInfoDisplayOptionsManager.useMoveInfoDisplay);
        }

        public void SetUseMoveInfoDisplay(bool useMoveInfoDisplay)
        {
            UFE2FTEMoveInfoDisplayOptionsManager.useMoveInfoDisplay = useMoveInfoDisplay;
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
