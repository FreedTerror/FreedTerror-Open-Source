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
            UFE2FTE.SetTextMessage(battleGUIDisplayToggleText, UFE2FTE.GetStringFromBool(UFE2FTE.Instance.displayBattleGUI));
        }

        public void ToggleBattleGUIDisplay()
        {
            UFE2FTE.Instance.displayBattleGUI = UFE2FTE.ToggleBool(UFE2FTE.Instance.displayBattleGUI);
        }
    }
}