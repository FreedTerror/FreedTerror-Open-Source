using UnityEngine;

namespace FreedTerror.UFE2
{
    public class UFEPausedGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] gameObjectArray;

        private void Update()
        {
            if (UFE.IsPaused() == true)
            {
                Utility.SetGameObjectActive(gameObjectArray, false);
            }
            else
            {
                Utility.SetGameObjectActive(gameObjectArray, true);
            }
        }
    }
}