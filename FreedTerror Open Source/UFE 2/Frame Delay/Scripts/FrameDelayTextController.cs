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
            UFE2Manager.SetTextMessage(frameDelayText, UFE2Manager.GetNormalFrameStringNumber(UFE2Manager.GetFrameDelay()));
        }
    }
}