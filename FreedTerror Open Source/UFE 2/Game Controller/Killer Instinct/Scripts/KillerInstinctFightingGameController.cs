using FPLibrary;
using UFE3D;
using UFENetcode;
using UnityEngine;

namespace FreedTerror.UFE2
{
    public class KillerInstinctFightingGameController : UFEBehaviour, UFEInterface
    {
        [System.Serializable]
        public class HitStrengthGaugeOptions
        {
            public HitStrengh hitStrength;
            [Fix64Range(-100f, 100f)]
            public Fix64 gaugePercentAmount;
        }

        #region Combo Value Methods

        [System.Serializable]
        public class ComboValueOptions
        {
            public GaugeId gaugeId;
            public HitStrengthGaugeOptions[] hitStrengthGaugeOptionsArray;
        }
        [SerializeField]
        private ComboValueOptions comboValueOptions;

        private void UpdateComboValueGauge(ControlsScript player)
        {
            if (player == null
                || player.opControlsScript == null)
            {
                return;
            }

            if (player.opControlsScript.currentSubState != SubStates.Stunned)
            {
                UFE2Manager.AddOrSubtractGaugePointsPercent(player, comboValueOptions.gaugeId, -(Fix64)100);
            }

            if (player.currentGaugesPoints[(int)comboValueOptions.gaugeId] >= player.myInfo.maxGaugePoints)
            {
                if (player.opControlsScript.Physics != null
                    && player.Physics.freeze == false)
                {
                    PlayBlowOutBasicMove(player.opControlsScript);

                    CastBlowOutMove(player.opControlsScript);

                    UFE2Manager.AddOrSubtractGaugePointsPercent(player, comboValueOptions.gaugeId, -(Fix64)100);
                }
            }
        }

        #endregion

        #region BlowOut Methods

        [System.Serializable]
        public class BlowOutOptions
        {
            public bool useBlowOutBasicMove;
            public bool resetXForce;
            public bool resetYForce;
            public bool resetZForce;
            public FPVector force;
            public bool useComboOptionsJuggleWeightAsWeight;
            public Fix64 weight;
            public bool useBlowOutMove;
            public string blowOutMoveName;

            public Fix64 GetWeight()
            {
                if (useComboOptionsJuggleWeightAsWeight == true)
                {
                    return UFE.config.comboOptions._juggleWeight;
                }
                else
                {
                    return weight;
                }  
            }
        }
        [SerializeField]
        private BlowOutOptions blowOutOptions;

        private void PlayBlowOutBasicMove(ControlsScript player)
        {
            if (blowOutOptions.useBlowOutBasicMove == false
                || player == null
                || player.Physics == null)
            {
                return;
            }

            player.currentState = PossibleStates.NeutralJump;

            player.currentSubState = SubStates.Stunned;

            player.stunTime = 999;

            player.comboHits = UFE.config.comboOptions.maxCombo;

            player.Physics.groundBounceTimes = UFE.config.groundBounceOptions._maximumBounces;

            player.Physics.ResetForces(blowOutOptions.resetXForce, blowOutOptions.resetYForce, blowOutOptions.resetZForce);

            player.Physics.AddForce(blowOutOptions.force, player.GetDirection(), true);

            player.Physics.ApplyNewWeight(blowOutOptions.GetWeight());
        }

        [NaughtyAttributes.Button]
        private void PlayBlowOutBasicMovePlayer1()
        {
            PlayBlowOutBasicMove(UFE.p1ControlsScript);
        }

        [NaughtyAttributes.Button]
        private void PlayBlowOutBasicMovePlayer2()
        {
            PlayBlowOutBasicMove(UFE.p2ControlsScript);
        }

        private void CastBlowOutMove(ControlsScript player)
        {
            if (blowOutOptions.useBlowOutMove == false
              || player == null)
            {
                return;
            }

            UFE2Manager.CastMoveByMoveName(player, blowOutOptions.blowOutMoveName);
        }

        [NaughtyAttributes.Button]
        private void CastBlowOutMovePlayer1()
        {
            CastBlowOutMove(UFE.p1ControlsScript);
        }

        [NaughtyAttributes.Button]
        private void CastBlowOutMovePlayer2()
        {
            CastBlowOutMove(UFE.p2ControlsScript);
        }

