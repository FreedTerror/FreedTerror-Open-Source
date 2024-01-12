using FPLibrary;
using UFE3D;
using UnityEngine;

namespace UFE2FTE
{
    [CreateAssetMenu(fileName = "New Game Settings", menuName = "U.F.E. 2 F.T.E./Settings/Game Settings")]
    public class GameSettingsScriptableObject : ScriptableObject
    {
        [Header("Round Options")]
        public int totalRounds = 3;
        public Fix64 timer = 99;
        [Header("Deterioration Options")]
        public Sizes damageDeterioration;
        public Sizes hitStunDeterioration;
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

            UFE.config.comboOptions.damageDeterioration = damageDeterioration;
            UFE.config.comboOptions.hitStunDeterioration = hitStunDeterioration;

            UFE.config.groundBounceOptions._maximumBounces = maximumGroundBounces;
            UFE.config.wallBounceOptions._maximumBounces = maximumWallBounces;

            UFE.config.blockOptions.allowAirBlock = allowAirBlock;

            UFE.config.blockOptions.parryType = parryType;
            UFE.config.blockOptions.allowAirParry = allowAirParry;
            UFE.config.blockOptions._parryTiming = parryTiming;
        }
    }
}