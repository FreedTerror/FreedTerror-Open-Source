using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror
{
    public class ShadowResolutionUIController : MonoBehaviour
    {   
        [SerializeField]
        private Text shadowResolutionText;
        private ShadowResolution previousShadowResolution;
        private readonly string playerPrefsKey = "ShadowResolution";

        private void Start()
        {
            if (PlayerPrefs.HasKey(playerPrefsKey) == true)
            {
                QualitySettings.shadowResolution = (ShadowResolution)PlayerPrefs.GetInt(playerPrefsKey);
            }

            if (System.Enum.IsDefined(typeof(ShadowResolution), QualitySettings.shadowResolution) == false)
            {
                QualitySettings.shadowResolution = ShadowResolution.Low;
            }

            previousShadowResolution = QualitySettings.shadowResolution;

            if (shadowResolutionText != null)
            {
                shadowResolutionText.text = Utility.AddSpacesBeforeCapitalLetters(System.Enum.GetName(typeof(ShadowResolution), QualitySettings.shadowResolution));
            }

            PlayerPrefs.SetInt(playerPrefsKey, (int)QualitySettings.shadowResolution);
        }

        private void Update()
        {
            if (previousShadowResolution != QualitySettings.shadowResolution)
            {
                previousShadowResolution = QualitySettings.shadowResolution;

                if (shadowResolutionText != null)
                {
                    shadowResolutionText.text = Utility.AddSpacesBeforeCapitalLetters(System.Enum.GetName(typeof(ShadowResolution), QualitySettings.shadowResolution));
                }

                PlayerPrefs.SetInt(playerPrefsKey, (int)QualitySettings.shadowResolution);
            }
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
        }
    }
}