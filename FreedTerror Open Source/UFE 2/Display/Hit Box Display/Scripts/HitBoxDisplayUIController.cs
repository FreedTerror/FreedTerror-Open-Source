using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class HitBoxDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private Text hitBoxDisplayModeText;
        private UFE2Manager.HitBoxDisplayMode previousHitBoxDisplayMode;
        [SerializeField]
        private Text hitBoxDisplayAlphaValueText;
        private int previousHitBoxDisplayAlphaValue;

        private void Start()
        {
            previousHitBoxDisplayMode = UFE2Manager.instance.hitBoxDisplayMode;

            if (hitBoxDisplayModeText != null)
            {
                hitBoxDisplayModeText.text = System.Enum.GetName(typeof(UFE2Manager.HitBoxDisplayMode), UFE2Manager.instance.hitBoxDisplayMode);
            }

            previousHitBoxDisplayAlphaValue = UFE2Manager.instance.hitBoxDisplayAlphaValue;

            if (hitBoxDisplayAlphaValueText != null)
            {
                hitBoxDisplayAlphaValueText.text = UFE2Manager.instance.hitBoxDisplayAlphaValue.ToString();
            }
        }

        private void Update()
        {
            if (previousHitBoxDisplayMode != UFE2Manager.instance.hitBoxDisplayMode)
            {
                previousHitBoxDisplayMode = UFE2Manager.instance.hitBoxDisplayMode;

                if (hitBoxDisplayModeText != null)
                {
                    hitBoxDisplayModeText.text = System.Enum.GetName(typeof(UFE2Manager.HitBoxDisplayMode), UFE2Manager.instance.hitBoxDisplayMode);
                }
            }

            if (previousHitBoxDisplayAlphaValue != UFE2Manager.instance.hitBoxDisplayAlphaValue)
            {
                previousHitBoxDisplayAlphaValue = UFE2Manager.instance.hitBoxDisplayAlphaValue;

                if (hitBoxDisplayAlphaValueText != null)
                {
                    hitBoxDisplayAlphaValueText.text = UFE2Manager.instance.hitBoxDisplayAlphaValue.ToString();
                }
            }
        }

        public void NextHitBoxDisplayMode()
        {
            UFE2Manager.instance.hitBoxDisplayMode = Utility.GetNextEnum(UFE2Manager.instance.hitBoxDisplayMode);
        }

        public void PreviousHitBoxDisplayMode()
        {
            UFE2Manager.instance.hitBoxDisplayMode = Utility.GetPreviousEnum(UFE2Manager.instance.hitBoxDisplayMode);
        }

        public void NextHitBoxDisplayAlphaValue()
        {
            if (UFE2Manager.instance.hitBoxDisplayAlphaValue == 255)
            {
                UFE2Manager.instance.hitBoxDisplayAlphaValue = 32;

                return;
            }

            UFE2Manager.instance.hitBoxDisplayAlphaValue += 32;

            if (UFE2Manager.instance.hitBoxDisplayAlphaValue > 255)
            {
                UFE2Manager.instance.hitBoxDisplayAlphaValue = 255;
            }
        }

        public void PreviousHitboxDisplayAlphaValue()
        {
            if (UFE2Manager.instance.hitBoxDisplayAlphaValue == 32)
            {
                UFE2Manager.instance.hitBoxDisplayAlphaValue = 255;

                return;
            }

            UFE2Manager.instance.hitBoxDisplayAlphaValue -= 32;

            if (UFE2Manager.instance.hitBoxDisplayAlphaValue < 32)
            {
                UFE2Manager.instance.hitBoxDisplayAlphaValue = 32;
            }
        }
    }
}
