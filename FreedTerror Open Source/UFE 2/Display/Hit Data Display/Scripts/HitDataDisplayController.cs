using UnityEngine;
using UnityEngine.UI;
using UFE3D;
using FPLibrary;

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

        private void Start()
        {
            SetAllGameObjectActive(false);
        }

        private void OnDisable()
        {
            UFE.OnHit -= OnHit;
            UFE.OnBlock -= OnBlock;
            UFE.OnBlock -= OnParry;
        }

        private void OnHit(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            SetHitDisplayData(player, hitInfo);
        }

        private void OnBlock(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            SetHitDisplayData(player, hitInfo);
        }

        private void OnParry(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            SetHitDisplayData(player, hitInfo);
        }

        private void SetHitDisplayData(ControlsScript player, Hit hit)
        {
            if (player == null
                || player != UFE2Manager.GetControlsScript(this.player)
                || hit == null)
            {
                return;
            }

            UFE2Manager.SetTextMessage(hitTypeText, UFE2Manager.GetStringFromEnum(hit.hitType, hit.hitConfirmType));

            UFE2Manager.SetTextMessage(damageOnHitText, GetStringFromDamageType(hit.damageType, hit._damageOnHit));

            UFE2Manager.SetTextMessage(minDamageOnHitText, GetStringFromDamageType(hit.damageType, hit._minDamageOnHit));

            UFE2Manager.SetTextMessage(damageOnBlockText, GetStringFromDamageType(hit.damageType, hit._damageOnBlock));

            UFE2Manager.SetTextMessage(frameAdvantageOnHitText, GetStringFromHitStunType(hit.hitStunType, GetHitStunValueFromHitOnHit(hit)));

            UFE2Manager.SetTextMessage(frameAdvantageOnBlockText, GetStringFromHitStunType(hit.hitStunType, GetHitStunValueFromHitOnBlock(hit)));

            SetGameObjectActiveFromHitConfirmType(hit.hitConfirmType, hit);
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

                return UFE2Manager.GetNormalPercentStringNumber((int)Fix64.Floor(value));
            }
            else if (damageType == DamageType.Points)
            {
                if (value < 0)
                {
                    value = 0;
                }

                return UFE2Manager.GetNormalStringNumber((int)Fix64.Floor(value));
            }

            return "";
        }

        private string GetStringFromHitStunType(HitStunType hitStunType, Fix64 value)
        {
            switch (hitStunType)
            {
                case HitStunType.FrameAdvantage:
                    if (value == 0)
                    {
                        return UFE2Manager.GetNormalStringNumber((int)value);
                    }
                    if (value > 0)
                    {
                        return UFE2Manager.GetPositiveStringNumber((int)value);
                    }
                    else if (value < 0)
                    {
                        return UFE2Manager.GetNegativeStringNumber((int)Fix64.Abs(value));
                    }
                    return "";

                case HitStunType.Frames:
                    if (value < 0)
                    {
                        value = 0;
                    }

                    return UFE2Manager.GetNormalFrameStringNumber((int)value);

                default:
                    return "";
            }
        }

        private void SetGameObjectActiveFromHitConfirmType(HitConfirmType hitConfirmType, Hit hit)
        {
            if (hitConfirmType == HitConfirmType.Hit)
            {
                UFE2Manager.SetGameObjectActive(armorBreakerGameObject, hit.armorBreaker);

                UFE2Manager.SetGameObjectActive(unblockableGameObject, hit.unblockable);

                UFE2Manager.SetGameObjectActive(forceStandGameObject, hit.forceStand);

                UFE2Manager.SetGameObjectActive(otgGameObject, hit.downHit);

                UFE2Manager.SetGameObjectActive(groundBounceGameObject, hit.groundBounce);

                UFE2Manager.SetGameObjectActive(wallBounceGameObject, hit.wallBounce);

                UFE2Manager.SetGameObjectActive(techableThrowGameObject, false);

                UFE2Manager.SetGameObjectActive(untechableThrowGameObject, false);
            }
            else if (hitConfirmType == HitConfirmType.Throw)
            {
                UFE2Manager.SetGameObjectActive(armorBreakerGameObject, false);

                UFE2Manager.SetGameObjectActive(unblockableGameObject, false);

                UFE2Manager.SetGameObjectActive(forceStandGameObject, false);

                UFE2Manager.SetGameObjectActive(otgGameObject, false);

                UFE2Manager.SetGameObjectActive(groundBounceGameObject, false);

                UFE2Manager.SetGameObjectActive(wallBounceGameObject, false);

                if (hit.techable == true)
                {
                    UFE2Manager.SetGameObjectActive(techableThrowGameObject, true);

                    UFE2Manager.SetGameObjectActive(untechableThrowGameObject, false);
                }
                else
                {
                    UFE2Manager.SetGameObjectActive(techableThrowGameObject, false);

                    UFE2Manager.SetGameObjectActive(untechableThrowGameObject, true);
                }
            }
        }

        private void SetAllGameObjectActive(bool active)
        {
            UFE2Manager.SetGameObjectActive(armorBreakerGameObject, active);

            UFE2Manager.SetGameObjectActive(unblockableGameObject, active);

            UFE2Manager.SetGameObjectActive(forceStandGameObject, active);

            UFE2Manager.SetGameObjectActive(otgGameObject, active);

            UFE2Manager.SetGameObjectActive(groundBounceGameObject, active);

            UFE2Manager.SetGameObjectActive(wallBounceGameObject, active);

            UFE2Manager.SetGameObjectActive(techableThrowGameObject, active);

            UFE2Manager.SetGameObjectActive(untechableThrowGameObject, active);
        }
    }
}
