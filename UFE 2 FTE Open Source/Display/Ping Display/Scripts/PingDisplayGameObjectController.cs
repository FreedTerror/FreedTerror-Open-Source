using UnityEngine;

namespace UFE2FTE
{
    public class PingDisplayGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] pingDisplayGameObjectArray;

        private void Update()
        {
            UFE2FTE.SetGameObjectActive(pingDisplayGameObjectArray, UFE2FTE.instance.displayPing);
        }
    }
}
