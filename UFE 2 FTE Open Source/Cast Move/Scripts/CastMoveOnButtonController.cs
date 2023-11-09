using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    public class CastMoveOnButtonController : MonoBehaviour
    {
        [System.Serializable]
        public class CastMoveOnButtonOptions
        {
            public string castMoveName;
            public ButtonPress buttonPress;
            public PlayerConditions[] playerConditionsArray;

            public static void CheckCastMoveOnButtonOptions(CastMoveOnButtonOptions castMoveOnButtonOptions, ControlsScript player, ButtonPress buttonPress)
            {
                if (castMoveOnButtonOptions == null
                    || player == null)
                {
                    return;
                }

                int length = castMoveOnButtonOptions.playerConditionsArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (IsButtonPressMatch(buttonPress, castMoveOnButtonOptions.buttonPress) == true
                        && PlayerConditions.CheckPlayerConditions(player, castMoveOnButtonOptions.playerConditionsArray) == true)
                    {
                        UFE2FTE.CastMoveByMoveName(player, castMoveOnButtonOptions.castMoveName);
                    }
                }
            }

            public static void CheckCastMoveOnButtonOptions(CastMoveOnButtonOptions[] castMoveOnButtonOptionsArray, ControlsScript player, ButtonPress buttonPress)
            {
                if (castMoveOnButtonOptionsArray == null)
                {
                    return;
                }

                int length = castMoveOnButtonOptionsArray.Length;
                for (int i = 0; i < length; i++)
                {
                    CheckCastMoveOnButtonOptions(castMoveOnButtonOptionsArray[i], player, buttonPress);
                }
            }

            public static bool IsButtonPressMatch(ButtonPress comparing, ButtonPress matching)
            {
                if (comparing == matching)
                {
                    return true;
                }

                return false;
            }
        }
        [SerializeField]
        private CastMoveOnButtonOptions[] castMoveOnButtonOptionsArray;

        [System.Serializable]
        public class PlayerConditions
        {
            public enum Target
            {
                Player,
                Opponent
            }
            public Target target;
            public BasicMoveReference[] basicMoveArray;
            public PossibleStates[] possibleStates;
            public SubStates[] subStates;

            public static bool CheckPlayerConditions(ControlsScript player, PlayerConditions playerConditions)
            {
                if (player == null
                    || playerConditions == null)
                {
                    return false;
                }

                ControlsScript controlsScript = player;
                if (playerConditions.target == Target.Player)
                {
                    controlsScript = player;
                }
                else
                {
                    controlsScript = player.opControlsScript;
                }

                if (IsBasicMoveMatch(controlsScript.currentBasicMove, playerConditions.basicMoveArray) == true
                    && IsPossibleStatesMatch(controlsScript.currentState, playerConditions.possibleStates) == true
                    && IsSubStatesMatch(controlsScript.currentSubState, playerConditions.subStates) == true)
                {
                    return true;
                }

                return false;
            }

            public static bool CheckPlayerConditions(ControlsScript player, PlayerConditions[] playerConditionsArray)
            {
                if (player == null
                    || playerConditionsArray == null)
                {
                    return false;
                }

                int length = playerConditionsArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (CheckPlayerConditions(player, playerConditionsArray[i]) == true)
                    {
                        continue;
                    }

                    return false;
                }

                return true;
            }

            public static bool IsBasicMoveMatch(BasicMoveReference comparing, BasicMoveReference matching)
            {
                if (comparing == matching)
                {
                    return true;
                }

                return false;
            }

            public static bool IsBasicMoveMatch(BasicMoveReference comparing, BasicMoveReference[] matchingArray)
            {
                if (matchingArray == null)
                {
                    return false;
                }

                int length = matchingArray.Length;
                if (length <= 0)
                {
                    return true;
                }
                for (int i = 0; i < length; i++)
                {
                    if (IsBasicMoveMatch(comparing, matchingArray[i]) == false)
                    {
                        continue;
                    }

                    return true;
                }

                return false;
            }

            public static bool IsPossibleStatesMatch(PossibleStates comparing, PossibleStates matching)
            {
                if (comparing == matching)
                {
                    return true;
                }

                return false;
            }

            public static bool IsPossibleStatesMatch(PossibleStates comparing, PossibleStates[] matchingArray)
            {
                if (matchingArray == null)
                {
                    return false;
                }

                int length = matchingArray.Length;
                if (length <= 0)
                {
                    return true;
                }
                for (int i = 0; i < length; i++)
                {
                    if (IsPossibleStatesMatch(comparing, matchingArray[i]) == false)
                    {
                        continue;
                    }

                    return true;
                }

                return false;
            }

            public static bool IsSubStatesMatch(SubStates comparing, SubStates matching)
            {
                if (comparing == matching)
                {
                    return true;
                }

                return false;
            }

            public static bool IsSubStatesMatch(SubStates comparing, SubStates[] matchingArray)
            {
                if (matchingArray == null)
                {
                    return false;
                }

                int length = matchingArray.Length;
                if (length <= 0)
                {
                    return true;
                }
                for (int i = 0; i < length; i++)
                {
                    if (IsSubStatesMatch(comparing, matchingArray[i]) == false)
                    {
                        continue;
                    }

                    return true;
                }

                return false;
            }
        }

        private void OnEnable()
        {
            UFE.OnButton += OnButton;
        }

        private void OnDisable()
        {
            UFE.OnButton -= OnButton;
        }

        private void OnButton(ButtonPress button, ControlsScript player)
        {
            CastMoveOnButtonOptions.CheckCastMoveOnButtonOptions(castMoveOnButtonOptionsArray, player, button);
        }
    }
}