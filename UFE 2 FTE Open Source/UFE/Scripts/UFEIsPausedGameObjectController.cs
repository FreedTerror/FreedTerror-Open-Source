using UnityEngine;

namespace UFE2FTE
{
    public class UFEIsPausedGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] gameObjectArray;

        private void Update()
        {
            if (UFE.isPaused() == true)
            {
                UFE2FTE.SetGameObjectActive(gameObjectArray, false);
            }
            else
            {
                UFE2FTE.SetGameObjectActive(gameObjectArray, true);
            }
        }
    }
}