using UFE3D;

namespace FreedTerror.UFE2
{
    [System.Serializable]
    public class CharacterData
    {
        public CharacterData opponentCharacterData;
        public CurrentFrameData currentFrameData;
        public bool armor;
        public int armorHitsRemaining;
        public bool invincibility;
        public bool standUp;

        public static void SetCharacterData(ControlsScript player, CharacterData characterData, CharacterData opponentCharacterData, OverrideOptions characterDataOverrideOptions)
        {
            if (player == null
                || player.opControlsScript == null
                || characterData == null
                || opponentCharacterData == null)
            {
                return;
            }

            characterData.opponentCharacterData = opponentCharacterData;

            if (player.currentMove == null)
            {
                characterData.currentFrameData = CurrentFrameData.Any;
            }
            else
            {
                characterData.currentFrameData = player.currentMove.currentFrameData;
            }

            if (player.currentMove != null
                && player.currentMove.armorOptions.hitAbsorption > 0
                && player.currentMove.armorOptions.hitsTaken < player.currentMove.armorOptions.hitAbsorption
                && player.currentMove.currentFrame >= player.currentMove.armorOptions.activeFramesBegin
                && player.currentMove.currentFrame <= player.currentMove.armorOptions.activeFramesEnds)
            {
                characterData.armor = true;

                characterData.armorHitsRemaining = player.currentMove.armorOptions.hitAbsorption - player.currentMove.armorOptions.hitsTaken;
            }
            else if (player.currentMove == null
                || player.currentMove.armorOptions.hitAbsorption <= 0
                || player.currentMove.armorOptions.hitsTaken >= player.currentMove.armorOptions.hitAbsorption
                || player.currentMove.currentFrame > player.currentMove.armorOptions.activeFramesEnds)
            {
                characterData.armor = false;

                characterData.armorHitsRemaining = 0;
            }

            if (player.currentMove != null)
            {
                int length = player.currentMove.invincibleBodyParts.Length;

                if (length <= 0)
                {
                    characterData.invincibility = false;
                }

                for (int i = 0; i < length; i++)
                {
                    if (player.currentMove.currentFrame >= player.currentMove.invincibleBodyParts[i].activeFramesBegin
                        && player.currentMove.currentFrame <= player.currentMove.invincibleBodyParts[i].activeFramesEnds
                        && player.currentMove.invincibleBodyParts[i].completelyInvincible == true)
                    {
                        characterData.invincibility = true;
                    }
                    else
                    {
                        characterData.invincibility = false;
                    }
                }
            }
            else
            {
                characterData.invincibility = false;
            }

            switch (player.currentBasicMoveReference)
            {
                case BasicMoveReference.StandUpDefault:
                case BasicMoveReference.StandUpFromAirJuggle:
                case BasicMoveReference.StandUpFromAirWallBounce:
                case BasicMoveReference.StandUpFromCrumple:
                case BasicMoveReference.StandUpFromGroundBounce:
                case BasicMoveReference.StandUpFromKnockBack:
                case BasicMoveReference.StandUpFromStandingHighHit:
                case BasicMoveReference.StandUpFromStandingMidHit:
                case BasicMoveReference.StandUpFromStandingWallBounce:
                case BasicMoveReference.StandUpFromSweep:
                    characterData.standUp = true;
                    break;

                default:
                    characterData.standUp = false;
                    break;
            }

            if (player.MoveSet.basicMoves.standUp.useMoveFile == true
                && player.MoveSet.basicMoves.standUp.moveInfo != null
                && player.currentMove != null
                && player.currentMove.moveName == player.MoveSet.basicMoves.standUp.moveInfo.moveName)
            {
                characterData.standUp = true;
            }
            else if (player.MoveSet.basicMoves.standUpFromAirHit.useMoveFile == true
                && player.MoveSet.basicMoves.standUpFromAirHit.moveInfo != null
                && player.currentMove != null
                && player.currentMove.moveName == player.MoveSet.basicMoves.standUpFromAirHit.moveInfo.moveName)
            {
                characterData.standUp = true;
            }
            else if (player.MoveSet.basicMoves.standUpFromAirWallBounce.useMoveFile == true
                && player.MoveSet.basicMoves.standUpFromAirWallBounce.moveInfo != null
                && player.currentMove != null
                && player.currentMove.moveName == player.MoveSet.basicMoves.standUpFromAirWallBounce.moveInfo.moveName)
            {
                characterData.standUp = true;
            }
            else if (player.MoveSet.basicMoves.standUpFromCrumple.useMoveFile == true
                && player.MoveSet.basicMoves.standUpFromCrumple.moveInfo != null
                && player.currentMove != null
                && player.currentMove.moveName == player.MoveSet.basicMoves.standUpFromCrumple.moveInfo.moveName)
            {
                characterData.standUp = true;
            }
            else if (player.MoveSet.basicMoves.standUpFromGroundBounce.useMoveFile == true
                && player.MoveSet.basicMoves.standUpFromGroundBounce.moveInfo != null
                && player.currentMove != null
                && player.currentMove.moveName == player.MoveSet.basicMoves.standUpFromGroundBounce.moveInfo.moveName)
            {
                characterData.standUp = true;
            }
            else if (player.MoveSet.basicMoves.standUpFromKnockBack.useMoveFile == true
                && player.MoveSet.basicMoves.standUpFromKnockBack.moveInfo != null
                && player.currentMove != null
                && player.currentMove.moveName == player.MoveSet.basicMoves.standUpFromKnockBack.moveInfo.moveName)
            {
                characterData.standUp = true;
            }
            else if (player.MoveSet.basicMoves.standUpFromStandingHighHit.useMoveFile == true
                && player.MoveSet.basicMoves.standUpFromStandingHighHit.moveInfo != null
                && player.currentMove != null
                && player.currentMove.moveName == player.MoveSet.basicMoves.standUpFromStandingHighHit.moveInfo.moveName)
            {
                characterData.standUp = true;
            }
            else if (player.MoveSet.basicMoves.standUpFromStandingMidHit.useMoveFile == true
                && player.MoveSet.basicMoves.standUpFromStandingMidHit.moveInfo != null
                && player.currentMove != null
                && player.currentMove.moveName == player.MoveSet.basicMoves.standUpFromStandingMidHit.moveInfo.moveName)
            {
                characterData.standUp = true;
            }
            else if (player.MoveSet.basicMoves.standUpFromStandingWallBounce.useMoveFile == true
                && player.MoveSet.basicMoves.standUpFromStandingWallBounce.moveInfo != null
                && player.currentMove != null
                && player.currentMove.moveName == player.MoveSet.basicMoves.standUpFromStandingWallBounce.moveInfo.moveName)
            {
                characterData.standUp = true;
            }
            else if (player.MoveSet.basicMoves.standUpFromSweep.useMoveFile == true
                && player.MoveSet.basicMoves.standUpFromSweep.moveInfo != null
                && player.currentMove != null
                && player.currentMove.moveName == player.MoveSet.basicMoves.standUpFromSweep.moveInfo.moveName)
            {
                characterData.standUp = true;
            }
            else if (player.currentMove == null)
            {
                characterData.standUp = false;
            }

            OverrideOptions.Override(characterDataOverrideOptions, player, characterData);
        }

