using System;
using UnityEngine;
using UFE3D;
using UFENetcode;
using FPLibrary;

namespace UFE2FTE
{
    public class SoulsLikeFightingGameController : UFEBehaviour, UFEInterface
    {
        // Keep an eye out for potential desyncs caused by this script.

        public delegate void ControlsScriptHandler(ControlsScript player);
        public static event ControlsScriptHandler OnBlockBreak;

        public static void CallOnBlockBreak(ControlsScript player)
        {
            if (OnBlockBreak == null)
            {
                return;
            }
            
            OnBlockBreak(player);
        }

        [Serializable]
        private class GaugeAmountOptions
        {
            public enum EventType
            {
                OnBlock,
                OnParry
            }
            public EventType[] eventTypes;
            public GaugeId[] gaugeIds;
            public HitStrengh[] hitStrengths;
            public bool usePlayer;
            public bool usePlayerOpponent;
            public enum GaugeGainType
            {
                Set,
                Add
            }     
            public GaugeGainType gaugeGainType;
            [Fix64Range(-100, 100)]
            public Fix64 percentAmount;

        }
        [SerializeField]
        private GaugeAmountOptions[] gaugeAmountOptions;

        [Serializable]
        private class StopGaugeDrainOptions
        {
            public SubStates[] stopGaugeDrainSubStates;
        }
        [SerializeField]
        private StopGaugeDrainOptions stopGaugeDrainOptions;

        [Serializable]
        private class StaminaOptions
        {
            public GaugeId staminaGaugeId;
            [Fix64Range(-100, 100)]
            public Fix64 passiveStaminaGaugePercentAmount;
            public BasicMoveReference[] noStaminaGaugePercentAmountBasicMoves;
        }
        [SerializeField]
        private StaminaOptions staminaOptions;

        [Serializable]
        private class ParryOptions
        {
            public GaugeId parryGaugeId;
            [Fix64Range(-100, 100)]
            public Fix64 passiveParryGaugePercentAmount;
            public ButtonPress parryButtonPress;

            public bool useParryConfirmMove;
            public Fix64 parryConfirmFreezingTime = 0.25;
            public bool useKillAllPlayerMovesOnParryConfirmMove;
            public string[] parryConfirmMoveNames =
                {"Stand Parry Confirm",
                "Crouch Parry Confirm",
                "Jump Parry Confirm"};
            public bool useParryConfirmReactBasicMove;
            public HitStrengh parryConfirmReactBasicMoveHitStrength;
            [HideInInspector]
            public string standParryConfirmReactBasicMoveAnimationName;
            [HideInInspector]
            public string crouchParryConfirmReactBasicMoveAnimationName;
            [HideInInspector]
            public string jumpParryConfirmReactBasicMoveAnimationName;
            public int parryConfirmReactBasicMoveStunFrames = 60;

            public bool useParryConfirmReactMove;
            public string standParryConfirmReactMoveName = "Stand Parry Confirm React";
            public string crouchParryConfirmReactMoveName = "Crouch Parry Confirm React";
            public string jumpParryConfirmReactMoveName = "Jump Parry Confirm React";
        }
        [SerializeField]
        private ParryOptions parryOptions;

        [Serializable]
        private class BlockBreakOptions
        {
            public bool useBlockBreakOnBlockOn0Stamina;
            public bool useBlockBreakOnParryOn0Stamina;
            public bool useAutoBlockBreakOnParry;
            public bool useResetPlayerStunOnParryBlockBreak;

            public bool useBlockBreakBasicMove;
            public HitStrengh blockBreakBasicMoveHitStrength;
            [HideInInspector]
            public string standHighBlockBreakBasicMoveAnimationName;
            [HideInInspector]
            public string standLowBlockBreakBasicMoveAnimationName;
            [HideInInspector]
            public string crouchBlockBreakBasicMoveAnimationName;
            [HideInInspector]
            public string jumpBlockBreakBasicMoveAnimationName;
            public int blockBreakBasicMoveStunFrames = 120;

            public bool useBlockBreakMove;
            public string standHighBlockBreakMoveName = "Stand High Block Break";
            public string standLowBlockBreakMoveName = "Stand Low Block Break";
            public string crouchBlockBreakMoveName = "Crouch Block Break";
            public string jumpBlockBreakMoveName = "Jump Block Break";
        }
        [SerializeField]
        private BlockBreakOptions blockBreakOptions;

        [Serializable]
        private class CharacterShakeOptions
        {
            public string[] characterShakeMoveNames;
            public enum CharacterShakeStartMode
            {
                EveryFrame,
                StartFrame
            }
            public CharacterShakeStartMode characterShakeStartMode;
            public int characterShakeStartFrame;
            public FPVector characterShakeOffset;
        }
        [SerializeField]
        private CharacterShakeOptions[] characterShakeOptions;

        private void OnEnable()
        {
            UFE.OnRoundBegins += OnRoundBegins;
            //UFE.OnHit += OnHit;
            UFE.OnBlock += OnBlock;
            UFE.OnParry += OnParry;
            UFE.OnMove += OnMove;
            UFE.OnButton += OnButton;
        }

        private void Start()
        {
            SetParryConfirmReactBasicMoveAnimationNames();

            SetBlockBreakBasicMoveAnimationNames();
        }

        private void OnDestroy()
        {
            UFE.OnRoundBegins -= OnRoundBegins;
            //UFE.OnHit -= OnHit;
            UFE.OnBlock -= OnBlock;
            UFE.OnParry -= OnParry;
            UFE.OnMove -= OnMove;
            UFE.OnButton -= OnButton;
        }

