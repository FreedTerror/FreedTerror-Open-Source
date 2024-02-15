using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class HitDataDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private Text hitDataDisplayText;
        private bool previousHitDataDisplay;

        private void Start()
        {
            previousHitDataDisplay = UFE2Manager.instance.displayHitData;

            if (hitDataDisplayText != null)
            {
                hitDataDisplayText.text = Utility.GetStringFromBool(UFE2Manager.instance.displayHitData);
            }
        }

        private void Update()
        {
            if (previousHitDataDisplay != UFE2Manager.instance.displayHitData)
            {
                previousHitDataDisplay = UFE2Manager.instance.displayHitData;

                if (hitDataDisplayText != null)
                {
                    hitDataDisplayText.text = Utility.GetStringFromBool(UFE2Manager.instance.displayHitData);
                }
            }
        }

        public void ToggleHitDataDisplay()
        {
            UFE2Manager.instance.displayHitData = !UFE2Manager.instance.displayHitData;
        }
    }
}
