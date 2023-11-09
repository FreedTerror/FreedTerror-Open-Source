using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class FrameDelayDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private Text frameDelayDisplayToggleText;

        private void Update()
        {
            UFE2FTE.SetTextMessage(frameDelayDisplayToggleText, UFE2FTE.GetStringFromBool(UFE2FTE.Instance.displayFrameDelay));
        }

        public void ToggleFrameDelayDisplay()
        {
            UFE2FTE.Instance.displayFrameDelay = UFE2FTE.ToggleBool(UFE2FTE.Instance.displayFrameDelay);
        }
    }
}