        [System.Serializable]
        public class OverrideOptions
        {
            public static void Override(OverrideOptions overrideOptions, ControlsScript player, CharacterData characterData)
            {
                if (overrideOptions == null
                    || player == null
                    || characterData == null)
                {
                    return;
                }

                CurrentFrameDataOverrideOptions.Override(overrideOptions.currentFrameDataOverrideOptionsArray, player, characterData);
            }

            [System.Serializable]
            public class CurrentFrameDataOverrideOptions
            {
                public CurrentFrameData currentFrameData;
                public string[] moveNameArray;

                public static void Override(CurrentFrameDataOverrideOptions currentFrameDataOverrideOptions, ControlsScript player, CharacterData characterData)
                {
                    if (currentFrameDataOverrideOptions == null
                        || player == null
                        || characterData == null)
                    {
                        return;
                    }

                    if (player.currentMove != null)
                    {
                        int length = currentFrameDataOverrideOptions.moveNameArray.Length;
                        for (int i = 0; i < length; i++)
                        {
                            if (player.currentMove.moveName != currentFrameDataOverrideOptions.moveNameArray[i])
                            {
                                continue;
                            }

                            characterData.currentFrameData = currentFrameDataOverrideOptions.currentFrameData;
                        }
                    }
                }

                public static void Override(CurrentFrameDataOverrideOptions[] currentFrameDataOverrideOptions, ControlsScript player, CharacterData characterData)
                {
                    if (currentFrameDataOverrideOptions == null
                        || player == null
                        || characterData == null)
                    {
                        return;
                    }

                    int length = currentFrameDataOverrideOptions.Length;
                    for (int i = 0; i < length; i++)
                    {
                        Override(currentFrameDataOverrideOptions[i], player, characterData);
                    }
                }
            }
            public CurrentFrameDataOverrideOptions[] currentFrameDataOverrideOptionsArray;
        }
    }
}