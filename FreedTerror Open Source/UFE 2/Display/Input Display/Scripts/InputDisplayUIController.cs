using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class InputDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private Text inputDisplayText;
        private bool previousInputDisplay;

        private void Start()
        {
            previousInputDisplay = UFE2Manager.instance.displayInputs;

            if (inputDisplayText != null)
            {
                inputDisplayText.text = Utility.GetStringFromBool(UFE2Manager.instance.displayInputs);
            }
        }

        private void Update()
        {
            if (previousInputDisplay != UFE2Manager.instance.displayInputs)
            {
                previousInputDisplay = UFE2Manager.instance.displayInputs;

                if (inputDisplayText != null)
                {
                    inputDisplayText.text = Utility.GetStringFromBool(UFE2Manager.instance.displayInputs);
                }
            }
        }

        public void ToggleInputDisplay()
        {
            UFE2Manager.instance.displayInputs = !UFE2Manager.instance.displayInputs;
        }
    }
}