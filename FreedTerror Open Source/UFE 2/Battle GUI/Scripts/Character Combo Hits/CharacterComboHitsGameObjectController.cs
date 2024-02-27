using UnityEngine;

namespace FreedTerror.UFE2
{
    public class CharacterComboHitsGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private int comboHits = 1;
        [SerializeField]
        private GameObject[] gameObjectArray;

        private void Update()
        {
            if (UFE2Manager.GetControlsScript(player) == null
                || UFE2Manager.GetControlsScript(player).opControlsScript == null)
            {
                return;
            }

            if (UFE2Manager.GetControlsScript(player).opControlsScript.comboHits >= comboHits)
            {
                Utility.SetGameObjectActive(gameObjectArray, true);
            }
            else
            {
                Utility.SetGameObjectActive(gameObjectArray, false);
            }
        }
    }
}
