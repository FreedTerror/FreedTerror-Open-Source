using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class BattleGUIDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private Text battleGUIDisplayText;
        private bool previousBattleGUIDisplay;

        private void Start()
        {
            previousBattleGUIDisplay = UFE2Manager.instance.displayBattleGUI;

            if (battleGUIDisplayText != null)
            {
                battleGUIDisplayText.text = Utility.GetStringFromBool(UFE2Manager.instance.displayBattleGUI);
            }
        }

        private void Update()
        {
            if (previousBattleGUIDisplay != UFE2Manager.instance.displayBattleGUI)
            {
                previousBattleGUIDisplay = UFE2Manager.instance.displayBattleGUI;

                if (battleGUIDisplayText != null)
                {
                    battleGUIDisplayText.text = Utility.GetStringFromBool(UFE2Manager.instance.displayBattleGUI);
                }
            }
        }

        public void ToggleBattleGUIDisplay()
        {
            UFE2Manager.instance.displayBattleGUI = !UFE2Manager.instance.displayBattleGUI;
        }
    }
}