using UnityEngine;
using UnityEngine.UI;
using FPLibrary;

namespace FreedTerror.UFE2
{
    public class TrainingModePositionUIController : MonoBehaviour
    {
      
        [SerializeField]
        private Text cornerPlayerText;
        private UFE2Manager.Player previousCornerPlayer;
        [SerializeField]
        private Text cornerPositionXOffsetText;
        private Fix64 previousCornerPositionXOffset;

        private void Start()
        {
            previousCornerPlayer = UFE2Manager.instance.trainingModeCornerPlayer;

            if (cornerPlayerText != null)
            {
                cornerPlayerText.text = Utility.AddSpacesBeforeNumbers(System.Enum.GetName(typeof(UFE2Manager.Player), UFE2Manager.instance.trainingModeCornerPlayer));
            }

            previousCornerPositionXOffset = UFE2Manager.instance.trainingModeCornerPositionXOffset;

            if (cornerPositionXOffsetText != null)
            {
                cornerPositionXOffsetText.text = UFE2Manager.instance.trainingModeCornerPositionXOffset.ToString();
            }
        }

        private void Update()
        {
            if (previousCornerPlayer != UFE2Manager.instance.trainingModeCornerPlayer)
            {
                previousCornerPlayer = UFE2Manager.instance.trainingModeCornerPlayer;

                if (cornerPlayerText != null)
                {
                    cornerPlayerText.text = Utility.AddSpacesBeforeNumbers(System.Enum.GetName(typeof(UFE2Manager.Player), UFE2Manager.instance.trainingModeCornerPlayer));
                }
            }

            if (previousCornerPositionXOffset != UFE2Manager.instance.trainingModeCornerPositionXOffset)
            {
                previousCornerPositionXOffset = UFE2Manager.instance.trainingModeCornerPositionXOffset;

                if (cornerPositionXOffsetText != null)
                {
                    cornerPositionXOffsetText.text = UFE2Manager.instance.trainingModeCornerPositionXOffset.ToString();
                }
            }    
        }

        public void NextCornerPlayer()
        {
            UFE2Manager.instance.trainingModeCornerPlayer = Utility.GetNextEnum(UFE2Manager.instance.trainingModeCornerPlayer);
        }

        public void PreviousCornerPlayer()
        {
            UFE2Manager.instance.trainingModeCornerPlayer = Utility.GetPreviousEnum(UFE2Manager.instance.trainingModeCornerPlayer);
        }

        public void NextCornerPositionXOffset()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE2Manager.instance.trainingModeCornerPositionXOffset += (Fix64)0.5;

            if (UFE2Manager.instance.trainingModeCornerPositionXOffset > UFE.config.cameraOptions._maxDistance)
            {
                UFE2Manager.instance.trainingModeCornerPositionXOffset = (Fix64)0;
            }
        }

        public void PreviousCornerPositionXOffset()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE2Manager.instance.trainingModeCornerPositionXOffset -= (Fix64)0.5;

            if (UFE2Manager.instance.trainingModeCornerPositionXOffset < (Fix64)0)
            {
                UFE2Manager.instance.trainingModeCornerPositionXOffset = UFE.config.cameraOptions._maxDistance;
            }
        }

        public void SetAllPlayersLeftCornerPosition()
        {
            UFE2Manager.SetAllPlayersLeftCornerPosition(UFE2Manager.GetControlsScript(UFE2Manager.instance.trainingModeCornerPlayer), UFE2Manager.instance.trainingModeCornerPositionXOffset);

            UFE.PauseGame(false);
        }

        public void SetAllPlayersRightCornerPosition()
        {
            UFE2Manager.SetAllPlayersRightCornerPosition(UFE2Manager.GetControlsScript(UFE2Manager.instance.trainingModeCornerPlayer), UFE2Manager.instance.trainingModeCornerPositionXOffset);

            UFE.PauseGame(false);
        }

        public void ResetAllPlayersPosition()
        {
            UFE2Manager.ResetAllPlayersPosition();

            UFE.PauseGame(false);
        }
    }
}
