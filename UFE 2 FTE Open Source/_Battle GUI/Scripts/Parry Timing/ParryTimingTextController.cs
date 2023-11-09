using FPLibrary;
using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class ParryTimingTextController : MonoBehaviour
    {
        [SerializeField]
        private Text parryTimingText;
        [SerializeField]
        private bool useFrameNumbers;

        private void Update()
        {
            UFE2FTE.SetTextMessage(parryTimingText, UFE2FTE.languageOptions.GetNormalFrameNumber((int)Fix64.Floor(UFE.config.blockOptions._parryTiming * UFE.config.fps)));
        }
    }
}