        public override void UFEFixedUpdate()
        {
            SetStopGaugeDrain(UFE.GetPlayer1ControlsScript());

            SetStopGaugeDrain(UFE.GetPlayer2ControlsScript());

            SetStaminaGauge(UFE.GetPlayer1ControlsScript());

            SetStaminaGauge(UFE.GetPlayer2ControlsScript());

            SetParryGauge(UFE.GetPlayer1ControlsScript());

            SetParryGauge(UFE.GetPlayer2ControlsScript());

            SetBlockBreakOnBlock(UFE.GetPlayer1ControlsScript());

            SetBlockBreakOnBlock(UFE.GetPlayer2ControlsScript());

            SetBlockBreakOnParry(UFE.GetPlayer1ControlsScript());

            SetBlockBreakOnParry(UFE.GetPlayer2ControlsScript());

            SetCharacterShakeOptions(UFE.GetPlayer1ControlsScript());

            SetCharacterShakeOptions(UFE.GetPlayer2ControlsScript());
        }

        #region UFE Events Methods

        private void OnRoundBegins(int newInt)
        {
            UFE2FTE.SetGaugePointsPercent(UFE.GetPlayer1ControlsScript(), staminaOptions.staminaGaugeId, 100);

            UFE2FTE.SetGaugePointsPercent(UFE.GetPlayer2ControlsScript(), staminaOptions.staminaGaugeId, 100);

            UFE2FTE.SetGaugePointsPercent(UFE.GetPlayer1ControlsScript(), parryOptions.parryGaugeId, 100);

            UFE2FTE.SetGaugePointsPercent(UFE.GetPlayer2ControlsScript(), parryOptions.parryGaugeId, 100);
        }

        private void OnHit(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {

        }

        private void OnBlock(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            SetGaugeAmountOptions(GaugeAmountOptions.EventType.OnBlock, gaugeAmountOptions, hitInfo, player);
        }

        private void OnParry(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            SetGaugeAmountOptions(GaugeAmountOptions.EventType.OnParry, gaugeAmountOptions, hitInfo, player);
        }

        private void OnMove(MoveInfo move, ControlsScript player)
        {
            if (move == null)
            {
                return;
            }

            SetParryConfirmOnMove(move, player);
        }

        private void OnButton(ButtonPress button, ControlsScript player)
        {
            SetParryOptionsOnButton(button, player);
        }

        private void SetParryOptionsOnButton(ButtonPress button, ControlsScript player)
        {
            if (player.currentGaugesPoints[(int)parryOptions.parryGaugeId] >= player.myInfo.maxGaugePoints
                && button == parryOptions.parryButtonPress
                && player.currentMove == null
                && player.isBlocking == true)
            {
                player.potentialParry = UFE.config.blockOptions._parryTiming;

                UFE2FTE.SetGaugePointsPercent(player, parryOptions.parryGaugeId, 0);
            }
        }

        #endregion

        #region Gauge Amount Options Methods

        private void SetGaugeAmountOptions(GaugeAmountOptions.EventType eventType, GaugeAmountOptions[] gaugeAmountOptions, Hit hitInfo, ControlsScript player)
        {
            int length = gaugeAmountOptions.Length;
            for (int i = 0; i < length; i++)
            {
                int lengthA = gaugeAmountOptions[i].eventTypes.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (eventType != gaugeAmountOptions[i].eventTypes[a])
                    {
                        continue;
                    }

                    int lengthB = gaugeAmountOptions[i].hitStrengths.Length;
                    for (int b = 0; b < lengthB; b++)
                    {
                        if (hitInfo.hitStrength != gaugeAmountOptions[i].hitStrengths[b])
                        {
                            continue;
                        }

                        if (gaugeAmountOptions[i].usePlayer == true)
                        {
                            int lengthC = gaugeAmountOptions[i].gaugeIds.Length;
                            for (int c = 0; c < lengthC; c++)
                            {
                                switch (gaugeAmountOptions[i].gaugeGainType)
                                {
                                    case GaugeAmountOptions.GaugeGainType.Set:
                                        UFE2FTE.SetGaugePointsPercent(player, gaugeAmountOptions[i].gaugeIds[c], gaugeAmountOptions[i].percentAmount);
                                        break;

                                    case GaugeAmountOptions.GaugeGainType.Add:
                                        UFE2FTE.AddOrSubtractGaugePointsPercent(player, gaugeAmountOptions[i].gaugeIds[c], gaugeAmountOptions[i].percentAmount);
                                        break;
                                }
                            }
                        }

                        if (gaugeAmountOptions[i].usePlayerOpponent == true)
                        {
                            int lengthC = gaugeAmountOptions[i].gaugeIds.Length;
                            for (int c = 0; c < lengthC; c++)
                            {
                                switch (gaugeAmountOptions[i].gaugeGainType)
                                {
                                    case GaugeAmountOptions.GaugeGainType.Set:
                                        UFE2FTE.SetGaugePointsPercent(player.opControlsScript, gaugeAmountOptions[i].gaugeIds[c], gaugeAmountOptions[i].percentAmount);
                                        break;

                                    case GaugeAmountOptions.GaugeGainType.Add:
                                        UFE2FTE.AddOrSubtractGaugePointsPercent(player.opControlsScript, gaugeAmountOptions[i].gaugeIds[c], gaugeAmountOptions[i].percentAmount);
                                        break;
                                }
                            }
                        }

                        break;
                    }

                    break;
                }
            }
        }

        #endregion

        #region Stop Gauge Drain Options Methods

