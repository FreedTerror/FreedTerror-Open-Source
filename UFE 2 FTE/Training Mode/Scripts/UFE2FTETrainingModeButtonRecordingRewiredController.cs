using System;
using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTETrainingModeButtonRecordingRewiredController : MonoBehaviour
    {
        [SerializeField]
        private ButtonPress[] excludedButtonPressArray;

        [Range(1, 10)]
        [SerializeField]
        private int availableTracks = 5;

        [SerializeField]
        private int maxRecordingFrames = 600;

        private void Start()
        {
            UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.SetAllPlayersRewiredInputController();

            UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.SetRecordedButtonPressDatas();
        }

        private void FixedUpdate()
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null
                || UFE.gameMode != GameMode.TrainingRoom)
            {
                UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.ResetButtonRecordingVariables();

                UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.SetRewiredInputControllersPlayback();

                return;
            }

            UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.excludedButtonPresses = excludedButtonPressArray;

            UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.availableTracks = availableTracks;

            UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.maxRecordingFrames = maxRecordingFrames;

            UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.Record();

            UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.Playback();
        }

        private void OnDestroy()
        {
            UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.SetRewiredInputControllersPlayback();
        }

        [NaughtyAttributes.Button]
        private void StartRecording()
        {
            UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.StartRecording();
        }

        [NaughtyAttributes.Button]
        private void StopRecording()
        {
            UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.StopRecording();
        }

        [NaughtyAttributes.Button]
        private void StartPlayback()
        {
            UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.StartPlayback();
        }

        [NaughtyAttributes.Button]
        private void StopPlayback()
        {
            UFE2FTETrainingModeButtonRecordingRewiredOptionsManager.StopPlayback();
        }
    }
}
