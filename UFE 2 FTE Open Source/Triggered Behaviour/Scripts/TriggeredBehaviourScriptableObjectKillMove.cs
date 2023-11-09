using UnityEngine;
using UFE3D;
using FPLibrary;

namespace UFE2FTE
{
    [CreateAssetMenu(menuName = "U.F.E. 2 F.T.E./Triggered Behaviour/Kill Move", fileName = "New Kill Move Triggered Behaviour")]
    public class TriggeredBehaviourScriptableObjectKillMove : TriggeredBehaviourScriptableObject
    {
        [SerializeField]
        private bool killCurrentMove;
        [SerializeField]
        private bool killStoredMove;
        [SerializeField]
        private TriggeredBehaviour.DelayActionTimeOptions delayActionTimeOptions;
        [SerializeField]
        private TriggeredBehaviour.TargetOptions targetOptions;

        public override void OnBasicMove(BasicMoveReference basicMove, ControlsScript player)
        {
            CheckTargetOptions(player, delayActionTimeOptions.GetDelayActionTime());
        }

        public override void OnMove(MoveInfo move, ControlsScript player)
        {
            CheckTargetOptions(player, delayActionTimeOptions.GetDelayActionTime());
        }

        public override void OnHit(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            CheckTargetOptions(player, delayActionTimeOptions.GetDelayActionTimeOnHit(hitInfo));
        }

        public override void OnBlock(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            CheckTargetOptions(player, delayActionTimeOptions.GetDelayActionTimeOnBlock(hitInfo));
        }

        public override void OnParry(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            CheckTargetOptions(player, delayActionTimeOptions.GetDelayActionTimeOnParry(hitInfo));
        }

        private void CheckTargetOptions(ControlsScript player, Fix64 delayActionTime)
        {
            if (player == null)
            {
                return;
            }

            if (TriggeredBehaviour.IsStringMatch(player.myInfo.characterName, targetOptions.excludedCharacterNameArray) == false)
            {
                if (targetOptions.usePlayer == true
                    && player.isAssist == false)
                {
                    KillMove(player, delayActionTime);
                }

                if (targetOptions.usePlayerAssist == true
                    && player.isAssist == true)
                {
                    KillMove(player, delayActionTime);
                }

                if (targetOptions.useAllPlayerAssists == true)
                {
                    int count = player.assists.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (TriggeredBehaviour.IsStringMatch(player.assists[i].myInfo.characterName, targetOptions.excludedCharacterNameArray) == true)
                        {
                            continue;
                        }

                        KillMove(player.assists[i], delayActionTime);
                    }
                }
            }

            if (player.opControlsScript != null)
            {
                if (TriggeredBehaviour.IsStringMatch(player.opControlsScript.myInfo.characterName, targetOptions.excludedCharacterNameArray) == false)
                {
                    if (targetOptions.useOpponent == true
                        && player.opControlsScript.isAssist == false)
                    {
                        KillMove(player.opControlsScript, delayActionTime);
                    }

                    if (targetOptions.useOpponentAssist == true
                        && player.opControlsScript.isAssist == true)
                    {
                        KillMove(player.opControlsScript, delayActionTime);
                    }

                    if (targetOptions.useAllOpponentAssists == true)
                    {
                        int count = player.opControlsScript.assists.Count;
                        for (int i = 0; i < count; i++)
                        {
                            if (TriggeredBehaviour.IsStringMatch(player.opControlsScript.assists[i].myInfo.characterName, targetOptions.excludedCharacterNameArray) == true)
                            {
                                continue;
                            }

                            KillMove(player.opControlsScript.assists[i], delayActionTime);
                        }
                    }
                }
            }
        }

        private void KillMove(ControlsScript player, Fix64 delayActionTime)
        {
            if (player == null)
            {
                return;
            }

            if (killCurrentMove == true)
            {
                if (delayActionTime > 0)
                {
                    UFE.DelaySynchronizedAction(player.KillCurrentMove, delayActionTime);
                }
                else
                {
                    player.KillCurrentMove();
                }
            }

            if (killStoredMove == true)
            {
                player.storedMove = null;
            }
        }
    }
}