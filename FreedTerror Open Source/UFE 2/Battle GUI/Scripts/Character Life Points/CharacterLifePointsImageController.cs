using UFE3D;
using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class CharacterLifePointsImageController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private Image lifePointsImage;
        [SerializeField]
        private Image lifePointsHitLossImage;
        [SerializeField]
        private float lifePointsHitLossImageSpeed;
        private float lifePointsHitLossImagePreviousAmount;
        [SerializeField]
        private Image lifePointsTotalLossImage;
        [SerializeField]
        private float lifePointsTotalLossImageSpeed;
        private bool lifePointsTotalLossImageIsHit;
        private float lifePointsTotalLossImageFillAmountOnFirstHit;

        private void Update()
        {
            float deltaTime = (float)UFE.fixedDeltaTime;

            UpdateLifePointsImage(UFE2Manager.GetControlsScript(player));

            UpdateLifePointsHitLossImage(UFE2Manager.GetControlsScript(player), deltaTime);

            UpdateLifePointsTotalLossImage(UFE2Manager.GetControlsScript(player), deltaTime);
        }

        private void UpdateLifePointsImage(ControlsScript player)
        {
            if (player == null
                || player.myInfo == null
                || lifePointsImage == null)
            {
                return;
            }

            lifePointsImage.fillAmount = (float)player.currentLifePoints / player.myInfo.lifePoints;
        }

        private void UpdateLifePointsHitLossImage(ControlsScript player, float deltaTime)
        {
            if (player == null
                || lifePointsImage == null
                || lifePointsHitLossImage == null)
            {
                return;
            }

            if (lifePointsImage.fillAmount > lifePointsHitLossImage.fillAmount)
            {
                lifePointsHitLossImage.fillAmount = lifePointsImage.fillAmount;
            }

            if (lifePointsImage.fillAmount != lifePointsHitLossImagePreviousAmount)
            {
                if (lifePointsImage.fillAmount < lifePointsHitLossImagePreviousAmount)
                {
                    lifePointsHitLossImage.fillAmount = lifePointsHitLossImagePreviousAmount;
                }

                lifePointsHitLossImagePreviousAmount = lifePointsImage.fillAmount;
            }

            switch (player.currentSubState)
            {
                case SubStates.Stunned:
                case SubStates.Blocking:
                    break;

                default:
                    lifePointsHitLossImage.fillAmount = Mathf.MoveTowards(lifePointsHitLossImage.fillAmount, lifePointsImage.fillAmount, lifePointsHitLossImageSpeed * deltaTime);
                    break;
            }
        }

        private void UpdateLifePointsTotalLossImage(ControlsScript player, float deltaTime)
        {
            if (player == null
                || lifePointsImage == null
                || lifePointsTotalLossImage == null)
            {
                return;
            }

            if (lifePointsImage.fillAmount > lifePointsTotalLossImage.fillAmount)
            {
                lifePointsTotalLossImage.fillAmount = lifePointsImage.fillAmount;
            }

            switch (player.currentSubState)
            {
                case SubStates.Stunned:
                case SubStates.Blocking:
                    if (lifePointsTotalLossImageIsHit == false)
                    {
                        lifePointsTotalLossImageIsHit = true;

                        lifePointsTotalLossImage.fillAmount = lifePointsTotalLossImageFillAmountOnFirstHit;
                    }
                    break;

                default:
                    lifePointsTotalLossImageIsHit = false;

                    lifePointsTotalLossImageFillAmountOnFirstHit = lifePointsImage.fillAmount;

                    lifePointsTotalLossImage.fillAmount = Mathf.MoveTowards(lifePointsTotalLossImage.fillAmount, lifePointsImage.fillAmount, lifePointsTotalLossImageSpeed * deltaTime);
                    break;
            }
        }
    }
}