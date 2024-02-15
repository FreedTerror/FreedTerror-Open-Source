using FPLibrary;
using UFE3D;
using UnityEngine;

namespace FreedTerror.UFE2
{
    public class ComboBreakerThing : MonoBehaviour
    {
        [SerializeField]
        private string[] comboBreakerAttemptMoveNameArray;
        [SerializeField]
        private string comboBreakerConfirmMoveName = "Combo Breaker Confirm";

        [SerializeField]
        private string[] counterBreakerConfirmMoveNameArray;

        [SerializeField]
        private string jumpThrowTechMoveName = "Jump Throw Tech";

        [SerializeField]
        private string knockdownMoveName = "Knockdown";
        [SerializeField]
        private string knockdownGroundBounceRiseMoveName = "Knockdown Ground Bounce Rise";

        [SerializeField]
        private string getupMoveName = "Getup";

        private void OnEnable()
        {
            UFE.OnMove += OnMove;
            UFE.OnHit += OnHit;      
        }

        private void OnDisable()
        {
            UFE.OnMove -= OnMove;
            UFE.OnHit -= OnHit;     
        }

        #region On Move Methods

        private void OnMove(MoveInfo move, ControlsScript player)
        {
            JumpThrowTechOnMove(player, move);

            KnockdownOnMove(player, move);

            KnockdownGroundBounceRiseOnMove(player, move);

            GetupOnMove(player, move);
        }

        private void GetupOnMove(ControlsScript player, MoveInfo moveInfo)
        {
            if (player == null
                || moveInfo == null
                || moveInfo.moveName != getupMoveName)
            {
                return;
            }

            player.currentSubState = SubStates.Stunned;
            player.ReleaseStun();
        }

        private void JumpThrowTechOnMove(ControlsScript player, MoveInfo moveInfo)
        {
            if (player == null
                || moveInfo == null
                || moveInfo.moveName != jumpThrowTechMoveName)
            {
                return;
            }

            UFE2Manager.SetPlayerPosition(player, new FPVector(player.worldTransform.position.x, player.opControlsScript.worldTransform.position.y, player.worldTransform.position.z));
            player.Physics.ApplyNewWeight(UFE.config.comboOptions._juggleWeight);
        }

        private void KnockdownOnMove(ControlsScript player, MoveInfo moveInfo)
        {
            if (player == null
                || moveInfo == null
                || moveInfo.moveName != knockdownMoveName)
            {
                return;
            }

            player.Physics.ResetWeight();
        }

        private void KnockdownGroundBounceRiseOnMove(ControlsScript player, MoveInfo moveInfo)
        {
            if (player == null
                || moveInfo == null
                || moveInfo.moveName != knockdownGroundBounceRiseMoveName)
            {
                return;
            }

            player.Physics.ApplyNewWeight(UFE.config.comboOptions._juggleWeight);
        }

        #endregion

        #region On Hit Methods

        private void OnHit(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            ComboBreakerAttemptOnHit(player, UFE2Manager.GetControlsScript(strokeHitBox), move, UFE2Manager.GetFreezingTimeFromHitOnHit(hitInfo));

            CounterBreakerConfirmOnHit(player, UFE2Manager.GetControlsScript(strokeHitBox), move);
        }

        private void ComboBreakerAttemptOnHit(ControlsScript player, ControlsScript opponent, MoveInfo moveInfo, Fix64 delayTime)
        {
            if (player == null
                || opponent == null
                || moveInfo == null
                || UFE2Manager.IsStringMatch(moveInfo.moveName, comboBreakerAttemptMoveNameArray) == false)
            {
                return;
            }

            if (delayTime > 0)
            {
                UFE.DelaySynchronizedAction(() =>
                {
                    UFE2Manager.CastMoveByMoveName(player, comboBreakerConfirmMoveName);
                    player.ReleaseStun();
                    player.Physics.ApplyNewWeight(UFE.config.comboOptions._juggleWeight);

                    UFE2Manager.SetPlayerPosition(opponent, new FPVector(opponent.worldTransform.position.x, player.worldTransform.position.y, opponent.worldTransform.position.z));
                    UFE2Manager.CastMoveByMoveName(opponent, comboBreakerConfirmMoveName);
                    opponent.ReleaseStun();
                    opponent.Physics.ApplyNewWeight(UFE.config.comboOptions._juggleWeight);
                },
                delayTime);
            }
            else
            {
                UFE2Manager.CastMoveByMoveName(player, comboBreakerConfirmMoveName);
                player.ReleaseStun();
                player.Physics.ApplyNewWeight(UFE.config.comboOptions._juggleWeight);

                UFE2Manager.SetPlayerPosition(opponent, new FPVector(opponent.worldTransform.position.x, player.worldTransform.position.y, opponent.worldTransform.position.z));
                UFE2Manager.CastMoveByMoveName(opponent, comboBreakerConfirmMoveName);
                opponent.ReleaseStun();
                opponent.Physics.ApplyNewWeight(UFE.config.comboOptions._juggleWeight);
            }
        }

        private void CounterBreakerConfirmOnHit(ControlsScript player, ControlsScript opponent, MoveInfo moveInfo)
        {
            if (player == null
                || opponent == null
                || moveInfo == null
                || UFE2Manager.IsStringMatch(moveInfo.moveName, counterBreakerConfirmMoveNameArray) == false)
            {
                return;
            }

            opponent.comboHits = 0;
            opponent.airJuggleHits = 0;
            opponent.Physics.groundBounceTimes = 0;
            opponent.Physics.wallBounceTimes = 0;
            opponent.Physics.ForceGrounded();

            UFE2Manager.SetPlayerPosition(player, new FPVector(player.opControlsScript.worldTransform.position.x + (Fix64)3 * -player.opControlsScript.mirror, player.opControlsScript.worldTransform.position.y, player.worldTransform.position.z));
            //UFE2FTE.KillAllMoves(player);
        }

        #endregion
    }
}