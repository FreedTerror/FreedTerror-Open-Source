using UnityEngine;
using UFE3D;
using FPLibrary;

namespace UFE2FTE
{
    [CreateAssetMenu(menuName = "U.F.E. 2 F.T.E./Triggered Behaviour/Set Combo Variables", fileName = "New Set Combo Variables Triggered Behaviour")]
    public class TriggeredBehaviourScriptableObjectSetComboVariables : TriggeredBehaviourScriptableObject
    {
        [SerializeField]
        private bool useComboHits;
        [SerializeField]
        private int comboHits;
        [SerializeField]
        private bool useAirJuggleHits;
        [SerializeField]
        private int airJuggleHits;
        [SerializeField]
        private bool useWallBounceTimes;
        [SerializeField]
        private int wallBounceTimes;
        [SerializeField]
        private bool useGroundBounceTimes;
        [SerializeField]
        private int groundBounceTimes;
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
                    SetComboVariables(player, delayActionTime);
                }

                if (targetOptions.usePlayerAssist == true
                    && player.isAssist == true)
                {
                    SetComboVariables(player, delayActionTime);
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

                        SetComboVariables(player.assists[i], delayActionTime);
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
                        SetComboVariables(player.opControlsScript, delayActionTime);
                    }

                    if (targetOptions.useOpponentAssist == true
                        && player.opControlsScript.isAssist == true)
                    {
                        SetComboVariables(player.opControlsScript, delayActionTime);
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

                            SetComboVariables(player.opControlsScript.assists[i], delayActionTime);
                        }
                    }
                }
            }
        }

        private void SetComboVariables(ControlsScript player, Fix64 delayActionTime)
        {
            if (player == null)
            {
                return;
            }

            int comboVariable = player.comboHits;
            if (useComboHits == true)
            {
                comboVariable = comboHits;
            }
            player.comboHits = comboVariable;

            comboVariable = player.airJuggleHits;
            if (useAirJuggleHits == true)
            {
                comboVariable = airJuggleHits;
            }
            player.airJuggleHits = comboVariable;

            if (player.Physics != null)
            {
                comboVariable = player.Physics.groundBounceTimes;
                if (useGroundBounceTimes == true)
                {
                    comboVariable = groundBounceTimes;
                }
                player.Physics.groundBounceTimes = comboVariable;

                comboVariable = player.Physics.wallBounceTimes;
                if (useWallBounceTimes == true)
                {
                    comboVariable = wallBounceTimes;
                }
                player.Physics.wallBounceTimes = comboVariable;
            }
        }
    }
}