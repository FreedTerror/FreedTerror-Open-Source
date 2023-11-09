using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class TriggeredBehaviourScriptableObject : ScriptableObject
    {
        public virtual void OnTriggeredBehaviour(ControlsScript player) { }

        public static void CallOnTriggeredBehaviour(TriggeredBehaviourScriptableObject triggeredBehaviourScriptableObject, ControlsScript player)
        {
            if (triggeredBehaviourScriptableObject == null)
            {
                return;
            }

            triggeredBehaviourScriptableObject.OnTriggeredBehaviour(player);
        }

        public static void CallOnTriggeredBehaviour(TriggeredBehaviourScriptableObject[] triggeredBehaviourScriptableObjectArray, ControlsScript player)
        {
            if (triggeredBehaviourScriptableObjectArray == null)
            {
                return;
            }

            int length = triggeredBehaviourScriptableObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                CallOnTriggeredBehaviour(triggeredBehaviourScriptableObjectArray[i], player);
            }
        }

        public virtual void OnBasicMove(BasicMoveReference basicMove, ControlsScript player) { }

        public static void CallOnBasicMove(TriggeredBehaviourScriptableObject triggeredBehaviourScriptableObject, BasicMoveReference basicMove, ControlsScript player)
        {
            if (triggeredBehaviourScriptableObject == null)
            {
                return;
            }

            triggeredBehaviourScriptableObject.OnBasicMove(basicMove, player);
        }

        public static void CallOnBasicMove(TriggeredBehaviourScriptableObject[] triggeredBehaviourScriptableObjectArray, BasicMoveReference basicMove, ControlsScript player)
        {
            if (triggeredBehaviourScriptableObjectArray == null)
            {
                return;
            }

            int length = triggeredBehaviourScriptableObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                CallOnBasicMove(triggeredBehaviourScriptableObjectArray[i], basicMove, player);
            }
        }

        public virtual void OnMove(MoveInfo move, ControlsScript player) { }

        public static void CallOnMove(TriggeredBehaviourScriptableObject triggeredBehaviourScriptableObject, MoveInfo move, ControlsScript player)
        {
            if (triggeredBehaviourScriptableObject == null)
            {
                return;
            }

            triggeredBehaviourScriptableObject.OnMove(move, player);
        }

        public static void CallOnMove(TriggeredBehaviourScriptableObject[] triggeredBehaviourScriptableObjectArray, MoveInfo move, ControlsScript player)
        {
            if (triggeredBehaviourScriptableObjectArray == null)
            {
                return;
            }

            int length = triggeredBehaviourScriptableObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                CallOnMove(triggeredBehaviourScriptableObjectArray[i], move, player);
            }
        }

        public virtual void OnHit(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player) { }

        public static void CallOnHit(TriggeredBehaviourScriptableObject triggeredBehaviourScriptableObject, HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            if (triggeredBehaviourScriptableObject == null)
            {
                return;
            }

            triggeredBehaviourScriptableObject.OnHit(strokeHitBox, move, hitInfo, player);
        }

        public static void CallOnHit(TriggeredBehaviourScriptableObject[] triggeredBehaviourScriptableObjectArray, HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            if (triggeredBehaviourScriptableObjectArray == null)
            {
                return;
            }

            int length = triggeredBehaviourScriptableObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                CallOnHit(triggeredBehaviourScriptableObjectArray[i], strokeHitBox, move, hitInfo, player);
            }
        }

        public virtual void OnBlock(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player) { }

        public static void CallOnBlock(TriggeredBehaviourScriptableObject triggeredBehaviourScriptableObject, HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            if (triggeredBehaviourScriptableObject == null)
            {
                return;
            }

            triggeredBehaviourScriptableObject.OnBlock(strokeHitBox, move, hitInfo, player);
        }

        public static void CallOnBlock(TriggeredBehaviourScriptableObject[] triggeredBehaviourScriptableObjectArray, HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            if (triggeredBehaviourScriptableObjectArray == null)
            {
                return;
            }

            int length = triggeredBehaviourScriptableObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                CallOnBlock(triggeredBehaviourScriptableObjectArray[i], strokeHitBox, move, hitInfo, player);
            }
        }

        public virtual void OnParry(HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player) { }

        public static void CallOnParry(TriggeredBehaviourScriptableObject triggeredBehaviourScriptableObject, HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            if (triggeredBehaviourScriptableObject == null)
            {
                return;
            }

            triggeredBehaviourScriptableObject.OnParry(strokeHitBox, move, hitInfo, player);
        }

        public static void CallOnParry(TriggeredBehaviourScriptableObject[] triggeredBehaviourScriptableObjectArray, HitBox strokeHitBox, MoveInfo move, Hit hitInfo, ControlsScript player)
        {
            if (triggeredBehaviourScriptableObjectArray == null)
            {
                return;
            }

            int length = triggeredBehaviourScriptableObjectArray.Length;
            for (int i = 0; i < length; i++)
            {
                CallOnParry(triggeredBehaviourScriptableObjectArray[i], strokeHitBox, move, hitInfo, player);
            }
        }
    }
}