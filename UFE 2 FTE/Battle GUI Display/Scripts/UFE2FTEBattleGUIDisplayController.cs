using UnityEngine;

namespace UFE2FTE
{   
    public class UFE2FTEBattleGUIDisplayController : MonoBehaviour
    {
        [SerializeField]
        private GameObject battleGUIGameObject;

        private void OnEnable()
        {
            UFE2FTEBattleGUIDisplayOptionsManager.useBattleGUIDisplay = true;
        }

        private void Update()
        {
            SetGameObjectActive(battleGUIGameObject, UFE2FTEBattleGUIDisplayOptionsManager.useBattleGUIDisplay);
        }

        private void OnDestroy()
        {
            UFE2FTEBattleGUIDisplayOptionsManager.useBattleGUIDisplay = true;
        }

        [NaughtyAttributes.Button]
        private void DisableBattleGUI()
        {
            UFE2FTEBattleGUIDisplayOptionsManager.useBattleGUIDisplay = false;
        }

        [NaughtyAttributes.Button]
        private void EnableBattleGUI()
        {
            UFE2FTEBattleGUIDisplayOptionsManager.useBattleGUIDisplay = true;
        }

        private static void SetGameObjectActive(GameObject gameObject, bool active)
        {
            if (gameObject == null)
            {
                return;
            }

            gameObject.SetActive(active);
        }
    }
}