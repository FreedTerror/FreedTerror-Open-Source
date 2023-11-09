using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class FrameDelayTextController : MonoBehaviour
    {
        [SerializeField]
        private Text frameDelayText;

        private void Update()
        {
            UFE2FTE.SetTextMessage(frameDelayText, UFE2FTE.languageOptions.GetNormalFrameNumber(UFE2FTE.GetFrameDelay()));
        }
    }
}