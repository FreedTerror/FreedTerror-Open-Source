using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class UFE2FTECharacterStunTimeImageController : MonoBehaviour
    {
        private enum Player
        {
            Player1,
            Player2
        }
        [SerializeField]
        private Player player;
        [SerializeField]
        private Image stunTimeImage;
        private float previousStunTimeImageFillAmount;
        [SerializeField]
        private Image highestStunTimeImage;
        private float highestStunTime;

        private void Update()
        {
            if (UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null)
            {
                return;
            }

            SetStunTimeImageFillAmount();
        }

        private void OnDisable()
        {
            if (stunTimeImage != null)
            {
                stunTimeImage.fillAmount = 0;
            }

            previousStunTimeImageFillAmount = 0;

            if (highestStunTimeImage != null)
            {
                highestStunTimeImage.fillAmount = 0;
            }

            highestStunTime = 0;
        }

        private void SetStunTimeImageFillAmount()
        {
            if (stunTimeImage == null)
            {
                return;
            }

            if (player == Player.Player1)
            {
                if ((float)UFE.GetPlayer1ControlsScript().stunTime > 0)
                {
                    if ((float)UFE.GetPlayer1ControlsScript().stunTime > highestStunTime)
                    {
                        highestStunTime = (float)UFE.GetPlayer1ControlsScript().stunTime;
                    }

                    stunTimeImage.fillAmount = (float)UFE.GetPlayer1ControlsScript().stunTime / highestStunTime;

                    if (highestStunTimeImage != null
                        && stunTimeImage.fillAmount > previousStunTimeImageFillAmount)
                    {
                        highestStunTimeImage.fillAmount = stunTimeImage.fillAmount;
                    }

                    previousStunTimeImageFillAmount = stunTimeImage.fillAmount;
                }
                else
                {
                    stunTimeImage.fillAmount = 0;

                    previousStunTimeImageFillAmount = 0;

                    if (highestStunTimeImage != null)
                    {
                        highestStunTimeImage.fillAmount = 0;
                    }

                    highestStunTime = 0;
                }
            }
            else if (player == Player.Player2)
            {
                if ((float)UFE.GetPlayer2ControlsScript().stunTime > 0)
                {
                    if ((float)UFE.GetPlayer2ControlsScript().stunTime > highestStunTime)
                    {
                        highestStunTime = (float)UFE.GetPlayer2ControlsScript().stunTime;
                    }

                    stunTimeImage.fillAmount = (float)UFE.GetPlayer2ControlsScript().stunTime / highestStunTime;

                    if (highestStunTimeImage != null
                        && stunTimeImage.fillAmount > previousStunTimeImageFillAmount)
                    {
                        highestStunTimeImage.fillAmount = stunTimeImage.fillAmount;
                    }

                    previousStunTimeImageFillAmount = stunTimeImage.fillAmount;
                }
                else
                {
                    stunTimeImage.fillAmount = 0;

                    previousStunTimeImageFillAmount = 0;

                    if (highestStunTimeImage != null)
                    {
                        highestStunTimeImage.fillAmount = 0;
                    }

                    highestStunTime = 0;
                } 
            }
        }
    }
}