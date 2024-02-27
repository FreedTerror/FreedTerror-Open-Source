using UnityEngine;

namespace FreedTerror.UFE2
{
    public class FrameDelayDisplayGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] frameDelayDisplayGameObjectArray;

        private void Update()
        {
            Utility.SetGameObjectActive(frameDelayDisplayGameObjectArray, UFE2Manager.instance.displayFrameDelay);
        }
    }
}