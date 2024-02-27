using UFE3D;
using UnityEngine;
using UnityEngine.UI;

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

        private void OnDisable()
        {
            UFE.OnMove -= OnMove;
        }

        private void OnMove(MoveInfo move, ControlsScript player)
        {
            UpdateMoveDataDisplay(player, move);
        }

        private void UpdateMoveDataDisplay(ControlsScript player, MoveInfo moveInfo)
        {
            if (player == null
                || player != UFE2Manager.GetControlsScript(this.player)
                || moveInfo == null)
            {
                return;
            }

            if (moveNameText != null)
            {
                moveNameText.text = moveInfo.moveName;
            }

            if (startupFramesText != null)
            {
                startupFramesText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber(moveInfo.activeFrames);
            }

            if (activeFramesText != null)
            {
                activeFramesText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber(moveInfo.activeFrames);
            }

            if (recoveryFramesText != null)
            {
                recoveryFramesText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber(moveInfo.recoveryFrames);
            }

            if (totalFramesText != null)
            {
                totalFramesText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber(moveInfo.totalFrames);
            }

            if (moveInfo.armorOptions.hitAbsorption <= 0)
            {
                if (armorActiveFramesGameObject != null)
                {
                    armorActiveFramesGameObject.SetActive(false);
                }

                if (armorHitAbsorptionGameObject != null)
                {
                    armorHitAbsorptionGameObject.SetActive(false);
                }

                if (armorDamageAbsorptionGameObject != null)
                {
                    armorDamageAbsorptionGameObject.SetActive(false);
                }
            }
            else
            {
                if (armorActiveFramesBeginText != null)
                {
                    armorActiveFramesBeginText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber(moveInfo.armorOptions.activeFramesBegin);
                }

                if (armorActiveFramesText != null)
                {
                    armorActiveFramesText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber(moveInfo.armorOptions.activeFramesEnds - moveInfo.armorOptions.activeFramesBegin);
                }

                if (armorActiveFramesEndsText != null)
                {
                    armorActiveFramesEndsText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber(moveInfo.armorOptions.activeFramesEnds);
                }

                if (armorHitAbsorptionText != null)
                {
                    armorHitAbsorptionText.text = UFE2Manager.instance.cachedStringData.GetPositiveStringNumber(moveInfo.armorOptions.hitAbsorption);
                }

                if (armorDamageAbsorptionText != null)
                {
                    armorDamageAbsorptionText.text = UFE2Manager.instance.cachedStringData.GetPositivePercentStringNumber(moveInfo.armorOptions.damageAbsorption);
                }

                if (armorActiveFramesGameObject != null)
                {
                    armorActiveFramesGameObject.SetActive(true);
                }

                if (armorHitAbsorptionGameObject != null)
                {
                    armorHitAbsorptionGameObject.SetActive(true);
                }

                if (armorDamageAbsorptionGameObject != null)
                {
                    armorDamageAbsorptionGameObject.SetActive(true);
                }
            }
        }
    }
}
