using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTEGraphicsOptionsUI : MonoBehaviour
    {
        [SerializeField]
        private Text resolutionText;
        [SerializeField]
        private string xName = " X ";
        [SerializeField]
        private string hZName = " HZ";
        private Resolution[] resolutions;
        private int resolutionIndex;

        [SerializeField]
        private Text qualityText;
        private string[] qualityNames;
        private int qualityIndex;  

        [SerializeField]
        private Toggle fullScreenToggle;

        [SerializeField]
        private Toggle vSyncToggle;

        [SerializeField]
        private Toggle shadowsToggle;

        private void Start()
        {
            InitializeGraphicsOptionsUI(); 
        }

        #region Initialize Methods

        private void InitializeGraphicsOptionsUI()
        {
            resolutions = Screen.resolutions;

            resolutionIndex = PlayerPrefs.GetInt("resolutionIndex");

            if (resolutionIndex > resolutions.Length - 1)
            {
                resolutionIndex = 0;
            }

            SetResolution();

            qualityNames = QualitySettings.names;

            qualityIndex = PlayerPrefs.GetInt("qualityIndex");

            if (qualityIndex > qualityNames.Length - 1)
            {
                qualityIndex = 0;
            }

            SetQuality();

            int fullScreen = PlayerPrefs.GetInt("fullScreen");

            if (fullScreen == 0)
            {
                SetUseFullscreen(false);
            }
            else
            {
                SetUseFullscreen(true);
            }

            int vSync = PlayerPrefs.GetInt("vSync");

            if (vSync == 0)
            {
                SetUseVSync(false);
            }
            else
            {
                SetUseVSync(true);
            }

            int shadows = PlayerPrefs.GetInt("shadows");

            if (shadows == 0)
            {
                SetUseShadows(false);
            }
            else
            {
                SetUseShadows(true);
            }
        }

        #endregion

        #region Resolution Methods

        public void NextResolution()
        {
            resolutionIndex++;

            if (resolutionIndex > resolutions.Length - 1)
            {
                resolutionIndex = 0;
            }

            SetResolution();
        }

        public void PreviousResolution()
        {
            resolutionIndex--;

            if (resolutionIndex < 0)
            {
                resolutionIndex = resolutions.Length - 1;
            }

            SetResolution();
        }

        private void SetResolution()
        {
            Resolution resolution = resolutions[resolutionIndex];

            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

            PlayerPrefs.SetInt("resolutionIndex", resolutionIndex);

            SetTextMessage(resolutionText, resolutions[resolutionIndex].width.ToString() + xName + resolutions[resolutionIndex].height.ToString() + " " + resolutions[resolutionIndex].refreshRate + hZName);
        }

        #endregion

        #region Quality Methods

        public void NextQuality()
        {
            qualityIndex++;

            if (qualityIndex > qualityNames.Length - 1)
            {
                qualityIndex = 0;
            }

            SetQuality();
        }

        public void PreviousQuality()
        {
            qualityIndex--;

            if (qualityIndex < 0)
            {
                qualityIndex = qualityNames.Length - 1;
            }

            SetQuality();
        }

        private void SetQuality()
        {
            QualitySettings.SetQualityLevel(qualityIndex);

            PlayerPrefs.SetInt("qualityIndex", qualityIndex);

            SetTextMessage(qualityText, qualityNames[qualityIndex]);

            int vSync = PlayerPrefs.GetInt("vSync");

            if (vSync == 0)
            {
                SetUseVSync(false);
            }
            else
            {
                SetUseVSync(true);
            }

            int shadows = PlayerPrefs.GetInt("shadows");

            if (shadows == 0)
            {
                SetUseShadows(false);
            }
            else
            {
                SetUseShadows(true);
            }
        }

        #endregion

        #region FullScreen Methods

        public void SetUseFullscreen(bool useFullscreen)
        {
            if (useFullscreen == true)
            {
                Screen.fullScreen = true;

                PlayerPrefs.SetInt("fullScreen", 1);
            }
            else
            {
                Screen.fullScreen = false;

                PlayerPrefs.SetInt("fullScreen", 0);
            }
            
            SetToggleIsOn(fullScreenToggle, useFullscreen);
        }

        #endregion

        #region VSync Methods

        public void SetUseVSync(bool useVSync)
        {
            if (useVSync == true)
            {
                QualitySettings.vSyncCount = 1;

                PlayerPrefs.SetInt("vSync", 1);
            }
            else
            {
                QualitySettings.vSyncCount = 0;

                PlayerPrefs.SetInt("vSync", 0);
            }
            
            SetToggleIsOn(vSyncToggle, useVSync);
        }

        #endregion

        #region Shadows Methods

        public void SetUseShadows(bool useShadows)
        {
            if (useShadows == true)
            {
                QualitySettings.shadows = ShadowQuality.All;

                PlayerPrefs.SetInt("shadows", 1);
            }
            else
            {
                QualitySettings.shadows = ShadowQuality.Disable;

                PlayerPrefs.SetInt("shadows", 0);
            }
            
            SetToggleIsOn(shadowsToggle, useShadows);
        }

        #endregion

        private static void SetTextMessage(Text text, string message, Color32? color = null)
        {
            if (text == null)
            {
                return;
            }

            text.text = message;

            if (color != null)
            {
                text.color = (Color32)color;
            }
        }

        private static void SetToggleIsOn(Toggle toggle, bool isOn)
        {
            if (toggle == null)
            {
                return;
            }

            toggle.isOn = isOn;
        }
    }
}
