using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class VSyncUIController : MonoBehaviour
    {   
        [SerializeField]
        private Text vSyncText;
        [SerializeField]
        private string zeroMessage = "0";
        [SerializeField]
        private string oneMessage = "1";
        [SerializeField]
        private string twoMessage = "2";
        [SerializeField]
        private string threeMessage = "3";
        [SerializeField]
        private string fourMessage = "4";
        private readonly string playerPrefsKey = "VSync";

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
            UFE2FTE.SetTextMessage(vSyncText, GetStringFromVSync(QualitySettings.vSyncCount));
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
                int vSync = PlayerPrefs.GetInt(playerPrefsKey);

                QualitySettings.vSyncCount = GetValidVSyncValue(vSync);             
            }

            PlayerPrefs.SetInt(playerPrefsKey, QualitySettings.vSyncCount);
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

            PlayerPrefs.SetInt(playerPrefsKey, QualitySettings.vSyncCount);
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

            PlayerPrefs.SetInt(playerPrefsKey, QualitySettings.vSyncCount);
        }

        private int GetValidVSyncValue(int vSync)
        {
            switch (vSync)
            {
                case 0:
                    return 0;

                case 1:
                    return 1;

                case 2:
                    return 2;

                case 3:
                    return 3;

                case 4:
                    return 4;

                default:
                    return 0;
            }
        }

        private string GetStringFromVSync(int vSync)
        {
            switch (vSync)
            {
                case 0:
                    return zeroMessage;

                case 1:
                    return oneMessage;

                case 2:
                    return twoMessage;

                case 3:
                    return threeMessage;

                case 4:
                    return fourMessage;

                default:
                    return zeroMessage;
            }
        }
    }
}