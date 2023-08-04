using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTETrainingModeAIModeUI : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode[] trainingModeAIModeOrderArray =
            { UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.Human,
            UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.Stand,
            UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.Crouch,
            UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.MoveForward,
            UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.MoveBackward,
            UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.BlockAll,
            UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.StandBlock,
            UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.CrouchBlock,
            UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.ParryAll,
            UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.StandParry,
            UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.CrouchParry,
            UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.AirParry,
            UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.NeutralJump,
            UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.ForwardJump,
            UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.BackwardJump };
        private int trainingModeAIModeOrderArrayIndex;

        [SerializeField]
        private Text trainingModeAIModeText;

        [SerializeField]
        private string humanName = "HUMAN";
        [SerializeField]
        private string standName = "STAND";
        [SerializeField]
        private string crouchName = "CROUCH";
        [SerializeField]
        private string moveForwardName = "MOVE FORWARD";
        [SerializeField]
        private string moveBackwardName = "MOVE BACKWARD";
        [SerializeField]
        private string blockAllName = "BLOCK ALL";
        [SerializeField]
        private string standBlockName = "STAND BLOCK";
        [SerializeField]
        private string crouchBlockName = "CROUCH BLOCK";
        [SerializeField]
        private string parryAllName = "PARRY ALL";
        [SerializeField]
        private string standParryName = "STAND PARRY";
        [SerializeField]
        private string crouchParryName = "CROUCH PARRY";
        [SerializeField]
        private string airParryName = "AIR PARRY";
        [SerializeField]
        private string neutralJumpName = "NEUTRAL JUMP";
        [SerializeField]
        private string forwardJumpName = "FORWARD JUMP";
        [SerializeField]
        private string backwardJumpName = "BACKWARD JUMP";

        private void Start()
        {
            SetTrainingModeAIModeOrderArrayIndex();

            SetTextMessage(trainingModeAIModeText, GetTrainingModeAIModeNameFromTrainingModeAIMode(UFE2FTETrainingModeAIModeOptionsManager.trainingModeAIMode));
        }

        public void NextTrainingModeAIMode()
        {
            trainingModeAIModeOrderArrayIndex++;

            if (trainingModeAIModeOrderArrayIndex > trainingModeAIModeOrderArray.Length - 1)
            {
                trainingModeAIModeOrderArrayIndex = 0;
            }

            UFE2FTETrainingModeAIModeOptionsManager.trainingModeAIMode = trainingModeAIModeOrderArray[trainingModeAIModeOrderArrayIndex];

            SetTextMessage(trainingModeAIModeText, GetTrainingModeAIModeNameFromTrainingModeAIMode(UFE2FTETrainingModeAIModeOptionsManager.trainingModeAIMode));
        }

        public void PreviousTrainingModeAIMode()
        {
            trainingModeAIModeOrderArrayIndex--;

            if (trainingModeAIModeOrderArrayIndex < 0)
            {
                trainingModeAIModeOrderArrayIndex = trainingModeAIModeOrderArray.Length - 1;
            }

            UFE2FTETrainingModeAIModeOptionsManager.trainingModeAIMode = trainingModeAIModeOrderArray[trainingModeAIModeOrderArrayIndex];

            SetTextMessage(trainingModeAIModeText, GetTrainingModeAIModeNameFromTrainingModeAIMode(UFE2FTETrainingModeAIModeOptionsManager.trainingModeAIMode));
        }

        private void SetTrainingModeAIModeOrderArrayIndex()
        {
            int length = trainingModeAIModeOrderArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (UFE2FTETrainingModeAIModeOptionsManager.trainingModeAIMode != trainingModeAIModeOrderArray[i]) continue;

                trainingModeAIModeOrderArrayIndex = i;

                break;
            }
        }

        private string GetTrainingModeAIModeNameFromTrainingModeAIMode(UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode trainingModeAIMode)
        {
            switch (trainingModeAIMode)
            {
                case UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.Human: return humanName;

                case UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.Stand: return standName;

                case UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.Crouch: return crouchName;

                case UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.MoveForward: return moveForwardName;

                case UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.MoveBackward: return moveBackwardName;

                case UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.BlockAll: return blockAllName;

                case UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.StandBlock: return standBlockName;

                case UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.CrouchBlock: return crouchBlockName;

                case UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.ParryAll: return parryAllName;

                case UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.StandParry: return standParryName;

                case UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.CrouchParry: return crouchParryName;

                case UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.AirParry: return airParryName;

                case UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.NeutralJump: return neutralJumpName;

                case UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.ForwardJump: return forwardJumpName;

                case UFE2FTETrainingModeAIModeOptionsManager.TrainingModeAIMode.BackwardJump: return backwardJumpName;

                default: return humanName;
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
