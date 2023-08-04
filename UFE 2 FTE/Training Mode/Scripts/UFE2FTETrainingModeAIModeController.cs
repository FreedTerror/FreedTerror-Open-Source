using UnityEngine;

namespace UFE2FTE
{
    public class UFE2FTETrainingModeAIModeController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode trainingModeAIMode;

        [SerializeField]
        private int activeFramesBeginOffset = -1;

        private void Start()
        {
            UFE2FTETrainingModeAIModeOptionsManager.trainingModeAIMode = UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.Human;
        }

        private void FixedUpdate()
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                UFE2FTETrainingModeAIModeOptionsManager.trainingModeAIMode = UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.Human;

                return;
            }

            UFE2FTETrainingModeAIModeOptionsManager.trainingModeAIMode = trainingModeAIMode;

            UFE2FTETrainingModeAIModeOptionsManager.activeFramesBeginOffset = activeFramesBeginOffset;

            UFE2FTETrainingModeAIModeOptionsManager.SetTrainingModeAIInput();
        }
    }
}
