using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class HitBoxDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private HitBoxDisplayScriptableObject hitBoxDisplayScriptableObject;
        [SerializeField]
        private Text hitBoxDisplayModeText;
        private HitBoxDisplayController.HitBoxDisplayMode previousHitBoxDisplayMode;
        [SerializeField]
        private Text hitBoxDisplayAlphaValueText;
        private int previousHitBoxDisplayAlphaValue;

        private void Start()
        {
            if (hitBoxDisplayScriptableObject != null)
            {
                previousHitBoxDisplayMode = hitBoxDisplayScriptableObject.hitBoxDisplayMode;

                if (hitBoxDisplayModeText != null)
                {
                    hitBoxDisplayModeText.text = Utility.AddSpacesBeforeCapitalLetters(System.Enum.GetName(typeof(HitBoxDisplayController.HitBoxDisplayMode), hitBoxDisplayScriptableObject.hitBoxDisplayMode));
                }

                previousHitBoxDisplayAlphaValue = hitBoxDisplayScriptableObject.hitBoxDisplayAlphaValue;

                if (hitBoxDisplayAlphaValueText != null)
                {
                    hitBoxDisplayAlphaValueText.text = hitBoxDisplayScriptableObject.hitBoxDisplayAlphaValue.ToString();
                }
            }
        }

        private void Update()
        {
            if (hitBoxDisplayScriptableObject != null)
            {
                if (previousHitBoxDisplayMode != hitBoxDisplayScriptableObject.hitBoxDisplayMode)
                {
                    previousHitBoxDisplayMode = hitBoxDisplayScriptableObject.hitBoxDisplayMode;

                    if (hitBoxDisplayModeText != null)
                    {
                        hitBoxDisplayModeText.text = System.Enum.GetName(typeof(HitBoxDisplayController.HitBoxDisplayMode), hitBoxDisplayScriptableObject.hitBoxDisplayMode);
                    }
                }

                if (previousHitBoxDisplayAlphaValue != hitBoxDisplayScriptableObject.hitBoxDisplayAlphaValue)
                {
                    previousHitBoxDisplayAlphaValue = hitBoxDisplayScriptableObject.hitBoxDisplayAlphaValue;

                    if (hitBoxDisplayAlphaValueText != null)
                    {
                        hitBoxDisplayAlphaValueText.text = hitBoxDisplayScriptableObject.hitBoxDisplayAlphaValue.ToString();
                    }
                }
            }
        }

        public void NextHitBoxDisplayMode()
        {
            if (hitBoxDisplayScriptableObject == null)
            {
                return;
            }

            hitBoxDisplayScriptableObject.hitBoxDisplayMode = Utility.GetNextEnum(hitBoxDisplayScriptableObject.hitBoxDisplayMode);
        }

        public void PreviousHitBoxDisplayMode()
        {
            if (hitBoxDisplayScriptableObject == null)
            {
                return;
            }

            hitBoxDisplayScriptableObject.hitBoxDisplayMode = Utility.GetPreviousEnum(hitBoxDisplayScriptableObject.hitBoxDisplayMode);
        }

        public void NextHitBoxDisplayAlphaValue()
        {
            if (hitBoxDisplayScriptableObject == null)
            {
                return;
            }

            if (hitBoxDisplayScriptableObject.hitBoxDisplayAlphaValue == 255)
            {
                hitBoxDisplayScriptableObject.hitBoxDisplayAlphaValue = 32;

                return;
            }

            hitBoxDisplayScriptableObject.hitBoxDisplayAlphaValue += 32;

            if (hitBoxDisplayScriptableObject.hitBoxDisplayAlphaValue > 255)
            {
                hitBoxDisplayScriptableObject.hitBoxDisplayAlphaValue = 255;
            }
        }

        public void PreviousHitboxDisplayAlphaValue()
        {
            if (hitBoxDisplayScriptableObject == null)
            {
                return;
            }

            if (hitBoxDisplayScriptableObject.hitBoxDisplayAlphaValue == 32)
            {
                hitBoxDisplayScriptableObject.hitBoxDisplayAlphaValue = 255;

                return;
            }

            hitBoxDisplayScriptableObject.hitBoxDisplayAlphaValue -= 32;

            if (hitBoxDisplayScriptableObject.hitBoxDisplayAlphaValue < 32)
            {
                hitBoxDisplayScriptableObject.hitBoxDisplayAlphaValue = 32;
            }
        }
    }
}
