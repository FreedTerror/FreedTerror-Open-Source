using UnityEngine;

namespace UFE2FTE
{
    public class CharacterPotentialParryGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private GameObject[] gameObjectArray;

        private void Update()
        {
            if (UFE2FTE.GetControlsScript(player) == null)
            {
                return;
            }

            if (UFE2FTE.GetControlsScript(player).potentialParry > 0)
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
