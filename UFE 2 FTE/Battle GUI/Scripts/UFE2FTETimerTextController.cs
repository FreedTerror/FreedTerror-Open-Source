using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTETimerTextController : MonoBehaviour
    {
        [SerializeField]
        private Text timerText;
        [SerializeField]
        private Vector3 infiniteTimeTimerTextRotation = new Vector3(0, 0, 90);
        [SerializeField]
        private string infiniteTimeName = "8";
        [SerializeField]
        private Color32 infiniteTimeColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        private UFE2FTEGCFreeStringNumbersScriptableObject gCFreeStringNumbersScriptableObject;

        private void Update()
        {
            if (gCFreeStringNumbersScriptableObject == null)
            {
                return;
            }

            SetTimerGUI((float)UFE.timer);
        }

        private void SetTimerGUI(float time)
        {
            if (timerText == null)
            {
                return;
            }

            int timerValue = Mathf.CeilToInt(time);

            timerText.text = UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, timerValue);

            if (UFE.gameMode == GameMode.TrainingRoom)
            {
                if (UFE.config.trainingModeOptions.freezeTime == true)
                {
                    timerText.rectTransform.localEulerAngles = infiniteTimeTimerTextRotation;

                    timerText.text = infiniteTimeName;

                    timerText.color = infiniteTimeColor;
                }
            }
            else
            {
                if (UFE.config.roundOptions.hasTimer == false)
                {
                    timerText.rectTransform.localEulerAngles = infiniteTimeTimerTextRotation;

                    timerText.text = infiniteTimeName;

                    timerText.color = infiniteTimeColor;
                }
            }
        }
    }
}