        private void SetStopGaugeDrain(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            int length = stopGaugeDrainOptions.stopGaugeDrainSubStates.Length;
            for (int i = 0; i < length; i++)
            {
                if (player.currentSubState != stopGaugeDrainOptions.stopGaugeDrainSubStates[i])
                {
                    continue;
                }

                player.gaugeDPS = 0;

                break;
            }
        }

        #endregion

        #region Stamina Options Methods

        private void SetStaminaGauge(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            if (player.currentMove == null)
            {
                int length = staminaOptions.noStaminaGaugePercentAmountBasicMoves.Length;
                for (int i = 0; i < length; i++)
                {
                    if (player.currentBasicMoveReference != staminaOptions.noStaminaGaugePercentAmountBasicMoves[i])
                    {
                        continue;
                    }

                    UFE2FTE.AddOrSubtractGaugePointsPercent(player, staminaOptions.staminaGaugeId, staminaOptions.passiveStaminaGaugePercentAmount);

                    break;
                }
            }
        }

        #endregion

        #region Parry Options Methods

        private void SetParryGauge(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            UFE2FTE.AddOrSubtractGaugePointsPercent(player, parryOptions.parryGaugeId, parryOptions.passiveParryGaugePercentAmount);
        }

        private void SetParryConfirmReactBasicMoveAnimationNames()
        {
            switch (parryOptions.parryConfirmReactBasicMoveHitStrength)
            {
                case HitStrengh.Weak:
                    parryOptions.standParryConfirmReactBasicMoveAnimationName = "blockingHighHit";

                    parryOptions.crouchParryConfirmReactBasicMoveAnimationName = "blockingCrouchingHit";

                    parryOptions.jumpParryConfirmReactBasicMoveAnimationName = "blockingAirHit";
                    return;

                case HitStrengh.Medium:
                    parryOptions.standParryConfirmReactBasicMoveAnimationName = "blockingHighHit_2";

                    parryOptions.crouchParryConfirmReactBasicMoveAnimationName = "blockingCrouchingHit_2";

                    parryOptions.jumpParryConfirmReactBasicMoveAnimationName = "blockingAirHit_2";
                    return;

                case HitStrengh.Heavy:
                    parryOptions.standParryConfirmReactBasicMoveAnimationName = "blockingHighHit_3";

                    parryOptions.crouchParryConfirmReactBasicMoveAnimationName = "blockingCrouchingHit_3";

                    parryOptions.jumpParryConfirmReactBasicMoveAnimationName = "blockingAirHit_3";
                    return;

                case HitStrengh.Custom1:
                    parryOptions.standParryConfirmReactBasicMoveAnimationName = "blockingHighHit_4";

                    parryOptions.crouchParryConfirmReactBasicMoveAnimationName = "blockingCrouchingHit_4";

                    parryOptions.jumpParryConfirmReactBasicMoveAnimationName = "blockingAirHit_4";
                    return;

                case HitStrengh.Custom2:
                    parryOptions.standParryConfirmReactBasicMoveAnimationName = "blockingHighHit_5";

                    parryOptions.crouchParryConfirmReactBasicMoveAnimationName = "blockingCrouchingHit_5";

                    parryOptions.jumpParryConfirmReactBasicMoveAnimationName = "blockingAirHit_5";
                    return;

                case HitStrengh.Custom3:
                    parryOptions.standParryConfirmReactBasicMoveAnimationName = "blockingHighHit_6";

                    parryOptions.crouchParryConfirmReactBasicMoveAnimationName = "blockingCrouchingHit_6";

                    parryOptions.jumpParryConfirmReactBasicMoveAnimationName = "blockingAirHit_6";
                    return;

                case HitStrengh.Custom4:
                    parryOptions.standParryConfirmReactBasicMoveAnimationName = "blockingHighHit_7";

                    parryOptions.crouchParryConfirmReactBasicMoveAnimationName = "blockingCrouchingHit_7";

                    parryOptions.jumpParryConfirmReactBasicMoveAnimationName = "blockingAirHit_7";
                    return;

                case HitStrengh.Custom5:
                    parryOptions.standParryConfirmReactBasicMoveAnimationName = "blockingHighHit_8";

                    parryOptions.crouchParryConfirmReactBasicMoveAnimationName = "blockingCrouchingHit_8";

                    parryOptions.jumpParryConfirmReactBasicMoveAnimationName = "blockingAirHit_8";
                    return;

                case HitStrengh.Custom6:
                    parryOptions.standParryConfirmReactBasicMoveAnimationName = "blockingHighHit_9";

                    parryOptions.crouchParryConfirmReactBasicMoveAnimationName = "blockingCrouchingHit_9";

                    parryOptions.jumpParryConfirmReactBasicMoveAnimationName = "blockingAirHit_9";
                    return;
            }
        }

