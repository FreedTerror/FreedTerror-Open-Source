using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class InputDisplayButtonIconController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform rectTransform;
        [SerializeField]
        private Text inputDisplayText;
        private RectTransform targetRectTransform;
        private Image targetImage;

        private void Start()
        {
            targetRectTransform = GetComponentInParent<RectTransform>();
            targetImage = targetRectTransform.GetComponentInParent<Image>();
        }

        private void Update()
        {
            if (rectTransform == null
                || targetRectTransform == null
                || targetImage == null)
            {
                return;          
            }

            rectTransform.sizeDelta = targetImage.rectTransform.sizeDelta;

            if (inputDisplayText != null)
            {
                targetImage.enabled = false;

                UFE2Manager.SetInputDisplayRotation(rectTransform, UFE2Manager.instance.inputDisplayScriptableObject.GetButtonPressFromInputDisplaySprite(targetImage.sprite));

                inputDisplayText.text = UFE2Manager.instance.inputDisplayScriptableObject.GetInputDisplayStringFromSprite(targetImage.sprite);
            }
        }
    }
}
