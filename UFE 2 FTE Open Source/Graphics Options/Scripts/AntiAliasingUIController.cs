using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class AntiAliasingUIController : MonoBehaviour
    {   
        [SerializeField]
        private Text antiAliasingText;
        [SerializeField]
        private string zeroMessage = "0";
        [SerializeField]
        private string twoMessage = "2";
        [SerializeField]
        private string fourMessage = "4";
        [SerializeField]
        private string eightMessage = "8";
        private readonly string playerPrefsKey = "AntiAliasing";

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
            UFE2FTE.SetTextMessage(antiAliasingText, GetMessageFromAntiAliasingValue(QualitySettings.antiAliasing));
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
                QualitySettings.antiAliasing = PlayerPrefs.GetInt(playerPrefsKey); 
            }

            QualitySettings.antiAliasing = GetValidAntiAliasingValue(QualitySettings.antiAliasing);

            PlayerPrefs.SetInt(playerPrefsKey, QualitySettings.antiAliasing);
        }

        public void NextAntiAliasing()
        {
            switch (QualitySettings.antiAliasing)
            {
                case 0:
                    QualitySettings.antiAliasing = 2;
                    break;

                case 2:
                    QualitySettings.antiAliasing = 4;
                    break;

                case 4:
                    QualitySettings.antiAliasing = 8;
                    break;

                case 8:
                    QualitySettings.antiAliasing = 0;
                    break;

                default:
                    QualitySettings.antiAliasing = 0;
                    break;
            }

            PlayerPrefs.SetInt(playerPrefsKey, QualitySettings.antiAliasing);
        }

        public void PreviousAntiAliasing()
        {
            switch (QualitySettings.antiAliasing)
            {
                case 0:
                    QualitySettings.antiAliasing = 8;
                    break;

                case 2:
                    QualitySettings.antiAliasing = 0;
                    break;

                case 4:
                    QualitySettings.antiAliasing = 2;
                    break;

                case 8:
                    QualitySettings.antiAliasing = 4;
                    break;

                default:
                    QualitySettings.antiAliasing = 0;
                    break;
            }

            PlayerPrefs.SetInt(playerPrefsKey, QualitySettings.antiAliasing);
        }

        private int GetValidAntiAliasingValue(int antiAliasing)
        {
            switch (antiAliasing)
            {
                case 0:
                    return 0;

                case 2:
                    return 2;

                case 4:
                    return 4;

                case 8:
                    return 8;

                default:
                    return 0;
            }
        }

        private string GetMessageFromAntiAliasingValue(int antiAliasing)
        {
            switch (antiAliasing)
            {
                case 0:
                    return zeroMessage;

                case 2:
                    return twoMessage;

                case 4:
                    return fourMessage;

                case 8:
                    return eightMessage;

                default:
                    return zeroMessage;
            }
        }
    }
}