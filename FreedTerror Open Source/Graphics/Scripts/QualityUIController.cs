using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror
{
    public class QualityUIController : MonoBehaviour
    {
        //The QualitySettings class seems to have a built in PlayerPrefs system.

        [SerializeField]
        private Text qualityText;
        private int previousQualityLevel;

        private void Start()
        {
            previousQualityLevel = QualitySettings.GetQualityLevel();

            if (qualityText != null)
            {
                qualityText.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
            }
        }

        private void Update()
        {
            if (previousQualityLevel != QualitySettings.GetQualityLevel())
            {
                previousQualityLevel = QualitySettings.GetQualityLevel();

                if (qualityText != null)
                {
                    qualityText.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
                }
            }
        }

        public void NextQuality()
        {
            int qualityLevel = QualitySettings.GetQualityLevel();

            qualityLevel += 1;

            if (qualityLevel > QualitySettings.names.Length - 1)
            {
                qualityLevel = 0;
            }

            QualitySettings.SetQualityLevel(qualityLevel);
        }

        public void PreviousQuality()
        {
            int qualityLevel = QualitySettings.GetQualityLevel();

            qualityLevel -= 1;

            if (qualityLevel < 0)
            {
                qualityLevel = QualitySettings.names.Length - 1;
            }

            QualitySettings.SetQualityLevel(qualityLevel);
        }
    }
}