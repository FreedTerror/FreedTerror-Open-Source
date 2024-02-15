using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace FreedTerror.UFE2
{
    public class MoveDataDisplayController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;
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
                || player != UFE2Manager.GetControlsScript(this.player)
                ||moveInfo == null)
            {
                return;
            }

            UFE2Manager.SetTextMessage(moveNameText, moveInfo.moveName);

            UFE2Manager.SetTextMessage(startupFramesText, UFE2Manager.GetNormalStringNumber(moveInfo.startUpFrames));

            UFE2Manager.SetTextMessage(activeFramesText, UFE2Manager.GetNormalStringNumber(moveInfo.activeFrames));

            UFE2Manager.SetTextMessage(recoveryFramesText, UFE2Manager.GetNormalStringNumber(moveInfo.recoveryFrames));

            UFE2Manager.SetTextMessage(totalFramesText, UFE2Manager.GetNormalStringNumber(moveInfo.totalFrames));

            if (moveInfo.armorOptions.hitAbsorption <= 0)
            {
                UFE2Manager.SetGameObjectActive(armorActiveFramesGameObject, false);
                UFE2Manager.SetGameObjectActive(armorHitAbsorptionGameObject, false);
                UFE2Manager.SetGameObjectActive(armorDamageAbsorptionGameObject, false);
            }
            else
            {
                UFE2Manager.SetGameObjectActive(armorActiveFramesGameObject, true);
                UFE2Manager.SetGameObjectActive(armorHitAbsorptionGameObject, true);
                UFE2Manager.SetGameObjectActive(armorDamageAbsorptionGameObject, true);

                UFE2Manager.SetTextMessage(armorActiveFramesBeginText, UFE2Manager.GetNormalStringNumber(moveInfo.armorOptions.activeFramesBegin));
                UFE2Manager.SetTextMessage(armorActiveFramesText, UFE2Manager.GetNormalStringNumber(moveInfo.armorOptions.activeFramesEnds - moveInfo.armorOptions.activeFramesBegin));
                UFE2Manager.SetTextMessage(armorActiveFramesEndsText, UFE2Manager.GetNormalStringNumber(moveInfo.armorOptions.activeFramesEnds));
                UFE2Manager.SetTextMessage(armorHitAbsorptionText, UFE2Manager.GetNormalStringNumber(moveInfo.armorOptions.hitAbsorption));
                UFE2Manager.SetTextMessage(armorDamageAbsorptionText, UFE2Manager.GetNormalPercentStringNumber(moveInfo.armorOptions.damageAbsorption));
            }
        }

        private void SetAllGameObjectsActive(bool active)
        {
            UFE2Manager.SetGameObjectActive(armorActiveFramesGameObject, active);
            UFE2Manager.SetGameObjectActive(armorHitAbsorptionGameObject, active);
            UFE2Manager.SetGameObjectActive(armorDamageAbsorptionGameObject, active);
        }
    }
}
