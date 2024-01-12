using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class BattleGUIDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private Text battleGUIDisplayToggleText;
 
        private void Update()
        {
            UFE2FTE.SetTextMessage(battleGUIDisplayToggleText, UFE2FTE.GetStringFromBool(UFE2FTE.instance.displayBattleGUI));
        }

        public void ToggleBattleGUIDisplay()
        {
            UFE2FTE.instance.displayBattleGUI = UFE2FTE.ToggleBool(UFE2FTE.instance.displayBattleGUI);
        }
    }
}