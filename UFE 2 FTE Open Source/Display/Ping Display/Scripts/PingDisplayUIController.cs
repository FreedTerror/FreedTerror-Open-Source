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
            UFE2FTE.SetTextMessage(pingDisplayToggleText, UFE2FTE.GetStringFromBool(UFE2FTE.instance.displayPing));
        }

        public void TogglePingDisplay()
        {
            UFE2FTE.instance.displayPing = UFE2FTE.ToggleBool(UFE2FTE.instance.displayPing);
        }
    }
}
