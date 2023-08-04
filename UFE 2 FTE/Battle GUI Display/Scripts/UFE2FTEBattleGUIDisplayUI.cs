using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEBattleGUIDisplayUI : MonoBehaviour
    {
        [SerializeField]
        private Toggle battleGUIDisplayToggle;

        private void Update()
        {
            SetToggleIsOn(battleGUIDisplayToggle, UFE2FTEBattleGUIDisplayOptionsManager.useBattleGUIDisplay);
        }

        public void SetUseBattleGUIDisplay(bool useBattleGUIDisplay)
        {
            UFE2FTEBattleGUIDisplayOptionsManager.useBattleGUIDisplay = useBattleGUIDisplay;
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