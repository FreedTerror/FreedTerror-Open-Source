using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace FreedTerror
{
    public class BackgroundImageUIController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        private EventSystem eventSystem;   
        public Image image;
        [HideInInspector]
        public bool useImageSprite;
        public Sprite imageSprite;
  
        private void OnEnable()
        {
            eventSystem = EventSystem.current;
        }

        private void Update()
        {
            if (eventSystem != null
                && eventSystem.currentSelectedGameObject != null)
            {
                BackgroundImageUIController backgroundImageController = eventSystem.currentSelectedGameObject.GetComponent<BackgroundImageUIController>();
                if (backgroundImageController != null
                    && backgroundImageController.useImageSprite == true
                    && backgroundImageController.image != null
                    && backgroundImageController.imageSprite != null)
                {
                    backgroundImageController.image.sprite = backgroundImageController.imageSprite;
                }
            }
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            if (eventSystem != null
                && eventSystem.currentSelectedGameObject != null)
            {
                BackgroundImageUIController backgroundImageController = eventSystem.currentSelectedGameObject.GetComponent<BackgroundImageUIController>();
                if (backgroundImageController != null)
                {
                    backgroundImageController.useImageSprite = false;
                }
            }

            if (image != null
                && imageSprite != null)
            {
                image.sprite = imageSprite;
            }
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            if (eventSystem != null
                && eventSystem.currentSelectedGameObject != null)
            {
                BackgroundImageUIController backgroundImageController = eventSystem.currentSelectedGameObject.GetComponent<BackgroundImageUIController>();
                if (backgroundImageController != null)
                {
                    backgroundImageController.useImageSprite = true;
                }
            }
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (eventSystem != null
                && eventSystem.currentSelectedGameObject != null)
            {
                BackgroundImageUIController backgroundImageController = eventSystem.currentSelectedGameObject.GetComponent<BackgroundImageUIController>();
                if (backgroundImageController != null)
                {
                    backgroundImageController.useImageSprite = false;
                }
            }

            if (image != null
                && imageSprite != null)
            {
                image.sprite = imageSprite;
            }
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if (eventSystem != null
                && eventSystem.currentSelectedGameObject != null)
            {
                BackgroundImageUIController backgroundImageController = eventSystem.currentSelectedGameObject.GetComponent<BackgroundImageUIController>();
                if (backgroundImageController != null)
                {
                    backgroundImageController.useImageSprite = true;
                }
            }
        }
    }
}
