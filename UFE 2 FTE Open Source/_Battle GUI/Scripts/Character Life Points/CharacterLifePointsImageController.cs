using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class CharacterLifePointsImageController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Image lifePointsImage;
        [SerializeField]
        private Image lifePointsHitLossImage;
        private enum LossImageMode
        {
            Constant,
            Custom1,
        }
        [SerializeField]
        private LossImageMode lifePointsHitLossImageMode;
        [SerializeField]
        private float lifePointsHitLossImageSpeed;
        private float lifePointsHitLossImagePreviousAmount;
        [SerializeField]
        private Image lifePointsTotalLossImage;
        [SerializeField]
        private LossImageMode lifePointsTotalLossImageMode;
        [SerializeField]
        private float lifePointsTotalLossImageSpeed;
        private bool lifePointsTotalLossImageIsHit;
        private float lifePointsTotalLossImageFillAmountOnFirstHit;

        private void Update()
        {
            float deltaTime = (float)UFE.fixedDeltaTime;

            SetCharacterLifePointsImage(UFE2FTE.GetControlsScript(player));

            SetCharacterLifePointsHitLossImage(UFE2FTE.GetControlsScript(player), deltaTime);

            SetCharacterLifePointsTotalLossImage(UFE2FTE.GetControlsScript(player), deltaTime);
        }

        private void SetCharacterLifePointsImage(ControlsScript player)
        {
            if (player == null
                || player.myInfo == null
                || lifePointsImage == null)
            {
                return;
            }

            lifePointsImage.fillAmount = (float)player.currentLifePoints / player.myInfo.lifePoints;
        }

        private void SetCharacterLifePointsHitLossImage(ControlsScript player, float deltaTime)
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

            switch (lifePointsHitLossImageMode)
            {
                case LossImageMode.Constant:
                    lifePointsHitLossImage.fillAmount = Mathf.MoveTowards(lifePointsHitLossImage.fillAmount, lifePointsImage.fillAmount, lifePointsHitLossImageSpeed * deltaTime);
                    break;

                case LossImageMode.Custom1:
                    switch (player.currentSubState)
                    {
                        case SubStates.Stunned:
                        case SubStates.Blocking:
                            break;

                        default:
                            lifePointsHitLossImage.fillAmount = Mathf.MoveTowards(lifePointsHitLossImage.fillAmount, lifePointsImage.fillAmount, lifePointsHitLossImageSpeed * deltaTime);
                            break;
                    }
                    break;
            }
        }

        private void SetCharacterLifePointsTotalLossImage(ControlsScript player, float deltaTime)
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

            switch (lifePointsTotalLossImageMode)
            {
                case LossImageMode.Constant:
                    lifePointsTotalLossImage.fillAmount = Mathf.MoveTowards(lifePointsTotalLossImage.fillAmount, lifePointsImage.fillAmount, lifePointsTotalLossImageSpeed * deltaTime);
                    break;

                case LossImageMode.Custom1:
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
                    break;
            }
        }
    }
}