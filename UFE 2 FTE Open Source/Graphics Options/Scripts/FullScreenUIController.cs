using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class FullScreenUIController : MonoBehaviour
    {
        //The Screen class seems to have a built in PlayerPrefs system.

        [SerializeField]
        private Text fullScreenText;
    
        private void Update()
        {
            UFE2FTE.SetTextMessage(fullScreenText, UFE2FTE.GetStringFromBool(Screen.fullScreen));
        }

        public void ToggleFullScreen()
        {
            if (Screen.fullScreen == true)
            {
                Screen.fullScreen = false;
            }
            else
            {
                Screen.fullScreen = true;
            }
        }
    }
}