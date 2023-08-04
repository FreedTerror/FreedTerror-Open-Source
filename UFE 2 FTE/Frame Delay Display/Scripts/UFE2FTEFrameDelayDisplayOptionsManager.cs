using UnityEngine;

namespace UFE2FTE
{
    public static class UFE2FTEFrameDelayDisplayOptionsManager
    {
        public static bool useFrameDelayDisplay = true;

        public static void SetUseFrameDelayDisplayWithPlayerPrefs(bool useFrameDelayDisplay)
        {
            if (useFrameDelayDisplay == true)
            {
                UFE2FTEFrameDelayDisplayOptionsManager.useFrameDelayDisplay = useFrameDelayDisplay;

                PlayerPrefs.SetInt("useFrameDelayDisplay", 1);
            }
            else
            {
                UFE2FTEFrameDelayDisplayOptionsManager.useFrameDelayDisplay = useFrameDelayDisplay;

                PlayerPrefs.SetInt("useFrameDelayDisplay", 0);
            }
        }

        public static void LoadUseFrameDelayDisplayFromPlayerPrefs(bool executeMethod)
        {
            if (executeMethod == false) return;

            int useFrameDelayDisplay = PlayerPrefs.GetInt("useFrameDelayDisplay");

            if (useFrameDelayDisplay == 0)
            {
                SetUseFrameDelayDisplayWithPlayerPrefs(false);
            }
            else
            {
                SetUseFrameDelayDisplayWithPlayerPrefs(true);
            }
        }
    }
}
