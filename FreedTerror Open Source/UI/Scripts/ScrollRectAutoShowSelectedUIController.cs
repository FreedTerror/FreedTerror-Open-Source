using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FreedTerror
{
    public class ScrollRectAutoShowSelectedUIController : MonoBehaviour
    {
        [SerializeField]
        private ScrollRect scrollRect;

        private void Update()
        {
            if (EventSystem.current != null
                && EventSystem.current.currentSelectedGameObject != null)
            {
                Utility.KeepChildInScrollViewPort(scrollRect, (RectTransform)EventSystem.current.currentSelectedGameObject.transform, new Vector2(0, 0));
            }
        }
    }
}