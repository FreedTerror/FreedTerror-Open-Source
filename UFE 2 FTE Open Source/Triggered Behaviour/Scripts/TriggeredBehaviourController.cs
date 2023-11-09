using System;
using UFE3D;
using UFENetcode;

namespace UFE2FTE
{
    public class TriggeredBehaviourController : UFEBehaviour, UFEInterface
    {
        [Serializable]
        public class CastingFrameOptions
        {
            public int[] castingFrameArray;
            public int ignoredCastingFrame;
            public bool useMoveNameArrayToTriggerBehaviour;
            public string[] moveNameArrayToTriggerBehaviour;
            public TriggeredBehaviourScriptableObject[] triggeredBehaviourScriptableObjectArray;
        }
        public CastingFrameOptions[] castingFrameOptionsArray;

        [Serializable]
        public class OnBasicMoveOptions
        {
            public bool useBasicMoveArrayToTriggerBehaviour;
            public BasicMoveReference[] basicMoveArrayToTriggerBehaviour;
            public TriggeredBehaviourScriptableObject[] triggeredBehaviourScriptableObjectArray;
        }
        public OnBasicMoveOptions[] onBasicMoveOptionsArray;

        [Serializable]
        public class OnMoveOptions
        {
            public bool useMoveNameArrayToTriggerBehaviour;
            public string[] moveNameArrayToTriggerBehaviour;
            public TriggeredBehaviourScriptableObject[] triggeredBehaviourScriptableObjectArray;
        }
        public OnMoveOptions[] onMoveOptionsArray;

        [Serializable]
        public class OnHitOptions
        {
            public bool useMoveNameArrayToTriggerBehaviour;
            public string[] moveNameArrayToTriggerBehaviour;
            public TriggeredBehaviourScriptableObject[] triggeredBehaviourScriptableObjectArray;
        }
        public OnHitOptions[] onHitOptionsArray;

        [Serializable]
        public class OnBlockOptions
        {
            public bool useMoveNameArrayToTriggerBehaviour;
            public string[] moveNameArrayToTriggerBehaviour;
            public TriggeredBehaviourScriptableObject[] triggeredBehaviourScriptableObjectArray;
        }
        public OnBlockOptions[] onBlockOptionsArray;

        [Serializable]
        public class OnParryOptions
        {
            public bool useMoveNameArrayToTriggerBehaviour;
            public string[] moveNameArrayToTriggerBehaviour;
            public TriggeredBehaviourScriptableObject[] triggeredBehaviourScriptableObjectArray;
        }
        public OnParryOptions[] onParryOptionsArray;

        private void OnEnable()
        {
            UFE.OnBasicMove += OnBasicMove;
            UFE.OnMove += OnMove;
            UFE.OnHit += OnHit;
            UFE.OnBlock += OnBlock;
            UFE.OnParry += OnParry;
        }

        private void OnDisable()
        {
            UFE.OnBasicMove -= OnBasicMove;
            UFE.OnMove -= OnMove;
            UFE.OnHit -= OnHit;
            UFE.OnBlock -= OnBlock;
            UFE.OnParry -= OnParry;
        }

        public override void UFEFixedUpdate()
        {
            if (UFE.GetPlayer1ControlsScript() != null)
            {
                CheckCastingFrameOptions(UFE.GetPlayer1ControlsScript());

                int count = UFE.GetPlayer1ControlsScript().assists.Count;
                for (int i = 0; i < count; i++)
                {
                    CheckCastingFrameOptions(UFE.GetPlayer1ControlsScript().assists[i]);
                }
            }

            if (UFE.GetPlayer2ControlsScript() != null)
            {
                CheckCastingFrameOptions(UFE.GetPlayer2ControlsScript());

                int count = UFE.GetPlayer2ControlsScript().assists.Count;
                for (int i = 0; i < count; i++)
                {
                    CheckCastingFrameOptions(UFE.GetPlayer2ControlsScript().assists[i]);
                }
            }
        }

