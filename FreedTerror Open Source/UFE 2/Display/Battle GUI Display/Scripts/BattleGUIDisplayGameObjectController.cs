using UnityEngine;

namespace FreedTerror.UFE2
{   
    public class BattleGUIDisplayGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] battleGUIDisplayGameObjectArray;

        private void Update()
        {
            Utility.SetGameObjectActive(battleGUIDisplayGameObjectArray, UFE2Manager.instance.displayBattleGUI);
        }
    }
}