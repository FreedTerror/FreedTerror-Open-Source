using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class HitBoxDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private Text hitBoxDisplayModeText;
        [SerializeField]
        private Text hitBoxDisplayAlphaValueText;

        private void Update()
        {
            UFE2FTE.SetTextMessage(hitBoxDisplayModeText, UFE2FTE.Instance.GetStringFromEnum(UFE2FTE.Instance.hitBoxDisplayMode));

            UFE2FTE.SetTextMessage(hitBoxDisplayAlphaValueText, UFE2FTE.languageOptions.GetNormalNumber(UFE2FTE.Instance.hitBoxDisplayAlphaValue));
        }

        public void NextHitBoxDisplayMode()
        {
            UFE2FTE.Instance.hitBoxDisplayMode = UFE2FTE.Instance.GetNextEnum(UFE2FTE.Instance.hitBoxDisplayMode);
        }

        public void PreviousHitBoxDisplayMode()
        {
            UFE2FTE.Instance.hitBoxDisplayMode = UFE2FTE.Instance.GetPreviousEnum(UFE2FTE.Instance.hitBoxDisplayMode);
        }

        public void NextHitBoxDisplayAlphaValue()
        {
            if (UFE2FTE.Instance.hitBoxDisplayAlphaValue == 255)
            {
                UFE2FTE.Instance.hitBoxDisplayAlphaValue = 32;

                return;
            }

            UFE2FTE.Instance.hitBoxDisplayAlphaValue += 32;

            if (UFE2FTE.Instance.hitBoxDisplayAlphaValue > 255)
            {
                UFE2FTE.Instance.hitBoxDisplayAlphaValue = 255;
            }
        }

        public void PreviousHitboxDisplayAlphaValue()
        {
            if (UFE2FTE.Instance.hitBoxDisplayAlphaValue == 32)
            {
                UFE2FTE.Instance.hitBoxDisplayAlphaValue = 255;

                return;
            }

            UFE2FTE.Instance.hitBoxDisplayAlphaValue -= 32;

            if (UFE2FTE.Instance.hitBoxDisplayAlphaValue < 32)
            {
                UFE2FTE.Instance.hitBoxDisplayAlphaValue = 32;
            }
        }
    }
}
