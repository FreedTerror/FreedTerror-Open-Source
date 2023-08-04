using UnityEngine;

namespace UFE2FTE
{
    public static class UFE2FTECameraShakeOptionsManager
    {
        public static bool useCameraShake = true;

        public static void SetUseCameraShakeWithPlayerPrefs(bool useCameraShake)
        {
            if (useCameraShake == true)
            {
                UFE2FTECameraShakeOptionsManager.useCameraShake = useCameraShake;

                PlayerPrefs.SetInt("useCameraShake", 1);
            }
            else
            {
                UFE2FTECameraShakeOptionsManager.useCameraShake = useCameraShake;

                PlayerPrefs.SetInt("useCameraShake", 0);
            }
        }

        public static void LoadUseCameraShakeFromPlayerPrefs(bool executeMethod)
        {
            if (executeMethod == false) return;

            int useCameraShake = PlayerPrefs.GetInt("useCameraShake");

            if (useCameraShake == 0)
            {
                SetUseCameraShakeWithPlayerPrefs(false);
            }
            else
            {
                SetUseCameraShakeWithPlayerPrefs(true);
            }
        }
    }
}