using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTESpawnNetworkGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] prefabArray;

        private void Start()
        {
            SpawnNetworkGameObject(prefabArray);    
        }

        private static void SpawnNetworkGameObject(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }

            UFE.SpawnGameObject(gameObject, Vector3.zero, Quaternion.identity, true, 0);
        }

        private static void SpawnNetworkGameObject(GameObject[] gameObjectArray)
        {
            if (gameObjectArray == null)
            {
                return;
            }

            int length = gameObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                SpawnNetworkGameObject(gameObjectArray[i]);
            }
        }
    }
}
