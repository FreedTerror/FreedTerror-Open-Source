using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FreedTerror
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
            if (EventSystem.current != null
                && EventSystem.current.currentSelectedGameObject == myGameObject
                && autoSelectSelectable != null)
            {
                autoSelectSelectable.Select();
            }
        }
    }
}