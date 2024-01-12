using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class HitDataDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private Text hitDataDisplayToggleText;

        private void Update()
        {
            UFE2FTE.SetTextMessage(hitDataDisplayToggleText, UFE2FTE.GetStringFromBool(UFE2FTE.instance.displayHitData));
        }

        public void ToggleHitDataDisplay()
        {
            UFE2FTE.instance.displayHitData = UFE2FTE.ToggleBool(UFE2FTE.instance.displayHitData);
        }
    }
}
