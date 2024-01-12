using UnityEngine;

namespace UFE2FTE
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
                    UFE2FTE.SetGameObjectActive(frameDelayDisplayGameObjectArray, true);
                }
                else
                {
                    UFE2FTE.SetGameObjectActive(frameDelayDisplayGameObjectArray, UFE2FTE.instance.displayFrameDelay);
                }
            }
            else
            {
                UFE2FTE.SetGameObjectActive(frameDelayDisplayGameObjectArray, UFE2FTE.instance.displayFrameDelay);
            }
        }
    }
}