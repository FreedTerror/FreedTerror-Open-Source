using UnityEngine;
using UFE3D;

namespace FreedTerror.UFE2
{
    public class AIController : MonoBehaviour
    {
        private ControlsScript myControlsScript;
        private int activeFramesBeginOffset = -1;

        private void Start()
        {
            myControlsScript = GetComponentInParent<ControlsScript>();
        }

        private void FixedUpdate()
        {
            UpdateAIInput(myControlsScript);
        }

        private void UpdateAIInput(ControlsScript player)
        {
            if (player == null
                || player.opControlsScript == null
                || UFE.IsPaused() == true)
            {
                return;
            }

            switch (UFE2Manager.instance.aiMode)
            {
                case UFE2Manager.AIMode.Crouch:
                    SetBlockVariables(player, false);

                    SetParryVariables(player, false);

                    UFE2Manager.PressAxis(UFE.GetController(player.playerNum), InputType.VerticalAxis, -1);
                    break;

                case UFE2Manager.AIMode.MoveForward:
                    SetBlockVariables(player, false);

                    SetParryVariables(player, false);

                    UFE2Manager.PressAxis(UFE.GetController(player.playerNum), InputType.HorizontalAxis, -player.mirror);
                    break;

                case UFE2Manager.AIMode.MoveBackward:
                    SetBlockVariables(player, false);

                    SetParryVariables(player, false);

                    UFE2Manager.PressAxis(UFE.GetController(player.playerNum), InputType.HorizontalAxis, player.mirror);
                    break;

                case UFE2Manager.AIMode.BlockAll:
                    SetBlockVariables(player, true);

                    SetParryVariables(player, false);

                    SetHitTypeInput(player.opControlsScript, UFE.GetController(player.playerNum));
                    break;

                case UFE2Manager.AIMode.StandBlock:
                    SetBlockVariables(player, true);

                    SetParryVariables(player, false);
                    break;

                case UFE2Manager.AIMode.CrouchBlock:
                    SetBlockVariables(player, true);

                    SetParryVariables(player, false);

                    UFE2Manager.PressAxis(UFE.GetController(player.playerNum), InputType.VerticalAxis, -1);
                    break;

                case UFE2Manager.AIMode.ParryAll:
                    SetBlockVariables(player, false);

                    SetParryVariables(player, true);

                    SetHitTypeInput(player.opControlsScript, UFE.GetController(player.playerNum));
                    break;

                case UFE2Manager.AIMode.StandParry:
                    SetBlockVariables(player, false);

                    SetParryVariables(player, true);
                    break;

                case UFE2Manager.AIMode.CrouchParry:
                    SetBlockVariables(player, false);

                    SetParryVariables(player, true);

                    UFE2Manager.PressAxis(UFE.GetController(player.playerNum), InputType.VerticalAxis, -1);
                    break;

                case UFE2Manager.AIMode.JumpParry:
                    SetBlockVariables(player, false);

                    switch (player.currentState)
                    {
                        case PossibleStates.NeutralJump:
                        case PossibleStates.ForwardJump:
                        case PossibleStates.BackJump:
                            SetParryVariables(player, true);
                            break;

                        default:
                            SetParryVariables(player, false);
                            break;
                    }

                    UFE2Manager.PressAxis(UFE.GetController(player.playerNum), InputType.VerticalAxis, 1);
                    break;

                case UFE2Manager.AIMode.NeutralJump:
                    SetBlockVariables(player, false);

                    SetParryVariables(player, false);

                    UFE2Manager.PressAxis(UFE.GetController(player.playerNum), InputType.VerticalAxis, 1);
                    break;

                case UFE2Manager.AIMode.ForwardJump:
                    SetBlockVariables(player, false);

                    SetParryVariables(player, false);

                    UFE2Manager.PressAxis(UFE.GetController(player.playerNum), InputType.HorizontalAxis, -player.mirror);

                    UFE2Manager.PressAxis(UFE.GetController(player.playerNum), InputType.VerticalAxis, 1);
                    break;

                case UFE2Manager.AIMode.BackwardJump:
                    SetBlockVariables(player, false);

                    SetParryVariables(player, false);

                    UFE2Manager.PressAxis(UFE.GetController(player.playerNum), InputType.HorizontalAxis, player.mirror);

                    UFE2Manager.PressAxis(UFE.GetController(player.playerNum), InputType.VerticalAxis, 1);
                    break;
            }

            TryThrowTechInput(player);
        }

        private static void SetBlockVariables(ControlsScript player, bool isBlocking)
        {
            if (player == null)
            {
                return;
            }

            if (isBlocking == true)
            {
                player.potentialBlock = true;

                if (UFE2Manager.GetButtonPress(UFE.config.blockOptions.blockType) != ButtonPress.Back)
                {
                    UFE2Manager.PressButton(UFE.GetController(player.playerNum), UFE2Manager.GetButtonPress(UFE.config.blockOptions.blockType));
                }
            }
            else
            {
                player.isBlocking = false;

                player.potentialBlock = false;
            }
        }

        private static void SetParryVariables(ControlsScript player, bool isParrying)
        {
            if (player == null)
            {
                return;
            }

            if (isParrying == true)
            {
                player.potentialParry = 1;
            }
            else
            {
                player.potentialParry = 0;
            }
        }

        private void SetHitTypeInput(ControlsScript attacker, UFEController defender)
        {
            if (attacker == null
                || defender == null)
            {
                return;
            }

            if (attacker.currentMove != null)
            {
                int length = attacker.currentMove.hits.Length;
                for (int i = 0; i < length; i++)
                {
                    if (attacker.currentMove.currentFrame >= attacker.currentMove.hits[i].activeFramesBegin + activeFramesBeginOffset
                        && attacker.currentMove.currentFrame < attacker.currentMove.hits[i].activeFramesEnds)
                    {
                        if (attacker.currentMove.hits[i].hitType == HitType.Low
                            || attacker.currentMove.hits[i].hitType == HitType.Sweep)
                        {
                            UFE2Manager.PressAxis(defender, InputType.VerticalAxis, -1);
                        }
                    }
                }
            }

            for (int i = 0; i < attacker.projectiles.Count; i++)
            {
                if (attacker.projectiles[i].data.hitType == HitType.Low
                    || attacker.projectiles[i].data.hitType == HitType.Sweep)
                {
                    UFE2Manager.PressAxis(defender, InputType.VerticalAxis, -1);
                }
            }
        }

        private void TryThrowTechInput(ControlsScript player)
        {
            if (UFE2Manager.instance.aiMode == UFE2Manager.AIMode.Human
                || UFE2Manager.instance.aiThrowTechMode == UFE2Manager.Toggle.Off
                || player == null
                || player.currentMove != null
                || player.opControlsScript == null
                || player.opControlsScript.currentMove == null
                || player.opControlsScript.currentMove.IsThrow(true) == false)
            {
                return;
            }

            UFE2Manager.CastMoveByMoveName(player, player.opControlsScript.currentMove.moveName);

            /*UFE2FTE.PressMoveInfoDefaultInputs(
                UFE.GetController(player.playerNum),
                UFE2FTE.GetMoveInfoFromControlsScriptByMoveName(player, player.opControlsScript.currentMove.moveName));*/
        }
    }
}
