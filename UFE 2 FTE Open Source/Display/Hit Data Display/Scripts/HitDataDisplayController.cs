using UnityEngine;
using UnityEngine.UI;
using UFE3D;
using FPLibrary;

namespace UFE2FTE
{
    public class HitDataDisplayController : MonoBehaviour
    {
        [SerializeField]
        private UFE2FTE.Player player;

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
                || player != UFE2FTE.GetControlsScript(this.player)
                || hit == null)
            {
                return;
            }

            UFE2FTE.SetTextMessage(hitTypeText, UFE2FTE.GetStringFromEnum(hit.hitType, hit.hitConfirmType));

            UFE2FTE.SetTextMessage(damageOnHitText, GetStringFromDamageType(hit.damageType, hit._damageOnHit));

            UFE2FTE.SetTextMessage(minDamageOnHitText, GetStringFromDamageType(hit.damageType, hit._minDamageOnHit));

            UFE2FTE.SetTextMessage(damageOnBlockText, GetStringFromDamageType(hit.damageType, hit._damageOnBlock));

            UFE2FTE.SetTextMessage(frameAdvantageOnHitText, GetStringFromHitStunType(hit.hitStunType, GetHitStunValueFromHitOnHit(hit)));

            UFE2FTE.SetTextMessage(frameAdvantageOnBlockText, GetStringFromHitStunType(hit.hitStunType, GetHitStunValueFromHitOnBlock(hit)));

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

                return UFE2FTE.GetNormalPercentStringNumber((int)Fix64.Floor(value));
            }
            else if (damageType == DamageType.Points)
            {
                if (value < 0)
                {
                    value = 0;
                }

                return UFE2FTE.GetNormalStringNumber((int)Fix64.Floor(value));
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
                        return UFE2FTE.GetNormalStringNumber((int)value);
                    }
                    if (value > 0)
                    {
                        return UFE2FTE.GetPositiveStringNumber((int)value);
                    }
                    else if (value < 0)
                    {
                        return UFE2FTE.GetNegativeStringNumber((int)Fix64.Abs(value));
                    }
                    return "";

                case HitStunType.Frames:
                    if (value < 0)
                    {
                        value = 0;
                    }

                    return UFE2FTE.GetNormalFrameStringNumber((int)value);

                default:
                    return "";
            }
        }

        private void SetGameObjectActiveFromHitConfirmType(HitConfirmType hitConfirmType, Hit hit)
        {
            if (hitConfirmType == HitConfirmType.Hit)
            {
                UFE2FTE.SetGameObjectActive(armorBreakerGameObject, hit.armorBreaker);

                UFE2FTE.SetGameObjectActive(unblockableGameObject, hit.unblockable);

                UFE2FTE.SetGameObjectActive(forceStandGameObject, hit.forceStand);

                UFE2FTE.SetGameObjectActive(otgGameObject, hit.downHit);

                UFE2FTE.SetGameObjectActive(groundBounceGameObject, hit.groundBounce);

                UFE2FTE.SetGameObjectActive(wallBounceGameObject, hit.wallBounce);

                UFE2FTE.SetGameObjectActive(techableThrowGameObject, false);

                UFE2FTE.SetGameObjectActive(untechableThrowGameObject, false);
            }
            else if (hitConfirmType == HitConfirmType.Throw)
            {
                UFE2FTE.SetGameObjectActive(armorBreakerGameObject, false);

                UFE2FTE.SetGameObjectActive(unblockableGameObject, false);

                UFE2FTE.SetGameObjectActive(forceStandGameObject, false);

                UFE2FTE.SetGameObjectActive(otgGameObject, false);

                UFE2FTE.SetGameObjectActive(groundBounceGameObject, false);

                UFE2FTE.SetGameObjectActive(wallBounceGameObject, false);

                if (hit.techable == true)
                {
                    UFE2FTE.SetGameObjectActive(techableThrowGameObject, true);

                    UFE2FTE.SetGameObjectActive(untechableThrowGameObject, false);
                }
                else
                {
                    UFE2FTE.SetGameObjectActive(techableThrowGameObject, false);

                    UFE2FTE.SetGameObjectActive(untechableThrowGameObject, true);
                }
            }
        }

        private void SetAllGameObjectActive(bool active)
        {
            UFE2FTE.SetGameObjectActive(armorBreakerGameObject, active);

            UFE2FTE.SetGameObjectActive(unblockableGameObject, active);

            UFE2FTE.SetGameObjectActive(forceStandGameObject, active);

            UFE2FTE.SetGameObjectActive(otgGameObject, active);

            UFE2FTE.SetGameObjectActive(groundBounceGameObject, active);

            UFE2FTE.SetGameObjectActive(wallBounceGameObject, active);

            UFE2FTE.SetGameObjectActive(techableThrowGameObject, active);

            UFE2FTE.SetGameObjectActive(untechableThrowGameObject, active);
        }
    }
}