        private void SetParryConfirmOnMove(MoveInfo move, ControlsScript player)
        {
            if (player == null
                || parryOptions.useParryConfirmMove == false)
            {
                return;
            }

            int length = parryOptions.parryConfirmMoveNames.Length;
            for (int i = 0; i < length; i++)
            {
                if (move.moveName != parryOptions.parryConfirmMoveNames[i])
                {
                    continue;
                }

                UFE2FTE.HitPause(player, parryOptions.parryConfirmFreezingTime);

                UFE2FTE.HitPause(player.opControlsScript, parryOptions.parryConfirmFreezingTime);

                if (player.playerNum == 1)
                {
                    if (parryOptions.useKillAllPlayerMovesOnParryConfirmMove == true)
                    {
                        UFE.DelaySynchronizedAction(UFE2FTE.KillAllMovesPlayer1, parryOptions.parryConfirmFreezingTime);
                    }

                    UFE.DelaySynchronizedAction(PlayParryConfirmReactBasicMovePlayer2, parryOptions.parryConfirmFreezingTime);

                    UFE.DelaySynchronizedAction(CastParryConfirmReactMovePlayer2, parryOptions.parryConfirmFreezingTime);
                }
                else if (player.playerNum == 2)
                {
                    if (parryOptions.useKillAllPlayerMovesOnParryConfirmMove == true)
                    {
                        UFE.DelaySynchronizedAction(UFE2FTE.KillAllMovesPlayer2, parryOptions.parryConfirmFreezingTime);
                    }

                    UFE.DelaySynchronizedAction(PlayParryConfirmReactBasicMovePlayer1, parryOptions.parryConfirmFreezingTime);

                    UFE.DelaySynchronizedAction(CastParryConfirmReactMovePlayer1, parryOptions.parryConfirmFreezingTime);
                }

                break;
            }
        }

        private void PlayParryConfirmReactBasicMovePlayer1()
        {
            if (parryOptions.useParryConfirmReactBasicMove == false)
            {
                return;
            }

            /*if (dontPlayIfParryConfirmReactIsPlaying == true
                && (UFE.GetPlayer1ControlsScript().currentBasicMove == BasicMoveReference.BlockingHighHit
                || UFE.GetPlayer1ControlsScript().currentBasicMove == BasicMoveReference.BlockingLowHit
                || UFE.GetPlayer1ControlsScript().currentBasicMove == BasicMoveReference.BlockingCrouchingHit
                || UFE.GetPlayer1ControlsScript().currentBasicMove == BasicMoveReference.BlockingAirHit)) return;*/

            UFE2FTE.KillAllMoves(UFE.GetPlayer1ControlsScript());

            BasicMoveInfo basicMoveInfo = null;

            switch (UFE.GetPlayer1ControlsScript().currentState)
            {
                case PossibleStates.Stand:
                    basicMoveInfo = UFE.GetPlayer1ControlsScript().MoveSet.basicMoves.blockingHighHit;

                    UFE.GetPlayer1ControlsScript().currentHitAnimation = parryOptions.standParryConfirmReactBasicMoveAnimationName;

                    UFE.GetPlayer1ControlsScript().Physics.ResetForces(true, true, true);
                    break;

                case PossibleStates.Crouch:
                    basicMoveInfo = UFE.GetPlayer1ControlsScript().MoveSet.basicMoves.blockingCrouchingHit;

                    UFE.GetPlayer1ControlsScript().currentHitAnimation = parryOptions.crouchParryConfirmReactBasicMoveAnimationName;

                    UFE.GetPlayer1ControlsScript().Physics.ResetForces(true, true, true);
                    break;

                case PossibleStates.NeutralJump:
                case PossibleStates.ForwardJump:
                case PossibleStates.BackJump:
                    basicMoveInfo = UFE.GetPlayer1ControlsScript().MoveSet.basicMoves.blockingAirHit;

                    UFE.GetPlayer1ControlsScript().currentHitAnimation = parryOptions.jumpParryConfirmReactBasicMoveAnimationName;

                    ForceGrounded(UFE.GetPlayer1ControlsScript(), 2);

                    UFE.GetPlayer1ControlsScript().Physics.ResetForces(true, true, true);
                    break;
            }

            UFE.GetPlayer1ControlsScript().currentSubState = SubStates.Stunned;

            UFE.GetPlayer1ControlsScript().stunTime = (Fix64)parryOptions.parryConfirmReactBasicMoveStunFrames / UFE.config.fps;

            //UFE.GetPlayer1ControlsScript().MoveSet.PlayBasicMove(basicMoveInfo, UFE.GetPlayer1ControlsScript().currentHitAnimation);

            UFE.GetPlayer1ControlsScript().MoveSet.PlayBasicMove(basicMoveInfo);

            if (basicMoveInfo.autoSpeed == true)
            {
                UFE.GetPlayer1ControlsScript().MoveSet.SetAnimationSpeed(UFE.GetPlayer1ControlsScript().currentHitAnimation, (UFE.GetPlayer1ControlsScript().MoveSet.GetAnimationLength(UFE.GetPlayer1ControlsScript().currentHitAnimation) / UFE.GetPlayer1ControlsScript().stunTime));
            }
        }

