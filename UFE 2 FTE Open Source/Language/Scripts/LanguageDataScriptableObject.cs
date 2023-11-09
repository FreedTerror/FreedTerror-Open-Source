using UnityEngine;
using UFE3D;

namespace UFE2FTE
{
    [CreateAssetMenu()]
    public class LanguageDataScriptableObject : ScriptableObject
    {
        public string LanguageName = "English";
        public bool DefaultLanguage = true;

        public string ProjectName = "Project Name";

        public string Armor = "Armor";
        public string ArmorBreaker = "Armor Breaker";
        public string ArmorBroken = "Armor Broken";
        public string ArmoredHit = "Armored Hit";
        public string Attributes = "Attributes";

        public string Back = "Back";
        public string BlockAll = "Block All";
        public string BlockStand = "Stand Block";
        public string BlockCrouch = "Crouch Block";

        public string Challenge = "Challenge";
        public string ChallengeMode = "Challenge Mode";
        public string Crouch = "Crouch";
        public string ChargeTiming = "Charge Timing";
        public string CounterHit = "Counter Hit";

        public string GroundBounce = "Ground Bounce";

        public string DoubleKo = "Double K.O.";
        public string Draw = "Draw";

        public string Friction = "Friction";
        public string FinalRound = "Final Round";
        public string Fight = "Fight";
        public string FrameAbbrevation = "F";
        public string FirstHit = "First Hit";
        public string ForceStand = "Force Stand";

        public string HighKnockdown = "High Knockdown";
        public string Human = "Human";
        public string HitBoxDisplaySpriteRenderer2DInfront = "2D Infront";
        public string HitBoxDisplaySpriteRenderer2DBehind = "2D Behind";
        public string HitBoxDisplayMeshRenderer3D = "3D";
        public string HitBoxDisplayPopcronGizmos2D = "Popcron Gizmos 2D";
        public string HitBoxDisplayPopcronGizmos3D = "Popcron Gizmos 3D";

        public string Infinite = "Infinite";
        public string Invincibility = "Invincibility";
        public string InputDisplaySequence = "Sequence";
        public string InputDisplayPress = "Press";
        public string InputDisplayRelease = "Release";
        public string InputDisplayForward = ">";
        public string InputDisplayBack = ">";
        public string InputDisplayUp = ">";
        public string InputDisplayUpForward = ">";
        public string InputDisplayUpBack = ">";
        public string InputDisplayDown = ">";
        public string InputDisplayDownForward = ">";
        public string InputDisplayDownBack = ">";
        public string InputDisplayButton1 = "1";
        public string InputDisplayButton2 = "2";
        public string InputDisplayButton3 = "3";
        public string InputDisplayButton4 = "4";
        public string InputDisplayButton5 = "5";
        public string InputDisplayButton6 = "6";
        public string InputDisplayButton7 = "7";
        public string InputDisplayButton8 = "8";
        public string InputDisplayButton9 = "9";
        public string InputDisplayButton10 = "10";
        public string InputDisplayButton11 = "11";
        public string InputDisplayButton12 = "12";

        public string Juggle = "Juggle";
        public string JumpNeutral = "Neutral Jump";
        public string JumpForward = "Forward Jump";
        public string JumpBackward = "Backward Jump";
        public string JumpStartupFrames = "Jump Startup Frames";
        public string JumpLandingFrames = "Jump Landing Frames";
        public string JumpForce = "Jump Force";
        public string JumpForwardDistance = "Jump Forward Distance";
        public string JumpBackDistance = "Jump Back Distance";

        public string Knockback = "Knockback";
        public string Ko = "K.O.";

        public string Low = "Low";
        public string Launcher = "Launcher";

        public string MoveForward = "Move Forward";
        public string MoveBackward = "Move Backward";
        public string MoveList = "Move List";
        public string MaxLifePoints = "Maximum Life Points";
        public string MaxGaugePoints = "Maximum Gauge Points";
        public string MoveForwardSpeed = "Walk Forward Speed";
        public string MoveBackSpeed = "Walk Back Speed";
        public string MoveSidewaysSpeed = "Walk Sideways Speed";
        public string Mid = "Mid";
        public string MidKnockdown = "Mid Knockdown";

        public string No = "No";
        public string Normal = "Normal";
        public string NegativeAbbrevation = "-";

        public string On = "On";
        public string Off = "Off";
        public string Overhead = "Overhead";
        public string OnlineMode = "Online Mode";
        public string Otg = "Otg";

        public string Pause = "Pause";
        public string Player1 = "Player 1";
        public string Player2 = "Player 2";
        public string PotentialParry = "PotentialParry";
        public string Parry = "Parry";
        public string ParryAll = "Parry All";
        public string ParryStand = "Stand Parry";
        public string ParryCrouch = "Crouch Parry";
        public string ParryJump = "Jump Parry";
     
        public string Perfect = "Perfect";
        public string Punish = "Punish";
        public string PercentAbbrevation = "%";
        public string PositiveAbbrevation = "+";

        public string Quit = "Quit";

        public string Refill = "Refill";
        public string Round = "Round";
        public string Ready = "Ready";

        public string Stand = "Stand";
        public string Sweep = "Sweep";
        public string Seconds = "Seconds";
        public string Success = "Success";
        public string SecondsAbbrevation = "S";

        public string TimeOut = "Time Out";
        public string TrainingMode = "Training Mode";
        public string TutorialMode = "Tutorial Mode";
        public string Tutorial = "Tutorial";
        public string Throw = "Throw";

        public string Unblockable;

        public string Weight = "Weight";
        public string WallBounce = "Wall Bounce";
        public string WakeUp = "Wall Bounce";

        public string Yes = "Yes";

        public string GetStringFromHitType(HitType hitType)
        {
            switch (hitType)
            {
                case HitType.Mid:
                    return Knockback;

                case HitType.Low:
                    return Low;

                case HitType.Overhead:
                    return Overhead;

                case HitType.Launcher:
                    return Launcher;

                case HitType.HighKnockdown:
                    return HighKnockdown;

                case HitType.MidKnockdown:
                    return MidKnockdown;

                case HitType.KnockBack:
                    return Knockback;

                case HitType.Sweep:
                    return Sweep;

                default:
                    return "";
            }
        }

        public string GetStringFromHitType(HitType hitType, HitConfirmType hitConfirmType)
        {
            if (hitConfirmType == HitConfirmType.Hit)
            {
                switch (hitType)
                {
                    case HitType.Mid:
                        return Mid;

                    case HitType.Low:
                        return Low;

                    case HitType.Overhead:
                        return Overhead;

                    case HitType.Launcher:
                        return Launcher;

                    case HitType.HighKnockdown:
                        return HighKnockdown;

                    case HitType.MidKnockdown:
                        return MidKnockdown;

                    case HitType.KnockBack:
                        return Knockback;

                    case HitType.Sweep:
                        return Sweep;

                    default:
                        return "";
                }
            }
            else if (hitConfirmType == HitConfirmType.Throw)
            {
                return Throw;
            }

            return "";
        }
    }
}