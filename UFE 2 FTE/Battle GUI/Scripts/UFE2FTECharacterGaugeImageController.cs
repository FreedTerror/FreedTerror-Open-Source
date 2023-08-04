using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTECharacterGaugeImageController : MonoBehaviour
    {
        private enum Player
        {
            Player1,
            Player2
        }
        [SerializeField]
        private Player player;
        [SerializeField]
        private GaugeId gaugeId;
        [SerializeField]
        private Image gaugeImage;
        [SerializeField]
        private Image gaugeCostLossImage;
        private enum LossImageMode
        {
            Constant,
            Custom1,
        }
        [SerializeField]
        private LossImageMode gaugeCostLossImageMode;
        [SerializeField]
        private float gaugeCostLossImageSpeed;
        private float gaugeCostLossImagePreviousAmount;
        [SerializeField]
        private Image gaugeTotalLossImage;
        [SerializeField]
        private LossImageMode gaugeTotalLossImageMode;
        [SerializeField]
        private float gaugeTotalLossImageSpeed;

        private void Update()
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                return;
            }

            float deltaTime = (float)UFE.fixedDeltaTime;

            SetCharacterGaugeImage(deltaTime);

            SetCharacterGaugeCostLossImage(deltaTime);

            SetCharacterGaugeTotalLossImage(deltaTime);
        }

        private void SetCharacterGaugeImage(float deltaTime)
        {
            if (gaugeImage == null)
            {
                return;
            }

            if (player == Player.Player1)
            {
                gaugeImage.fillAmount = (float)UFE.GetPlayer1ControlsScript().currentGaugesPoints[(int)gaugeId] / UFE.GetPlayer1ControlsScript().myInfo.maxGaugePoints;
            }
            else if (player == Player.Player2)
            {
                gaugeImage.fillAmount = (float)UFE.GetPlayer2ControlsScript().currentGaugesPoints[(int)gaugeId] / UFE.GetPlayer2ControlsScript().myInfo.maxGaugePoints;
            }
        }

        private void SetCharacterGaugeCostLossImage(float deltaTime)
        {
            if (gaugeImage == null
                || gaugeCostLossImage == null)
            {
                return;
            }

            if (gaugeImage.fillAmount > gaugeCostLossImage.fillAmount)
            {
                gaugeCostLossImage.fillAmount = gaugeImage.fillAmount;
            }

            if (gaugeImage.fillAmount != gaugeCostLossImagePreviousAmount)
            {
                if (gaugeImage.fillAmount < gaugeCostLossImagePreviousAmount)
                {
                    gaugeCostLossImage.fillAmount = gaugeCostLossImagePreviousAmount;
                }

                gaugeCostLossImagePreviousAmount = gaugeImage.fillAmount;
            }

            switch (gaugeCostLossImageMode)
            {
                case LossImageMode.Constant:
                    gaugeCostLossImage.fillAmount = Mathf.MoveTowards(gaugeCostLossImage.fillAmount, gaugeImage.fillAmount, gaugeCostLossImageSpeed * deltaTime);
                    break;

                case LossImageMode.Custom1:
                    if (player == Player.Player1
                        && UFE.GetPlayer1ControlsScript().currentMove == null
                        && UFE.GetPlayer1ControlsScript().currentSubState != SubStates.Stunned
                        && UFE.GetPlayer1ControlsScript().currentSubState != SubStates.Blocking
                        && UFE.GetPlayer2ControlsScript().currentSubState != SubStates.Stunned
                        && UFE.GetPlayer2ControlsScript().currentSubState != SubStates.Blocking)
                    {
                        gaugeCostLossImage.fillAmount = Mathf.MoveTowards(gaugeCostLossImage.fillAmount, gaugeImage.fillAmount, gaugeCostLossImageSpeed * deltaTime);
                    }
                    else if (player == Player.Player2
                        && UFE.GetPlayer2ControlsScript().currentMove == null
                        && UFE.GetPlayer1ControlsScript().currentSubState != SubStates.Stunned
                        && UFE.GetPlayer1ControlsScript().currentSubState != SubStates.Blocking
                        && UFE.GetPlayer2ControlsScript().currentSubState != SubStates.Stunned
                        && UFE.GetPlayer2ControlsScript().currentSubState != SubStates.Blocking)
                    {
                        gaugeCostLossImage.fillAmount = Mathf.MoveTowards(gaugeCostLossImage.fillAmount, gaugeImage.fillAmount, gaugeCostLossImageSpeed * deltaTime);
                    }
                    break;
            }
        }

        private void SetCharacterGaugeTotalLossImage(float deltaTime)
        {
            if (gaugeImage == null
                || gaugeTotalLossImage == null)
            {
                return;
            }

            if (gaugeImage.fillAmount > gaugeTotalLossImage.fillAmount)
            {
                gaugeTotalLossImage.fillAmount = gaugeImage.fillAmount;
            }

            switch (gaugeTotalLossImageMode)
            {
                case LossImageMode.Constant:
                    gaugeTotalLossImage.fillAmount = Mathf.MoveTowards(gaugeTotalLossImage.fillAmount, gaugeImage.fillAmount, gaugeTotalLossImageSpeed * deltaTime);
                    break;

                case LossImageMode.Custom1:
                    if (player == Player.Player1
                        && UFE.GetPlayer1ControlsScript().currentMove == null
                        && UFE.GetPlayer1ControlsScript().currentSubState != SubStates.Stunned
                        && UFE.GetPlayer1ControlsScript().currentSubState != SubStates.Blocking
                        && UFE.GetPlayer2ControlsScript().currentSubState != SubStates.Stunned
                        && UFE.GetPlayer2ControlsScript().currentSubState != SubStates.Blocking)
                    {
                        gaugeTotalLossImage.fillAmount = Mathf.MoveTowards(gaugeTotalLossImage.fillAmount, gaugeImage.fillAmount, gaugeTotalLossImageSpeed * deltaTime);
                    }
                    else if (player == Player.Player2
                        && UFE.GetPlayer2ControlsScript().currentMove == null
                        && UFE.GetPlayer1ControlsScript().currentSubState != SubStates.Stunned
                        && UFE.GetPlayer1ControlsScript().currentSubState != SubStates.Blocking
                        && UFE.GetPlayer2ControlsScript().currentSubState != SubStates.Stunned
                        && UFE.GetPlayer2ControlsScript().currentSubState != SubStates.Blocking)
                    {
                        gaugeTotalLossImage.fillAmount = Mathf.MoveTowards(gaugeTotalLossImage.fillAmount, gaugeImage.fillAmount, gaugeTotalLossImageSpeed * deltaTime);
                    }
                    break;
            }
        }     
    }
}
