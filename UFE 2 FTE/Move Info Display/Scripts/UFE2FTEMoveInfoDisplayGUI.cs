using System;
using UnityEngine;
using UnityEngine.UI;
using UFE3D;

namespace UFE2FTE
{
    public class UFE2FTEMoveInfoDisplayGUI : MonoBehaviour
    {
        // TODO: Look into frame advantage total text on throw hit.

        [SerializeField]
        private string midHitTypeName = "MID";
        [SerializeField]
        private string lowHitTypeName = "LOW";
        [SerializeField]
        private string overheadHitTypeName = "OVERHEAD";
        [SerializeField]
        private string launcherHitTypeName = "LAUNCHER";
        [SerializeField]
        private string highKnockdownHitTypeName = "HIGH KNOCKDOWN";
        [SerializeField]
        private string midKnockdownHitTypeName = "MID KNOCKDOWN";
        [SerializeField]
        private string knockbackHitTypeName = "KNOCKBACK";
        [SerializeField]
        private string sweepHitTypeName = "SWEEP";
        [SerializeField]
        private string throwHitTypeName = "THROW";
        [SerializeField]
        private UFE2FTEGCFreeStringNumbersScriptableObject gCFreeStringNumbersScriptableObject;

        [Serializable]
        public class PlayerMoveInfoDisplayGUI
        {
            [HideInInspector]
            public bool isHit;

            public Text lifePointsText;

            public Text moveNameText;

            public Text hitTypeText;

            public Text minDamageOnHitText;
            public Text damageOnHitText;       
            public Text damageOnBlockText;

            public Text startupFramesText;
            public Text activeFramesText;
            public Text recoveryFramesText;
            public Text totalFramesText;

            public Text frameAdvantageOnHitText;
            public Text frameAdvantageOnBlockText;
            public Text frameAdvantageTotalText;
            [HideInInspector]
            public int frameAdvantageTotal;

            public Text armorActiveFramesBeginText;
            public Text armorActiveFramesEndsText;
            public Text armorHitAbsorptionText;

            public GameObject armorBreakerGameObject;
            public GameObject unblockableGameObject;
            public GameObject groundBounceGameObject;
            public GameObject wallBounceGameObject;
            public GameObject otgGameObject;
            public GameObject throwTechableGameObject;
            public GameObject throwUntechableGameObject;
        }
        [SerializeField]
        private PlayerMoveInfoDisplayGUI player1MoveInfoDisplayGUI;
        [SerializeField]
        private PlayerMoveInfoDisplayGUI player2MoveInfoDisplayGUI;

        private PlayerMoveInfoDisplayGUI GetPlayerMoveInfoDisplay(ControlsScript player)
        {
            if (player == UFE.GetPlayer1ControlsScript())
            {
                return player1MoveInfoDisplayGUI;
            }
            else if (player == UFE.GetPlayer2ControlsScript())
            {
                return player2MoveInfoDisplayGUI;
            }

            return player1MoveInfoDisplayGUI;
        }

        private void OnEnable()
        {
            
        }

        private void Update()
        {
            if (UFE.isPaused() == true
                || UFE.GetPlayer1ControlsScript() == null
                || UFE.GetPlayer2ControlsScript() == null
                || gCFreeStringNumbersScriptableObject == null)
            {
                return;
            }

            SetPlayerLifePointsText(UFE.GetPlayer1ControlsScript());

            SetPlayerLifePointsText(UFE.GetPlayer2ControlsScript());

            if (UFE.GetPlayer1ControlsScript().currentMove != null)
            {
                DecrementPlayerFrameAdvantage(UFE.GetPlayer1ControlsScript());

                int length = UFE.GetPlayer1ControlsScript().currentMove.hits.Length;
                for (int i = 0; i < length; i++)
                {
                    if (UFE.GetPlayer1ControlsScript().currentMove.currentFrame >= UFE.GetPlayer1ControlsScript().currentMove.hits[i].activeFramesBegin)
                    {
                        DisplayHitData(UFE.GetPlayer1ControlsScript(), UFE.GetPlayer1ControlsScript().currentMove.hits[i]);
                    }
                }
            }
            else
            {
                IncrementPlayerFrameAdvantage(UFE.GetPlayer1ControlsScript());
            }

            if (UFE.GetPlayer2ControlsScript().currentMove != null)
            {
                DecrementPlayerFrameAdvantage(UFE.GetPlayer2ControlsScript());

                int length = UFE.GetPlayer2ControlsScript().currentMove.hits.Length;
                for (int i = 0; i < length; i++)
                {
                    if (UFE.GetPlayer2ControlsScript().currentMove.currentFrame >= UFE.GetPlayer2ControlsScript().currentMove.hits[i].activeFramesBegin)
                    {
                        DisplayHitData(UFE.GetPlayer2ControlsScript(), UFE.GetPlayer2ControlsScript().currentMove.hits[i]);
                    }
                }
            }
            else
            {
                IncrementPlayerFrameAdvantage(UFE.GetPlayer2ControlsScript());
            }
        }

