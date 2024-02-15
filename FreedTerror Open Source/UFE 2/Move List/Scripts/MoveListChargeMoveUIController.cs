using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace FreedTerror.UFE2
{
    public class MoveListChargeMoveUIController : MonoBehaviour
    {
        [SerializeField]
        private Text chargeTimingText;
        [SerializeField]
        private bool useDefaultInputs = true;
        [SerializeField]
        private bool useAlternativeInputs;
   
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
            if (useDefaultInputs == true)
            {
                PopulateWithText(moveInfo.defaultInputs);
            }

            if (useAlternativeInputs == true)
            {
                PopulateWithText(moveInfo.altInputs);
            }

            MoveListPopulateUIController.OnPopulateEvent -= OnPopulateEvent;
        }

        private void PopulateWithText(MoveInputs moveInputs)
        {
            if (chargeTimingText == null
                || moveInputs == null)
            {
                return;
            }

            chargeTimingText.text = "Charge Timing" + " " + moveInputs._chargeTiming + " " + "Seconds";
            if (moveInputs.chargeMove == false)
            {
                chargeTimingText.gameObject.SetActive(false);
            }
        }
    }
}