using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class MoveListMoveDescriptionUIController : MonoBehaviour
    {
        [SerializeField]
        private Text moveDescriptionText;

        private void Awake()
        {
            MoveListPopulateUIController.OnPopulateEvent += OnPopulateEvent;
        }

        private void OnDestroy()
        {
            MoveListPopulateUIController.OnPopulateEvent -= OnPopulateEvent;
        }

        private void OnPopulateEvent(MoveInfo moveInfo)
        {
            PopulateWithText(moveInfo);

            MoveListPopulateUIController.OnPopulateEvent -= OnPopulateEvent;
        }

        private void PopulateWithText(MoveInfo moveInfo)
        {
            if (moveInfo == null
                || moveDescriptionText == null)
            {
                return;
            }

            moveDescriptionText.text = moveInfo.description;

            if (moveInfo.description == "")
            {
                moveDescriptionText.gameObject.SetActive(false);
            }
        }
    }
}