        private void PlayParryConfirmReactBasicMovePlayer2()
        {
            if (parryOptions.useParryConfirmReactBasicMove == false)
            {
                return;
            }

            /*if (dontPlayIfParryConfirmReactIsPlaying == true
                && (UFE.GetPlayer2ControlsScript().currentBasicMove == BasicMoveReference.BlockingHighHit
                || UFE.GetPlayer2ControlsScript().currentBasicMove == BasicMoveReference.BlockingLowHit
                || UFE.GetPlayer2ControlsScript().currentBasicMove == BasicMoveReference.BlockingCrouchingHit
                || UFE.GetPlayer2ControlsScript().currentBasicMove == BasicMoveReference.BlockingAirHit)) return;*/

            UFE2FTE.KillAllMoves(UFE.GetPlayer2ControlsScript());

            BasicMoveInfo basicMoveInfo = null;

            switch (UFE.GetPlayer2ControlsScript().currentState)
            {
                case PossibleStates.Stand:
                    basicMoveInfo = UFE.GetPlayer2ControlsScript().MoveSet.basicMoves.blockingHighHit;

                    UFE.GetPlayer2ControlsScript().currentHitAnimation = parryOptions.standParryConfirmReactBasicMoveAnimationName;

                    UFE.GetPlayer1ControlsScript().Physics.ResetForces( true, true, true);
                    break;

                case PossibleStates.Crouch:
                    basicMoveInfo = UFE.GetPlayer2ControlsScript().MoveSet.basicMoves.blockingCrouchingHit;

                    UFE.GetPlayer2ControlsScript().currentHitAnimation = parryOptions.crouchParryConfirmReactBasicMoveAnimationName;

                    UFE.GetPlayer2ControlsScript().Physics.ResetForces(true, true, true);
                    break;

                case PossibleStates.NeutralJump:
                case PossibleStates.ForwardJump:
                case PossibleStates.BackJump:
                    basicMoveInfo = UFE.GetPlayer2ControlsScript().MoveSet.basicMoves.blockingAirHit;

                    UFE.GetPlayer2ControlsScript().currentHitAnimation = parryOptions.jumpParryConfirmReactBasicMoveAnimationName;

                    ForceGrounded(UFE.GetPlayer2ControlsScript(), 2);

                    UFE.GetPlayer2ControlsScript().Physics.ResetForces(true, true, true);
                    break;
            }

            UFE.GetPlayer2ControlsScript().currentSubState = SubStates.Stunned;

            UFE.GetPlayer2ControlsScript().stunTime = (Fix64)parryOptions.parryConfirmReactBasicMoveStunFrames / UFE.config.fps;

            //UFE.GetPlayer2ControlsScript().MoveSet.PlayBasicMove(basicMoveInfo, UFE.GetPlayer2ControlsScript().currentHitAnimation);

            UFE.GetPlayer2ControlsScript().MoveSet.PlayBasicMove(basicMoveInfo);

            if (basicMoveInfo.autoSpeed == true)
            {
                UFE.GetPlayer2ControlsScript().MoveSet.SetAnimationSpeed(UFE.GetPlayer2ControlsScript().currentHitAnimation, (UFE.GetPlayer2ControlsScript().MoveSet.GetAnimationLength(UFE.GetPlayer2ControlsScript().currentHitAnimation) / UFE.GetPlayer2ControlsScript().stunTime));
            }
        }

        private void CastParryConfirmReactMovePlayer1()
        {
            if (parryOptions.useParryConfirmReactMove == false)
            {
                return;
            }

            switch (UFE.GetPlayer1ControlsScript().currentState)
            {
                case PossibleStates.Stand:
                    UFE2FTE.CastMoveByMoveName(UFE.GetPlayer1ControlsScript(), parryOptions.standParryConfirmReactMoveName);
                    break;

                case PossibleStates.Crouch:
                    UFE2FTE.CastMoveByMoveName(UFE.GetPlayer1ControlsScript(), parryOptions.crouchParryConfirmReactMoveName);
                    break;

                case PossibleStates.NeutralJump:
                case PossibleStates.ForwardJump:
                case PossibleStates.BackJump:
                    UFE2FTE.CastMoveByMoveName(UFE.GetPlayer1ControlsScript(), parryOptions.jumpParryConfirmReactMoveName);
                    break;
            }
        }

        private void CastParryConfirmReactMovePlayer2()
        {
            if (parryOptions.useParryConfirmReactMove == false)
            {
                return;
            }

            switch (UFE.GetPlayer2ControlsScript().currentState)
            {
                case PossibleStates.Stand:
                    UFE2FTE.CastMoveByMoveName(UFE.GetPlayer2ControlsScript(), parryOptions.standParryConfirmReactMoveName);
                    break;

                case PossibleStates.Crouch:
                    UFE2FTE.CastMoveByMoveName(UFE.GetPlayer2ControlsScript(), parryOptions.crouchParryConfirmReactMoveName);
                    break;

                case PossibleStates.NeutralJump:
                case PossibleStates.ForwardJump:
                case PossibleStates.BackJump:
                    UFE2FTE.CastMoveByMoveName(UFE.GetPlayer2ControlsScript(), parryOptions.jumpParryConfirmReactMoveName);
                    break;
            }
        }

        #endregion

        #region Block Break Options Methods

