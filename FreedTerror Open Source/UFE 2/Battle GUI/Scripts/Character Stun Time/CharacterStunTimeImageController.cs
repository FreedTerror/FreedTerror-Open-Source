using UnityEngine;
using UnityEngine.UI;
using FPLibrary;

namespace FreedTerror.UFE2
{
    public class CharacterStunTimeImageController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
        [SerializeField]
        private Image stunTimeImage;
        private float previousStunTimeImageFillAmount;
        [SerializeField]
        private Image highestStunTimeImage;
        private Fix64 highestStunTime;

        private void Update()
        {
            SetStunTimeImageFillAmount(UFE2Manager.GetControlsScript(player));
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

        private void SetStunTimeImageFillAmount(ControlsScript player)
        {
            if (player == null
                || player.opControlsScript == null
                || stunTimeImage == null)
            {
                return;
            }

            if (player.opControlsScript.stunTime > 0)
            {
                if (player.opControlsScript.stunTime > highestStunTime)
                {
                    highestStunTime = player.opControlsScript.stunTime;
                }

                stunTimeImage.fillAmount = (float)(player.opControlsScript.stunTime / highestStunTime);

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