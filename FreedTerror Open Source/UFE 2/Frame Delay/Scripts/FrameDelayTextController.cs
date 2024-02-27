using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class FrameDelayTextController : MonoBehaviour
    {
        [SerializeField]
        private Text frameDelayText;

        private void Update()
        {
            if (frameDelayText != null)
            {
                frameDelayText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber(UFE2Manager.GetFrameDelay());
            }
        }
    }
}