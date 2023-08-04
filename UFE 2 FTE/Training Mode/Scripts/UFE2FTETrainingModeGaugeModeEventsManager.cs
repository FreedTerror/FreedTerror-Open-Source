namespace UFE2FTE
{
    public static class UFE2FTETrainingModeGaugeModeEventsManager
    {
        public delegate void LifeBarTrainingModeHandler(LifeBarTrainingMode lifeBarTrainingMode, LifeBarTrainingMode gaugeBarTrainingMode);
        public static event LifeBarTrainingModeHandler OnTrainingModeGaugeMode;

        public static void CallOnTrainingModeGaugeMode(LifeBarTrainingMode lifeBarTrainingMode, LifeBarTrainingMode gaugeBarTrainingMode)
        {
            if (OnTrainingModeGaugeMode == null)
            {
                return;
            }

            OnTrainingModeGaugeMode(lifeBarTrainingMode, gaugeBarTrainingMode);
        }
    }
}
