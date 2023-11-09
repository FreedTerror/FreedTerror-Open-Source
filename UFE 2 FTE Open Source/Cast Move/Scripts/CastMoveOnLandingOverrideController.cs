using UnityEngine;
using UFENetcode;

namespace UFE2FTE
{
    public class CastMoveOnLandingOverrideController : UFEBehaviour, UFEInterface
    {
        [SerializeField]
        private string[] includedMoveNameArray;

        public void FixedUpdate()
        {
            TryCastMoveOnLandingOverrideAllControlsScripts(includedMoveNameArray);   
        }

        public static void TryCastMoveOnLandingOverrideAllControlsScripts(string[] includedMoveNameArray)
        {
            if (UFE.GetPlayer1ControlsScript() != null)
            {
                CastMoveOnLandingOverride(UFE.GetPlayer1ControlsScript(), includedMoveNameArray);

                int count = UFE.GetPlayer1ControlsScript().assists.Count;
                for (int i = 0; i < count; i++)
                {
                    CastMoveOnLandingOverride(UFE.GetPlayer1ControlsScript().assists[i], includedMoveNameArray);
                }
            }

            if (UFE.GetPlayer2ControlsScript() != null)
            {
                CastMoveOnLandingOverride(UFE.GetPlayer2ControlsScript(), includedMoveNameArray);

                int count = UFE.GetPlayer2ControlsScript().assists.Count;
                for (int i = 0; i < count; i++)
                {
                    CastMoveOnLandingOverride(UFE.GetPlayer2ControlsScript().assists[i], includedMoveNameArray);
                }
            }
        }

        public static void CastMoveOnLandingOverride(ControlsScript player, string[] includedMoveNameArray)
        {
            if (player == null
                || player.currentMove == null
                || player.currentMove.cancelMoveWhenLanding == false
                || player.currentMove.landingMoveLink == null
                || player.Physics.IsGrounded() == false
                || IsStringMatch(player.currentMove.moveName, includedMoveNameArray) == false)
            {
                return;
            }

            player.CastMove(player.currentMove.landingMoveLink, true);
        }

        public static bool IsStringMatch(string comparing, string matching)
        {
            if (comparing == matching)
            {
                return true;
            }

            return false;
        }

        public static bool IsStringMatch(string comparing, string[] matching)
        {
            if (matching == null)
            {
                return false;
            }

            int length = matching.Length;
            for (int i = 0; i < length; i++)
            {
                if (IsStringMatch(comparing, matching[i]) == false)
                {
                    continue;
                }

                return true;
            }

            return false;
        }
    }
}