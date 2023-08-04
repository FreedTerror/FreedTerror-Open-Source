namespace UFE2FTE
{
    public static class UFE2FTETrainingModeGaugeModeOptionsManager
    {
        public static LifeBarTrainingMode trainingModeLifeMode { get; private set; } = LifeBarTrainingMode.Normal;
        public static LifeBarTrainingMode trainingModeGaugeMode { get; private set; } = LifeBarTrainingMode.Normal;

        public static void SetCurrentTrainingModeLifeMode(LifeBarTrainingMode lifeBarTrainingMode)
        {
            trainingModeLifeMode = lifeBarTrainingMode;

            UFE2FTEHelperMethodsManager.SetAllPlayersTrainingModeLifeMode(lifeBarTrainingMode);
        }

        public static void SetCurrentTrainingModeGaugeMode(LifeBarTrainingMode lifeBarTrainingMode)
        {
            trainingModeGaugeMode = lifeBarTrainingMode;

            UFE2FTEHelperMethodsManager.SetAllPlayersTrainingModeGaugeMode(trainingModeGaugeMode);
        }
    }
}
