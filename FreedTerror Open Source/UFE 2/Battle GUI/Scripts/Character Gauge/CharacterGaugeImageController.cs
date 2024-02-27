using UFE3D;
using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class CharacterGaugeImageController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private GaugeId gaugeId;
        [SerializeField]
        private Image gaugeImage;
        [SerializeField]
        private Image gaugeCostLossImage;
        [SerializeField]
        private float gaugeCostLossImageSpeed;
        private float gaugeCostLossImagePreviousAmount;
        [SerializeField]
        private Image gaugeTotalLossImage;
        [SerializeField]
        private float gaugeTotalLossImageSpeed;

        private void Update()
        {
            float deltaTime = (float)UFE.fixedDeltaTime;

            UpdateGaugeImage(UFE2Manager.GetControlsScript(player));

            UpdateGaugeCostLossImage(UFE2Manager.GetControlsScript(player), deltaTime);

            UpdateGaugeTotalLossImage(UFE2Manager.GetControlsScript(player), deltaTime);
        }

        private void UpdateGaugeImage(ControlsScript player)
        {
            if (player == null
                || player.myInfo == null
                || gaugeImage == null)
            {
                return;
            }

            gaugeImage.fillAmount = (float)player.currentGaugesPoints[(int)gaugeId] / player.myInfo.maxGaugePoints;
        }

        private void UpdateGaugeCostLossImage(ControlsScript player, float deltaTime)
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
        }

        private void UpdateGaugeTotalLossImage(ControlsScript player, float deltaTime)
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
        }     
    }
}
