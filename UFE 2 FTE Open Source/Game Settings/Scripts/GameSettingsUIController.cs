using FPLibrary;
using UFE3D;
using UnityEngine;
using UnityEngine.UI;

namespace UFE2FTE
{
    public class GameSettingsUIController : MonoBehaviour
    {
        [SerializeField]
        private Text totalRoundsText;
        private Fix64 previousTimerValue;
        [SerializeField]
        private Text timerText;
        [SerializeField]
        private Text damageDeteriorationText;
        [SerializeField]
        private Text hitStunDeteriorationText;
        [SerializeField]
        private Text maximumGroundBouncesText;
        [SerializeField]
        private Text maximumWallBouncesText;
        [SerializeField]
        private Text allowAirBlockText;
        [SerializeField]
        private Text parryTypeText;
        [SerializeField]
        private Text allowAirParryText;
        private Fix64 previousParryTiming;
        [SerializeField]
        private Text parryTimingText;
        [SerializeField]
        private Text parryTimingFramesText;

        private void OnEnable()
        {
            previousTimerValue = UFE.config.roundOptions._timer;
            UFE2FTE.SetTextMessage(timerText, UFE.config.roundOptions._timer.ToString());

            previousParryTiming = UFE.config.blockOptions._parryTiming;
            UFE2FTE.SetTextMessage(parryTimingText, UFE.config.blockOptions._parryTiming.ToString());
            UFE2FTE.SetTextMessage(parryTimingFramesText, UFE2FTE.GetNormalFrameStringNumber((int)Fix64.Floor(UFE.config.blockOptions._parryTiming * UFE.config.fps)));
        }

        private void Update()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE2FTE.SetTextMessage(totalRoundsText, UFE2FTE.GetNormalStringNumber(UFE.config.roundOptions.totalRounds));

            if (previousTimerValue != UFE.config.roundOptions._timer)
            {
                previousTimerValue = UFE.config.roundOptions._timer;
                UFE2FTE.SetTextMessage(timerText, UFE.config.roundOptions._timer.ToString());
            }

            UFE2FTE.SetTextMessage(damageDeteriorationText, System.Enum.GetName(typeof(Sizes), UFE.config.comboOptions.damageDeterioration));

            UFE2FTE.SetTextMessage(hitStunDeteriorationText, System.Enum.GetName(typeof(Sizes), UFE.config.comboOptions.hitStunDeterioration));

            UFE2FTE.SetTextMessage(maximumGroundBouncesText, UFE2FTE.GetNormalStringNumber(UFE.config.groundBounceOptions._maximumBounces));

            UFE2FTE.SetTextMessage(maximumWallBouncesText, UFE2FTE.GetNormalStringNumber(UFE.config.wallBounceOptions._maximumBounces));

            UFE2FTE.SetTextMessage(allowAirBlockText, UFE2FTE.GetStringFromBool(UFE.config.blockOptions.allowAirBlock));

            switch (UFE.config.blockOptions.parryType)
            {
                case ParryType.TapForward:
                    UFE2FTE.SetTextMessage(parryTypeText, UFE2FTE.GetStringFromBool(true));
                    break;

                default:
                    UFE2FTE.SetTextMessage(parryTypeText, UFE2FTE.GetStringFromBool(false));
                    break;
            }

            UFE2FTE.SetTextMessage(allowAirParryText, UFE2FTE.GetStringFromBool(UFE.config.blockOptions.allowAirParry));

            if (previousParryTiming != UFE.config.blockOptions._parryTiming)
            {
                previousParryTiming = UFE.config.blockOptions._parryTiming;
                UFE2FTE.SetTextMessage(parryTimingText, UFE.config.blockOptions._parryTiming.ToString());
                UFE2FTE.SetTextMessage(parryTimingFramesText, UFE2FTE.GetNormalFrameStringNumber((int)Fix64.Floor(UFE.config.blockOptions._parryTiming * UFE.config.fps)));
            }
        }

        public void DefaultGameSettings()
        {
            if (UFE2FTE.instance.defaultGameSettingsScriptableObject == null)
            {
                return;
            }

            UFE2FTE.instance.defaultGameSettingsScriptableObject.UpdateGameSettings();
        }

        public void NextTotalRoundsValue()
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

        public void PreviousTotalRoundsValue()
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

        public void NextTimerValue()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.roundOptions._timer += (Fix64)30;

            if (UFE.config.roundOptions._timer > 990)
            {
                UFE.config.roundOptions._timer = 30;
            }

            UFE.config.roundOptions._timer = Fix64.Round(UFE.config.roundOptions._timer * 100) / 100;

            UFE.SetTimer(UFE.config.roundOptions._timer);
        }

        public void PreviousTimerValue()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.roundOptions._timer -= (Fix64)30;

            if (UFE.config.roundOptions._timer < 30)
            {
                UFE.config.roundOptions._timer = 990;
            }

            UFE.config.roundOptions._timer = Fix64.Round(UFE.config.roundOptions._timer * 100) / 100;

            UFE.SetTimer(UFE.config.roundOptions._timer);
        }

        public void NextDamageDeteriorationValue()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.comboOptions.damageDeterioration = UFE2FTE.GetNextEnum(UFE.config.comboOptions.damageDeterioration);
        }

        public void PreviousDamageDeteriorationValue()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.comboOptions.damageDeterioration = UFE2FTE.GetNextEnum(UFE.config.comboOptions.damageDeterioration);
        }

        public void NextHitStunDeteriorationValue()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.comboOptions.hitStunDeterioration = UFE2FTE.GetNextEnum(UFE.config.comboOptions.hitStunDeterioration);
        }

        public void PreviousHitStunDeteriorationValue()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.comboOptions.hitStunDeterioration = UFE2FTE.GetNextEnum(UFE.config.comboOptions.hitStunDeterioration);
        }

        public void NextMaximumGroundBounceValue()
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

        public void PreviousMaximumGroundBounceValue()
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

        public void NextMaximumWallBounceValue()
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

        public void PreviousMaximumWallBounceValue()
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

        public void ToggleAllowAirBlock()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.blockOptions.allowAirBlock = UFE2FTE.ToggleBool(UFE.config.blockOptions.allowAirBlock);
        }

        public void ToggleParryType()
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

        public void ToggleAllowAirParry()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.blockOptions.allowAirParry = UFE2FTE.ToggleBool(UFE.config.blockOptions.allowAirParry);
        }

        public void NextParryTimingValue()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.blockOptions._parryTiming += (Fix64)0.01;

            if (UFE.config.blockOptions._parryTiming > 1)
            {
                UFE.config.blockOptions._parryTiming = 0;
            }

            UFE.config.blockOptions._parryTiming = Fix64.Round(UFE.config.blockOptions._parryTiming * 100) / 100;
        }

        public void PreviousParryTimingValue()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.blockOptions._parryTiming -= (Fix64)0.01;

            if (UFE.config.blockOptions._parryTiming < 0)
            {
                UFE.config.blockOptions._parryTiming = 1;
            }

            UFE.config.blockOptions._parryTiming = Fix64.Round(UFE.config.blockOptions._parryTiming * 100) / 100;
        }
    }
}