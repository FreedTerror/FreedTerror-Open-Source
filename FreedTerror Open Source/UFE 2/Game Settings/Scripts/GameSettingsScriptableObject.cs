using FPLibrary;
using UFE3D;
using UnityEngine;

namespace FreedTerror.UFE2
{
    [CreateAssetMenu(menuName = "FreedTerror/UFE 2/Settings/Game Settings", fileName = "New Game Settings")]
    public class GameSettingsScriptableObject : ScriptableObject
    {
        [Header("Round Options")]
        public int totalRounds = 3;
        public Fix64 timer = 99;
        [Header("Scaling Options")]
        public Sizes damageScaling;
        public Sizes hitStunScaling;
        [Header("Combo Options")]
        public int maximumComboHits = 99;
        [Header("Bounce Options")]
        public int maximumGroundBounces = 1;
        public int maximumWallBounces = 1;
        [Header("Block Options")]
        public BlockType blockInput;
        public bool allowAirBlock;
        [Header("Parry Options")]
        public ParryType parryInput;
        public bool allowAirParry;
        public Fix64 parryTiming;

        [NaughtyAttributes.Button]
        public void UpdateGameSettings()
        {
            if (UFE.config == null)
            {
                return;
            }

            UFE.config.roundOptions.totalRounds = totalRounds;
            UFE.config.roundOptions._timer = timer;
            UFE.SetTimer(UFE.config.roundOptions._timer);

            UFE.config.comboOptions.damageDeterioration = damageScaling;
            UFE.config.comboOptions.hitStunDeterioration = hitStunScaling;

            UFE.config.comboOptions.maxCombo = maximumComboHits;

            UFE.config.groundBounceOptions._maximumBounces = maximumGroundBounces;
            UFE.config.wallBounceOptions._maximumBounces = maximumWallBounces;

            UFE.config.blockOptions.allowAirBlock = allowAirBlock;

            UFE.config.blockOptions.parryType = parryInput;
            UFE.config.blockOptions.allowAirParry = allowAirParry;
            UFE.config.blockOptions._parryTiming = parryTiming;
        }
    }
}