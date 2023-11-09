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
            UFE2FTE.SetTextMessage(hitDataDisplayToggleText, UFE2FTE.GetStringFromBool(UFE2FTE.Instance.displayHitData));
        }

        public void ToggleHitDataDisplay()
        {
            UFE2FTE.Instance.displayHitData = UFE2FTE.ToggleBool(UFE2FTE.Instance.displayHitData);
        }
    }
}