        private void SetBlockBreakBasicMoveAnimationNames()
        {
            switch (blockBreakOptions.blockBreakBasicMoveHitStrength)
            {
                case HitStrengh.Weak:
                    blockBreakOptions.standHighBlockBreakBasicMoveAnimationName = "blockingHighHit";

                    blockBreakOptions.standLowBlockBreakBasicMoveAnimationName = "blockingLowHit";

                    blockBreakOptions.crouchBlockBreakBasicMoveAnimationName = "blockingCrouchingHit";

                    blockBreakOptions.jumpBlockBreakBasicMoveAnimationName = "blockingAirHit";
                    return;

                case HitStrengh.Medium:
                    blockBreakOptions.standHighBlockBreakBasicMoveAnimationName = "blockingHighHit_2";

                    blockBreakOptions.standLowBlockBreakBasicMoveAnimationName = "blockingLowHit_2";

                    blockBreakOptions.crouchBlockBreakBasicMoveAnimationName = "blockingCrouchingHit_2";

                    blockBreakOptions.jumpBlockBreakBasicMoveAnimationName = "blockingAirHit_2";
                    return;

                case HitStrengh.Heavy:
                    blockBreakOptions.standHighBlockBreakBasicMoveAnimationName = "blockingHighHit_3";

                    blockBreakOptions.standLowBlockBreakBasicMoveAnimationName = "blockingLowHit_3";

                    blockBreakOptions.crouchBlockBreakBasicMoveAnimationName = "blockingCrouchingHit_3";

                    blockBreakOptions.jumpBlockBreakBasicMoveAnimationName = "blockingAirHit_3";
                    return;

                case HitStrengh.Custom1:
                    blockBreakOptions.standHighBlockBreakBasicMoveAnimationName = "blockingHighHit_4";

                    blockBreakOptions.standLowBlockBreakBasicMoveAnimationName = "blockingLowHit_4";

                    blockBreakOptions.crouchBlockBreakBasicMoveAnimationName = "blockingCrouchingHit_4";

                    blockBreakOptions.jumpBlockBreakBasicMoveAnimationName = "blockingAirHit_4";
                    return;

                case HitStrengh.Custom2:
                    blockBreakOptions.standHighBlockBreakBasicMoveAnimationName = "blockingHighHit_5";

                    blockBreakOptions.standLowBlockBreakBasicMoveAnimationName = "blockingLowHit_5";

                    blockBreakOptions.crouchBlockBreakBasicMoveAnimationName = "blockingCrouchingHit_5";

                    blockBreakOptions.jumpBlockBreakBasicMoveAnimationName = "blockingAirHit_5";
                    return;

                case HitStrengh.Custom3:
                    blockBreakOptions.standHighBlockBreakBasicMoveAnimationName = "blockingHighHit_6";

                    blockBreakOptions.standLowBlockBreakBasicMoveAnimationName = "blockingLowHit_6";

                    blockBreakOptions.crouchBlockBreakBasicMoveAnimationName = "blockingCrouchingHit_6";

                    blockBreakOptions.jumpBlockBreakBasicMoveAnimationName = "blockingAirHit_6";
                    return;

                case HitStrengh.Custom4:
                    blockBreakOptions.standHighBlockBreakBasicMoveAnimationName = "blockingHighHit_7";

                    blockBreakOptions.standLowBlockBreakBasicMoveAnimationName = "blockingLowHit_7";

                    blockBreakOptions.crouchBlockBreakBasicMoveAnimationName = "blockingCrouchingHit_7";

                    blockBreakOptions.jumpBlockBreakBasicMoveAnimationName = "blockingAirHit_7";
                    return;

                case HitStrengh.Custom5:
                    blockBreakOptions.standHighBlockBreakBasicMoveAnimationName = "blockingHighHit_8";

                    blockBreakOptions.standLowBlockBreakBasicMoveAnimationName = "blockingLowHit_8";

                    blockBreakOptions.crouchBlockBreakBasicMoveAnimationName = "blockingCrouchingHit_8";

                    blockBreakOptions.jumpBlockBreakBasicMoveAnimationName = "blockingAirHit_8";
                    return;

                case HitStrengh.Custom6:
                    blockBreakOptions.standHighBlockBreakBasicMoveAnimationName = "blockingHighHit_9";

                    blockBreakOptions.standLowBlockBreakBasicMoveAnimationName = "blockingLowHit_9";

                    blockBreakOptions.crouchBlockBreakBasicMoveAnimationName = "blockingCrouchingHit_9";

                    blockBreakOptions.jumpBlockBreakBasicMoveAnimationName = "blockingAirHit_9";
                    return;
            }
        }

        private void SetBlockBreakOnBlock(ControlsScript player)
        {
            if (blockBreakOptions.useBlockBreakOnBlockOn0Stamina == true)
            {
                if (player.currentGaugesPoints[(int)staminaOptions.staminaGaugeId] <= 0
                    && player.currentBasicMoveReference == BasicMoveReference.BlockingHighHit
                    && player.currentSubState == SubStates.Blocking
                    && player.Physics.freeze == false)
                {
                    PlayBlockBreakBasicMove(player, BasicMoveReference.BlockingHighHit);

                    CastBlockBreakMove(player, BasicMoveReference.BlockingHighHit);

                    CallOnBlockBreak(player);
                }
                else if (player.currentGaugesPoints[(int)staminaOptions.staminaGaugeId] <= 0
                    && player.currentBasicMoveReference == BasicMoveReference.BlockingLowHit
                    && player.currentSubState == SubStates.Blocking
                    && player.Physics.freeze == false)
                {
                    PlayBlockBreakBasicMove(player, BasicMoveReference.BlockingLowHit);

                    CastBlockBreakMove(player, BasicMoveReference.BlockingLowHit);

                    CallOnBlockBreak(player);
                }
                else if (player.currentGaugesPoints[(int)staminaOptions.staminaGaugeId] <= 0
                    && player.currentBasicMoveReference == BasicMoveReference.BlockingCrouchingHit
                    && player.currentSubState == SubStates.Blocking
                    && player.Physics.freeze == false)
                {
                    PlayBlockBreakBasicMove(player, BasicMoveReference.BlockingCrouchingHit);

                    CastBlockBreakMove(player, BasicMoveReference.BlockingCrouchingHit);

                    CallOnBlockBreak(player);
                }
                else if (player.currentGaugesPoints[(int)staminaOptions.staminaGaugeId] <= 0
                    && player.currentBasicMoveReference == BasicMoveReference.BlockingAirHit
                    && player.currentSubState == SubStates.Blocking
                    && player.Physics.freeze == false)
                {
                    PlayBlockBreakBasicMove(player, BasicMoveReference.BlockingAirHit);

                    CastBlockBreakMove(player, BasicMoveReference.BlockingAirHit);

                    CallOnBlockBreak(player);
                }
            }
        }

