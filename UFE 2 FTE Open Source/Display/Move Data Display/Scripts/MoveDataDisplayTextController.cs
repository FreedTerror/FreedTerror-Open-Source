using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class MoveDataDisplayController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;
        [SerializeField]
        private Text moveNameText;
        [SerializeField]
        private Text startupFramesText;
        [SerializeField]
        private Text activeFramesText;
        [SerializeField]
        private Text recoveryFramesText;
        [SerializeField]
        private Text totalFramesText;
        [SerializeField]
        private GameObject armorActiveFramesGameObject;
        [SerializeField]
        private Text armorActiveFramesBeginText;
        [SerializeField]
        private Text armorActiveFramesText;
        [SerializeField]
        private Text armorActiveFramesEndsText;
        [SerializeField]
        private GameObject armorHitAbsorptionGameObject;
        [SerializeField]
        private Text armorHitAbsorptionText;
        [SerializeField]
        private GameObject armorDamageAbsorptionGameObject;
        [SerializeField]
        private Text armorDamageAbsorptionText;

        private void OnEnable()
        {
            UFE.OnMove += OnMove;
        }

        private void Start()
        {
            SetAllGameObjectsActive(false);
        }

        private void OnDisable()
        {
            UFE.OnMove -= OnMove;
        }

        private void OnMove(MoveInfo move, ControlsScript player)
        {
            DisplayMoveData(player, move);
        }

        private void DisplayMoveData(ControlsScript player, MoveInfo moveInfo)
        {
            if (player == null
                || player != UFE2FTE.GetControlsScript(this.player)
                ||moveInfo == null)
            {
                return;
            }

            UFE2FTE.SetTextMessage(moveNameText, moveInfo.moveName);

            UFE2FTE.SetTextMessage(startupFramesText, UFE2FTE.GetNormalStringNumber(moveInfo.startUpFrames));

            UFE2FTE.SetTextMessage(activeFramesText, UFE2FTE.GetNormalStringNumber(moveInfo.activeFrames));

            UFE2FTE.SetTextMessage(recoveryFramesText, UFE2FTE.GetNormalStringNumber(moveInfo.recoveryFrames));

            UFE2FTE.SetTextMessage(totalFramesText, UFE2FTE.GetNormalStringNumber(moveInfo.totalFrames));

            if (moveInfo.armorOptions.hitAbsorption <= 0)
            {
                UFE2FTE.SetGameObjectActive(armorActiveFramesGameObject, false);
                UFE2FTE.SetGameObjectActive(armorHitAbsorptionGameObject, false);
                UFE2FTE.SetGameObjectActive(armorDamageAbsorptionGameObject, false);
            }
            else
            {
                UFE2FTE.SetGameObjectActive(armorActiveFramesGameObject, true);
                UFE2FTE.SetGameObjectActive(armorHitAbsorptionGameObject, true);
                UFE2FTE.SetGameObjectActive(armorDamageAbsorptionGameObject, true);

                UFE2FTE.SetTextMessage(armorActiveFramesBeginText, UFE2FTE.GetNormalStringNumber(moveInfo.armorOptions.activeFramesBegin));
                UFE2FTE.SetTextMessage(armorActiveFramesText, UFE2FTE.GetNormalStringNumber(moveInfo.armorOptions.activeFramesEnds - moveInfo.armorOptions.activeFramesBegin));
                UFE2FTE.SetTextMessage(armorActiveFramesEndsText, UFE2FTE.GetNormalStringNumber(moveInfo.armorOptions.activeFramesEnds));
                UFE2FTE.SetTextMessage(armorHitAbsorptionText, UFE2FTE.GetNormalStringNumber(moveInfo.armorOptions.hitAbsorption));
                UFE2FTE.SetTextMessage(armorDamageAbsorptionText, UFE2FTE.GetNormalPercentStringNumber(moveInfo.armorOptions.damageAbsorption));
            }
        }

        private void SetAllGameObjectsActive(bool active)
        {
            UFE2FTE.SetGameObjectActive(armorActiveFramesGameObject, active);
            UFE2FTE.SetGameObjectActive(armorHitAbsorptionGameObject, active);
            UFE2FTE.SetGameObjectActive(armorDamageAbsorptionGameObject, active);
        }
    }
}
