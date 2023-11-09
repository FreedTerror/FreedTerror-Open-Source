using UnityEngine;

namespace UFE2FTE
{
    public class CharacterComboHitsGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private int comboHits = 1;
        [SerializeField]
        private GameObject[] gameObjectArray;

        private void Update()
        {
            if (UFE2FTE.GetControlsScript(player) == null
                || UFE2FTE.GetControlsScript(player).opControlsScript == null)
            {
                return;
            }

            if (UFE2FTE.GetControlsScript(player).opControlsScript.comboHits >= comboHits)
            {
                UFE2FTE.SetGameObjectActive(gameObjectArray, true);
            }
            else
            {
                UFE2FTE.SetGameObjectActive(gameObjectArray, false);
            }
        }
    }
}
