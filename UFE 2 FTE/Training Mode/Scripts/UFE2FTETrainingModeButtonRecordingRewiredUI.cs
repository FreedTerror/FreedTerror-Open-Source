using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTETrainingModeButtonRecordingRewiredUI : MonoBehaviour
    {
        [SerializeField]
        private Text trackNumberText;

        private void Start()
        {
            SetTrackNumberText();
        }

        public void NextTrack()
        {
            UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.NextTrack();

            SetTrackNumberText();
        }

        public void PreviousTrack()
        {
            UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.PreviousTrack();

            SetTrackNumberText();
        }

        public void StartRecording()
        {
            UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.StartRecording();
     
            UFE.PauseGame(false);
        }

        public void StopRecording()
        {
            UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.StopRecording();

            UFE.PauseGame(false);
        }

        public void StartPlayback()
        {
            UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.StartPlayback();

            UFE.PauseGame(false);
        }

        public void StopPlayback()
        {
            UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.StopPlayback();

            UFE.PauseGame(false);
        }

        private void SetTrackNumberText()
        {
            int displayNumber = UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.currentTrack + 1;

            SetTextMessage(trackNumberText, displayNumber.ToString());
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