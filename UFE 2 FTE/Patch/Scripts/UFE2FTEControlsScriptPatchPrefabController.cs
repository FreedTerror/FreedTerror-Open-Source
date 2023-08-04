using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTEControlsScriptPatchPrefabController : MonoBehaviour
    {
        // Keep an eye out for potential desyncs caused by this script.

        private ControlsScript myControlsScript;

        [SerializeField]
        private bool useStandingUpAndCrouchingDownAnimationPatch = true;
        [SerializeField]
        private bool useMovementAnimationPostMovePatch = true;

        private void Start()
        {
            myControlsScript = GetComponentInParent<ControlsScript>();
        }

        private void FixedUpdate()
        {
            SetStandingUpAndCrouchingDownAnimationPatch(myControlsScript);

            SetMoveAnimationPostMovePatch(myControlsScript);
        }

        private void SetStandingUpAndCrouchingDownAnimationPatch(ControlsScript player)
        {
            if (player == null
                || useStandingUpAndCrouchingDownAnimationPatch == false)
            {
                return;
            }

            if (player.currentMove == null
                && player.currentState == PossibleStates.Crouch
                && player.currentSubState != SubStates.Stunned
                && player.currentSubState != SubStates.Blocking
                && player.MoveSet.GetCurrentClipName() != "crouching_2"
                && player.MoveSet.GetCurrentClipName() != "blockingCrouchingPose"
                && player.MoveSet.GetCurrentClipName() != "blockingCrouchingHit"
                && player.MoveSet.GetCurrentClipName() != "parryCrouching"
                && player.MoveSet.GetCurrentClipName() != "getHitCrouching")
            {
                if (player.MoveSet.GetCurrentClipName() != "crouching")
                {
                    player.MoveSet.PlayBasicMove(player.MoveSet.basicMoves.crouching, false);
                }
            }

            if (player.MoveSet.GetCurrentClipName() == "crouching_2"
                && player.MoveSet.AnimationTimesPlayed("crouching_2") >= 1
                || player.MoveSet.GetCurrentClipName() == "crouching_3"
                && player.MoveSet.AnimationTimesPlayed("crouching_3") >= 1)
            {
                player.MoveSet.SetAnimationPosition(1);
            }
        }

        private void SetMoveAnimationPostMovePatch(ControlsScript player)
        {
            if (player == null
                || useMovementAnimationPostMovePatch == false)
            {
                return;
            }

            if (player.currentMove != null
                && player.currentState == PossibleStates.Stand)
            {
                player.currentSubState = SubStates.Resting;
            }
        }
    }
}