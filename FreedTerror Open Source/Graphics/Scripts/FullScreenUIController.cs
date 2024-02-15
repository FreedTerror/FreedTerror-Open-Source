using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror
{
    public class FullScreenUIController : MonoBehaviour
    {
        //The Screen class seems to have a built in PlayerPrefs system.

        [SerializeField]
        private Text fullScreenText;
        private bool previousFullScreen;

        private void Start()
        {
            previousFullScreen = Screen.fullScreen;

            if (fullScreenText != null)
            {
                fullScreenText.text = Utility.GetStringFromBool(Screen.fullScreen);
            }
        }

        private void Update()
        {
            if (previousFullScreen != Screen.fullScreen)
            {
                previousFullScreen = Screen.fullScreen;

                if (fullScreenText != null)
                {
                    fullScreenText.text = Utility.GetStringFromBool(Screen.fullScreen);
                }
            }
        }

        public void ToggleFullScreen()
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}