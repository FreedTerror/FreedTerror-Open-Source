using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class ResolutionUIController : MonoBehaviour
    {
        //The Screen class seems to have a built in PlayerPrefs system.

        [SerializeField]
        private Text resolutionText;
        private int resolutionsIndex;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            resolutionsIndex = GetResolutionsIndex();

            UFE2FTE.SetTextMessage(resolutionText, GetResolutionMessage(Screen.currentResolution));
        }

        public void NextResolution()
        {
            NextResolutionsIndex();

            Screen.SetResolution(
                Screen.resolutions[resolutionsIndex].width, 
                Screen.resolutions[resolutionsIndex].height, 
                Screen.fullScreen);

            UFE2FTE.SetTextMessage(resolutionText, GetResolutionMessage(Screen.resolutions[resolutionsIndex]));  
        }

        public void PreviousResolution()
        {
            PreviousResolutionsIndex();

            Screen.SetResolution(
                Screen.resolutions[resolutionsIndex].width,
                Screen.resolutions[resolutionsIndex].height,
                Screen.fullScreen);

            UFE2FTE.SetTextMessage(resolutionText, GetResolutionMessage(Screen.resolutions[resolutionsIndex]));
        }

        public void DefaultResolution()
        {
            resolutionsIndex = Screen.resolutions.Length - 1;

            Screen.SetResolution(
                Screen.resolutions[resolutionsIndex].width,
                Screen.resolutions[resolutionsIndex].height,
                Screen.fullScreen);

            UFE2FTE.SetTextMessage(resolutionText, GetResolutionMessage(Screen.resolutions[resolutionsIndex]));
        }

        private int GetResolutionsIndex()
        {
            int resolutionsIndex = 0;

            int length = Screen.resolutions.Length;
            for (int i = 0; i < length; i++)
            {
                if (Screen.currentResolution.width == Screen.resolutions[i].width
                    && Screen.currentResolution.height == Screen.resolutions[i].height)
                {
                    resolutionsIndex = i;

                    break;
                }
            }

            return resolutionsIndex;
        }

        private void NextResolutionsIndex()
        {
            resolutionsIndex++;

            if (resolutionsIndex > Screen.resolutions.Length - 1)
            {
                resolutionsIndex = 0;
            }
        }

        private void PreviousResolutionsIndex()
        {
            resolutionsIndex--;

            if (resolutionsIndex < 0)
            {
                resolutionsIndex = Screen.resolutions.Length - 1;
            }
        }

        private string GetResolutionMessage(Resolution resolution)
        {
            return resolution.width +
                " x " +
                resolution.height +
                " " +
                resolution.refreshRateRatio +
                " " +
                "Hz";
        }
    }
}