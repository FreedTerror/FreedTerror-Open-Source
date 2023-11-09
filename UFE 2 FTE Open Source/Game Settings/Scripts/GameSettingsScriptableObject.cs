using UnityEngine;
using UFE3D;
using FPLibrary;

namespace UFE2FTE
{
    [CreateAssetMenu()]
    public class GameSettingsScriptableObject : ScriptableObject
    {
        [Header("Round Options")]
        public int totalRounds = 3;
        public Fix64 timer = 99;
        [Header("Bounce Options")]
        public int maximumGroundBounces = 1;
        public int maximumWallBounces = 1;
        [Header("Block Options")]
        public bool allowAirBlock;
        [Header("Parry Options")]
        public ParryType parryType;
        public bool allowAirParry;
        public Fix64 parryTiming;

        public void UpdateGameSettings()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.roundOptions.totalRounds = totalRounds;
            UFE.config.roundOptions._timer = timer;

            UFE.config.groundBounceOptions._maximumBounces = maximumGroundBounces;
            UFE.config.wallBounceOptions._maximumBounces = maximumWallBounces;

            UFE.config.blockOptions.allowAirBlock = allowAirBlock;

            UFE.config.blockOptions.parryType = parryType;
            UFE.config.blockOptions.allowAirParry = allowAirParry;
            UFE.config.blockOptions._parryTiming = parryTiming;
        }
    }
}