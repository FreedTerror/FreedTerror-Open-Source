using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UFE2FTE
{
    [RequireComponent(typeof(ScrollRect))]
    public class UFE2FTEReplaySelectionScreenScrollRectEnsureVisible : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private bool useOrginalCode;
        private RectTransform scrollRectTransform;
        private RectTransform contentPanel;
        private RectTransform selectedRectTransform;
        private GameObject lastSelected;
        private Vector2 targetPos;

        private void Start()
        {
            scrollRectTransform = GetComponent<RectTransform>();

            if (contentPanel == null)
                contentPanel = GetComponent<ScrollRect>().content;

            targetPos = contentPanel.anchoredPosition;
        }

        private void Update()
        {
            if (useOrginalCode == true
                && _mouseHover == false)
            {
                Autoscroll();
            }
            else if (useOrginalCode == false)
            {
                Autoscroll();
            }
        }

        private void Autoscroll()
        {
            if (contentPanel == null)
            {
                contentPanel = GetComponent<ScrollRect>().content;
            }

            GameObject selected = EventSystem.current.currentSelectedGameObject;

            if (useOrginalCode == false
                && _mouseHover == true
                && Input.GetMouseButton(0) == true)
            {
                return;
            }
            if (useOrginalCode == false
                && Input.touchCount > 0)
            {
                return;
            }
            if (selected == null)
            {
                return;
            }
            if (selected.transform.parent != contentPanel.transform)
            {
                return;
            }
            if (selected == lastSelected)
            {
                return;
            }

            selectedRectTransform = (RectTransform)selected.transform;

            targetPos.x = contentPanel.anchoredPosition.x;

            targetPos.y = -(selectedRectTransform.localPosition.y) - (selectedRectTransform.rect.height / 2);

            targetPos.y = Mathf.Clamp(targetPos.y, 0, contentPanel.sizeDelta.y - scrollRectTransform.sizeDelta.y);

            contentPanel.anchoredPosition = targetPos;

            lastSelected = selected;
        }

        bool _mouseHover;
        public void OnPointerEnter(PointerEventData eventData)
        {
            _mouseHover = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _mouseHover = false;
        }
    }
}
