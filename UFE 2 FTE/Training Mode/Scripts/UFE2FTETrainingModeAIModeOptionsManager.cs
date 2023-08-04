using UFE3D;

namespace UFE2FTE
{
    public static class UFE2FTETrainingModeAIModeOptionsManager
    {
        public enum TrainingModeAIMode
        {
            Human,
            Stand,
            Crouch,
            MoveForward,
            MoveBackward,
            BlockAll,
            StandBlock,
            CrouchBlock,
            ParryAll,
            StandParry,
            CrouchParry,
            AirParry,
            NeutralJump,
            ForwardJump,
            BackwardJump
        }
        public static TrainingModeAIMode trainingModeAIMode;

        public static int activeFramesBeginOffset = -1;

        public static void SetTrainingModeAIInput()
        {
            switch (trainingModeAIMode)
            {
                case TrainingModeAIMode.Crouch:
                    SetBlockVariables(UFE.GetPlayer2ControlsScript(), false);

                    SetParryVariables(UFE.GetPlayer2ControlsScript(), false);

                    UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.VerticalAxis, -1);
                    break;

                case TrainingModeAIMode.MoveForward:
                    SetBlockVariables(UFE.GetPlayer2ControlsScript(), false);

                    SetParryVariables(UFE.GetPlayer2ControlsScript(), false);

                    if (UFE.GetPlayer2ControlsScript().mirror == -1)
                    {
                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.HorizontalAxis, 1);
                    }
                    else
                    {
                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.HorizontalAxis, -1);
                    }
                    break;

                case TrainingModeAIMode.MoveBackward:
                    SetBlockVariables(UFE.GetPlayer2ControlsScript(), false);

                    SetParryVariables(UFE.GetPlayer2ControlsScript(), false);

                    if (UFE.GetPlayer2ControlsScript().mirror == -1)
                    {
                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.HorizontalAxis, -1);
                    }
                    else
                    {
                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.HorizontalAxis, 1);
                    }
                    break;

                case TrainingModeAIMode.BlockAll:
                    SetBlockVariables(UFE.GetPlayer2ControlsScript(), true);

                    SetParryVariables(UFE.GetPlayer2ControlsScript(), false);

                    SetHitTypeInput(UFE.GetPlayer1ControlsScript(), UFE.GetPlayer2Controller());
                    break;

                case TrainingModeAIMode.StandBlock:
                    SetBlockVariables(UFE.GetPlayer2ControlsScript(), true);

                    SetParryVariables(UFE.GetPlayer2ControlsScript(), false);
                    break;

                case TrainingModeAIMode.CrouchBlock:
                    SetBlockVariables(UFE.GetPlayer2ControlsScript(), true);

                    SetParryVariables(UFE.GetPlayer2ControlsScript(), false);

                    UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.VerticalAxis, -1);
                    break;

                case TrainingModeAIMode.ParryAll:
                    SetBlockVariables(UFE.GetPlayer2ControlsScript(), false);

                    SetParryVariables(UFE.GetPlayer2ControlsScript(), true);

                    SetHitTypeInput(UFE.GetPlayer1ControlsScript(), UFE.GetPlayer2Controller());
                    break;

                case TrainingModeAIMode.StandParry:
                    SetBlockVariables(UFE.GetPlayer2ControlsScript(), false);

                    SetParryVariables(UFE.GetPlayer2ControlsScript(), true);
                    break;

                case TrainingModeAIMode.CrouchParry:
                    SetBlockVariables(UFE.GetPlayer2ControlsScript(), false);

                    SetParryVariables(UFE.GetPlayer2ControlsScript(), true);

                    UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.VerticalAxis, -1);
                    break;

                case TrainingModeAIMode.AirParry:
                    SetBlockVariables(UFE.GetPlayer2ControlsScript(), false);

                    switch (UFE.GetPlayer2ControlsScript().currentState)
                    {
                        case PossibleStates.NeutralJump:
                        case PossibleStates.ForwardJump:
                        case PossibleStates.BackJump:
                            SetParryVariables(UFE.GetPlayer2ControlsScript(), true);
                            break;

                        default:
                            SetParryVariables(UFE.GetPlayer2ControlsScript(), false);
                            break;
                    }

                    UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.VerticalAxis, 1);
                    break;

                case TrainingModeAIMode.NeutralJump:
                    SetBlockVariables(UFE.GetPlayer2ControlsScript(), false);

                    SetParryVariables(UFE.GetPlayer2ControlsScript(), false);

                    UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.VerticalAxis, 1);
                    break;

                case TrainingModeAIMode.ForwardJump:
                    SetBlockVariables(UFE.GetPlayer2ControlsScript(), false);

                    SetParryVariables(UFE.GetPlayer2ControlsScript(), false);

                    if (UFE.GetPlayer2ControlsScript().mirror == -1)
                    {
                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.HorizontalAxis, 1);

                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.VerticalAxis, 1);
                    }
                    else
                    {
                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.HorizontalAxis, -1);

                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.VerticalAxis, 1);
                    }
                    break;

                case TrainingModeAIMode.BackwardJump:
                    SetBlockVariables(UFE.GetPlayer2ControlsScript(), false);

                    SetParryVariables(UFE.GetPlayer2ControlsScript(), false);

                    if (UFE.GetPlayer2ControlsScript().mirror == -1)
                    {
                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.HorizontalAxis, -1);

                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.VerticalAxis, 1);
                    }
                    else
                    {
                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.HorizontalAxis, 1);

                        UFE2FTEHelperMethodsManager.PressAxis(UFE.GetPlayer2Controller(), InputType.VerticalAxis, 1);
                    }
                    break;
            }
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

                if (UFE2FTEHelperMethodsManager.GetButtonPressFromBlockType(UFE.config.blockOptions.blockType) != ButtonPress.Back)
                {
                    UFE2FTEHelperMethodsManager.PressButton(UFE.GetPlayer2Controller(), UFE2FTEHelperMethodsManager.GetButtonPressFromBlockType(UFE.config.blockOptions.blockType));
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

        private static void SetHitTypeInput(ControlsScript attacker, UFEController defender)
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
                        && attacker.currentMove.currentFrame < UFE.GetPlayer1ControlsScript().currentMove.hits[i].activeFramesEnds)
                    {
                        if (attacker.currentMove.hits[i].hitType == HitType.Low
                            || attacker.currentMove.hits[i].hitType == HitType.Sweep)
                        {
                            UFE2FTEHelperMethodsManager.PressAxis(defender, InputType.VerticalAxis, -1);
                        }
                    }
                }
            }

            for (int i = 0; i < attacker.projectiles.Count; i++)
            {
                if (attacker.projectiles[i].data.hitType == HitType.Low
                    || attacker.projectiles[i].data.hitType == HitType.Sweep)
                {
                    UFE2FTEHelperMethodsManager.PressAxis(defender, InputType.VerticalAxis, -1);
                }
            }
        }
    }
}