        #endregion

        #region Combo Breaker Methods

        [System.Serializable]
        public class ComboBreakerOptions
        {
            public string standComboBreakerAttemptMoveName = "Stand Combo Breaker Attempt";
            public string jumpComboBreakerAttemptMoveName = "Jump Combo Breaker Attempt";

            public GaugeId gaugeId;
            public HitStrengthGaugeOptions[] hitStrengthGaugeOptionsArray;

            [Fix64Range(0, 100)]
            public Fix64 lightComboBreakerRequiredGaugePercent = 25;
            [Fix64Range(0, 100)]
            public Fix64 mediumComboBreakerRequiredGaugePercent = 50;
            [Fix64Range(0, 100)]
            public Fix64 heavyComboBreakerRequiredGaugePercent = 75;
            [Fix64Range(0, 100)]
            public Fix64 anyComboBreakerRequiredGaugePercent = 100;

            public ButtonPress[] lightComboBreakerButtonPressArray;
            public ButtonPress[] mediumComboBreakerButtonPressArray;
            public ButtonPress[] heavyComboBreakerButtonPressArray;
        }
        [SerializeField]
        private ComboBreakerOptions comboBreakerOptions;

        private void UpdateComboBreakerGauge(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            if (player.currentSubState != SubStates.Stunned)
            {
                UFE2Manager.AddOrSubtractGaugePointsPercent(player, comboBreakerOptions.gaugeId, -(Fix64)100);
            }
        }

        private void CastComboBreakerMove(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            if (player.currentState == PossibleStates.Stand)
            {
                UFE2Manager.CastMoveByMoveName(player, comboBreakerOptions.standComboBreakerAttemptMoveName);
                player.ReleaseStun();
            }
            else
            {
                UFE2Manager.CastMoveByMoveName(player, comboBreakerOptions.jumpComboBreakerAttemptMoveName);
                player.ReleaseStun();
            }
        }

        [NaughtyAttributes.Button]
        private void CastComboBreakerMovePlayer1()
        {
            CastComboBreakerMove(UFE.p1ControlsScript);
        }

        [NaughtyAttributes.Button]
        private void CastComboBreakerMovePlayer2()
        {
            CastComboBreakerMove(UFE.p2ControlsScript);
        }

        #endregion

        #region LockOut Methods

        [System.Serializable]
        public class LockOutOptions
        {
            public GaugeId gaugeId;
            [Fix64Range(-100f, 0f)]
            public Fix64 passiveGaugeLossPercentAmount;
        }
        [SerializeField]
        private LockOutOptions lockOutOptions;

        private void UpdateLockOutGauge(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            if (player.currentSubState != SubStates.Stunned)
            {
                UFE2Manager.AddOrSubtractGaugePointsPercent(player, lockOutOptions.gaugeId, -(Fix64)100);
            }

            UFE2Manager.AddOrSubtractGaugePointsPercent(player, lockOutOptions.gaugeId, lockOutOptions.passiveGaugeLossPercentAmount);
        }

        #endregion

        private void OnEnable()
        {
            UFE.OnButton += OnButton;
            UFE.OnHit += OnHit;          
        }

        //private void FixedUpdate()
        public override void UFEFixedUpdate()
        {
            UpdateComboValueGauge(UFE.p1ControlsScript);

            UpdateComboValueGauge(UFE.p2ControlsScript);

            UpdateComboBreakerGauge(UFE.p1ControlsScript);

            UpdateComboBreakerGauge(UFE.p2ControlsScript);

            UpdateLockOutGauge(UFE.p1ControlsScript);

            UpdateLockOutGauge(UFE.p2ControlsScript);
        }

        private void OnDisable ()
        {
            UFE.OnButton -= OnButton;
            UFE.OnHit -= OnHit;
        }