        private void OnDisable()
        {
            
        }

        private void SetPlayerLifePointsText(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            SetTextMessage(GetPlayerMoveInfoDisplay(player).lifePointsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, Mathf.FloorToInt((float)player.currentLifePoints)));
        }

        private void IncrementPlayerFrameAdvantage(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            switch (player.currentSubState)
            {
                case SubStates.Stunned:
                case SubStates.Blocking:
                    if (GetPlayerMoveInfoDisplay(player).isHit == false)
                    {
                        GetPlayerMoveInfoDisplay(player.opControlsScript).frameAdvantageTotal = 0;
                    }

                    GetPlayerMoveInfoDisplay(player).isHit = true;

                    GetPlayerMoveInfoDisplay(player.opControlsScript).frameAdvantageTotal++;

                    SetTextMessage(GetPlayerMoveInfoDisplay(player.opControlsScript).frameAdvantageTotalText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, GetPlayerMoveInfoDisplay(player.opControlsScript).frameAdvantageTotal));
                    break;

                default:
                    GetPlayerMoveInfoDisplay(player).isHit = false;

                    if (GetPlayerMoveInfoDisplay(player.opControlsScript).frameAdvantageTotal == 0)
                    {
                        SetTextMessage(GetPlayerMoveInfoDisplay(player.opControlsScript).frameAdvantageTotalText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, GetPlayerMoveInfoDisplay(player.opControlsScript).frameAdvantageTotal));
                    }
                    break;
            }
        }

        private void DecrementPlayerFrameAdvantage(ControlsScript player)
        {
            if (player == null)
            {
                return;
            }

            switch (player.currentSubState)
            {
                case SubStates.Stunned:
                case SubStates.Blocking:
                    GetPlayerMoveInfoDisplay(player.opControlsScript).isHit = true;

                    GetPlayerMoveInfoDisplay(player).frameAdvantageTotal = 0;
                    break;

                default:
                    if (GetPlayerMoveInfoDisplay(player.opControlsScript).isHit == false) return;

                    GetPlayerMoveInfoDisplay(player).frameAdvantageTotal++;

                    SetTextMessage(GetPlayerMoveInfoDisplay(player.opControlsScript).frameAdvantageTotalText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.negativeStringNumberArray, GetPlayerMoveInfoDisplay(player.opControlsScript).frameAdvantageTotal));
                    break;
            }
        }

        private void DisplayHitData(ControlsScript player, Hit hit)
        {
            if (player == null
                || hit == null)
            {
                return;
            }

            SetTextMessage(GetPlayerMoveInfoDisplay(player).moveNameText, player.currentMove.moveName);

            SetTextMessage(GetPlayerMoveInfoDisplay(player).startupFramesText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, player.currentMove.startUpFrames));

            SetTextMessage(GetPlayerMoveInfoDisplay(player).activeFramesText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, player.currentMove.activeFrames));

            SetTextMessage(GetPlayerMoveInfoDisplay(player).recoveryFramesText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, player.currentMove.recoveryFrames));

            SetTextMessage(GetPlayerMoveInfoDisplay(player).totalFramesText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, player.currentMove.totalFrames));

            SetTextMessage(GetPlayerMoveInfoDisplay(player).armorActiveFramesBeginText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, player.currentMove.armorOptions.activeFramesBegin));

            SetTextMessage(GetPlayerMoveInfoDisplay(player).armorActiveFramesEndsText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, player.currentMove.armorOptions.activeFramesEnds));

            SetTextMessage(GetPlayerMoveInfoDisplay(player).armorHitAbsorptionText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, player.currentMove.armorOptions.hitAbsorption));

            if (hit.hitConfirmType == HitConfirmType.Hit)
            {
                SetTextMessage(GetPlayerMoveInfoDisplay(player).hitTypeText, GetHitTypeNameFromHitType(hit.hitType));

                if (hit.damageType == DamageType.Percentage)
                {
                    SetTextMessage(GetPlayerMoveInfoDisplay(player).damageOnHitText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positivePercentStringNumberArray, UnityEngine.Mathf.FloorToInt((float)hit._damageOnHit)));

                    SetTextMessage(GetPlayerMoveInfoDisplay(player).minDamageOnHitText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positivePercentStringNumberArray, UnityEngine.Mathf.FloorToInt((float)hit._minDamageOnHit)));

                    SetTextMessage(GetPlayerMoveInfoDisplay(player).damageOnBlockText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positivePercentStringNumberArray, UnityEngine.Mathf.FloorToInt((float)hit._damageOnBlock)));
                }
                else if (hit.damageType == DamageType.Points)
                {
                    SetTextMessage(GetPlayerMoveInfoDisplay(player).damageOnHitText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, UnityEngine.Mathf.FloorToInt((float)hit._damageOnHit)));

                    SetTextMessage(GetPlayerMoveInfoDisplay(player).minDamageOnHitText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, UnityEngine.Mathf.FloorToInt((float)hit._minDamageOnHit)));

                    SetTextMessage(GetPlayerMoveInfoDisplay(player).damageOnBlockText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, UnityEngine.Mathf.FloorToInt((float)hit._damageOnBlock)));
                }

                switch (hit.hitStunType)
                {
                    case HitStunType.FrameAdvantage:
                        if (hit.frameAdvantageOnHit < 0)
                        {
                            SetTextMessage(GetPlayerMoveInfoDisplay(player).frameAdvantageOnHitText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.negativeStringNumberArray, UnityEngine.Mathf.Abs(hit.frameAdvantageOnHit)));
                        }
                        else if (hit.frameAdvantageOnHit >= 0)
                        {
                            SetTextMessage(GetPlayerMoveInfoDisplay(player).frameAdvantageOnHitText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, hit.frameAdvantageOnHit));
                        }

                        if (hit.frameAdvantageOnBlock < 0)
                        {
                            SetTextMessage(GetPlayerMoveInfoDisplay(player).frameAdvantageOnBlockText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.negativeStringNumberArray, UnityEngine.Mathf.Abs(hit.frameAdvantageOnBlock)));
                        }
                        else if (hit.frameAdvantageOnHit >= 0)
                        {
                            SetTextMessage(GetPlayerMoveInfoDisplay(player).frameAdvantageOnBlockText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, hit.frameAdvantageOnBlock));
                        }
                        break;

                    case HitStunType.Frames:
                        if (hit.frameAdvantageOnHit < 0)
                        {
                            SetTextMessage(GetPlayerMoveInfoDisplay(player).frameAdvantageOnHitText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.negativeFrameStringNumberArray, UnityEngine.Mathf.Abs(hit.frameAdvantageOnHit)));
                        }
                        else if (hit.frameAdvantageOnHit >= 0)
                        {
                            SetTextMessage(GetPlayerMoveInfoDisplay(player).frameAdvantageOnHitText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveFrameStringNumberArray, hit.frameAdvantageOnHit));
                        }

                        if (hit.frameAdvantageOnBlock < 0)
                        {
                            SetTextMessage(GetPlayerMoveInfoDisplay(player).frameAdvantageOnBlockText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.negativeFrameStringNumberArray, UnityEngine.Mathf.Abs(hit.frameAdvantageOnBlock)));
                        }
                        else if (hit.frameAdvantageOnHit >= 0)
                        {
                            SetTextMessage(GetPlayerMoveInfoDisplay(player).frameAdvantageOnBlockText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveFrameStringNumberArray, hit.frameAdvantageOnBlock));
                        }
                        break;

                    case HitStunType.Seconds:
                        if (hit.frameAdvantageOnHit < 0)
                        {
                            SetTextMessage(GetPlayerMoveInfoDisplay(player).frameAdvantageOnHitText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveSecondsStringNumberArray, UnityEngine.Mathf.Abs(hit.frameAdvantageOnHit)));
                        }
                        else if (hit.frameAdvantageOnHit >= 0)
                        {
                            SetTextMessage(GetPlayerMoveInfoDisplay(player).frameAdvantageOnHitText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveSecondsStringNumberArray, hit.frameAdvantageOnHit));
                        }

                        if (hit.frameAdvantageOnBlock < 0)
                        {
                            SetTextMessage(GetPlayerMoveInfoDisplay(player).frameAdvantageOnBlockText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveSecondsStringNumberArray, UnityEngine.Mathf.Abs(hit.frameAdvantageOnBlock)));
                        }
                        else if (hit.frameAdvantageOnHit >= 0)
                        {
                            SetTextMessage(GetPlayerMoveInfoDisplay(player).frameAdvantageOnBlockText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveSecondsStringNumberArray, hit.frameAdvantageOnBlock));
                        }
                        break;
                }

                SetGameObjectActive(GetPlayerMoveInfoDisplay(player).armorBreakerGameObject, hit.armorBreaker);

                SetGameObjectActive(GetPlayerMoveInfoDisplay(player).unblockableGameObject, hit.unblockable);

                SetGameObjectActive(GetPlayerMoveInfoDisplay(player).groundBounceGameObject, hit.groundBounce);

                SetGameObjectActive(GetPlayerMoveInfoDisplay(player).wallBounceGameObject, hit.wallBounce);

                SetGameObjectActive(GetPlayerMoveInfoDisplay(player).otgGameObject, hit.downHit);

                SetGameObjectActive(GetPlayerMoveInfoDisplay(player).throwTechableGameObject, false);

                SetGameObjectActive(GetPlayerMoveInfoDisplay(player).throwUntechableGameObject, false);
            }
            else if (hit.hitConfirmType == HitConfirmType.Throw)
            {
                SetTextMessage(GetPlayerMoveInfoDisplay(player).hitTypeText, throwHitTypeName);

                SetTextMessage(GetPlayerMoveInfoDisplay(player).minDamageOnHitText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, 0));

                SetTextMessage(GetPlayerMoveInfoDisplay(player).damageOnHitText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, 0));

                SetTextMessage(GetPlayerMoveInfoDisplay(player).damageOnBlockText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, 0));

                SetTextMessage(GetPlayerMoveInfoDisplay(player).frameAdvantageOnHitText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, 0));

                SetTextMessage(GetPlayerMoveInfoDisplay(player).frameAdvantageOnBlockText, UFE2FTEGCFreeStringNumbersScriptableObject.GetStringFromStringArray(gCFreeStringNumbersScriptableObject, gCFreeStringNumbersScriptableObject.positiveStringNumberArray, 0));

                SetGameObjectActive(GetPlayerMoveInfoDisplay(player).armorBreakerGameObject, false);

                SetGameObjectActive(GetPlayerMoveInfoDisplay(player).armorBreakerGameObject, false);

                SetGameObjectActive(GetPlayerMoveInfoDisplay(player).unblockableGameObject, false);

                SetGameObjectActive(GetPlayerMoveInfoDisplay(player).groundBounceGameObject, false);

                SetGameObjectActive(GetPlayerMoveInfoDisplay(player).wallBounceGameObject, false);

                SetGameObjectActive(GetPlayerMoveInfoDisplay(player).otgGameObject, hit.downHit);

                if (hit.techable == true)
                {
                    SetGameObjectActive(GetPlayerMoveInfoDisplay(player).throwTechableGameObject, true);

                    SetGameObjectActive(GetPlayerMoveInfoDisplay(player).throwUntechableGameObject, false);
                }
                else
                {
                    SetGameObjectActive(GetPlayerMoveInfoDisplay(player).throwTechableGameObject, false);

                    SetGameObjectActive(GetPlayerMoveInfoDisplay(player).throwUntechableGameObject, true);
                }
            }
        }

        private string GetHitTypeNameFromHitType(HitType hitType)
        {
            switch (hitType)
            {
                case HitType.Mid: return midHitTypeName;

                case HitType.Low: return lowHitTypeName;

                case HitType.Overhead: return overheadHitTypeName;

                case HitType.Launcher: return launcherHitTypeName;

                case HitType.HighKnockdown: return highKnockdownHitTypeName;

                case HitType.MidKnockdown: return midKnockdownHitTypeName;

                case HitType.KnockBack: return knockbackHitTypeName;

                case HitType.Sweep: return sweepHitTypeName;

                default: return "";
            }
        }

        private static void SetGameObjectActive(GameObject gameObject, bool active)
        {
            if (gameObject == null)
            {
                return;
            }

            gameObject.SetActive(active);
        }

        private static void SetTextMessage(Text text, string message, Color32? color = null)
        {
            if (text == null)
            {
                return;
            }

            text.text = message;

            if (color != null)
            {
                text.color = (Color32)color;
            }
        }

        // Credit to Matt Ponton for contributing to the code.
    }
}