        private void CheckCastingFrameOptions(ControlsScript player)
        {
            if (player == null
                || player.currentMove == null)
            {
                return;
            }

            CastingFrameOptions[] optionsArray = castingFrameOptionsArray;

            int length = optionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (TriggeredBehaviour.IsIntMatch(player.currentMove.currentFrame, optionsArray[i].castingFrameArray) == false)
                {
                    continue;
                }

                if (optionsArray[i].useMoveNameArrayToTriggerBehaviour == true
                    && TriggeredBehaviour.IsStringMatch(player.currentMove.moveName, optionsArray[i].moveNameArrayToTriggerBehaviour) == true)
                {
                    TriggeredBehaviourScriptableObject.CallOnTriggeredBehaviour(optionsArray[i].triggeredBehaviourScriptableObjectArray, player);
                }
            }
        }

        private void OnBasicMove(BasicMoveReference basicMove, ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            OnBasicMoveOptions[] optionsArray = onBasicMoveOptionsArray;

            int length = optionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (optionsArray[i].useBasicMoveArrayToTriggerBehaviour == true
                    && TriggeredBehaviour.IsBasicMoveMatch(basicMove, optionsArray[i].basicMoveArrayToTriggerBehaviour) == true)
                {
                    TriggeredBehaviourScriptableObject.CallOnBasicMove(optionsArray[i].triggeredBehaviourScriptableObjectArray, basicMove, player);
                }
            }
        }

        private void OnMove(MoveInfo move, ControlsScript player)
        {
            if (move == null
                || player == null)
            {
                return;
            }

            OnMoveOptions[] optionsArray = onMoveOptionsArray;

            int length = optionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (optionsArray[i].useMoveNameArrayToTriggerBehaviour == true
                    && TriggeredBehaviour.IsStringMatch(move.moveName, optionsArray[i].moveNameArrayToTriggerBehaviour) == true)
                {
                    TriggeredBehaviourScriptableObject.CallOnMove(optionsArray[i].triggeredBehaviourScriptableObjectArray, move, player);
                }
            }
        }

        private void OnHit(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            if (strokeHitBox == null
                || move == null
                || hitInfo == null
                || player == null)
            {
                return;
            }

            OnHitOptions[] optionsArray = onHitOptionsArray;

            int length = optionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (optionsArray[i].useMoveNameArrayToTriggerBehaviour == true
                    && TriggeredBehaviour.IsStringMatch(move.moveName, optionsArray[i].moveNameArrayToTriggerBehaviour) == true)
                {
                    TriggeredBehaviourScriptableObject.CallOnHit(optionsArray[i].triggeredBehaviourScriptableObjectArray, strokeHitBox, move, hitInfo, player);
                }
            }
        }

        private void OnBlock(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            if (strokeHitBox == null
                || move == null
                || hitInfo == null
                || player == null)
            {
                return;
            }

            OnBlockOptions[] optionsArray = onBlockOptionsArray;

            int length = optionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (optionsArray[i].useMoveNameArrayToTriggerBehaviour == true
                    && TriggeredBehaviour.IsStringMatch(move.moveName, optionsArray[i].moveNameArrayToTriggerBehaviour) == true)
                {
                    TriggeredBehaviourScriptableObject.CallOnBlock(optionsArray[i].triggeredBehaviourScriptableObjectArray, strokeHitBox, move, hitInfo, player);
                }
            }
        }

        private void OnParry(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            if (strokeHitBox == null
                || move == null
                || hitInfo == null
                || player == null)
            {
                return;
            }

            OnParryOptions[] optionsArray = onParryOptionsArray;

            int length = optionsArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (optionsArray[i].useMoveNameArrayToTriggerBehaviour == true
                    && TriggeredBehaviour.IsStringMatch(move.moveName, optionsArray[i].moveNameArrayToTriggerBehaviour) == true)
                {
                    TriggeredBehaviourScriptableObject.CallOnParry(optionsArray[i].triggeredBehaviourScriptableObjectArray, strokeHitBox, move, hitInfo, player);
                }
            }
        }
    }
}