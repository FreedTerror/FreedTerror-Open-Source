using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTECharacterLifePointsImageController : MonoBehaviour
    {
        private enum Player
        {
            Player1,
            Player2
        }
        [SerializeField]
        private Player player;
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
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                return;
            }

            float deltaTime = (float)UFE.fixedDeltaTime;

            SetCharacterLifePointsImage(deltaTime);

            SetCharacterLifePointsHitLossImage(deltaTime);

            SetCharacterLifePointsTotalLossImage(deltaTime);
        }

        private void SetCharacterLifePointsImage(float deltaTime)
        {
            if (lifePointsImage == null)
            {
                return;
            }
        
            if (player == Player.Player1)
            {
                lifePointsImage.fillAmount = (float)UFE.GetPlayer1ControlsScript().currentLifePoints / UFE.GetPlayer1ControlsScript().myInfo.lifePoints;
            }
            else if (player == Player.Player2)
            {
                lifePointsImage.fillAmount = (float)UFE.GetPlayer2ControlsScript().currentLifePoints / UFE.GetPlayer2ControlsScript().myInfo.lifePoints;
            }
        }

        private void SetCharacterLifePointsHitLossImage(float deltaTime)
        {
            if (lifePointsImage == null
                && lifePointsHitLossImage == null)
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
                    if (player == Player.Player1)
                    {
                        switch (UFE.GetPlayer1ControlsScript().currentSubState)
                        {
                            case SubStates.Stunned:
                            case SubStates.Blocking:
                                break;

                            default:
                                lifePointsHitLossImage.fillAmount = Mathf.MoveTowards(lifePointsHitLossImage.fillAmount, lifePointsImage.fillAmount, lifePointsHitLossImageSpeed * deltaTime);
                                break;
                        }
                    }
                    else if (player == Player.Player2)
                    {
                        switch (UFE.GetPlayer2ControlsScript().currentSubState)
                        {
                            case SubStates.Stunned:
                            case SubStates.Blocking:
                                break;

                            default:
                                lifePointsHitLossImage.fillAmount = Mathf.MoveTowards(lifePointsHitLossImage.fillAmount, lifePointsImage.fillAmount, lifePointsHitLossImageSpeed * deltaTime);
                                break;
                        }
                    }
                    break;
            }
        }

        private void SetCharacterLifePointsTotalLossImage(float deltaTime)
        {
            if (lifePointsImage == null
                && lifePointsTotalLossImage == null)
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
                    if (player == Player.Player1)
                    {
                        switch (UFE.GetPlayer1ControlsScript().currentSubState)
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
                    else if (player == Player.Player2)
                    {
                        switch (UFE.GetPlayer2ControlsScript().currentSubState)
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
                    break;
            }
        }
    }
}