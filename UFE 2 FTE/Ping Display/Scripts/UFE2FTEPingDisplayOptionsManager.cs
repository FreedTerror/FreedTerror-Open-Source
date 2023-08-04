using UnityEngine;

namespace UFE2FTE
{
    public static class UFE2FTEPingDisplayOptionsManager
    {
        public static bool usePingDisplay = true;

        public static void SetUsePingDisplayWithPlayerPrefs(bool usePingDisplay)
        {
            if (usePingDisplay == true)
            {
                UFE2FTEPingDisplayOptionsManager.usePingDisplay = usePingDisplay;

                PlayerPrefs.SetInt("usePingDisplay", 1);
            }
            else
            {
                UFE2FTEPingDisplayOptionsManager.usePingDisplay = usePingDisplay;

                PlayerPrefs.SetInt("usePingDisplay", 0);
            }
        }

        public static void LoadUsePingDisplayFromPlayerPrefs(bool executeMethod)
        {
            if (executeMethod == false)
            {
                return;
            }

            int usePingDisplay = PlayerPrefs.GetInt("usePingDisplay");

            if (usePingDisplay == 0)
            {
                SetUsePingDisplayWithPlayerPrefs(false);
            }
            else
            {
                SetUsePingDisplayWithPlayerPrefs(true);
            }
        }
    }
}
