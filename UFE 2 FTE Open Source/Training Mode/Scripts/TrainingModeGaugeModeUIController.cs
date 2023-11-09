using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class TrainingModeGaugeModeUIController : MonoBehaviour
    {
        [SerializeField]
        private Text gaugeModeText;

        private void Update()
        {
            if (UFE.config != null)
            {
                UFE2FTE.SetTextMessage(gaugeModeText, UFE2FTE.GetStringFromEnum(UFE.config.trainingModeOptions.p1Gauge));
            }
        }

        public void NextTrainingModeGaugeMode()
        {
            UFE2FTE.NextTrainingModeGaugeMode();
        }

        public void PreviousTrainingModeGaugeMode()
        {
            UFE2FTE.PreviousTrainingModeGaugeMode();
        }
    }
}