        private void OnButton(ButtonPress button, ControlsScript player)
        {
            if (player.currentGaugesPoints[(int)lockOutOptions.gaugeId] > 0)
            {
                return;
            }

            if (player.currentGaugesPoints[(int)comboBreakerOptions.gaugeId] == player.myInfo.maxGaugePoints * (comboBreakerOptions.lightComboBreakerRequiredGaugePercent / 100))
            {
                if (UFE2Manager.IsButtonPressMatch(button, comboBreakerOptions.lightComboBreakerButtonPressArray) == true)
                {
                    UFE.FireAlert("Combo Breaker L", player);
                    CastComboBreakerMove(player);            
                }
                else if (UFE2Manager.IsButtonPressMatch(button, comboBreakerOptions.mediumComboBreakerButtonPressArray) == true)
                {
                    UFE.FireAlert("Lockout L", player);
                    UFE2Manager.AddOrSubtractGaugePointsPercent(player, lockOutOptions.gaugeId, 75);
                }
                else if (UFE2Manager.IsButtonPressMatch(button, comboBreakerOptions.heavyComboBreakerButtonPressArray) == true)
                {
                    UFE.FireAlert("Lockout L", player);
                    UFE2Manager.AddOrSubtractGaugePointsPercent(player, lockOutOptions.gaugeId, 75);
                }
            }
            else if (player.currentGaugesPoints[(int)comboBreakerOptions.gaugeId] == player.myInfo.maxGaugePoints * (comboBreakerOptions.mediumComboBreakerRequiredGaugePercent / 100))
            {
                if (UFE2Manager.IsButtonPressMatch(button, comboBreakerOptions.lightComboBreakerButtonPressArray) == true)
                {
                    UFE.FireAlert("Lockout M", player);
                    UFE2Manager.AddOrSubtractGaugePointsPercent(player, lockOutOptions.gaugeId, 75);
                }
                else if (UFE2Manager.IsButtonPressMatch(button, comboBreakerOptions.mediumComboBreakerButtonPressArray) == true)
                {
                    UFE.FireAlert("Combo Breaker M", player);
                    CastComboBreakerMove(player);
                }
                else if (UFE2Manager.IsButtonPressMatch(button, comboBreakerOptions.heavyComboBreakerButtonPressArray) == true)
                {
                    UFE.FireAlert("Lockout M", player);
                    UFE2Manager.AddOrSubtractGaugePointsPercent(player, lockOutOptions.gaugeId, 75);
                }
            }
            else if (player.currentGaugesPoints[(int)comboBreakerOptions.gaugeId] == player.myInfo.maxGaugePoints * (comboBreakerOptions.heavyComboBreakerRequiredGaugePercent / 100))
            {
                if (UFE2Manager.IsButtonPressMatch(button, comboBreakerOptions.lightComboBreakerButtonPressArray) == true)
                {
                    UFE.FireAlert("Lockout H", player);
                    UFE2Manager.AddOrSubtractGaugePointsPercent(player, lockOutOptions.gaugeId, 75);
                }
                else if (UFE2Manager.IsButtonPressMatch(button, comboBreakerOptions.mediumComboBreakerButtonPressArray) == true)
                {
                    UFE.FireAlert("Lockout H", player);
                    UFE2Manager.AddOrSubtractGaugePointsPercent(player, lockOutOptions.gaugeId, 75);
                }
                else if (UFE2Manager.IsButtonPressMatch(button, comboBreakerOptions.heavyComboBreakerButtonPressArray) == true)
                {
                    UFE.FireAlert("Combo Breaker H", player);
                    CastComboBreakerMove(player);             
                }
            }
        }

        private void OnHit(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            int length = comboValueOptions.hitStrengthGaugeOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (hitInfo.hitStrength != comboValueOptions.hitStrengthGaugeOptionsArray[i].hitStrength)
                {
                    continue;
                }

                UFE2Manager.AddOrSubtractGaugePointsPercent(player, comboValueOptions.gaugeId, comboValueOptions.hitStrengthGaugeOptionsArray[i].gaugePercentAmount);
            }

            length = comboBreakerOptions.hitStrengthGaugeOptionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (hitInfo.hitStrength != comboBreakerOptions.hitStrengthGaugeOptionsArray[i].hitStrength)
                {
                    continue;
                }

                UFE2Manager.SetGaugePointsPercent(player.opControlsScript, comboBreakerOptions.gaugeId, comboBreakerOptions.hitStrengthGaugeOptionsArray[i].gaugePercentAmount);
            }

            /*if (move.moveName == comboBreakerOptions.standComboBreakerAttemptMoveName
                || move.moveName == comboBreakerOptions.jumpComboBreakerAttemptMoveName)
            {

            }*/
        }
    }
}