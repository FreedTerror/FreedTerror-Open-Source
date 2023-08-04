using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTETrainingModeGaugeModeUI : MonoBehaviour
    {
        [SerializeField]
        private string normalName = "NORMAL";
        [SerializeField]
        private string refillName = "REFILL";
        [SerializeField]
        private string infiniteName = "INFINITE";

        [SerializeField]
        private Text trainingModeLifeModeText;

        [SerializeField]
        private LifeBarTrainingMode[] trainingModeLifeModeOrderArray =
            { LifeBarTrainingMode.Normal,
            LifeBarTrainingMode.Refill,
            LifeBarTrainingMode.Infinite };
        private int trainingModeLifeModeOrderArrayIndex;

        [SerializeField]
        private Text trainingModeGaugeModeText;

        [SerializeField]
        private LifeBarTrainingMode[] trainingModeGaugeModeOrderArray =
            { LifeBarTrainingMode.Normal,
            LifeBarTrainingMode.Refill,
            LifeBarTrainingMode.Infinite };
        private int trainingModeGaugeModeOrderArrayIndex;

        private void Start()
        {
            SetTrainingModeLifeModeOrderArrayIndex();

            SetTextMessage(trainingModeLifeModeText, GetTrainingModeGaugeModeNameFromLifeBarTrainingMode(UFE2FTETrainingModeGaugeModeOptionsManager.trainingModeLifeMode));

            SetTrainingModeGaugeModeOrderIndex();

            SetTextMessage(trainingModeGaugeModeText, GetTrainingModeGaugeModeNameFromLifeBarTrainingMode(UFE2FTETrainingModeGaugeModeOptionsManager.trainingModeGaugeMode));
        }

        public void NextTrainingModeLifeMode()
        {
            trainingModeLifeModeOrderArrayIndex++;

            if (trainingModeLifeModeOrderArrayIndex > trainingModeLifeModeOrderArray.Length - 1)
            {
                trainingModeLifeModeOrderArrayIndex = 0;
            }

            UFE2FTETrainingModeGaugeModeOptionsManager.SetCurrentTrainingModeLifeMode(trainingModeLifeModeOrderArray[trainingModeLifeModeOrderArrayIndex]);

            SetTextMessage(trainingModeLifeModeText, GetTrainingModeGaugeModeNameFromLifeBarTrainingMode(UFE2FTETrainingModeGaugeModeOptionsManager.trainingModeLifeMode));

            UFE2FTETrainingModeGaugeModeEventsManager.CallOnTrainingModeGaugeMode(UFE2FTETrainingModeGaugeModeOptionsManager.trainingModeLifeMode, UFE2FTETrainingModeGaugeModeOptionsManager.trainingModeGaugeMode);
        }

        public void PreviousTrainingModeLifeMode()
        {
            trainingModeLifeModeOrderArrayIndex--;

            if (trainingModeLifeModeOrderArrayIndex < 0)
            {
                trainingModeLifeModeOrderArrayIndex = trainingModeLifeModeOrderArray.Length - 1;
            }

            UFE2FTETrainingModeGaugeModeOptionsManager.SetCurrentTrainingModeLifeMode(trainingModeLifeModeOrderArray[trainingModeLifeModeOrderArrayIndex]);

            SetTextMessage(trainingModeLifeModeText, GetTrainingModeGaugeModeNameFromLifeBarTrainingMode(UFE2FTETrainingModeGaugeModeOptionsManager.trainingModeLifeMode));

            UFE2FTETrainingModeGaugeModeEventsManager.CallOnTrainingModeGaugeMode(UFE2FTETrainingModeGaugeModeOptionsManager.trainingModeLifeMode, UFE2FTETrainingModeGaugeModeOptionsManager.trainingModeGaugeMode);
        }

        private void SetTrainingModeLifeModeOrderArrayIndex()
        {
            int length = trainingModeLifeModeOrderArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (UFE2FTETrainingModeGaugeModeOptionsManager.trainingModeLifeMode != trainingModeLifeModeOrderArray[i])
                {
                    continue;
                }

                trainingModeLifeModeOrderArrayIndex = i;

                break;
            }
        }

        public void NextTrainingModeGaugeMode()
        {
            trainingModeGaugeModeOrderArrayIndex++;

            if (trainingModeGaugeModeOrderArrayIndex > trainingModeGaugeModeOrderArray.Length - 1)
            {
                trainingModeGaugeModeOrderArrayIndex = 0;
            }

            UFE2FTETrainingModeGaugeModeOptionsManager.SetCurrentTrainingModeGaugeMode(trainingModeGaugeModeOrderArray[trainingModeGaugeModeOrderArrayIndex]);

            SetTextMessage(trainingModeGaugeModeText, GetTrainingModeGaugeModeNameFromLifeBarTrainingMode(UFE2FTETrainingModeGaugeModeOptionsManager.trainingModeGaugeMode));

            UFE2FTETrainingModeGaugeModeEventsManager.CallOnTrainingModeGaugeMode(UFE2FTETrainingModeGaugeModeOptionsManager.trainingModeLifeMode, UFE2FTETrainingModeGaugeModeOptionsManager.trainingModeGaugeMode);
        }

        public void PreviousTrainingModeGaugeMode()
        {
            trainingModeGaugeModeOrderArrayIndex--;

            if (trainingModeGaugeModeOrderArrayIndex < 0)
            {
                trainingModeGaugeModeOrderArrayIndex = trainingModeGaugeModeOrderArray.Length - 1;
            }

            UFE2FTETrainingModeGaugeModeOptionsManager.SetCurrentTrainingModeGaugeMode(trainingModeGaugeModeOrderArray[trainingModeGaugeModeOrderArrayIndex]);

            SetTextMessage(trainingModeGaugeModeText, GetTrainingModeGaugeModeNameFromLifeBarTrainingMode(UFE2FTETrainingModeGaugeModeOptionsManager.trainingModeGaugeMode));

            UFE2FTETrainingModeGaugeModeEventsManager.CallOnTrainingModeGaugeMode(UFE2FTETrainingModeGaugeModeOptionsManager.trainingModeLifeMode, UFE2FTETrainingModeGaugeModeOptionsManager.trainingModeGaugeMode);
        }

        private void SetTrainingModeGaugeModeOrderIndex()
        {
            int length = trainingModeGaugeModeOrderArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (UFE2FTETrainingModeGaugeModeOptionsManager.trainingModeGaugeMode != trainingModeGaugeModeOrderArray[i])
                {
                    continue;
                }

                trainingModeGaugeModeOrderArrayIndex = i;

                break;
            }
        }

        private string GetTrainingModeGaugeModeNameFromLifeBarTrainingMode(LifeBarTrainingMode lifeBarTrainingMode)
        {
            switch (lifeBarTrainingMode)
            {
                case LifeBarTrainingMode.Normal: return normalName;

                case LifeBarTrainingMode.Refill: return refillName;

                case LifeBarTrainingMode.Infinite: return infiniteName;

                default: return normalName;
            }
        }

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
    }
}
