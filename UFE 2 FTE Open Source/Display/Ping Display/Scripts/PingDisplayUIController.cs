using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class PingDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private Text pingDisplayToggleText;

        private void Update()
        {
            UFE2FTE.SetTextMessage(pingDisplayToggleText, UFE2FTE.GetStringFromBool(UFE2FTE.Instance.displayPing));
        }

        public void TogglePingDisplay()
        {
            UFE2FTE.Instance.displayPing = UFE2FTE.ToggleBool(UFE2FTE.Instance.displayPing);
        }
    }
}
