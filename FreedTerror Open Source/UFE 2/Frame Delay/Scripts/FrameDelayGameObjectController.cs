using UnityEngine;

namespace FreedTerror.UFE2
{
    public class FrameDelayDisplayGameObjectController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] frameDelayDisplayGameObjectArray;

        private void Update()
        {
            if (UFE.config != null)
            {
                if (UFE.config.networkOptions.applyFrameDelayOffline == true)
                {
                    UFE2Manager.SetGameObjectActive(frameDelayDisplayGameObjectArray, true);
                }
                else
                {
                    UFE2Manager.SetGameObjectActive(frameDelayDisplayGameObjectArray, UFE2Manager.instance.displayFrameDelay);
                }
            }
            else
            {
                UFE2Manager.SetGameObjectActive(frameDelayDisplayGameObjectArray, UFE2Manager.instance.displayFrameDelay);
            }
        }
    }
}