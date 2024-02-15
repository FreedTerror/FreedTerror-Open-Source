using UnityEngine;

namespace FreedTerror.UFE2
{
    public class SpawnUFENetworkGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] prefabArray;

        private void Start()
        {
            UFE2Manager.SpawnUFENetworkGameObject(prefabArray);
        }
    }
}
