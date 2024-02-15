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
                UFE2Manager.SetGameObjectActive(gameObjectArray, false);
            }
            else
            {
                UFE2Manager.SetGameObjectActive(gameObjectArray, true);
            }
        }
    }
}