namespace UFE2FTE
{
    public static class UFE2FTEHitBoxDisplayEventsManager
    {
        public delegate void HitBoxDisplayHandler(UFE2FTEHitBoxDisplayOptionsManager.DisplayMode displayMode, byte alphaValue, bool useProjectileTotalHitsText);
        public static event HitBoxDisplayHandler OnHitBoxDisplay;

        public static void CallOnHitBoxDisplay(UFE2FTEHitBoxDisplayOptionsManager.DisplayMode displayMode, byte alphaValue, bool useProjectileTotalHitsText)
        {
            if (OnHitBoxDisplay == null)
            {
                return;
            }

            OnHitBoxDisplay(displayMode, alphaValue, useProjectileTotalHitsText);
        }
    }
}
