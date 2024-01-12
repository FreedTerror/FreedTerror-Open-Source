using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
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

                UFE2FTE.SetInputDisplayRotation(rectTransform, UFE2FTE.instance.inputDisplayScriptableObject.GetButtonPressFromInputDisplaySprite(targetImage.sprite));

                inputDisplayText.text = UFE2FTE.instance.inputDisplayScriptableObject.GetInputDisplayStringFromSprite(targetImage.sprite);
            }
        }
    }
}
