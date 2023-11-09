using System;
using UnityEngine;
using UFE3D;
using FPLibrary;

namespace UFE2FTE
{
    [CreateAssetMenu(menuName = "U.F.E. 2 F.T.E./Triggered Behaviour/Set Transform", fileName = "New Set Transform Triggered Behaviour")]
    public class TriggeredBehaviourScriptableObjectSetTransform : TriggeredBehaviourScriptableObject
    {
        [Serializable]
        private class PositionOptions
        {
            public FPVector position;
            public bool usePlayerMirror;
            public bool usePlayerXPosition;
            public bool usePlayerYPosition;
            public bool usePlayerZPosition;
            public bool useOpponentMirror;
            public bool useOpponentXPosition;
            public bool useOpponentYPosition;
            public bool useOpponentZPosition;
            [Header("Custom Behaviours")]
            public bool useComboBreakerSetPosition;

            public FPVector GetPositionFromPlayer(ControlsScript player, Fix64 delayActionTime)
            {
                FPVector position = this.position;

                if (player == null
                    || player.worldTransform == null)
                {
                    return position;
                }

                int xPositionMultiplier = GetXPositionMultiplier();

                if (usePlayerMirror == true)
                {
                    xPositionMultiplier = player.mirror;
                }

                if (usePlayerXPosition == true)
                {
                    position.x = player.worldTransform.position.x + this.position.x * -xPositionMultiplier;
                }

                if (usePlayerYPosition == true)
                {
                    position.y = player.worldTransform.position.y + this.position.y;
                }

                if (usePlayerZPosition == true)
                {
                    position.z = player.worldTransform.position.z + this.position.z;
                }

                if (player.opControlsScript != null
                    || player.opControlsScript.worldTransform != null)
                {
                    if (useOpponentMirror == true)
                    {
                        xPositionMultiplier = player.opControlsScript.mirror;
                    }

                    if (useOpponentXPosition == true)
                    {
                        position.x = player.opControlsScript.worldTransform.position.x + this.position.x * -xPositionMultiplier;
                    }

                    if (useOpponentYPosition == true)
                    {
                        position.y = player.opControlsScript.worldTransform.position.y + this.position.y;
                    }

                    if (useOpponentZPosition == true)
                    {
                        position.z = player.opControlsScript.worldTransform.position.z + this.position.z;
                    }
                }

                if (useComboBreakerSetPosition == true)
                {
                    CallOnSetPositionComboBreaker(player, delayActionTime);

                    return player.worldTransform.position;
                }

                return position;

                int GetXPositionMultiplier()
                {
                    if (position.x >= 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }
        [SerializeField]
        private PositionOptions positionOptions;

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
                    SetPlayerPosition(player, positionOptions.GetPositionFromPlayer(player, delayActionTime));
                }

                if (targetOptions.usePlayerAssist == true
                    && player.isAssist == true)
                {
                    SetPlayerPosition(player, positionOptions.GetPositionFromPlayer(player, delayActionTime));
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

                        SetPlayerPosition(player.assists[i], positionOptions.GetPositionFromPlayer(player, delayActionTime));
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
                        SetPlayerPosition(player.opControlsScript, positionOptions.GetPositionFromPlayer(player.opControlsScript, delayActionTime));
                    }

                    if (targetOptions.useOpponentAssist == true
                        && player.opControlsScript.isAssist == true)
                    {
                        SetPlayerPosition(player.opControlsScript, positionOptions.GetPositionFromPlayer(player.opControlsScript, delayActionTime));
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

                            SetPlayerPosition(player.opControlsScript.assists[i], positionOptions.GetPositionFromPlayer(player.opControlsScript.assists[i], delayActionTime));
                        }
                    }
                }
            }
        }

        private void SetPlayerPosition(ControlsScript player, FPVector position)
        {
            if (player == null
                || player.worldTransform == null)
            {
                return;
            }

            player.worldTransform.position = position;
        }

        public delegate void SetPositionComboBreakerHandler(ControlsScript player, Fix64 delayActionTime);
        public static event SetPositionComboBreakerHandler OnSetPositionComboBreaker;

        public static void CallOnSetPositionComboBreaker(ControlsScript player, Fix64 delayActionTime)
        {
            UFE2FTE.AddComponentToControlsScriptCharacterGameObject<TriggeredBehaviourGameObjectController>(player);

            if (OnSetPositionComboBreaker == null)
            {
                return;
            }

            OnSetPositionComboBreaker(player, delayActionTime);
        }

        public static void SetPositionComboBreaker(ControlsScript player)
        {
            if (player == null
                || player.worldTransform == null
                || player.opControlsScript == null
                || player.opControlsScript.worldTransform == null)
            {
                return;
            }

            player.worldTransform.position = new FPVector(
                player.worldTransform.position.x,
                player.opControlsScript.worldTransform.position.y,
                player.worldTransform.position.z);
        }
    }
}