        private void SetBlockBreakOnParry(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            if (blockBreakOptions.useBlockBreakOnParryOn0Stamina == true)
            {
                if (player.opControlsScript.currentGaugesPoints[(int)staminaOptions.staminaGaugeId] <= 0
                    && player.currentBasicMoveReference == BasicMoveReference.ParryHigh
                    && player.Physics.freeze == false)
                {
                    if (blockBreakOptions.useResetPlayerStunOnParryBlockBreak == true)
                    {
                        player.ReleaseStun();
                    }

                    PlayBlockBreakBasicMove(player.opControlsScript, BasicMoveReference.Idle, true);

                    CastBlockBreakMove(player, BasicMoveReference.Idle);
                }
                else if (player.opControlsScript.currentGaugesPoints[(int)staminaOptions.staminaGaugeId] <= 0
                    && player.currentBasicMoveReference == BasicMoveReference.ParryLow
                    && player.Physics.freeze == false)
                {
                    if (blockBreakOptions.useResetPlayerStunOnParryBlockBreak == true)
                    {
                        player.ReleaseStun();
                    }

                    PlayBlockBreakBasicMove(player.opControlsScript, BasicMoveReference.Idle, true);

                    CastBlockBreakMove(player, BasicMoveReference.Idle);
                }
                else if (player.opControlsScript.currentGaugesPoints[(int)staminaOptions.staminaGaugeId] <= 0
                    && player.currentBasicMoveReference == BasicMoveReference.ParryCrouching
                    && player.Physics.freeze == false)
                {
                    if (blockBreakOptions.useResetPlayerStunOnParryBlockBreak == true)
                    {
                        player.ReleaseStun();
                    }

                    PlayBlockBreakBasicMove(player.opControlsScript, BasicMoveReference.Idle, true);

                    CastBlockBreakMove(player, BasicMoveReference.Idle);
                }
                else if (player.opControlsScript.currentGaugesPoints[(int)staminaOptions.staminaGaugeId] <= 0
                    && player.currentBasicMoveReference == BasicMoveReference.ParryAir
                    && player.Physics.freeze == false)
                {
                    if (blockBreakOptions.useResetPlayerStunOnParryBlockBreak == true)
                    {
                        player.ReleaseStun();
                    }

                    PlayBlockBreakBasicMove(player.opControlsScript, BasicMoveReference.Idle, true);

                    CastBlockBreakMove(player, BasicMoveReference.Idle);
                }
            }

            if (blockBreakOptions.useAutoBlockBreakOnParry == true)
            {
                if (player.currentBasicMoveReference == BasicMoveReference.ParryHigh
                    && player.Physics.freeze == false)
                {
                    if (blockBreakOptions.useResetPlayerStunOnParryBlockBreak == true)
                    {
                        player.ReleaseStun();
                    }

                    PlayBlockBreakBasicMove(player.opControlsScript, BasicMoveReference.Idle, true);

                    CastBlockBreakMove(player, BasicMoveReference.Idle);
                }
                else if (player.currentBasicMoveReference == BasicMoveReference.ParryLow
                    && player.Physics.freeze == false)
                {
                    if (blockBreakOptions.useResetPlayerStunOnParryBlockBreak == true)
                    {
                        player.ReleaseStun();
                    }

                    PlayBlockBreakBasicMove(player.opControlsScript, BasicMoveReference.Idle, true);

                    CastBlockBreakMove(player, BasicMoveReference.Idle);
                }
                else if (player.currentBasicMoveReference == BasicMoveReference.ParryCrouching
                    && player.Physics.freeze == false)
                {
                    if (blockBreakOptions.useResetPlayerStunOnParryBlockBreak == true)
                    {
                        player.ReleaseStun();
                    }

                    PlayBlockBreakBasicMove(player.opControlsScript, BasicMoveReference.Idle, true);

                    CastBlockBreakMove(player, BasicMoveReference.Idle);
                }
                else if (player.currentBasicMoveReference == BasicMoveReference.ParryAir
                    && player.Physics.freeze == false)
                {
                    if (blockBreakOptions.useResetPlayerStunOnParryBlockBreak == true)
                    {
                        player.ReleaseStun();
                    }

                    PlayBlockBreakBasicMove(player.opControlsScript, BasicMoveReference.Idle, true);

                    CastBlockBreakMove(player, BasicMoveReference.Idle);
                }
            }
        }

