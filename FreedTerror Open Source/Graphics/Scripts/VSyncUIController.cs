using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror
{
    public class VSyncUIController : MonoBehaviour
    {   
        [SerializeField]
        private Text vSyncText;
        private int previousVSync;
        private readonly string playerPrefsKey = "VSync";

        private void Start()
        {
            if (PlayerPrefs.HasKey(playerPrefsKey) == true)
            {
                QualitySettings.vSyncCount = PlayerPrefs.GetInt(playerPrefsKey);
            }

            previousVSync = QualitySettings.vSyncCount;

            if (vSyncText != null)
            {
                vSyncText.text = QualitySettings.vSyncCount.ToString();
            }

            PlayerPrefs.SetInt(playerPrefsKey, QualitySettings.vSyncCount);
        }

        private void Update()
        {
            if (previousVSync != QualitySettings.vSyncCount)
            {
                previousVSync = QualitySettings.vSyncCount;

                if (vSyncText != null)
                {
                    vSyncText.text = QualitySettings.vSyncCount.ToString();
                }

                PlayerPrefs.SetInt(playerPrefsKey, QualitySettings.vSyncCount);
            }
        }

        public void NextVSync()
        {
            switch (QualitySettings.vSyncCount)
            {
                case 0:
                    QualitySettings.vSyncCount = 1;
                    break;

                case 1:
                    QualitySettings.vSyncCount = 2;
                    break;

                case 2:
                    QualitySettings.vSyncCount = 3;
                    break;

                case 3:
                    QualitySettings.vSyncCount = 4;
                    break;

                case 4:
                    QualitySettings.vSyncCount = 0;
                    break;

                default:
                    QualitySettings.vSyncCount = 0;
                    break;
            }
        }

        public void PreviousVSync()
        {
            switch (QualitySettings.vSyncCount)
            {
                case 0:
                    QualitySettings.vSyncCount = 4;
                    break;

                case 1:
                    QualitySettings.vSyncCount = 0;
                    break;

                case 2:
                    QualitySettings.vSyncCount = 1;
                    break;

                case 3:
                    QualitySettings.vSyncCount = 2;
                    break;

                case 4:
                    QualitySettings.vSyncCount = 3;
                    break;

                default:
                    QualitySettings.vSyncCount = 0;
                    break;
            }
        }
    }
}