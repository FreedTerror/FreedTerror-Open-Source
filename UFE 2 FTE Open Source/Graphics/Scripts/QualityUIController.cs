using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class QualityUIController : MonoBehaviour
    {
        //The QualitySettings class seems to have a built in PlayerPrefs system.

        [SerializeField]
        private Text qualityText;
  
        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            UFE2FTE.SetTextMessage(qualityText, QualitySettings.names[QualitySettings.GetQualityLevel()]);
        }

        private void Initialize()
        {
            UFE2FTE.SetTextMessage(qualityText, QualitySettings.names[QualitySettings.GetQualityLevel()]);
        }

        public void NextQuality()
        {
            int qualityLevel = QualitySettings.GetQualityLevel();

            qualityLevel++;

            if (qualityLevel > QualitySettings.names.Length - 1)
            {
                qualityLevel = 0;
            }

            QualitySettings.SetQualityLevel(qualityLevel);
        }

        public void PreviousQuality()
        {
            int qualityLevel = QualitySettings.GetQualityLevel();

            qualityLevel--;

            if (qualityLevel < 0)
            {
                qualityLevel = QualitySettings.names.Length - 1;
            }

            QualitySettings.SetQualityLevel(qualityLevel);
        }
    }
}