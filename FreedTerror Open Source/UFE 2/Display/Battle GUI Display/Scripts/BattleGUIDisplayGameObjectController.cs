using UnityEngine;

namespace FreedTerror.UFE2
{   
    public class BattleGUIDisplayGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] battleGUIDisplayGameObjectArray;

        private void Update()
        {
            UFE2Manager.SetGameObjectActive(battleGUIDisplayGameObjectArray, UFE2Manager.instance.displayBattleGUI);
        }
    }
}