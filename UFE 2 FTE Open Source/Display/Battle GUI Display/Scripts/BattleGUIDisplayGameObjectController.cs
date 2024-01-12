using UnityEngine;

namespace UFE2FTE
{   
    public class BattleGUIDisplayGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] battleGUIDisplayGameObjectArray;

        private void Update()
        {
            UFE2FTE.SetGameObjectActive(battleGUIDisplayGameObjectArray, UFE2FTE.instance.displayBattleGUI);
        }
    }
}