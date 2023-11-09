using UnityEngine;

namespace UFE2FTE
{
    public class SpawnNetworkGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] prefabArray;

        private void Start()
        {
            UFE2FTE.SpawnNetworkGameObject(prefabArray);    
        }
    }
}
