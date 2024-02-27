using FPLibrary;
using UFE3D;
using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class HitDataDisplayController : MonoBehaviour
    {
        [SerializeField]
        private UFE2Manager.Player player;

        [SerializeField]
        private Text hitTypeText;

        [SerializeField]
        private Text damageOnHitText;
        [SerializeField]
        private Text minDamageOnHitText;
        [SerializeField]
        private Text damageOnBlockText;
        [SerializeField]
        private Text frameAdvantageOnHitText;
        [SerializeField]
        private Text frameAdvantageOnBlockText;

        [SerializeField]
        private GameObject armorBreakerGameObject;
        [SerializeField]
        private GameObject unblockableGameObject;   
        [SerializeField]
        private GameObject forceStandGameObject;
        [SerializeField]
        private GameObject otgGameObject;
        [SerializeField]
        private GameObject groundBounceGameObject;
        [SerializeField]
        private GameObject wallBounceGameObject;
        [SerializeField]
        private GameObject techableThrowGameObject;
        [SerializeField]
        private GameObject untechableThrowGameObject;

        private void OnEnable()
        {
            UFE.OnHit += OnHit;
            UFE.OnBlock += OnBlock;
            UFE.OnBlock += OnParry;
        }

        private void OnDisable()
        {
            UFE.OnHit -= OnHit;
            UFE.OnBlock -= OnBlock;
            UFE.OnBlock -= OnParry;
        }

        private void OnHit(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            UpdateHitDisplayData(player, hitInfo);
        }

        private void OnBlock(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            UpdateHitDisplayData(player, hitInfo);
        }

        private void OnParry(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            UpdateHitDisplayData(player, hitInfo);
        }

        private void UpdateHitDisplayData(ControlsScript player, Hit hit)
        {
            if (player == null
                || player != UFE2Manager.GetControlsScript(this.player)
                || hit == null)
            {
                return;
            }

            if (hitTypeText != null)
            {
                if (hit.hitConfirmType == HitConfirmType.Hit)
                {
                    hitTypeText.text = System.Enum.GetName(typeof(HitType), hit.hitConfirmType);
                }
                else if (hit.hitConfirmType == HitConfirmType.Throw)
                {
                    hitTypeText.text = System.Enum.GetName(typeof(HitConfirmType), hit.hitConfirmType);
                }
            }

            if (damageOnHitText != null)
            {
                damageOnHitText.text = GetStringFromDamageType(hit.damageType, hit._damageOnHit);
            }

            if (minDamageOnHitText != null)
            {
                minDamageOnHitText.text = GetStringFromDamageType(hit.damageType, hit._minDamageOnHit);
            }

            if (damageOnBlockText != null)
            {
                damageOnBlockText.text = GetStringFromDamageType(hit.damageType, hit._damageOnBlock);
            }

            if (frameAdvantageOnHitText != null)
            {
                frameAdvantageOnHitText.text = GetStringFromHitStunType(hit.hitStunType, GetHitStunValueFromHitOnHit(hit));
            }

            if (frameAdvantageOnBlockText != null)
            {
                frameAdvantageOnBlockText.text = GetStringFromHitStunType(hit.hitStunType, GetHitStunValueFromHitOnBlock(hit));
            }

            if (hit.hitConfirmType == HitConfirmType.Hit)
            {
                if (armorBreakerGameObject != null)
                {
                    armorBreakerGameObject.SetActive(hit.armorBreaker);
                }

                if (unblockableGameObject != null)
                {
                    unblockableGameObject.SetActive(hit.unblockable);
                }

                if (forceStandGameObject != null)
                {
                    forceStandGameObject.SetActive(hit.forceStand);
                }

                if (otgGameObject != null)
                {
                    otgGameObject.SetActive(hit.downHit);
                }

                if (groundBounceGameObject != null)
                {
                    groundBounceGameObject.SetActive(hit.groundBounce);
                }

                if (wallBounceGameObject != null)
                {
                    wallBounceGameObject.SetActive(hit.wallBounce);
                }

                if (techableThrowGameObject != null)
                {
                    techableThrowGameObject.SetActive(false);
                }

                if (untechableThrowGameObject != null)
                {
                    untechableThrowGameObject.SetActive(false);
                }
            }
            else if (hit.hitConfirmType == HitConfirmType.Throw)
            {
                if (armorBreakerGameObject != null)
                {
                    armorBreakerGameObject.SetActive(false);
                }

                if (unblockableGameObject != null)
                {
                    unblockableGameObject.SetActive(false);
                }

                if (forceStandGameObject != null)
                {
                    forceStandGameObject.SetActive(false);
                }

                if (otgGameObject != null)
                {
                    otgGameObject.SetActive(false);
                }

                if (groundBounceGameObject != null)
                {
                    groundBounceGameObject.SetActive(false);
                }

                if (wallBounceGameObject != null)
                {
                    wallBounceGameObject.SetActive(false);
                }

                if (hit.techable == true)
                {
                    if (techableThrowGameObject != null)
                    {
                        techableThrowGameObject.SetActive(true);
                    }

                    if (untechableThrowGameObject != null)
                    {
                        untechableThrowGameObject.SetActive(false);
                    }
                }
                else
                {
                    if (techableThrowGameObject != null)
                    {
                        techableThrowGameObject.SetActive(false);
                    }

                    if (untechableThrowGameObject != null)
                    {
                        untechableThrowGameObject.SetActive(true);
                    }
                }
            }
        }

        private Fix64 GetHitStunValueFromHitOnHit(Hit hit)
        {
            if (hit == null)
            {
                return 0;
            }

            switch (hit.hitStunType)
            {
                case HitStunType.FrameAdvantage:
                    return hit.frameAdvantageOnHit;

                case HitStunType.Frames:
                    return hit._hitStunOnHit;

                default:
                    return 0;
            }
        }

        private Fix64 GetHitStunValueFromHitOnBlock(Hit hit)
        {
            if (hit == null)
            {
                return 0;
            }

            switch (hit.hitStunType)
            {
                case HitStunType.FrameAdvantage:
                    return hit.frameAdvantageOnBlock;

                case HitStunType.Frames:
                    return hit._hitStunOnBlock;

                default:
                    return 0;
            }
        }

        private string GetStringFromDamageType(DamageType damageType, Fix64 value)
        {
            if (damageType == DamageType.Percentage)
            {
                if (value < 0)
                {
                    value = 0;
                }

                return UFE2Manager.instance.cachedStringData.GetPositivePercentStringNumber((int)Fix64.Floor(value));
            }
            else if (damageType == DamageType.Points)
            {
                if (value < 0)
                {
                    value = 0;
                }

                return UFE2Manager.instance.cachedStringData.GetPositiveStringNumber((int)Fix64.Floor(value));
            }

            return "";
        }

        private string GetStringFromHitStunType(HitStunType hitStunType, Fix64 value)
        {
            if (value >= 0)
            {
                return UFE2Manager.instance.cachedStringData.GetPositiveStringNumber((int)value);
            }
            else
            {
                return UFE2Manager.instance.cachedStringData.GetNegativeStringNumber((int)Fix64.Abs(value));
            }
        }
    }
}