        private void PlayBlockBreakBasicMove(ControlsScript player, BasicMoveReference basicMove = BasicMoveReference.Idle, bool dontPlayIfBlockBreakIsPlaying = false)
        {
            if (player == null
                || blockBreakOptions.useBlockBreakBasicMove == false)
            {
                return;
            }

            if (dontPlayIfBlockBreakIsPlaying == true
                && (player.currentBasicMoveReference == BasicMoveReference.BlockingHighHit
                || player.currentBasicMoveReference == BasicMoveReference.BlockingLowHit
                || player.currentBasicMoveReference == BasicMoveReference.BlockingCrouchingHit
                || player.currentBasicMoveReference == BasicMoveReference.BlockingAirHit))
            {
                return;
            }

            UFE2FTE.KillAllMoves(player);

            BasicMoveInfo basicMoveInfo = null;

            switch (basicMove)
            {
                case BasicMoveReference.BlockingHighHit:
                    basicMoveInfo = player.MoveSet.basicMoves.blockingHighHit;

                    player.currentHitAnimation = blockBreakOptions.standHighBlockBreakBasicMoveAnimationName;
                    break;

                case BasicMoveReference.BlockingLowHit:
                    basicMoveInfo = player.MoveSet.basicMoves.blockingLowHit;

                    player.currentHitAnimation = blockBreakOptions.standLowBlockBreakBasicMoveAnimationName;
                    break;

                case BasicMoveReference.BlockingCrouchingHit:
                    basicMoveInfo = player.MoveSet.basicMoves.blockingCrouchingHit;

                    player.currentHitAnimation = blockBreakOptions.crouchBlockBreakBasicMoveAnimationName;
                    break;

                case BasicMoveReference.BlockingAirHit:
                    basicMoveInfo = player.MoveSet.basicMoves.blockingAirHit;

                    player.currentHitAnimation = blockBreakOptions.jumpBlockBreakBasicMoveAnimationName;

                    ForceGrounded(player, 2);

                    player.Physics.ResetForces(true, true, true);
                    break;

                default:
                    switch (player.currentState)
                    {
                        case PossibleStates.Stand:
                            basicMoveInfo = player.MoveSet.basicMoves.blockingHighHit;

                            player.currentHitAnimation = blockBreakOptions.standHighBlockBreakBasicMoveAnimationName;
                            break;

                        case PossibleStates.Crouch:
                            basicMoveInfo = player.MoveSet.basicMoves.blockingCrouchingHit;

                            player.currentHitAnimation = blockBreakOptions.crouchBlockBreakBasicMoveAnimationName;
                            break;

                        case PossibleStates.NeutralJump:
                        case PossibleStates.ForwardJump:
                        case PossibleStates.BackJump:
                            basicMoveInfo = player.MoveSet.basicMoves.blockingAirHit;

                            player.currentHitAnimation = blockBreakOptions.jumpBlockBreakBasicMoveAnimationName;

                            ForceGrounded(player, 2);

                            player.Physics.ResetForces(true, true, true);
                            break;
                    }
                    break;
            }

            player.currentSubState = SubStates.Stunned;

            player.stunTime = (Fix64)blockBreakOptions.blockBreakBasicMoveStunFrames / UFE.config.fps;

            //player.MoveSet.PlayBasicMove(basicMoveInfo, player.currentHitAnimation);

            player.MoveSet.PlayBasicMove(basicMoveInfo);

            if (basicMoveInfo.autoSpeed == true)
            {
                player.MoveSet.SetAnimationSpeed(player.currentHitAnimation, (player.MoveSet.GetAnimationLength(player.currentHitAnimation) / player.stunTime));
            }
        }

        private void CastBlockBreakMove(ControlsScript player, BasicMoveReference basicMove = BasicMoveReference.Idle)
        {
            if (player == null
                || blockBreakOptions.useBlockBreakMove == false)
            {
                return;
            }

            switch (basicMove)
            {
                case BasicMoveReference.BlockingHighHit:
                    UFE2FTE.CastMoveByMoveName(player, blockBreakOptions.standHighBlockBreakMoveName);
                    break;

                case BasicMoveReference.BlockingLowHit:
                    UFE2FTE.CastMoveByMoveName(player, blockBreakOptions.standLowBlockBreakMoveName);
                    break;

                case BasicMoveReference.BlockingCrouchingHit:
                    UFE2FTE.CastMoveByMoveName(player, blockBreakOptions.crouchBlockBreakMoveName);
                    break;

                case BasicMoveReference.BlockingAirHit:
                    UFE2FTE.CastMoveByMoveName(player, blockBreakOptions.jumpBlockBreakMoveName);
                    break;

                default:
                    switch (player.currentState)
                    {
                        case PossibleStates.Stand:
                            UFE2FTE.CastMoveByMoveName(player, blockBreakOptions.standHighBlockBreakMoveName);
                            break;

                        case PossibleStates.Crouch:
                            UFE2FTE.CastMoveByMoveName(player, blockBreakOptions.crouchBlockBreakMoveName);
                            break;

                        case PossibleStates.NeutralJump:
                        case PossibleStates.ForwardJump:
                        case PossibleStates.BackJump:
                            UFE2FTE.CastMoveByMoveName(player, blockBreakOptions.jumpBlockBreakMoveName);
                            break;
                    }
                    break;
            }
        }

        #endregion

        #region Character Shake Options Methods

        private void SetCharacterShakeOptions(ControlsScript player)
        {
            if (player.currentMove == null)
            {
                return;
            }

            int length = characterShakeOptions.Length;
            for (int i = 0; i < length; i++)
            {
                int lengthA = characterShakeOptions[i].characterShakeMoveNames.Length;
                for (int a = 0; a < lengthA; a++)
                {
                    if (player.currentMove.moveName != characterShakeOptions[i].characterShakeMoveNames[a])
                    {
                        continue;
                    }

                    switch (characterShakeOptions[i].characterShakeStartMode)
                    {
                        case CharacterShakeOptions.CharacterShakeStartMode.EveryFrame:                          
                            UFE2FTE.ShakeCharacterPosition(player, characterShakeOptions[i].characterShakeOffset);
                            break;

                        case CharacterShakeOptions.CharacterShakeStartMode.StartFrame:
                            if (player.currentMove.currentFrame >= characterShakeOptions[i].characterShakeStartFrame)
                            {
                                UFE2FTE.ShakeCharacterPosition(player, characterShakeOptions[i].characterShakeOffset);
                            }
                            break;
                    }

                    break;
                }
            }
        }

        #endregion

        public static void ForceGrounded(ControlsScript player, int timesToExecute = 1)
        {
            if (player == null
                || player.Physics == null)
            {
                return;
            }

            for (int i = 0; i < timesToExecute; i++)
            {
                player.Physics.ForceGrounded();
            }
        }
    }
}