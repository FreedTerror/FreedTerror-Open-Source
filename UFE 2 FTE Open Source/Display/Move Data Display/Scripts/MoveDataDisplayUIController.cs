using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class MoveDataDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private Text moveDataDisplayToggleText;

        private void Update()
        {
            UFE2FTE.SetTextMessage(moveDataDisplayToggleText, UFE2FTE.GetStringFromBool(UFE2FTE.instance.displayMoveData));
        }

        public void ToggleMoveDataDisplay()
        {
            UFE2FTE.instance.displayMoveData = UFE2FTE.ToggleBool(UFE2FTE.instance.displayMoveData);
        }
    }
}
