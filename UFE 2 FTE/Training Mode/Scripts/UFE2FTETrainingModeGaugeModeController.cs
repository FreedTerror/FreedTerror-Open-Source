using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTETrainingModeGaugeModeController : MonoBehaviour
    {
        [SerializeField]
        private LifeBarTrainingMode defaultTrainingModeLifeMode = LifeBarTrainingMode.Normal;
        [SerializeField]
        private LifeBarTrainingMode currentTrainingModeLifeMode = LifeBarTrainingMode.Normal;

        [SerializeField]
        private LifeBarTrainingMode defaultTrainingModeGaugeMode = LifeBarTrainingMode.Normal;
        [SerializeField]
        private LifeBarTrainingMode currentTrainingModeGaugeMode = LifeBarTrainingMode.Normal;

        private void OnEnable()
        {
            UFE2FTETrainingModeGaugeModeEventsManager.OnTrainingModeGaugeMode += OnTrainingModeGaugeMode;
        }

        private void Start()
        {
            currentTrainingModeLifeMode = defaultTrainingModeLifeMode;

            currentTrainingModeGaugeMode = defaultTrainingModeGaugeMode;
        }

        private void Update()
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                currentTrainingModeLifeMode = defaultTrainingModeLifeMode;

                currentTrainingModeGaugeMode = defaultTrainingModeGaugeMode;
            }

            UFE2FTETrainingModeGaugeModeOptionsManager.SetCurrentTrainingModeLifeMode(currentTrainingModeLifeMode);

            UFE2FTETrainingModeGaugeModeOptionsManager.SetCurrentTrainingModeGaugeMode(currentTrainingModeGaugeMode);
        }

        private void OnDisable()
        {
            UFE2FTETrainingModeGaugeModeEventsManager.OnTrainingModeGaugeMode -= OnTrainingModeGaugeMode;
        }

        private void OnTrainingModeGaugeMode(LifeBarTrainingMode lifeBarTrainingMode, LifeBarTrainingMode gaugeBarTrainingMode)
        {
            currentTrainingModeLifeMode = lifeBarTrainingMode;

            currentTrainingModeGaugeMode = gaugeBarTrainingMode;
        }
    }
}