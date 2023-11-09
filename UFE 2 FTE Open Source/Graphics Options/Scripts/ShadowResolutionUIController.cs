using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class ShadowResolutionUIController : MonoBehaviour
    {   
        [SerializeField]
        private Text shadowResolutionText;
        private readonly string playerPrefsKey = "ShadowResolution";

        private void OnEnable()
        {
            QualitySettings.activeQualityLevelChanged += activeQualityLevelChanged;
        }

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            UFE2FTE.SetTextMessage(shadowResolutionText, System.Enum.GetName(typeof(ShadowResolution), QualitySettings.shadowResolution));
        }

        private void OnDisable()
        {
            QualitySettings.activeQualityLevelChanged -= activeQualityLevelChanged;
        }

        private void activeQualityLevelChanged(int previousQuality, int currentQuality)
        {
            Initialize();
        }

        private void Initialize()
        {
            if (PlayerPrefs.HasKey(playerPrefsKey) == true)
            {
                int shadowResolution = PlayerPrefs.GetInt(playerPrefsKey);

                if (System.Enum.IsDefined(typeof(ShadowResolution), (ShadowResolution)shadowResolution) == true)
                {
                    QualitySettings.shadowResolution = (ShadowResolution)shadowResolution;
                }
                else
                {
                    QualitySettings.shadowResolution = ShadowResolution.Low;
                }
            }

            PlayerPrefs.SetInt(playerPrefsKey, (int)QualitySettings.shadowResolution);
        }

        public void NextShadowResolution()
        {
            switch (QualitySettings.shadowResolution)
            {
                case ShadowResolution.Low:
                    QualitySettings.shadowResolution = ShadowResolution.Medium;
                    break;

                case ShadowResolution.Medium:
                    QualitySettings.shadowResolution = ShadowResolution.High;
                    break;

                case ShadowResolution.High:
                    QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
                    break;

                case ShadowResolution.VeryHigh:
                    QualitySettings.shadowResolution = ShadowResolution.Low;
                    break;

                default:
                    QualitySettings.shadowResolution = ShadowResolution.Low;
                    break;
            }

            PlayerPrefs.SetInt(playerPrefsKey, (int)QualitySettings.shadowResolution);
        }

        public void PreviousShadowResolution()
        {
            switch (QualitySettings.shadowResolution)
            {
                case ShadowResolution.Low:
                    QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
                    break;

                case ShadowResolution.Medium:
                    QualitySettings.shadowResolution = ShadowResolution.Low;
                    break;

                case ShadowResolution.High:
                    QualitySettings.shadowResolution = ShadowResolution.Medium;
                    break;

                case ShadowResolution.VeryHigh:
                    QualitySettings.shadowResolution = ShadowResolution.High;
                    break;

                default:
                    QualitySettings.shadowResolution = ShadowResolution.Low;
                    break;
            }

            PlayerPrefs.SetInt(playerPrefsKey, (int)QualitySettings.shadowResolution);
        }
    }
}