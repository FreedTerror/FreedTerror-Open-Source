using UnityEngine;
using UFE3D;
using FPLibrary;

namespace UFE2FTE
{
    [CreateAssetMenu(menuName = "U.F.E. 2 F.T.E./Triggered Behaviour/Cast Move", fileName = "New Cast Move Triggered Behaviour")]
    public class TriggeredBehaviourScriptableObjectCastMove : TriggeredBehaviourScriptableObject
    {
        [SerializeField]
        private string castMoveName;
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
                    CallOnCastMove(player, castMoveName, delayActionTime);
                }

                if (targetOptions.usePlayerAssist == true
                    && player.isAssist == true)
                {
                    CallOnCastMove(player, castMoveName, delayActionTime);
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

                        CallOnCastMove(player.assists[i], castMoveName, delayActionTime);
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
                        CallOnCastMove(player.opControlsScript, castMoveName, delayActionTime);
                    }

                    if (targetOptions.useOpponentAssist == true
                        && player.opControlsScript.isAssist == true)
                    {
                        CallOnCastMove(player.opControlsScript, castMoveName, delayActionTime);
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

                            CallOnCastMove(player.opControlsScript.assists[i], castMoveName, delayActionTime);
                        }
                    }
                }
            }
        }

        public delegate void CastMoveHandler(ControlsScript player, string moveName, Fix64 delayActionTime);
        public static event CastMoveHandler OnCastMove;

        public static void CallOnCastMove(ControlsScript player, string moveName, Fix64 delayActionTime)
        {
            UFE2FTE.AddComponentToControlsScriptCharacterGameObject<TriggeredBehaviourGameObjectController>(player);

            if (OnCastMove == null)
            {
                return;
            }

            OnCastMove(player, moveName, delayActionTime);
        }
    }
}