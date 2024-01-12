using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Frame Delay Settings", menuName = "U.F.E. 2 F.T.E./Settings/Frame Delay Settings")]
    public class FrameDelayScriptableObject : ScriptableObject
    {
        public int minFrameDelay = 3;
        public int maxFrameDelay = 10;
        public int defaultFrameDelay = 3;
        public bool applyFrameDelayOffline;

        public void UpdateFrameDelaySettings()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.networkOptions.minFrameDelay = minFrameDelay;
            UFE.config.networkOptions.maxFrameDelay = maxFrameDelay;
            UFE.config.networkOptions.defaultFrameDelay = defaultFrameDelay;
            UFE.config.networkOptions.applyFrameDelayOffline = applyFrameDelayOffline;
        }
    }
}