using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class InputDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private Text inputDisplayToggleText;

        private void Update()
        {
            UFE2FTE.SetTextMessage(inputDisplayToggleText, UFE2FTE.GetStringFromBool(UFE2FTE.Instance.displayInputs));
        }

        public void ToggleInputDisplay()
        {
            UFE2FTE.Instance.displayInputs = UFE2FTE.ToggleBool(UFE2FTE.Instance.displayInputs);
        }
    }
}