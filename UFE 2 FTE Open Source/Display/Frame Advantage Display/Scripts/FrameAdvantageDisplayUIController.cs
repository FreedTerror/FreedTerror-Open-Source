using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class FrameAdvantageDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private Text frameAdvantageDisplayToggleText;

        private void Update()
        {
            UFE2FTE.SetTextMessage(frameAdvantageDisplayToggleText, UFE2FTE.GetStringFromBool(UFE2FTE.Instance.displayFrameAdvantage));
        }

        public void ToggleFrameAdvantageDisplay()
        {
            UFE2FTE.Instance.displayFrameAdvantage = UFE2FTE.ToggleBool(UFE2FTE.Instance.displayFrameAdvantage);
        }
    }
}
