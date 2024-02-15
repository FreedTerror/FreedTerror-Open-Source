using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror
{
    public class AntiAliasingUIController : MonoBehaviour
    {   
        [SerializeField]
        private Text antiAliasingText;
        private int previousAntiAliasing;
        private readonly string playerPrefsKey = "AntiAliasing";

        private void Start()
        {
            if (PlayerPrefs.HasKey(playerPrefsKey) == true)
            {
                QualitySettings.antiAliasing = PlayerPrefs.GetInt(playerPrefsKey);
            }

            previousAntiAliasing = QualitySettings.antiAliasing;

            if (antiAliasingText != null)
            {
                antiAliasingText.text = QualitySettings.antiAliasing.ToString();
            }

            PlayerPrefs.SetInt(playerPrefsKey, QualitySettings.antiAliasing);
        }

        private void Update()
        {
            if (previousAntiAliasing != QualitySettings.antiAliasing)
            {
                previousAntiAliasing = QualitySettings.antiAliasing;

                if (antiAliasingText != null)
                {
                    antiAliasingText.text = QualitySettings.antiAliasing.ToString();
                }

                PlayerPrefs.SetInt(playerPrefsKey, QualitySettings.antiAliasing);
            }
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
        }
    }
}