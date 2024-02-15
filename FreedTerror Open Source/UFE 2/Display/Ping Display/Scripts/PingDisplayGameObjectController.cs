using UnityEngine;

namespace FreedTerror.UFE2
{
    public class PingDisplayGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] pingDisplayGameObjectArray;

        private void Update()
        {
            UFE2Manager.SetGameObjectActive(pingDisplayGameObjectArray, UFE2Manager.instance.displayPing);
        }
    }
}
