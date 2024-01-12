using UnityEngine;

namespace UFE2FTE
{
    public class SpawnNetworkGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] prefabArray;

        private void Start()
        {
            SpawnNetworkGameObject();
        }

        [NaughtyAttributes.Button]
        private void SpawnNetworkGameObject()
        {
            UFE2FTE.SpawnNetworkGameObject(prefabArray);
        }
    }
}
