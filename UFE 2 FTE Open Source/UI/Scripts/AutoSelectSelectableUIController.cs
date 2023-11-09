using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UFE2FTE
{
    public class AutoSelectSelectableUIController : MonoBehaviour
    {
        private GameObject myGameObject;
        [SerializeField]
        private Selectable autoSelectSelectable;

        private void Awake()
        {
            myGameObject = gameObject;
        }

        private void Update()
        {
            AutoSelectSelectableOnThisSelectedGameObject();
        }

        private void AutoSelectSelectableOnThisSelectedGameObject()
        {
            if (EventSystem.current != null
                && EventSystem.current.currentSelectedGameObject == myGameObject)
            {
                SelectSelectable(autoSelectSelectable);
            }
        }

        private static void SelectSelectable(Selectable selectable)
        {
            if (selectable == null)
            {
                return;
            }

            selectable.Select();
        }
    }
}