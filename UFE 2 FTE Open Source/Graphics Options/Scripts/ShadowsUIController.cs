using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class ShadowsUIController : MonoBehaviour
    {   
        [SerializeField]
        private Text shadowsQualityText;
        private readonly string playerPrefsKey = "Shadows";

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
            UFE2FTE.SetTextMessage(shadowsQualityText, System.Enum.GetName(typeof(ShadowQuality), QualitySettings.shadows));
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
                QualitySettings.shadows = (ShadowQuality)PlayerPrefs.GetInt(playerPrefsKey);
            }

            if (System.Enum.IsDefined(typeof(ShadowQuality), QualitySettings.shadows) == false)
            {
                QualitySettings.shadows = ShadowQuality.Disable;
            }

            PlayerPrefs.SetInt(playerPrefsKey, (int)QualitySettings.shadows);
        }

        public void NextShadows()
        {
            switch (QualitySettings.shadows)
            {
                case ShadowQuality.Disable:
                    QualitySettings.shadows = ShadowQuality.HardOnly;
                    break;

                case ShadowQuality.HardOnly:
                    QualitySettings.shadows = ShadowQuality.All;
                    break;

                case ShadowQuality.All:
                    QualitySettings.shadows = ShadowQuality.Disable;
                    break;

                default:
                    QualitySettings.shadows = ShadowQuality.Disable;
                    break;
            }

            PlayerPrefs.SetInt(playerPrefsKey, (int)QualitySettings.shadows);
        }

        public void PreviousShadows()
        {
            switch (QualitySettings.shadows)
            {
                case ShadowQuality.Disable:
                    QualitySettings.shadows = ShadowQuality.All;
                    break;

                case ShadowQuality.HardOnly:
                    QualitySettings.shadows = ShadowQuality.Disable;
                    break;

                case ShadowQuality.All:
                    QualitySettings.shadows = ShadowQuality.HardOnly;
                    break;

                default:
                    QualitySettings.shadows = ShadowQuality.Disable;
                    break;
            }

            PlayerPrefs.SetInt(playerPrefsKey, (int)QualitySettings.shadows);
        }
    }
}