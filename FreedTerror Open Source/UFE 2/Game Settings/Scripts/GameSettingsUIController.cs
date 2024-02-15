using FPLibrary;
using UFE3D;
using UnityEngine;
using UnityEngine.UI;

namespace FreedTerror.UFE2
{
    public class GameSettingsUIController : MonoBehaviour
    {
        [SerializeField]
        private Text totalRoundsText;
        private int previousTotalRounds;

        [SerializeField]
        private Text timerText;
        private Fix64 previousTimer;

        [SerializeField]
        private Text maximumComboHitsText;
        private int previousMaximumComboHits;

        [SerializeField]
        private Text maximumGroundBouncesText;
        private int previousMaximumGroundBounces;

        [SerializeField]
        private Text maximumWallBouncesText;
        private int previousMaximumWallBounces;

        [SerializeField]
        private Text damageScalingText;
        private Sizes previousDamageScaling;

        [SerializeField]
        private Text hitStunScalingText;
        private Sizes previousHitStunScaling;

        [SerializeField]
        private Text blockInputText;
        private BlockType previousBlockInput;

        [SerializeField]
        private Text allowAirBlockText;

        [SerializeField]
        private Text parryInputText;
        private ParryType previousParryInput;

        [SerializeField]
        private Text allowAirParryText;

        [SerializeField]
        private Text parryTimingText;
        [SerializeField]
        private Text parryTimingFramesText;
        private Fix64 previousParryTiming;

        private void Start()
        {
            if (UFE.config == null)
            {
                return;
            }

            previousTotalRounds = UFE.config.roundOptions.totalRounds;

            if (totalRoundsText != null)
            {
                totalRoundsText.text = UFE.config.roundOptions.totalRounds.ToString();
            }

            previousTimer = UFE.config.roundOptions._timer;

            if (timerText != null)
            {
                timerText.text = UFE.config.roundOptions._timer.ToString();
            }

            previousMaximumComboHits = UFE.config.comboOptions.maxCombo;

            if (maximumComboHitsText != null)
            {
                maximumComboHitsText.text = UFE.config.comboOptions.maxCombo.ToString();
            }

            previousMaximumGroundBounces = UFE.config.groundBounceOptions._maximumBounces;

            if (maximumGroundBouncesText != null)
            {
                maximumGroundBouncesText.text = UFE.config.groundBounceOptions._maximumBounces.ToString();
            }

            previousMaximumWallBounces = UFE.config.wallBounceOptions._maximumBounces;

            if (maximumWallBouncesText != null)
            {
                maximumWallBouncesText.text = UFE.config.wallBounceOptions._maximumBounces.ToString();
            }

            previousDamageScaling = UFE.config.comboOptions.damageDeterioration;

            if (damageScalingText != null)
            {
                damageScalingText.text = Utility.AddSpacesBeforeCapitalLetters(System.Enum.GetName(typeof(Sizes), UFE.config.comboOptions.damageDeterioration));
            }

            previousHitStunScaling = UFE.config.comboOptions.hitStunDeterioration;

            if (hitStunScalingText != null)
            {
                hitStunScalingText.text = Utility.AddSpacesBeforeCapitalLetters(System.Enum.GetName(typeof(Sizes), UFE.config.comboOptions.hitStunDeterioration));
            }

            previousBlockInput = UFE.config.blockOptions.blockType;

            if (blockInputText != null)
            {
                blockInputText.text = Utility.AddSpacesBeforeCapitalLetters(System.Enum.GetName(typeof(BlockType), UFE.config.blockOptions.blockType));
            }

            previousParryInput = UFE.config.blockOptions.parryType;

            if (parryInputText != null)
            {
                parryInputText.text = Utility.AddSpacesBeforeCapitalLetters(System.Enum.GetName(typeof(ParryType), UFE.config.blockOptions.parryType));
            }

            previousParryTiming = UFE.config.blockOptions._parryTiming;

            if (parryTimingText != null)
            {
                parryTimingText.text = UFE.config.blockOptions._parryTiming.ToString();
            }

            if (parryTimingFramesText != null)
            {
                parryTimingFramesText.text = Fix64.Floor(UFE.config.blockOptions._parryTiming * UFE.config.fps).ToString();
            }
        }

