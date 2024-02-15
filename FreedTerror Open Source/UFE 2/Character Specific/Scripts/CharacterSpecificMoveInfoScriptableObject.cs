using UFE3D;
using UnityEngine;

namespace FreedTerror.UFE2
{
    [CreateAssetMenu(menuName = "FreedTerror/UFE 2/Character Info/Character Specific Move Info", fileName = "New Character Specific Move Info")]
    public class CharacterSpecificMoveInfoScriptableObject : ScriptableObject
    {
        public UFE3D.CharacterInfo characterInfo;

        [System.Serializable]
        public class DefaultMoveInfoOptions
        {
            public CombatStances combatStance;
            public MoveInfo introMoveInfo;
            public MoveInfo roundWonMoveInfo;
            public MoveInfo timeOutMoveInfo;
            public MoveInfo gameWonMoveInfo;
        }
        public DefaultMoveInfoOptions[] defaultMoveInfoOptionsArray;

        [System.Serializable]
        public class OpponentMoveInfoOptions
        {
            public UFE3D.CharacterInfo opponentCharacterInfo;
            public CombatStances combatStance;
            public MoveInfo introMoveInfo;
            public MoveInfo roundWonMoveInfo;
            public MoveInfo timeOutMoveInfo;
            public MoveInfo gameWonMoveInfo;
        }
        public OpponentMoveInfoOptions[] opponentMoveInfoOptionsArray;
    }
}
