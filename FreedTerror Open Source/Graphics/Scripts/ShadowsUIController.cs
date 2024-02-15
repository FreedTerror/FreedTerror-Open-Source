using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror
{
    public class ShadowsUIController : MonoBehaviour
    {   
        [SerializeField]
        private Text shadowsQualityText;
        private ShadowQuality previousShadowsQuality;
        private readonly string playerPrefsKey = "Shadows";

        private void Start()
        {
            if (PlayerPrefs.HasKey(playerPrefsKey) == true)
            {
                QualitySettings.shadows = (ShadowQuality)PlayerPrefs.GetInt(playerPrefsKey);
            }

            if (System.Enum.IsDefined(typeof(ShadowQuality), QualitySettings.shadows) == false)
            {
                QualitySettings.shadows = ShadowQuality.Disable;
            }

            previousShadowsQuality = QualitySettings.shadows;

            if (shadowsQualityText != null)
            {
                shadowsQualityText.text = Utility.AddSpacesBeforeCapitalLetters(System.Enum.GetName(typeof(ShadowQuality), QualitySettings.shadows));
            }

            PlayerPrefs.SetInt(playerPrefsKey, (int)QualitySettings.shadows);
        }

        private void Update()
        {
            if (previousShadowsQuality != QualitySettings.shadows)
            {
                previousShadowsQuality = QualitySettings.shadows;

                if (shadowsQualityText != null)
                {
                    shadowsQualityText.text = Utility.AddSpacesBeforeCapitalLetters(System.Enum.GetName(typeof(ShadowQuality), QualitySettings.shadows));
                }

                PlayerPrefs.SetInt(playerPrefsKey, (int)QualitySettings.shadows);
            }
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
        }
    }
}