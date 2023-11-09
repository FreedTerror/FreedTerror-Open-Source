using UnityEngine;
using FPLibrary;
using System;

namespace UFE2FTE
{
    public class TriggeredBehaviourGameObjectController : MonoBehaviour
    {
        private ControlsScript myControlsScript;

        private void Awake()
        {
            if (myControlsScript == null)
            {
                myControlsScript = GetComponentInParent<ControlsScript>();
            }           
        }

        private void OnEnable()
        {
            TriggeredBehaviourScriptableObjectCastMove.OnCastMove += OnOverrideMove;
            TriggeredBehaviourScriptableObjectSetTransform.OnSetPositionComboBreaker += OnSetPositionComboBreaker;
        }

        private void Start()
        {
            if (myControlsScript == null)
            {
                myControlsScript = GetComponentInParent<ControlsScript>();
            }
        }

        private void OnDisable()
        {
            TriggeredBehaviourScriptableObjectCastMove.OnCastMove -= OnOverrideMove;
            TriggeredBehaviourScriptableObjectSetTransform.OnSetPositionComboBreaker -= OnSetPositionComboBreaker;
        }

        private void OnOverrideMove(ControlsScript player, string moveName, Fix64 delayActionTime)
        {
            if (player == null
                || player != myControlsScript)
            {
                return;
            }

            CastMoveByMoveNameDelaySynchronizedAction(moveName, delayActionTime);
        }

        #region Stand Move Name Methods

        private static readonly string StandLightAttackMoveName = "Stand Light Attack";
        private void CastMoveByMoveNameStandLightAttack()
        {
            UFE2FTE.CastMoveByMoveName(myControlsScript, StandLightAttackMoveName);
        }

        private static readonly string StandComboBreakerConfirmMoveName = "Stand Combo Breaker Confirm";
        private void CastMoveByMoveNameStandComboBreakerConfirmReaction()
        {
            UFE2FTE.CastMoveByMoveName(myControlsScript, StandComboBreakerConfirmMoveName);
        }

        #endregion

        #region Crouch Move Name Methods

        private static readonly string CrouchLightAttackMoveName = "Crouch Light Attack";
        private void CastMoveByMoveNameCrouchLightAttack()
        {
            UFE2FTE.CastMoveByMoveName(myControlsScript, CrouchLightAttackMoveName);
        }

        #endregion

        #region Jump Move Name Methods

        private static readonly string JumpLightAttackMoveName = "Jump Light Attack";
        private void CastMoveByMoveNameJumpLightAttack()
        {
            UFE2FTE.CastMoveByMoveName(myControlsScript, JumpLightAttackMoveName);
        }

        private readonly string JumpComboBreakerConfirmMoveName = "Jump Combo Breaker Confirm";
        private void CastMoveByMoveNameJumpComboBreakerConfirmReaction()
        {
            UFE2FTE.CastMoveByMoveName(myControlsScript, JumpComboBreakerConfirmMoveName);
        }

        #endregion

        private void CastMoveByMoveNameDelaySynchronizedAction(string moveName, Fix64 delayActionTime)
        {
            #region Stand Move Name

            if (moveName == StandLightAttackMoveName)
            {
                if (delayActionTime > 0)
                {
                    UFE.DelaySynchronizedAction(CastMoveByMoveNameStandLightAttack, delayActionTime);
                }
                else
                {
                    UFE2FTE.CastMoveByMoveName(myControlsScript, moveName);
                }

                return;
            }
            else if (moveName == StandComboBreakerConfirmMoveName)
            {
                if (delayActionTime > 0)
                {
                    UFE.DelaySynchronizedAction(CastMoveByMoveNameStandComboBreakerConfirmReaction, delayActionTime);
                }
                else
                {
                    UFE2FTE.CastMoveByMoveName(myControlsScript, moveName);
                }

                return;
            }

            #endregion

            #region Crouch Move Name

            else if (moveName == CrouchLightAttackMoveName)
            {
                if (delayActionTime > 0)
                {
                    UFE.DelaySynchronizedAction(CastMoveByMoveNameCrouchLightAttack, delayActionTime);
                }
                else
                {
                    UFE2FTE.CastMoveByMoveName(myControlsScript, moveName);
                }

                return;
            }

            #endregion

            #region Jump Move Name

            else if (moveName == JumpLightAttackMoveName)
            {
                if (delayActionTime > 0)
                {
                    UFE.DelaySynchronizedAction(CastMoveByMoveNameJumpLightAttack, delayActionTime);
                }
                else
                {
                    UFE2FTE.CastMoveByMoveName(myControlsScript, moveName);
                }

                return;
            }
            else if (moveName == JumpComboBreakerConfirmMoveName)
            {
                if (delayActionTime > 0)
                {
                    UFE.DelaySynchronizedAction(CastMoveByMoveNameJumpComboBreakerConfirmReaction, delayActionTime);
                }
                else
                {
                    UFE2FTE.CastMoveByMoveName(myControlsScript, moveName);
                }

                return;
            }

            #endregion

            if (delayActionTime > 0)
            {
#if UNITY_EDITOR
                Debug.Log("Can't delay this action.");
#endif

                UFE2FTE.CastMoveByMoveName(myControlsScript, moveName);
            }
            else
            {
                UFE2FTE.CastMoveByMoveName(myControlsScript, moveName);
            }
        }

        private void OnSetPositionComboBreaker(ControlsScript player, Fix64 delayActionTime)
        {
            if (player == null
                || player != myControlsScript)
            {
                return;
            }

            if (delayActionTime > 0)
            {
                UFE.DelaySynchronizedAction(SetPositionComboBreaker, delayActionTime);
            }
            else
            {
                SetPositionComboBreaker();
            }
        }

        private void SetPositionComboBreaker()
        {
            TriggeredBehaviourScriptableObjectSetTransform.SetPositionComboBreaker(myControlsScript);
        }
    }
}