        private void Update()
        {
            if (UFE.config == null)
            {
                return;
            }

            if (previousTotalRounds != UFE.config.roundOptions.totalRounds)
            {
                previousTotalRounds = UFE.config.roundOptions.totalRounds;

                if (totalRoundsText != null)
                {
                    totalRoundsText.text = UFE.config.roundOptions.totalRounds.ToString();
                }
            }

            if (previousTimer != UFE.config.roundOptions._timer)
            {
                previousTimer = UFE.config.roundOptions._timer;

                if (timerText != null)
                {
                    timerText.text = UFE.config.roundOptions._timer.ToString();
                }
            }

            if (previousMaximumComboHits != UFE.config.comboOptions.maxCombo)
            {
                previousMaximumComboHits = UFE.config.comboOptions.maxCombo;

                if (maximumComboHitsText != null)
                {
                    maximumComboHitsText.text = UFE.config.comboOptions.maxCombo.ToString();
                }
            }

            if (previousMaximumGroundBounces != UFE.config.groundBounceOptions._maximumBounces)
            {
                previousMaximumGroundBounces = UFE.config.groundBounceOptions._maximumBounces;

                if (maximumGroundBouncesText != null)
                {
                    maximumGroundBouncesText.text = UFE.config.groundBounceOptions._maximumBounces.ToString();
                }
            }

            if (previousMaximumWallBounces != UFE.config.wallBounceOptions._maximumBounces)
            {
                previousMaximumWallBounces = UFE.config.wallBounceOptions._maximumBounces;

                if (maximumWallBouncesText != null)
                {
                    maximumWallBouncesText.text = UFE.config.wallBounceOptions._maximumBounces.ToString();
                }
            }

            if (previousDamageScaling != UFE.config.comboOptions.damageDeterioration)
            {
                previousDamageScaling = UFE.config.comboOptions.damageDeterioration;

                if (damageScalingText != null)
                {
                    damageScalingText.text = Utility.AddSpacesBeforeCapitalLetters(System.Enum.GetName(typeof(Sizes), UFE.config.comboOptions.damageDeterioration));
                }
            }

            if (previousHitStunScaling != UFE.config.comboOptions.hitStunDeterioration)
            {
                previousHitStunScaling = UFE.config.comboOptions.hitStunDeterioration;

                if (hitStunScalingText != null)
                {
                    hitStunScalingText.text = Utility.AddSpacesBeforeCapitalLetters(System.Enum.GetName(typeof(Sizes), UFE.config.comboOptions.hitStunDeterioration));
                }
            }

            if (previousBlockInput != UFE.config.blockOptions.blockType)
            {
                previousBlockInput = UFE.config.blockOptions.blockType;

                if (blockInputText != null)
                {
                    blockInputText.text = Utility.AddSpacesBeforeCapitalLetters(System.Enum.GetName(typeof(BlockType), UFE.config.blockOptions.blockType));
                }
            }

            if (allowAirBlockText != null)
            {
                allowAirBlockText.text = Utility.GetStringFromBool(UFE.config.blockOptions.allowAirBlock);
            }

            if (previousParryInput != UFE.config.blockOptions.parryType)
            {
                previousParryInput = UFE.config.blockOptions.parryType;

                if (parryInputText != null)
                {
                    parryInputText.text = Utility.AddSpacesBeforeCapitalLetters(System.Enum.GetName(typeof(ParryType), UFE.config.blockOptions.parryType));
                }
            }

            if (allowAirParryText != null)
            {
                allowAirParryText.text = Utility.GetStringFromBool(UFE.config.blockOptions.allowAirParry);
            }

            if (previousParryTiming != UFE.config.blockOptions._parryTiming)
            {
                previousParryTiming = UFE.config.blockOptions._parryTiming;

                if (parryTimingText != null)
                {
                    parryTimingText.text = UFE.config.blockOptions._parryTiming.ToString();
                }

                if (parryTimingFramesText != null)
                {
                    parryTimingFramesText.text = Fix64.Floor(UFE.config.blockOptions._parryTiming * UFE.config.fps).ToString();
                }
            }
        }

        public void DefaultGameSettings()
        {
            if (UFE2Manager.instance.defaultGameSettingsScriptableObject == null)
            {
                return;
            }

            UFE2Manager.instance.defaultGameSettingsScriptableObject.UpdateGameSettings();
        }

        public void NextTotalRounds()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.roundOptions.totalRounds += 2;

            if (UFE.config.roundOptions.totalRounds > 9)
            {
                UFE.config.roundOptions.totalRounds = 1;
            }
        }

        public void PreviousTotalRounds()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.roundOptions.totalRounds -= 2;

