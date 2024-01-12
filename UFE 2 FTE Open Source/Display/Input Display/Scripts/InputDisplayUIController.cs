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
            UFE2FTE.SetTextMessage(inputDisplayToggleText, UFE2FTE.GetStringFromBool(UFE2FTE.instance.displayInputs));
        }

        public void ToggleInputDisplay()
        {
            UFE2FTE.instance.displayInputs = UFE2FTE.ToggleBool(UFE2FTE.instance.displayInputs);
        }
    }
}