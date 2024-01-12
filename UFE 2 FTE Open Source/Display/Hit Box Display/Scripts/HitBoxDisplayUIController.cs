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
            UFE2FTE.SetTextMessage(hitBoxDisplayModeText, UFE2FTE.instance.GetStringFromEnum(UFE2FTE.instance.hitBoxDisplayMode));

            UFE2FTE.SetTextMessage(hitBoxDisplayAlphaValueText, UFE2FTE.GetNormalStringNumber(UFE2FTE.instance.hitBoxDisplayAlphaValue));
        }

        public void NextHitBoxDisplayMode()
        {
            UFE2FTE.instance.hitBoxDisplayMode = UFE2FTE.instance.GetNextEnum(UFE2FTE.instance.hitBoxDisplayMode);
        }

        public void PreviousHitBoxDisplayMode()
        {
            UFE2FTE.instance.hitBoxDisplayMode = UFE2FTE.instance.GetPreviousEnum(UFE2FTE.instance.hitBoxDisplayMode);
        }

        public void NextHitBoxDisplayAlphaValue()
        {
            if (UFE2FTE.instance.hitBoxDisplayAlphaValue == 255)
            {
                UFE2FTE.instance.hitBoxDisplayAlphaValue = 32;

                return;
            }

            UFE2FTE.instance.hitBoxDisplayAlphaValue += 32;

            if (UFE2FTE.instance.hitBoxDisplayAlphaValue > 255)
            {
                UFE2FTE.instance.hitBoxDisplayAlphaValue = 255;
            }
        }

        public void PreviousHitboxDisplayAlphaValue()
        {
            if (UFE2FTE.instance.hitBoxDisplayAlphaValue == 32)
            {
                UFE2FTE.instance.hitBoxDisplayAlphaValue = 255;

                return;
            }

            UFE2FTE.instance.hitBoxDisplayAlphaValue -= 32;

            if (UFE2FTE.instance.hitBoxDisplayAlphaValue < 32)
            {
                UFE2FTE.instance.hitBoxDisplayAlphaValue = 32;
            }
        }
    }
}
