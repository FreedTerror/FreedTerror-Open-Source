using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class MoveDataDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private Text moveDataDisplayText;
        private bool previousMoveDataDisplay;

        private void Start()
        {
            previousMoveDataDisplay = UFE2Manager.instance.displayMoveData;

            if (moveDataDisplayText != null)
            {
                moveDataDisplayText.text = Utility.GetStringFromBool(UFE2Manager.instance.displayMoveData);
            }
        }

        private void Update()
        {
            if (previousMoveDataDisplay != UFE2Manager.instance.displayMoveData)
            {
                previousMoveDataDisplay = UFE2Manager.instance.displayMoveData;

                if (moveDataDisplayText != null)
                {
                    moveDataDisplayText.text = Utility.GetStringFromBool(UFE2Manager.instance.displayMoveData);
                }
            }
        }

        public void ToggleMoveDataDisplay()
        {
            UFE2Manager.instance.displayMoveData = !UFE2Manager.instance.displayMoveData;
        }
    }
}
