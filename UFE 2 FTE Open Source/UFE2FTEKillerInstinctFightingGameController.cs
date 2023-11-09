using System;
using UnityEngine;
using UFE3D;
using UFENetcode;
using FPLibrary;

namespace UFE2FTE
{
    public class UFE2FTEKillerInstinctFightingGameController : UFEBehaviour, UFEInterface
    {
       /*
        // Keep an eye out for desyncs caused by this script.

        [Serializable]
        private class AppliedForce
        {
            public bool resetXForce;
            public bool resetYForce;
            public bool resetZForce;
            public FPVector force;
        }

        [Serializable]
        private class GaugeAmountOptions
        {
            public enum EventType
            {
                OnHit,
                OnBlock,
                OnParry
            }
            public EventType[] eventTypes;
            public GaugeId[] gaugeIds;
            public HitStrengh[] hitStrengths;
            public bool usePlayer;
            public bool useOpponent;
            [Range(-100, 100)]
            public float percentAmount;
        }
        [Header("GAUGE AMOUNT OPTIONS")]
        [SerializeField]
        private GaugeAmountOptions[] gaugeAmountOptions;

        [Serializable]
        private class ComboValueOptions
        {
            public GaugeId comboValueGaugeId;

            public bool useBlowOutBasicMove;
            public AppliedForce blowOutBasicMoveAppliedForce;

            public bool useBlowOutMove;
            public string blowOutMoveName = "Blow Out";

            public string blowOutAlertName = "Blow Out";
        }
        [Header("COMBO VALUE OPTIONS")]
        [SerializeField]
        private ComboValueOptions comboValueOptions;

        [Serializable]
        private class ComboBreakerOptions
        {
            public GaugeId comboBreakerGaugeId;

            public Fix64 comboBreakerLightRequiredGaugePercentAmount = 25;
            public Fix64 comboBreakerMediumRequiredGaugePercentAmount = 50;
            public Fix64 comboBreakerHeavyRequiredGaugePercentAmount = 75;
            public Fix64 comboBreakerAnyRequiredGaugePercentAmount = 100;

            public ButtonPress[] comboBreakerLightButtonPresses;
            public ButtonPress[] comboBreakerMediumButtonPresses;
            public ButtonPress[] comboBreakerHeavyButtonPresses;

            public string standComboBreakerMoveName = "Stand Combo Breaker";
            public string crouchComboBreakerMoveName = "Crouch Combo Breaker";
            public string jumpComboBreakerMoveName = "Jump Combo Breaker";

            public bool useKillAllOpponentMovesOnComboBreaker;

            public string comboBreakerLightAlertName = "COMBO BREAKER L";
            public string comboBreakerMediumAlertName = "COMBO BREAKER M";
            public string comboBreakerHeavyAlertName = "COMBO BREAKER H";
        }
        [Header("COMBO BREAKER OPTIONS")]
        [SerializeField]
        private ComboBreakerOptions comboBreakerOptions;

        [Serializable]
        private class CounterBreakerOptions
        {
            public string standCounterBreakerAttemptMoveName = "Stand Counter Breaker Attempt";
            public string crouchCounterBreakerAttemptMoveName = "Crouch Counter Breaker Attempt";
            public string jumpCounterBreakerAttemptMoveName = "Jump Counter Breaker Attempt";
            public FPVector opponentRelativePosition;
        }
        [Header("COUNTER BREAKER OPTIONS")]
        [SerializeField]
        private CounterBreakerOptions counterBreakerOptions;

        [Serializable]
        private class LockOutOptions
        {
            public GaugeId lockOutGaugeId;
            [Range(-100, 0)]
            public float passiveLockOutGaugePercentAmount;

            public string lockOutLightAlertName = "LOCK OUT L";
            public string lockOutMediumAlertName = "LOCK OUT M";
            public string lockOutHeavyAlertName = "LOCK OUT H";

            public GaugeId counterBreakerLockOutGaugeId;
            [Range(-100, 0)]
            public Fix64 passiveCounterBreakerLockOutGaugePercentAmount;
        }
        [Header("LOCK OUT OPTIONS")]
        [SerializeField]
        private LockOutOptions lockOutOptions;

        void OnEnable()
        {
            SubscribeToUFEEvents();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        public override void UFEFixedUpdate()
        {
            SetComboValueGauge(UFE.GetPlayer1ControlsScript());

            SetComboValueGauge(UFE.GetPlayer2ControlsScript());

            SetComboBreakerGauge(UFE.GetPlayer1ControlsScript());

            SetComboBreakerGauge(UFE.GetPlayer2ControlsScript());

            SetLockOutGauge(UFE.GetPlayer1ControlsScript());

            SetLockOutGauge(UFE.GetPlayer2ControlsScript());
        }

        #region Gauge Amount Options Methods

        private void SetGaugeAmountOptions(GaugeAmountOptions.EventType eventType, GaugeAmountOptions[] gaugeAmountOptions, Hit hitInfo, ControlsScript player)
        {
            int length = gaugeAmountOptions.Length;
            for (int i = 0; i < length; i++)
            {
                int lengthA = gaugeAmountOptions[i].eventTypes.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (eventType != gaugeAmountOptions[i].eventTypes[a]) continue;

                    int lengthB = gaugeAmountOptions[i].hitStrengths.Length;
                    for (int b = 0; b < lengthB; b++)
                    {
                        if (hitInfo.hitStrength != gaugeAmountOptions[i].hitStrengths[b]) continue;

                        if (gaugeAmountOptions[i].usePlayer == true)
                        {
                            int lengthC = gaugeAmountOptions[i].gaugeIds.Length;
                            for (int c = 0; c < lengthC; c++)
                            {
                                UFE2FTE.AddGaugePercentAmount(player, gaugeAmountOptions[i].gaugeIds[c], gaugeAmountOptions[i].percentAmount);
                            }
                        }

                        if (gaugeAmountOptions[i].useOpponent == true)
                        {
                            int lengthC = gaugeAmountOptions[i].gaugeIds.Length;
                            for (int c = 0; c < lengthC; c++)
                            {
                                UFE2FTE.AddGaugePercentAmount(player, gaugeAmountOptions[i].gaugeIds[c], gaugeAmountOptions[i].percentAmount);
                            }
                        }

                        break;
                    }

                    break;
                }
            }
        }

        #endregion

        #region Combo Value Options Methods

        private void SetComboValueGauge(ControlsScript player)
        {
            if (player.opControlsScript.currentSubState != SubStates.Stunned)
            {
                UFE2FTE.SetGaugePercentAmount(player, comboValueOptions.comboValueGaugeId, 0);
            }
        }

        private void AddComboValueGaugePercentAmount(ControlsScript player, GaugeId gaugeId, Fix64 gaugePercentAmount)
        {
            if (player.currentGaugesPoints[(int)gaugeId] == player.myInfo.maxGaugePoints * ((Fix64)99 / 100))
            {
                UFE2FTE.SetGaugePercentAmount(player, gaugeId, 100);

                return;
            }

            player.currentGaugesPoints[(int)gaugeId] += player.myInfo.maxGaugePoints * (gaugePercentAmount / 100);

            if (player.currentGaugesPoints[(int)gaugeId] >= player.myInfo.maxGaugePoints)
            {
                UFE2FTE.SetGaugePercentAmount(player, gaugeId, 99);
            }
            else if (player.currentGaugesPoints[(int)gaugeId] < 0)
            {
                player.currentGaugesPoints[(int)gaugeId] = 0;
            }
        }

        private void SetBlowOutPlayer1()
        {
            if (UFE.GetPlayer1ControlsScript().currentGaugesPoints[(int)comboValueOptions.comboValueGaugeId] != UFE.GetPlayer1ControlsScript().opControlsScript.myInfo.maxGaugePoints) return;

            UFE2FTE.SetGaugePercentAmount(UFE.GetPlayer2ControlsScript(), comboBreakerOptions.comboBreakerGaugeId, 0);

            UFE2FTE.SetGaugePercentAmount(UFE.GetPlayer2ControlsScript(), lockOutOptions.lockOutGaugeId, 0);

            UFE2FTE.SetGaugePercentAmount(UFE.GetPlayer2ControlsScript(), lockOutOptions.counterBreakerLockOutGaugeId, 0);

            PlayBlowOutBasicMove(UFE.GetPlayer2ControlsScript());

            CastBlowOutMove(UFE.GetPlayer2ControlsScript());

            UFE.FireAlert(comboValueOptions.blowOutAlertName, UFE.GetPlayer2ControlsScript());
        }

        private void SetBlowOutPlayer2()
        {
            if (UFE.GetPlayer2ControlsScript().currentGaugesPoints[(int)comboValueOptions.comboValueGaugeId] != UFE.GetPlayer2ControlsScript().opControlsScript.myInfo.maxGaugePoints) return;

            UFE2FTE.SetGaugePercentAmount(UFE.GetPlayer1ControlsScript(), comboBreakerOptions.comboBreakerGaugeId, 0);

            UFE2FTE.SetGaugePercentAmount(UFE.GetPlayer1ControlsScript(), lockOutOptions.lockOutGaugeId, 0);

            UFE2FTE.SetGaugePercentAmount(UFE.GetPlayer1ControlsScript(), lockOutOptions.counterBreakerLockOutGaugeId, 0);

            PlayBlowOutBasicMove(UFE.GetPlayer1ControlsScript());

            CastBlowOutMove(UFE.GetPlayer1ControlsScript());

            UFE.FireAlert(comboValueOptions.blowOutAlertName, UFE.GetPlayer1ControlsScript());
        }

        private void PlayBlowOutBasicMove(ControlsScript player)
        {
            if (comboValueOptions.useBlowOutBasicMove == false) return;

            UFE2FTE.ResetForces(player, comboValueOptions.blowOutBasicMoveAppliedForce.resetXForce, comboValueOptions.blowOutBasicMoveAppliedForce.resetYForce, comboValueOptions.blowOutBasicMoveAppliedForce.resetZForce);

            UFE2FTE.AddForce(player, FPVector.ToFPVector(comboValueOptions.blowOutBasicMoveAppliedForce.force));

            BasicMoveInfo basicMoveInfo = null;

            basicMoveInfo = player.MoveSet.basicMoves.getHitAir;

            player.currentHitAnimation = player.MoveSet.basicMoves.getHitAir.name;

            player.currentSubState = SubStates.Stunned;

            player.stunTime = 999;

            player.MoveSet.PlayBasicMove(basicMoveInfo, player.currentHitAnimation, 0, false, true);

            if (basicMoveInfo.autoSpeed == true)
            {
                Fix64 airTime = player.Physics.GetPossibleAirTime(player.Physics.activeForces.y);

                if (player.MoveSet.basicMoves.fallingFromAirHit.animMap[0].clip == null) airTime *= 2;

                player.MoveSet.SetAnimationNormalizedSpeed(player.currentHitAnimation, (player.MoveSet.GetAnimationLength(player.currentHitAnimation) / airTime));
            }
        }

        private void CastBlowOutMove(ControlsScript player)
        {
            if (comboValueOptions.useBlowOutMove == false) return;

            UFE2FTE.CastMove(player, comboValueOptions.blowOutMoveName);
        }

        #endregion

        #region Combo Breaker Options Methods

        private void SetComboBreakerGauge(ControlsScript player)
        {
            if (player.currentState == PossibleStates.Down)
            {
                UFE2FTE.SetGaugePercentAmount(player, comboBreakerOptions.comboBreakerGaugeId, 0);
            }

            if (player.currentSubState != SubStates.Stunned)
            {
                UFE2FTE.SetGaugePercentAmount(player, comboBreakerOptions.comboBreakerGaugeId, 0);
            }
        }

        private void SetComboBreakerOnButton(ButtonPress button, ControlsScript player)
        {
            if (player.currentGaugesPoints[(int)comboBreakerOptions.comboBreakerGaugeId] == 0 // remove later
                || player.currentGaugesPoints[(int)lockOutOptions.lockOutGaugeId] > 0
                || player.currentGaugesPoints[(int)lockOutOptions.counterBreakerLockOutGaugeId] > 0)
            {
                return;
            }

            int length = comboBreakerOptions.comboBreakerLightButtonPresses.Length;
            for (int i = 0; i < length; i++)
            {
                if (button != comboBreakerOptions.comboBreakerLightButtonPresses[i]) continue;

                if (player.currentGaugesPoints[(int)comboBreakerOptions.comboBreakerGaugeId] == player.myInfo.maxGaugePoints * ((Fix64)comboBreakerOptions.comboBreakerLightRequiredGaugePercentAmount / 100)
                    || player.currentGaugesPoints[(int)comboBreakerOptions.comboBreakerGaugeId] == player.myInfo.maxGaugePoints * ((Fix64)comboBreakerOptions.comboBreakerAnyRequiredGaugePercentAmount / 100))
                {
                    CastComboBreakerMove(player);

                    if (comboBreakerOptions.useKillAllOpponentMovesOnComboBreaker == true)
                    {
                        UFE2FTE.KillAllMoves(player.opControlsScript);
                    }

                    UFE.FireAlert(comboBreakerOptions.comboBreakerLightAlertName, player);
                }
                else if (player.currentGaugesPoints[(int)comboBreakerOptions.comboBreakerGaugeId] != player.myInfo.maxGaugePoints * ((Fix64)comboBreakerOptions.comboBreakerLightRequiredGaugePercentAmount / 100))
                {
                    UFE2FTE.SetGaugePercentAmount(player, lockOutOptions.lockOutGaugeId, player.myInfo.maxGaugePoints);

                    UFE.FireAlert(lockOutOptions.lockOutLightAlertName, player);
                }

                break;
            }

            length = comboBreakerOptions.comboBreakerMediumButtonPresses.Length;
            for (int i = 0; i < length; i++)
            {
                if (button != comboBreakerOptions.comboBreakerMediumButtonPresses[i]) continue;

                if (player.currentGaugesPoints[(int)comboBreakerOptions.comboBreakerGaugeId] == player.myInfo.maxGaugePoints * ((Fix64)comboBreakerOptions.comboBreakerMediumRequiredGaugePercentAmount / 100)
                    || player.currentGaugesPoints[(int)comboBreakerOptions.comboBreakerGaugeId] == player.myInfo.maxGaugePoints * ((Fix64)comboBreakerOptions.comboBreakerAnyRequiredGaugePercentAmount / 100))
                {
                    CastComboBreakerMove(player);

                    if (comboBreakerOptions.useKillAllOpponentMovesOnComboBreaker == true)
                    {
                        UFE2FTE.KillAllMoves(player.opControlsScript);
                    }

                    UFE.FireAlert(comboBreakerOptions.comboBreakerMediumAlertName, player);
                }
                else if (player.currentGaugesPoints[(int)comboBreakerOptions.comboBreakerGaugeId] != player.myInfo.maxGaugePoints * ((Fix64)comboBreakerOptions.comboBreakerMediumRequiredGaugePercentAmount / 100))
                {
                    UFE2FTE.SetGaugePercentAmount(player, lockOutOptions.lockOutGaugeId, player.myInfo.maxGaugePoints);

                    UFE.FireAlert(lockOutOptions.lockOutMediumAlertName, player);
                }

                break;
            }

            length = comboBreakerOptions.comboBreakerHeavyButtonPresses.Length;
            for (int i = 0; i < length; i++)
            {
                if (button != comboBreakerOptions.comboBreakerHeavyButtonPresses[i]) continue;

                if (player.currentGaugesPoints[(int)comboBreakerOptions.comboBreakerGaugeId] == player.myInfo.maxGaugePoints * ((Fix64)comboBreakerOptions.comboBreakerHeavyRequiredGaugePercentAmount / 100)
                    || player.currentGaugesPoints[(int)comboBreakerOptions.comboBreakerGaugeId] == player.myInfo.maxGaugePoints * ((Fix64)comboBreakerOptions.comboBreakerAnyRequiredGaugePercentAmount / 100))
                {
                    CastComboBreakerMove(player);

                    if (comboBreakerOptions.useKillAllOpponentMovesOnComboBreaker == true)
                    {
                        UFE2FTE.KillAllMoves(player.opControlsScript);
                    }

                    UFE.FireAlert(comboBreakerOptions.comboBreakerHeavyAlertName, player);
                }
                else if (player.currentGaugesPoints[(int)comboBreakerOptions.comboBreakerGaugeId] != player.myInfo.maxGaugePoints * ((Fix64)comboBreakerOptions.comboBreakerHeavyRequiredGaugePercentAmount / 100))
                {
                    UFE2FTE.SetGaugePercentAmount(player, lockOutOptions.lockOutGaugeId, player.myInfo.maxGaugePoints);

                    UFE.FireAlert(lockOutOptions.lockOutHeavyAlertName, player);
                }

                break;
            }
        }

        private void CastComboBreakerMove(ControlsScript player)
        {
            switch (player.currentState)
            {
                case PossibleStates.Stand:
                    UFE2FTE.CastMove(player, comboBreakerOptions.standComboBreakerMoveName);
                    break;

                case PossibleStates.Crouch:
                    UFE2FTE.CastMove(player, comboBreakerOptions.crouchComboBreakerMoveName);
                    break;

                case PossibleStates.NeutralJump:
                case PossibleStates.ForwardJump:
                case PossibleStates.BackJump:
                    UFE2FTE.CastMove(player, comboBreakerOptions.jumpComboBreakerMoveName);
                    break;
            }
        }

        #endregion

        #region Lock Out Options Methods

        private void SetLockOutGauge(ControlsScript player)
        {
            if (player.currentSubState != SubStates.Stunned)
            {
                UFE2FTE.SetGaugePercentAmount(player, lockOutOptions.lockOutGaugeId, 0);

                UFE2FTE.SetGaugePercentAmount(player, lockOutOptions.counterBreakerLockOutGaugeId, 0);
            }

            UFE2FTE.AddGaugePercentAmount(player, lockOutOptions.lockOutGaugeId, lockOutOptions.passiveLockOutGaugePercentAmount);

            UFE2FTE.AddGaugePercentAmount(player, lockOutOptions.counterBreakerLockOutGaugeId, lockOutOptions.passiveCounterBreakerLockOutGaugePercentAmount);
        }

        #endregion

        #region UFE Event Methods

        private void SubscribeToUFEEvents()
        {
            //UFE.OnRoundBegins += this.OnRoundBegins;
            //UFE.OnGaugeUpdate += this.OnGaugeUpdate;
            UFE.OnHit += this.OnHit;      
            //UFE.OnBlock += this.OnBlock;
            //UFE.OnParry += this.OnParry;
            UFE.OnMove += this.OnMove;
            UFE.OnButton += this.OnButton;
        }

        private void UnsubscribeFromUFEEvents()
        {
            //UFE.OnRoundBegins -= OnRoundBegins;
            //UFE.OnGaugeUpdate -= this.OnGaugeUpdate;
            UFE.OnHit -= this.OnHit;
            //UFE.OnBlock -= this.OnBlock;
            //UFE.OnParry -= this.OnParry;
            UFE.OnMove -= this.OnMove;      
            UFE.OnButton -= this.OnButton;
        }

        private void OnGaugeUpdate(int targetGauge, float newValue, ControlsScript character)
        {

        }

        private void OnHit(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            if (move.moveName == "Stand Counter Breaker Confirm")
            {
                UFE2FTE.SetGaugePercentAmount(player.opControlsScript, lockOutOptions.lockOutGaugeId, 0);

                UFE2FTE.SetGaugePercentAmount(player.opControlsScript, lockOutOptions.counterBreakerLockOutGaugeId, player.opControlsScript.myInfo.maxGaugePoints);
            }

            switch (hitInfo.hitStrength)
            {
                case HitStrengh.Weak:
                    AddComboValueGaugePercentAmount(player, comboValueOptions.comboValueGaugeId, 10);

                    UFE2FTE.SetGaugePercentAmount(player.opControlsScript, comboBreakerOptions.comboBreakerGaugeId, 25);
                    break;

                case HitStrengh.Medium:
                    AddComboValueGaugePercentAmount(player, comboValueOptions.comboValueGaugeId, 8);

                    UFE2FTE.SetGaugePercentAmount(player.opControlsScript, comboBreakerOptions.comboBreakerGaugeId, 50);
                    break;

                case HitStrengh.Heavy:
                    AddComboValueGaugePercentAmount(player, comboValueOptions.comboValueGaugeId, 6);

                    UFE2FTE.SetGaugePercentAmount(player.opControlsScript, comboBreakerOptions.comboBreakerGaugeId, 75);
                    break;
            }

            UFE.DelaySynchronizedAction(this.SetBlowOutPlayer1, player.GetHitFreezingTime(hitInfo.hitStrength));

            UFE.DelaySynchronizedAction(this.SetBlowOutPlayer2, player.GetHitFreezingTime(hitInfo.hitStrength));
        }

        private void OnMove(MoveInfo move, ControlsScript player)
        {
            if (move == null) return;

            if (move.moveName == counterBreakerOptions.standCounterBreakerAttemptMoveName)
            {
                SetPlayerPosition(player, player.opControlsScript.worldTransform.position, FPVector.ToFPVector(counterBreakerOptions.opponentRelativePosition));

                UFE2FTE.ResetForces(player, true, true, true);

                UFE2FTE.AddForce(player, player.opControlsScript.Physics.activeForces);

                UFE2FTE.SetGaugePointsValue(player.opControlsScript, comboBreakerOptions.comboBreakerGaugeId, comboBreakerOptions.comboBreakerAnyRequiredGaugePercentAmount);
            }

            if (move.moveName == "Stand Counter Breaker Recovery")
            {
                UFE2FTE.ForceGrounded(player.opControlsScript);

                ReleaseStun(player.opControlsScript);
            }
        }

        private void OnButton(ButtonPress button, ControlsScript player)
        {
            SetComboBreakerOnButton(button, player);
        }

        #endregion

        #region UFE Helper Methods 

        private void SetPlayerPosition(ControlsScript player, FPVector position, FPVector offset)
        {
            offset.x *= player.mirror;

            player.worldTransform.position = position + offset;
        }

        private void ReleaseStun(ControlsScript player)
        {
            player.ReleaseStun();

            //player.isAirRecovering = false;
        }

        private Fix64 GetAppliedGravity(ControlsScript player)
        {
            Fix64 appliedGravity = player.Physics.appliedGravity;

            return appliedGravity;
        }

        private void SetAppliedGravity(ControlsScript player, Fix64 appliedGravity)
        {
            player.Physics.appliedGravity = appliedGravity;
        }

        private void ApplyNewWeight(ControlsScript player, Fix64 newWeight)
        {
            player.Physics.ApplyNewWeight(newWeight);
        }

        private void ResetWeight(ControlsScript player)
        {
            player.Physics.ResetWeight();
        }

        #endregion
       */
    }
}