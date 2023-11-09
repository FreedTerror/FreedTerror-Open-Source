using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class CharacterGaugeImageController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
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
            float deltaTime = (float)UFE.fixedDeltaTime;

            SetCharacterGaugeImage(UFE2FTE.GetControlsScript(player));

            SetCharacterGaugeCostLossImage(UFE2FTE.GetControlsScript(player), deltaTime);

            SetCharacterGaugeTotalLossImage(UFE2FTE.GetControlsScript(player), deltaTime);
        }

        private void SetCharacterGaugeImage(ControlsScript player)
        {
            if (player == null
                || player.myInfo == null
                || gaugeImage == null)
            {
                return;
            }

            gaugeImage.fillAmount = (float)player.currentGaugesPoints[(int)gaugeId] / player.myInfo.maxGaugePoints;
        }

        private void SetCharacterGaugeCostLossImage(ControlsScript player, float deltaTime)
        {
            if (player == null
                || gaugeImage == null
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
                    if (player.opControlsScript != null)
                    {
                        if (player.currentMove == null
                            && player.currentSubState != SubStates.Stunned
                            && player.currentSubState != SubStates.Blocking
                            && player.opControlsScript.currentSubState != SubStates.Stunned
                            && player.opControlsScript.currentSubState != SubStates.Blocking)
                        {
                            gaugeCostLossImage.fillAmount = Mathf.MoveTowards(gaugeCostLossImage.fillAmount, gaugeImage.fillAmount, gaugeCostLossImageSpeed * deltaTime);
                        }
                    }
                    break;
            }
        }

        private void SetCharacterGaugeTotalLossImage(ControlsScript player, float deltaTime)
        {
            if (player == null
                || gaugeImage == null
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
                    if (player.opControlsScript != null)
                    {
                        if (player.currentMove == null
                            && player.currentSubState != SubStates.Stunned
                            && player.currentSubState != SubStates.Blocking
                            && player.opControlsScript.currentSubState != SubStates.Stunned
                            && player.opControlsScript.currentSubState != SubStates.Blocking)
                        {
                            gaugeTotalLossImage.fillAmount = Mathf.MoveTowards(gaugeTotalLossImage.fillAmount, gaugeImage.fillAmount, gaugeTotalLossImageSpeed * deltaTime);
                        }
                    }
                    break;
            }
        }     
    }
}