            if (UFE.config.roundOptions.totalRounds < 1)
            {
                UFE.config.roundOptions.totalRounds = 9;
            }
        }

        public void NextTimer()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.roundOptions._timer += (Fix64)30;

            if (UFE.config.roundOptions._timer > (Fix64)990)
            {
                UFE.config.roundOptions._timer = (Fix64)30;
            }

            UFE.config.roundOptions._timer = Fix64.Round(UFE.config.roundOptions._timer * (Fix64)100) / (Fix64)100;

            UFE.SetTimer(UFE.config.roundOptions._timer);
        }

        public void PreviousTimer()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.roundOptions._timer -= (Fix64)30;

            if (UFE.config.roundOptions._timer < (Fix64)30)
            {
                UFE.config.roundOptions._timer = (Fix64)990;
            }

            UFE.config.roundOptions._timer = Fix64.Round(UFE.config.roundOptions._timer * (Fix64)100) / (Fix64)100;

            UFE.SetTimer(UFE.config.roundOptions._timer);
        }

        public void NextDamageScaling()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.comboOptions.damageDeterioration = Utility.GetNextEnum(UFE.config.comboOptions.damageDeterioration);
        }

        public void PreviousDamageScaling()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.comboOptions.damageDeterioration = Utility.GetNextEnum(UFE.config.comboOptions.damageDeterioration);
        }

        public void NextHitStunScaling()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.comboOptions.hitStunDeterioration = Utility.GetNextEnum(UFE.config.comboOptions.hitStunDeterioration);
        }

        public void PreviousHitStunScaling()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.comboOptions.hitStunDeterioration = Utility.GetNextEnum(UFE.config.comboOptions.hitStunDeterioration);
        }

        public void NextMaximumComboHits()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.comboOptions.maxCombo += 1;

            if (UFE.config.comboOptions.maxCombo > 99)
            {
                UFE.config.comboOptions.maxCombo = 1;
            }
        }

        public void PreviousMaximumComboHits()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.comboOptions.maxCombo -= 1;

            if (UFE.config.comboOptions.maxCombo < 1)
            {
                UFE.config.comboOptions.maxCombo = 99;
            }
        }

        public void NextMaximumGroundBounces()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.groundBounceOptions._maximumBounces += 1;

            if (UFE.config.groundBounceOptions._maximumBounces > 99)
            {
                UFE.config.groundBounceOptions._maximumBounces = 0;
            }
        }

        public void PreviousMaximumGroundBounces()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.groundBounceOptions._maximumBounces -= 1;

            if (UFE.config.groundBounceOptions._maximumBounces < 0)
            {
                UFE.config.groundBounceOptions._maximumBounces = 99;
            }
        }

        public void NextMaximumWallBounces()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.wallBounceOptions._maximumBounces += 1;

            if (UFE.config.wallBounceOptions._maximumBounces > 99)
            {
                UFE.config.wallBounceOptions._maximumBounces = 0;
            }
        }

        public void PreviousMaximumWallBounces()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.wallBounceOptions._maximumBounces -= 1;

            if (UFE.config.wallBounceOptions._maximumBounces < 0)
            {
                UFE.config.wallBounceOptions._maximumBounces = 99;
            }
        }

        public void NextBlockInput()
        {
            if (UFE.config == null)
            {
                return;
            }

            switch (UFE.config.blockOptions.blockType)
            {
                case BlockType.None:
                    UFE.config.blockOptions.blockType = BlockType.HoldBack;
                    break;

                default:
                    UFE.config.blockOptions.blockType = BlockType.None;
                    break;
            }
        }

        public void PreviousBlockInput()
        {
            if (UFE.config == null)
            {
                return;
            }

            switch (UFE.config.blockOptions.blockType)
            {
                case BlockType.None:
                    UFE.config.blockOptions.blockType = BlockType.HoldBack;
                    break;

                default:
                    UFE.config.blockOptions.blockType = BlockType.None;
                    break;
            }
        }

        public void ToggleAirBlock()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.blockOptions.allowAirBlock = !UFE.config.blockOptions.allowAirBlock;
        }

        public void NextParryInput()
        {
            if (UFE.config == null)
            {
                return;
            }

            switch (UFE.config.blockOptions.parryType)
            {
                case ParryType.TapForward:
                    UFE.config.blockOptions.parryType = ParryType.None;
                    break;

                default:
                    UFE.config.blockOptions.parryType = ParryType.TapForward;
                    break;
            }
        }

        public void PreviousParryInput()
        {
            if (UFE.config == null)
            {
                return;
            }

            switch (UFE.config.blockOptions.parryType)
            {
                case ParryType.TapForward:
                    UFE.config.blockOptions.parryType = ParryType.None;
                    break;

                default:
                    UFE.config.blockOptions.parryType = ParryType.TapForward;
                    break;
            }
        }

        public void ToggleAirParry()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.blockOptions.allowAirParry = !UFE.config.blockOptions.allowAirParry;
        }

        public void DefaultParryTiming()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.blockOptions._parryTiming = UFE2Manager.instance.defaultGameSettingsScriptableObject.parryTiming;
        }

        public void NextParryTiming()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.blockOptions._parryTiming += (Fix64)0.01;

            if (UFE.config.blockOptions._parryTiming > (Fix64)1)
            {
                UFE.config.blockOptions._parryTiming = (Fix64)0;
            }

            UFE.config.blockOptions._parryTiming = Fix64.Round(UFE.config.blockOptions._parryTiming * (Fix64)100) / (Fix64)100;
        }

        public void PreviousParryTiming()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.blockOptions._parryTiming -= (Fix64)0.01;

            if (UFE.config.blockOptions._parryTiming < (Fix64)0)
            {
                UFE.config.blockOptions._parryTiming = (Fix64)1;
            }

            UFE.config.blockOptions._parryTiming = Fix64.Round(UFE.config.blockOptions._parryTiming * (Fix64)100) / (Fix64)100;
        }
    }
}