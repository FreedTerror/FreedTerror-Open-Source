using UnityEngine;

namespace FreedTerror.UFE2
{
    [CreateAssetMenu(menuName = "FreedTerror/UFE 2/Settings/Frame Delay Settings", fileName = "New Frame Delay Settings")]
    public class FrameDelayScriptableObject : ScriptableObject
    {
        public int minFrameDelay = 3;
        public int maxFrameDelay = 10;
        public int defaultFrameDelay = 3;
        public bool applyFrameDelayOffline;

        [NaughtyAttributes.Button]
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