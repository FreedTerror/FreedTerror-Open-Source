using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace FreedTerror.UFE2
{
    public class TrainingModeGaugeModeUIController : MonoBehaviour
    {
        [SerializeField]
        private Text gaugeModeText;
        private LifeBarTrainingMode previousLifeBarTrainingMode;

        private void Start()
        {
            previousLifeBarTrainingMode = UFE.config.trainingModeOptions.p1Gauge;

            if (gaugeModeText != null)
            {
                gaugeModeText.text = System.Enum.GetName(typeof(UFE3D.LifeBarTrainingMode), UFE.config.trainingModeOptions.p1Gauge);
            }
        }

        private void Update()
        {
            if (previousLifeBarTrainingMode != UFE.config.trainingModeOptions.p1Gauge)
            {
                previousLifeBarTrainingMode = UFE.config.trainingModeOptions.p1Gauge;

                if (gaugeModeText != null)
                {
                    gaugeModeText.text = System.Enum.GetName(typeof(UFE3D.LifeBarTrainingMode), UFE.config.trainingModeOptions.p1Gauge);
                }
            }
        }

        public void NextTrainingModeGaugeMode()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.trainingModeOptions.p1Gauge = Utility.GetNextEnum(UFE.config.trainingModeOptions.p1Gauge);
            UFE.config.trainingModeOptions.p2Gauge = UFE.config.trainingModeOptions.p1Gauge;
        }

        public void PreviousTrainingModeGaugeMode()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.trainingModeOptions.p1Gauge = Utility.GetPreviousEnum(UFE.config.trainingModeOptions.p1Gauge);
            UFE.config.trainingModeOptions.p2Gauge = UFE.config.trainingModeOptions.p1Gauge;
        }
    }
}