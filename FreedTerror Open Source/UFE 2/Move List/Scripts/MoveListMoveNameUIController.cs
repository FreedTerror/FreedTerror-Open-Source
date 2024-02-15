using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace FreedTerror.UFE2
{
    public class MoveListMoveNameUIController : MonoBehaviour
    {
        [SerializeField]
        private Text moveNameText;

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
                || moveNameText == null)
            {
                return;
            }

            moveNameText.text = moveInfo.moveName;

            if (moveInfo.moveName == "")
            {
                moveNameText.gameObject.SetActive(false);
            }
        }
    }
}