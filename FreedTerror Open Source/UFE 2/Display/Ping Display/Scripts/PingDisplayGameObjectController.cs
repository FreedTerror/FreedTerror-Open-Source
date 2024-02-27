using UnityEngine;

namespace FreedTerror.UFE2
{
    public class PingDisplayGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] pingDisplayGameObjectArray;

        private void Update()
        {
            Utility.SetGameObjectActive(pingDisplayGameObjectArray, UFE2Manager.instance.displayPing);
        }
    }
}
