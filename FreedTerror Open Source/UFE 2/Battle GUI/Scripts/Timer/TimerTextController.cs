using FPLibrary;
using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class TimerTextController : MonoBehaviour
    {
        [SerializeField]
        private Text timerText;

        private void Update()
        {
            if (timerText != null)
            {
                timerText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber((int)Fix64.Ceiling(UFE.timer));
            }
        }
    }
}
